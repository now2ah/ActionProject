using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Action.Util;
using Action.UI;
using Action.Units;
using Action.Game;
using Action.SO;
using Action.State;

namespace Action.Manager
{
    public enum eGamePhase
    {
        TownBuild,
        Hunt,
        Defense
    }

    public class GameManager : Singleton<GameManager>
    {
        public Constant Constants;

        bool _isLive;
        eGamePhase _gamePhase;
        bool _isPlaying;

        Vector3 _startPosition;         //베이스 위치
        IEnumerator _waveStartCoroutine;

        StateMachine _phaseStateMachine;
        TownBuildState _townBuildState;
        HuntState _huntState;
        DefenseState _defenseState;

        ActionTime _gameTimer;
        ActionTime _phaseTimer;
        ActionTime _refreshTimer;

        [SerializeField]
        float _townBuildPhaseTime;
        [SerializeField]
        float _huntPhaseTime;
        [SerializeField]
        float _defensePhaseTime;
        public ActionTime PhaseTimer => _phaseTimer;
        public ActionTime RefreshTimer => _refreshTimer;

        public UnityEvent OnRefresh;
        public UnityEvent OnFinishLevel;

        Vector3 _startPos;
        Vector3 _defenseSpawnPos;
        GameObject _playerBase;
        GameObject _commanderUnitObj;
        Commander _commanderUnit;

        //temporary
        GameObject _playerBasePrefab;
        GameObject _commanderPrefab;

        List<Spawner> _enemySpawners;

        List<GameObject> _playerBuildingPrefabs;
        List<GameObject> _playerBuildings;

        List<GameObject> _playerUnitPrefabs;
        List<GameObject> _playerUnits;

        List<GameObject> _enemyUnitPrefabs;
        [SerializeReference]
        List<GameObject> _enemyUnits;

        EnemyWaves _enemyWaves;
        int _curWaveOrder;
        EnemyWaves _huntEnemyWaves;
        int _curHuntWaveOrder;

        GameObject _hitBoxPrefab;
        GameObject _hitEffectPrefab;
        Material _hitMaterial;
        GameObject _projectilePrefab;
        GameObject _buildingIndicatorPrefab;
        GameObject _huntStageSpawnerPrefab;
        GameObject _huntSpawner;
        GameObject _defenseStageSpawnerPrefab;
        GameObject _defenseSpawner;
        GameObject _coinPrefab;

        [SerializeReference]
        GameData _gameData;

        public bool IsLive { get { return _isLive; } set { _isLive = value; } }
        public bool IsPlaying { get { return _isPlaying; } }
        public eGamePhase Phase => _gamePhase;
        public StateMachine PhaseStateMachine => _phaseStateMachine;
        public float TownBuildPhaseTime { get { return _townBuildPhaseTime; } set { _townBuildPhaseTime = value; } }
        public float HuntPhaseTime { get { return _huntPhaseTime; } set { _huntPhaseTime = value; } }
        public float DefensePhaseTime { get { return _defensePhaseTime; } set { _defensePhaseTime = value; } }
        public Vector3 StartPos { get { return _startPos; } }
        public Vector3 DefenseSpawnPos { get { return _defenseSpawnPos; } }
        public GameObject PlayerBase { get { return _playerBase; } set { _playerBase = value; } }
        public GameObject CommanderObj { get { return _commanderUnitObj; } set { _commanderUnitObj = value; } }
        public Commander CommanderUnit { get { return _commanderUnit; } }
        public List<Spawner> EnemySpawners { get { return _enemySpawners; } }
        public List<GameObject> PlayerBuildings { get { return _playerBuildings; } }
        public List<GameObject> PlayerUnits { get { return _playerUnits; } }
        public List<GameObject> EnemyUnits { get { return _enemyUnits; } }
        public EnemyWaves EnemyWaves { get { return _enemyWaves; } }
        public EnemyWaves HuntEnemyWaves { get { return _huntEnemyWaves; } }
        public GameObject DefenseSpawner => _defenseSpawner;
        public GameObject CoinPrefab => _coinPrefab;
        public GameObject HitBoxPrefab => _hitBoxPrefab;
        public GameObject HitEffectPrefab => _hitEffectPrefab;
        public Material HitMaterial => _hitMaterial;
        public GameObject ProjectilePrefab => _projectilePrefab;
        public GameObject BuildingIndicatorPrefab => _buildingIndicatorPrefab;
        public GameData GameData => _gameData;

