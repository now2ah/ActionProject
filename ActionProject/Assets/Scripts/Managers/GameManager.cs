using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Action.Util;
using Action.UI;
using Action.Units;
using Action.Game;

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
        eGamePhase _gamePhase;
        bool _isPlaying;            
        
        Vector3 _startPosition;         //베이스 위치
        IEnumerator _waveStartCoroutine;

        ActionTime _gameTimer;
        ActionTime _phaseTimer;
        ActionTime _refreshTimer;

        public ActionTime PhaseTimer => _phaseTimer;
        public ActionTime RefreshTimer => _refreshTimer;

        public UnityEvent OnRefresh;

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
        List<GameObject> _enemyUnits;

        List<Dictionary<Constant.eEnemyType, int>> _enemyWave;

        GameObject _hitBoxPrefab;
        GameObject _hitEffectPrefab;
        Material _hitMaterial;
        GameObject _projectilePrefab;

        [SerializeField]
        Resource _resource;
        public Resource Resource => _resource;

        public GameObject PlayerBase { get { return _playerBase; } set { _playerBase = value; } }
        public GameObject CommanderObj { get { return _commanderUnitObj; } set { _commanderUnitObj = value; } }
        public Commander CommanderUnit { get { return _commanderUnit; } }
        public List<GameObject> PlayerBuildings { get { return _playerBuildings; } }
        public List<GameObject> PlayerUnits { get { return _playerUnits; } }
        public List<GameObject> EnemyUnits { get { return _enemyUnits; } }
        public List<Dictionary<Constant.eEnemyType , int>> EnemyWave { get { return _enemyWave; } }
        public GameObject HitBoxPrefab => _hitBoxPrefab;
        public GameObject HitEffectPrefab => _hitEffectPrefab;
        public Material HitMaterial => _hitMaterial;
        public GameObject ProjectilePrefab => _projectilePrefab;

        public override void Initialize()
        {
            base.Initialize();
            _waveStartCoroutine = _StartWaveCoroutine(5, 1, 0, Constant.eEnemyType.NORMAL);

            _gameTimer = gameObject.AddComponent<ActionTime>();
            _phaseTimer = gameObject.AddComponent<ActionTime>();
            _refreshTimer = gameObject.AddComponent<ActionTime>();
            _playerBasePrefab = Resources.Load("Prefabs/Buildings/PlayerBase") as GameObject;
            _commanderPrefab = Resources.Load("Prefabs/Units/Player/Commander") as GameObject;
            _enemySpawners = new List<Spawner>();
            _playerBuildingPrefabs = new List<GameObject>();
            _playerBuildings = new List<GameObject>();
            _playerUnitPrefabs = new List<GameObject>();
            _playerUnits = new List<GameObject>();
            _enemyUnitPrefabs = new List<GameObject>();
            _enemyUnitPrefabs.Add(Resources.Load("Prefabs/Units/Enemy/NormalEnemy") as GameObject);
            _enemyUnitPrefabs.Add(Resources.Load("Prefabs/Units/Enemy/RangeEnemy") as GameObject);
            _enemyUnits = new List<GameObject>();
            _enemyWave = new List<Dictionary<Constant.eEnemyType, int>>();
            _hitBoxPrefab = Resources.Load("Prefabs/Misc/HitBox") as GameObject;
            _hitEffectPrefab = Resources.Load("Prefabs/Misc/Hiteffect") as GameObject;
            _hitMaterial = Resources.Load("Materials/HitEffectMat") as Material;
            _projectilePrefab = Resources.Load("Prefabs/Misc/Projectile") as GameObject;

            _AddEnemySpawners();
        }

        public void GameStart()
        {
            _isPlaying = true;
            _startPosition = _FindBasePoint();

            _CreateStartBase();
            _CreateCommanderUnit();

            _PrepareResource();

            _StartGameTimer();
            _StartRefreshTimer();

            StartPhase(eGamePhase.TownBuild);

            //StartWave(5, 1, 0);
        }

        public void GameOver()
        {

        }

        public void StartPhase(eGamePhase phase)
        {
            _gamePhase = phase;
            _StartPhaseTimer(phase);
            //Logger.Log(phase.ToString());
        }

        public void StartWave(int unitCountPerWave, float timeRate, int spawnerIndex, Constant.eEnemyType type)
        {
            if (null != _waveStartCoroutine)
            {
                StopCoroutine(_StartWaveCoroutine(unitCountPerWave, timeRate, spawnerIndex, type));
                StartCoroutine(_StartWaveCoroutine(unitCountPerWave, timeRate, spawnerIndex, type));
            }
        }

        public void StartWave(Dictionary<Constant.eEnemyType, int> wave, float timeRate, int spawnerIndex)
        {
            foreach (var item in wave)
            {
                StopCoroutine(_StartWaveCoroutine(item.Value, timeRate, spawnerIndex, item.Key));
                StartCoroutine(_StartWaveCoroutine(item.Value, timeRate, spawnerIndex, item.Key));
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

        void _AddEnemySpawners()
        {
            Spawner[] objs = FindObjectsByType<Spawner>(FindObjectsSortMode.None);
            if (0 < objs.Length)
            {
                for (int i = 0; i < objs.Length; i++)
                    _enemySpawners.Add(objs[i]);
            }
        }

        void _StartGameTimer()
        {
            _gameTimer.TickStart();
        }

        void _CheckPhaseTime()
        {
            UIManager.Instance.RefreshTownStageUI();

            if(_phaseTimer.IsFinish)
            {
                if ((int)_gamePhase + 1 > 2)
                    _gamePhase = 0;
                else
                    _gamePhase++;


                _phaseTimer.ResetTimer();
                StartPhase(_gamePhase);
            }
        }

        void _CheckRefreshTime()
        {
            if (null != _refreshTimer)
            {
                if (_refreshTimer.IsFinish)
                {
                    _refreshTimer.ResetTimer();
                    _StartRefreshTimer();
                }
            }
        }

        void _StartPhaseTimer(eGamePhase phase)
        {
            switch (phase)
            {
                case eGamePhase.TownBuild:
                    _phaseTimer.TickStart(Constant.TOWNBUILD_PHASE_TIME);
                    break;
                case eGamePhase.Hunt:
                    _phaseTimer.TickStart(Constant.HUNT_PHASE_TIME);
                    break;
                case eGamePhase.Defense:
                    _phaseTimer.TickStart(Constant.DEFENSE_PHASE_TIME);
                    break;
            }
        }

        void _StartRefreshTimer()
        {
            if (null != _refreshTimer)
            {
                _refreshTimer.TickStart(Constant.GAME_REFRESH_TIME);
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

        void _CreateCommanderUnit()
        {
            if(null == _commanderUnitObj)
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
                    startPos = _playerBase.gameObject.transform.position + new Vector3(20.0f, 0.0f, /*-(baseExtentsZ + 1.0f)*/ 0.0f);
                }
                _commanderUnitObj = Instantiate(_commanderPrefab, startPos, Quaternion.identity);
                _commanderUnit = _commanderUnitObj.GetComponent<Commander>();
                _playerUnits.Add(_commanderUnitObj);
            }
        }

        IEnumerator  _StartWaveCoroutine(int unitCountPerWave, float timeRate, int spawnerIndex, Constant.eEnemyType type)
        {
            int count = unitCountPerWave;

            if (0 < _enemySpawners.Count)
            {
                while (count > 0)
                {
                    GameObject obj = _enemySpawners[spawnerIndex].CreateObject(_enemyUnitPrefabs[(int)type]);
                    _enemyUnits.Add(obj);
                    count--;
                    yield return new WaitForSeconds(timeRate);
                }
            }
        }

        void _PrepareResource()
        {
            if (null == _resource)
            {
                _resource = new Resource();
            }
        }

        private void Update()
        {
            if(_isPlaying)
            {
                _CheckPhaseTime();
                _CheckRefreshTime();
            }
        }

        #region TEST
        public void CreateCharacter()
        {
            _CreateCommanderUnit();
        }

        public void CreateTestWave()
        {
            Dictionary<Constant.eEnemyType, int> enemyGroup = new Dictionary<Constant.eEnemyType, int>();
            enemyGroup.Add(Constant.eEnemyType.NORMAL, 5);
            enemyGroup.Add(Constant.eEnemyType.RANGE, 2);
            if (null != _enemyWave)
            {
                _enemyWave.Add(enemyGroup);
            }
        }

        #endregion
    }
}