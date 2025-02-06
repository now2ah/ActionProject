using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
        bool _isPaused;

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
        public UnityEvent OnDefenseDone;

        Vector3 _startPos;
        Vector3 _defenseSpawnPos;
        GameObject _playerBase;
        GameObject _commanderUnitObj;
        Commander _commanderUnit;

        //temporary
        GameObject _playerBasePrefab;
        GameObject _commanderPrefab;

        List<Spawner> _enemySpawners;

        [SerializeReference]
        List<GameObject> _playerBuildings;
        GameObject _playerBuildingObj;

        List<GameObject> _playerUnitPrefabs;
        List<GameObject> _playerUnits;

        List<GameObject> _enemyUnitPrefabs;
        [SerializeReference]
        List<GameObject> _enemyUnits;

        EnemyWaves _enemyWaves;
        int _curWaveOrder;
        EnemyWaves _huntEnemyWaves;
        int _curHuntWaveOrder;

        GameObject _waveCam;

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

        GameData _gameData;

        public bool IsPaused { get { return _isPaused; } set { _isPaused = value; } }
        public bool IsLive { get { return _isLive; } set { _isLive = value; } }
        public bool IsPlaying { get { return _isPlaying; } }
        public eGamePhase Phase => _gamePhase;
        public StateMachine PhaseStateMachine => _phaseStateMachine;
        public HuntState HuntState => _huntState;
        public DefenseState DefenseState => _defenseState;
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
        public GameData GameData { get { return _gameData; } set { _gameData = value; } }

        public override void Initialize()
        {
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
            _defenseSpawnPos = new Vector3(-50.0f, 7.5f, 0.0f);
            _enemySpawners = new List<Spawner>();
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
            OnDefenseDone.AddListener(_OnDefenseDone);

            _SetUpSpawners();
        }


        //-> Save System
        public Data GetWrappedGameData()
        {
            Data data = new Data();
            data.date = System.DateTime.Now.ToString();
            data.gameData = _gameData;
            CommanderData commanderData = _commanderUnit.UnitData as CommanderData;
            for(int i=0; i<_commanderUnit.AbilitySlots.Length; i++)
                commanderData.abilityDatas.Add(_commanderUnit.AbilitySlots[i].abilityData);
            data.gameData.commanderData.abilityDatas = commanderData.abilityDatas;
            return data;
        }

        public void Stop()
        {
            _isLive = false;
            Time.timeScale = 0f;
        }

        public void Resume()
        {
            _isPaused = false;
            _isLive = true;
            _isPlaying = true;
            Time.timeScale = 1;
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (_isPlaying && !_isPaused)
            {
                _isPaused = true;
                _isPlaying = false;
                Stop();
                if (null == UIManager.Instance.PausePanel)
                    UIManager.Instance.PausePanel = UIManager.Instance.CreateUI("PausePanel", UIManager.Instance.MainCanvas);

                UIManager.Instance.PausePanel.SetActive(true);
                UIManager.Instance.PausePanel.transform.SetAsLastSibling();
            }
        }

        // -> 수정
        public void GameStart()
        {
            if (!_isLive)
                _isLive = true;

            _isPlaying = true;

            //-> Save system
            if (null == _gameData)
            {
                _gameData = new GameData();
                _gameData.unitData = new List<PlayerUnitData>();
                _gameData.curHuntWaveOrder = -1;
            }

            //Mode Manager? Mode 시작마다?
            PoolManager.Instance.Initialize();

            //-> Mode 관련 클래스
            _CreateCommanderUnit();
            _AddBuildings();
            _PrepareResource();

            _StartGameTimer();
            _StartRefreshTimer();

            _phaseStateMachine.Initialize(_townBuildState);
            _StartPhaseTimer(eGamePhase.TownBuild);
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

        //-> Defense Mode
        public void StartWave(EnemyWaves waves, float timeRate)
        {
            _gameData.curHuntWaveOrder++;
            if (_gameData.curHuntWaveOrder < waves.enemyWaveList.Count)
            {
                StartCoroutine(_StartHuntWaveCoroutine(waves, _gameData.curHuntWaveOrder, timeRate));
            }
        }

        //-> Defense Mode
        public void StartWave(List<GameObject> waves, float timeRate, Spawner spawner)
        {
            UIManager.Instance.WaveCamUI.Show();
            StartCoroutine(_StartDefenseWaveCoroutine(waves, timeRate, spawner));
        }

        //-> Defense Mode
        public void AddHuntSpawner()
        {
            Spawner[] spawners = _huntSpawner.GetComponentsInChildren<Spawner>();
            foreach (var spawner in spawners)
                _enemySpawners.Add(spawner);
        }

        //-> Defense Mode
        public void SetActiveHuntSpawner(bool isOn)
        {
            if (null != _commanderUnit)
            {
                _huntSpawner.SetActive(isOn);
            }
        }

        //-> Defense Mode
        public void AddDefenseSpawner()
        {
            if (_defenseSpawner.TryGetComponent<Spawner>(out Spawner comp))
                _enemySpawners.Add(comp);
        }

        //-> Defense Mode
        public void SetActiveDefenseSpawner(bool isOn)
        {
            if (null != _defenseSpawner)
                _defenseSpawner.SetActive(isOn);
        }

        //-> Defense Mode
        public void SetDefenseSpawner()
        {
            if (null != _defenseSpawner)
                _defenseSpawner.transform.position = _defenseSpawnPos;
        }

        //-> Defense Mode
        public void FindSpawnerPoint()
        {
            GameObject obj = GameObject.FindWithTag("DefenseSpawnerPoint");
            _defenseSpawner.transform.position = obj.transform.position;
        }

        //-> Defense Mode
        public void InactiveDefaultBuildings()
        {
            Building[] buildings = FindObjectsByType<Building>(FindObjectsSortMode.None);
            foreach (var building in buildings)
            {
                building.gameObject.SetActive(false);
            }
        }

        public void ResetGame()
        {
            _isLive = false;
            _isPlaying = false;
            _isPaused = false;
            _gameData = null;
            if (null != _commanderUnit)
            {
                InputManager.Instance.RemoveListeners(_commanderUnit);
                Destroy(_commanderUnit.gameObject);
                _commanderUnit = null;
            }
            foreach (var building in _playerBuildings)
                building.SetActive(false);
            _gameTimer.ResetTimer();
            _refreshTimer.ResetTimer();
            _phaseTimer.ResetTimer();

            _playerUnits.Clear();

            foreach (var unit in _enemyUnits)
                unit.GetComponent<EnemyUnit>().Pool.Free(unit.GetComponent<EnemyUnit>());
            _enemyUnits.Clear();

            foreach (var building in _playerBuildings)
                Destroy(building);
            _playerBuildings.Clear();

            if (null != UIManager.Instance.TownStagePanel)
                UIManager.Instance.TownStagePanel.Hide();
            if (null != UIManager.Instance.ExpBarUI)
                UIManager.Instance.ExpBarUI.Hide();
            if (null != UIManager.Instance.PhaseTextUI)
                UIManager.Instance.PhaseTextUI.Hide();
        }

        //-> Defense or City builder Mode
        public void CheckConstructBuilding()
        {
            if (0 < _playerBuildings.Count)
            {
                foreach (var building in _playerBuildings)
                {
                    if (building.TryGetComponent<Building>(out Building comp))
                    {
                        BuildingData data = comp.UnitData as BuildingData;
                        if (comp.PrepareState == comp.StateMachine.CurState)
                        {
                            comp.StateMachine.ChangeState(comp.IdleState);
                            _gameData.resource.Gold += data.requireGold;
                        }
                    }
                }
            }
        }

        //-> Save System
        public void AutoSave()
        {
            SaveSystem.Instance.Save(0, GetWrappedGameData());
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

        //-> Defense Mode
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

        //-> Defense, CityBuilder Mode
        void _AddBuildings()
        {
            if (0 < _playerBuildings.Count)
            {
                foreach (var building in _playerBuildings)
                    building.SetActive(true);
            }
            else
            {
                if (null == _playerBuildingObj)
                    _playerBuildingObj = new GameObject("Buildings");
                
                _playerBuildingObj.transform.SetParent(gameObject.transform);
                Building[] buildings = FindObjectsByType<Building>(FindObjectsSortMode.None);
                foreach (var building in buildings)
                {
                    if (building.TryGetComponent<Building>(out Building comp))
                        comp.Initialize();
                    _playerBuildings.Add(building.gameObject);
                    building.transform.SetParent(_playerBuildingObj.transform);
                    if ("PlayerBase" == building.name)
                        _playerBase = building.gameObject;
                }
            }
        }

        //-> GameMode
        void _CreateCommanderUnit()
        {
            Vector3 startPos = new Vector3(-100.0f, 6.0f, 0.0f);
            _commanderUnitObj = Instantiate(_commanderPrefab, startPos, Quaternion.identity);
            _commanderUnit = _commanderUnitObj.GetComponent<Commander>();
            _playerUnits.Add(_commanderUnitObj);

            InputManager.Instance.AddListeners(_commanderUnit);
        }

        //-> GameMode
        IEnumerator _StartHuntWaveCoroutine(EnemyWaves waves, int order, float timeRate)
        {
            if (0 < _enemySpawners.Count)
            {
                int[] enemyCounts = new int[waves.enemyWaveList[order].enemyGroupList.Count];
                for (int i=0; i< waves.enemyWaveList[order].enemyGroupList.Count; i++)
                    enemyCounts[i] = waves.enemyWaveList[order].enemyGroupList[i].enemyAmount;
                int allEnemyCount = 0;
                foreach (var num in enemyCounts)
                    allEnemyCount += num;

                while (0 < allEnemyCount)
                {
                    int enemyRandNum = Random.Range(0, waves.enemyWaveList[order].enemyGroupList.Count);
                    if (0 == enemyCounts[enemyRandNum])
                        continue;
                    
                    GameObject obj = PoolManager.Instance.GetEnemyPool(waves.enemyWaveList[order].enemyGroupList[enemyRandNum].type).GetNew().gameObject;

                    if (obj.TryGetComponent<Unit>(out Unit comp))
                    {
                        comp.Initialize();
                        comp.UnitPanel.ApplyHPValue(comp.UnitData.hp, comp.UnitData.maxHp);
                    }

                    _enemyUnits.Add(obj);

                    int spawnerRandNum = Random.Range(0, _enemySpawners.Count);
                    _enemySpawners[spawnerRandNum].SpawnObject(obj);

                    enemyCounts[enemyRandNum]--;
                    allEnemyCount--;

                    yield return new WaitForSeconds(timeRate);
                }
            }
        }

        //-> Defense Mode
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

        //-> Defense Mode
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
                UIManager.Instance.TownStagePanel.Show();
                AudioManager.Instance.PlayBGM(AudioManager.eBGM.TOWN);
                
            }
            else if (_gamePhase == eGamePhase.TownBuild)
            {
                _phaseStateMachine.ChangeState(_townBuildState);
                //_LoadData();
            }
            else if (_gamePhase == eGamePhase.Defense)
            {
                _phaseStateMachine.ChangeState(_defenseState);
                //_LoadData();
            }
        }

        void _OnStartHuntPhase()
        {
            //_SaveData();
            _phaseStateMachine.ChangeState(_huntState);
        }

        void _OnDefenseDone()
        {
            if ((int)_gamePhase + 1 > 2)
                _gamePhase = 0;
            else
                _gamePhase++;

            _phaseTimer.ResetTimer();
            ChangePhase(_gamePhase);
            _phaseStateMachine.ChangeState(_townBuildState);
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

        void _CheckDefenseDone()
        {
            if (_IsClearDefenseEnemies() &&
                _phaseStateMachine.CurState == _defenseState)
            {
                OnDefenseDone?.Invoke();
            }
        }


        private void Awake()
        {
            _isLive = true;
        }

        private void Update()
        {
            if (!_isLive)
                return;

            if (_isPlaying)
            {
                _CheckPhaseTime();
                _CheckRefreshTime();
                _CheckDefenseDone();

                if (null != _phaseStateMachine)
                    _phaseStateMachine.Update();
            }
        }

        #region TEST
        public void SetCharacterTestScene()
        {
            _isPlaying = true;
            PoolManager.Instance.Initialize();
            //_CreateCommanderUnit();
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