        public override void Initialize()
        {
            base.Initialize();

            _LoadAssets();
            InputManager.Instance.PauseMenu.performed += ctx => { OnPause(ctx); };
            _phaseStateMachine = new StateMachine();
            _townBuildState = new TownBuildState();
            _huntState = new HuntState();
            _defenseState = new DefenseState();
            _townBuildPhaseTime = Constants.TOWNBUILD_PHASE_TIME;
            _huntPhaseTime = Constants.HUNT_PHASE_TIME;
            _defensePhaseTime = Constants.DEFENSE_PHASE_TIME;
            _gameTimer = gameObject.AddComponent<ActionTime>();
            _phaseTimer = gameObject.AddComponent<ActionTime>();
            _refreshTimer = gameObject.AddComponent<ActionTime>();
            _startPos = new Vector3(-150.0f, 6.0f, -15.0f);
            _defenseSpawnPos = new Vector3(0.0f, 7.5f, 0.0f);
            _enemySpawners = new List<Spawner>();
            _playerBuildingPrefabs = new List<GameObject>();
            _playerBuildings = new List<GameObject>();
            _playerUnitPrefabs = new List<GameObject>();
            _playerUnits = new List<GameObject>();
            _enemyUnitPrefabs = new List<GameObject>();
            _enemyUnits = new List<GameObject>();
            _enemyUnitPrefabs.Add(Resources.Load("Prefabs/Units/Enemy/NormalEnemy") as GameObject);
            _enemyUnitPrefabs.Add(Resources.Load("Prefabs/Units/Enemy/RangeEnemy") as GameObject);
            _curWaveOrder = -1;

            SceneManager.Instance.OnInGameSceneLoaded.AddListener(_OnStartInGamePhase);
            SceneManager.Instance.OnHuntStageSceneLoaded.AddListener(_OnStartHuntPhase);

            _SetUpSpawners();
        }

        public void Stop()
        {
            _isLive = false;
            Time.timeScale = 0f;
        }

        public void Resume()
        {
            _isLive = true;
            Time.timeScale = 1;
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (_isPlaying)
            {
                Stop();
                if (null == UIManager.Instance.PausePanel)
                    UIManager.Instance.PausePanel = UIManager.Instance.CreateUI("PausePanel", UIManager.Instance.MainCanvas);

                UIManager.Instance.PausePanel.SetActive(true);
            }
        }

        public void GameStart()
        {
            _isPlaying = true;
            _startPosition = _FindBasePoint();

            PoolManager.Instance.Initialize();

            _CreateStartBase();
            _CreateCommanderUnit();
            _AddBuildings();
            _SetBuildingData();

            UIManager.Instance.ExpPanel.SetActive(true);
            UIManager.Instance.ExpBarUI.ApplyExpValue(_commanderUnit.PlayerUnitData.exp, _commanderUnit.PlayerUnitData.nextExp);

            _PrepareResource();

            _StartGameTimer();
            _StartRefreshTimer();

            _phaseStateMachine.Initialize(_townBuildState);
            StartPhase(eGamePhase.TownBuild);
            //StartWave(5, 1, 0);
        }

        public void GameOver()
        {

        }

        public void ChangePhase(eGamePhase phase)
        {
            _gamePhase = phase;
            switch (phase)
            {
                case eGamePhase.TownBuild:
                    StartPhase(eGamePhase.TownBuild);
                    break;

                case eGamePhase.Hunt:
                    StartPhase(eGamePhase.Hunt);
                    break;

                case eGamePhase.Defense:
                    StartPhase(eGamePhase.Defense);
                    break;
            }
        }

        public void StartPhase(eGamePhase phase)
        {
            _StartPhaseTimer(phase);
            switch (phase)
            {
                case eGamePhase.TownBuild:
                    
                    break;

                case eGamePhase.Hunt:
                    SceneManager.Instance.LoadGameScene(3);
                    break;

                case eGamePhase.Defense:
                    SceneManager.Instance.LoadGameScene(2);
                    break;
            }
        }

        public void StartWave(EnemyWaves waves, float timeRate)
        {
            _gameData.curHuntWaveOrder++;
            if (_gameData.curHuntWaveOrder < waves.enemyWaveList.Count)
            {
                int randNum = Random.Range(0, _enemySpawners.Count);
                StartCoroutine(_StartHuntWaveCoroutine(waves, _gameData.curHuntWaveOrder, timeRate, randNum));
            }
        }

        public void StartWave(List<GameObject> waves, float timeRate, Spawner spawner)
        {
            StartCoroutine(_StartDefenseWaveCoroutine(waves, timeRate, spawner));
        }

        //public void AddAllEnemySpawners()
        //{
        //    _enemySpawners.Clear();
        //    Spawner[] objs = FindObjectsByType<Spawner>(FindObjectsSortMode.None);
        //    if (0 < objs.Length)
        //    {
        //        for (int i = 0; i < objs.Length; i++)
        //            _enemySpawners.Add(objs[i]);
        //    }
        //}

        public void AddHuntSpawner()
        {
            Spawner[] spawners = _huntSpawner.GetComponentsInChildren<Spawner>();
            foreach (var spawner in spawners)
                _enemySpawners.Add(spawner);
        }

        public void SetActiveHuntSpawner(bool isOn)
        {
            if (null != _commanderUnit)
            {
                _huntSpawner.SetActive(isOn);
            }
        }

        public void AddDefenseSpawner()
        {
            if (_defenseSpawner.TryGetComponent<Spawner>(out Spawner comp))
                _enemySpawners.Add(comp);
        }

        public void SetActiveDefenseSpawner(bool isOn)
        {
            if (null != _defenseSpawner)
                _defenseSpawner.SetActive(isOn);
        }

        public void SetDefenseSpawner()
        {
            if (null != _defenseSpawner)
                _defenseSpawner.transform.position = _defenseSpawnPos;
        }

        public void FindSpawnerPoint()
        {
            GameObject obj = GameObject.FindWithTag("DefenseSpawnerPoint");
            _defenseSpawner.transform.position = obj.transform.position;
        }

        public void InactiveDefaultBuildings()
        {
            Building[] buildings = FindObjectsByType<Building>(FindObjectsSortMode.None);
            foreach (var building in buildings)
            {
                building.gameObject.SetActive(false);
            }
        }

        Vector3 _FindBasePoint()
        {
            GameObject obj = GameObject.FindWithTag("StartingBasePoint");
            if (null == obj)
                return Vector3.zero;
            else
                return obj.transform.position;
        }

        void _LoadAssets()
        {
            Constants = Resources.Load("ScriptableObject/Constant") as Constant;
            _playerBasePrefab = Resources.Load("Prefabs/Buildings/PlayerBase") as GameObject;
            _commanderPrefab = Resources.Load("Prefabs/Units/Player/Commander") as GameObject;
            _enemyWaves = Resources.Load("ScriptableObject/EnemyWaves") as EnemyWaves;
            _huntEnemyWaves = Resources.Load("ScriptableObject/HuntEnemyWaves") as EnemyWaves;
            _hitBoxPrefab = Resources.Load("Prefabs/Misc/HitBox") as GameObject;
            _hitEffectPrefab = Resources.Load("Prefabs/Misc/Hiteffect") as GameObject;
            _hitMaterial = Resources.Load("Materials/HitEffectMat") as Material;
            _projectilePrefab = Resources.Load("Prefabs/Misc/Projectile") as GameObject;
            _buildingIndicatorPrefab = Resources.Load("Prefabs/Misc/ArrowIcon") as GameObject;
            _huntStageSpawnerPrefab = Resources.Load("Prefabs/Misc/HuntSpawner") as GameObject;
            _defenseStageSpawnerPrefab = Resources.Load("Prefabs/Misc/DefenseSpawner") as GameObject;
            _coinPrefab = Resources.Load("Prefabs/Misc/Coin") as GameObject;
        }

        void _SetUpSpawners()
        {
            _huntSpawner = Instantiate<GameObject>(_huntStageSpawnerPrefab, transform);
            _huntSpawner.SetActive(false);
            _defenseSpawner = Instantiate<GameObject>(_defenseStageSpawnerPrefab, transform);
            _defenseSpawner.SetActive(false);
        }

        void _StartGameTimer()
        {
            _gameTimer.TickStart();
        }

        void _StartRefreshTimer()
        {
            if (null != _refreshTimer)
            {
                _refreshTimer.TickStart(Constants.GAME_REFRESH_TIME);
                OnRefresh?.Invoke();
            }
        }

        void _CreateStartBase()
        {
            if (null == _playerBase)
            {
                _playerBase = Instantiate(_playerBasePrefab, _startPosition, Quaternion.identity);
                _playerBuildings.Add(_playerBase);
            }
        }

        void _AddBuildings()
        {
            GameObject obj = new GameObject("Buildings");
            obj.transform.SetParent(gameObject.transform);
            Building[] buildings = FindObjectsByType<Building>(FindObjectsSortMode.None);
            foreach (var building in buildings)
            {
                _playerBuildings.Add(building.gameObject);
                building.transform.SetParent(obj.transform);
            }
        }

        void _CreateCommanderUnit()
        {
            if (null == _commanderUnitObj)
            {
                Vector3 startPos = Vector3.zero;
                if (null != _playerBase)
                {
                    //임시 베이스 사이즈 계산식(에셋 적용 후 변경)
                    float baseExtentsZ = 0.0f;
                    if (_playerBase.gameObject.TryGetComponent<BoxCollider>(out BoxCollider comp))
                    {
                        baseExtentsZ = comp.size.z + 5.0f;     //임시
                    }
                    startPos = _playerBase.gameObject.transform.position + new Vector3(0.0f, Constants.GROUND_Y_POS, /*-(baseExtentsZ + 1.0f)*/ -20.0f);
                }
                _commanderUnitObj = Instantiate(_commanderPrefab, startPos, Quaternion.identity);
                _commanderUnit = _commanderUnitObj.GetComponent<Commander>();
                _playerUnits.Add(_commanderUnitObj);
            }
        }

        IEnumerator _StartHuntWaveCoroutine(EnemyWaves waves, int order, float timeRate, int spawnerIndex)
        {
            if (0 < _enemySpawners.Count)
            {
                foreach (var item in waves.enemyWaveList[order].enemyGroupList)
                {
                    int count = item.enemyAmount;
                    while (count > 0)
                    {
                        GameObject obj = PoolManager.Instance.GetEnemyPool(item.type).GetNew().gameObject;
                        if (obj.TryGetComponent<Unit>(out Unit comp))
                        {
                            comp.Initialize();
                            comp.UnitPanel.ApplyHPValue(comp.UnitData.hp, comp.UnitData.maxHp);
                        }

                        _enemyUnits.Add(obj);

                        _enemySpawners[spawnerIndex].SpawnObject(obj);

                        count--;
                        yield return new WaitForSeconds(timeRate);
                    }
                }
            }
        }

        IEnumerator _StartDefenseWaveCoroutine(List<GameObject> waves, float timeRate, Spawner spawner)
        {
            if (0 < _enemySpawners.Count)
            {
                foreach (var item in waves)
                {
                    yield return new WaitForSeconds(timeRate);

                    GameObject obj = item;
                    obj.SetActive(true);
                    if (obj.TryGetComponent<Unit>(out Unit comp))
                    {
                        comp.Initialize();
                        comp.UnitPanel.ApplyHPValue(comp.UnitData.hp, comp.UnitData.maxHp);
                    }

                    spawner.SpawnObject(obj);
                }
            }
        }

        void _PrepareResource()
        {
            if (null == _gameData.resource)
            {
                _gameData.resource = new Resource();
                _gameData.resource.Initialize();
            }
        }

        void _SetBuildingData()
        {
            Building[] buildings = FindObjectsByType<Building>(FindObjectsSortMode.None);
            for (int i = 0; i < buildings.Length; i++)
            {
                string buildingName = buildings[i].name;

                switch(buildingName)
                {
                    case "Tower_Base_North":
                        _gameData.towerBaseN = buildings[i].BuildingData;
                        break;
                    case "Tower_Base_South":
                        _gameData.towerBaseS = buildings[i].BuildingData;
                        break;
                    case "Fence":
                        _gameData.fence = buildings[i].BuildingData;
                        break;
                }
            }
        }

        void _SaveData()
        {
            string data = JsonParser.ObjectToJson(_gameData);
            string path = Path.Combine(Application.dataPath, "autoSave");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            JsonParser.CreateJsonFile(path, "autoSave", data);
        }

        void _LoadData()
        {
            string path = Path.Combine(Application.dataPath, "autoSave");

            if (!Directory.Exists(path))
                Logger.Log("file is not exist.");
            _gameData = JsonParser.LoadJsonFile<GameData>(path, "autoSave");
        }

        bool _IsClearDefenseEnemies()
        {
            if (0 == _enemyUnits.Count)
                return true;
            else
                return false;
        }

        void _OnStartInGamePhase()
        {
            if (!_isPlaying)
            {
                GameStart();
                CameraManager.Instance.CreateFixedVirtualCamera();
                UIManager.Instance.CreateTownStagePanel();
            }
            else if (_gamePhase == eGamePhase.TownBuild)
            {
                _phaseStateMachine.ChangeState(_townBuildState);
                _LoadData();
            }
            else if (_gamePhase == eGamePhase.Defense)
            {
                _phaseStateMachine.ChangeState(_defenseState);
                //_LoadData();
            }
                
        }

        void _OnStartHuntPhase()
        {
            _SaveData();
            _phaseStateMachine.ChangeState(_huntState);
        }

        void _StartPhaseTimer(eGamePhase phase)
        {
            //Logger.Log(phase.ToString());
            switch (phase)
            {
                case eGamePhase.TownBuild:
                    _phaseTimer.TickStart(_townBuildPhaseTime);
                    break;
                case eGamePhase.Hunt:
                    _phaseTimer.TickStart(_huntPhaseTime);
                    break;
                case eGamePhase.Defense:
                    _phaseTimer.TickStart(_defensePhaseTime);
                    break;
            }
        }

        void _CheckPhaseTime()
        {
            UIManager.Instance.RefreshTownStageUI();

            if (_phaseTimer.IsFinished &&
                _phaseStateMachine.CurState != _defenseState)
            {
                if ((int)_gamePhase + 1 > 2)
                    _gamePhase = 0;
                else
                    _gamePhase++;

                _phaseTimer.ResetTimer();
                ChangePhase(_gamePhase);
                //StartPhase(_gamePhase);
            }
            else if (_IsClearDefenseEnemies() &&
                _phaseStateMachine.CurState == _defenseState)
            {
                if ((int)_gamePhase + 1 > 2)
                    _gamePhase = 0;
                else
                    _gamePhase++;

                _phaseTimer.ResetTimer();
                ChangePhase(_gamePhase);
            }
        }

        void _CheckRefreshTime()
        {
            if (null != _refreshTimer)
            {
                if (_refreshTimer.IsFinished)
                {
                    _refreshTimer.ResetTimer();
                    _StartRefreshTimer();
                }
            }
        }


        private void Awake()
        {
            _isLive = true;
            _gameData = new GameData();
            _gameData.curHuntWaveOrder = -1;
        }

        private void Update()
        {
            if (!_isLive)
                return;

            if (_isPlaying)
            {
                _CheckPhaseTime();
                _CheckRefreshTime();

                if (null != _phaseStateMachine)
                    _phaseStateMachine.Update();
            }
        }

        #region TEST
        public void SetCharacterTestScene()
        {
            _isPlaying = true;
            PoolManager.Instance.Initialize();
            _CreateCommanderUnit();
            //UIManager.Instance.ExpPanel.SetActive(true);
            //UIManager.Instance.ExpBarUI.ApplyExpValue(_commanderUnit.PlayerUnitData.exp, _commanderUnit.PlayerUnitData.nextExp);
            //_PrepareResource();
            //_StartGameTimer();
            //_StartRefreshTimer();

            _phaseStateMachine.Initialize(_townBuildState);
            //StartPhase(eGamePhase.TownBuild);
            _StartRefreshTimer();
        }

        public void CreateTestWave()
        {
            _SetUpSpawners();
            AddDefenseSpawner();
            FindSpawnerPoint();
            StartWave(_enemyWaves, 10.0f);
        }
        #endregion
    }
}