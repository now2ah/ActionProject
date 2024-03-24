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
        public Vector3 spawnPoint;      //임시 몬스터 스폰포인트

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

        Spawner _spawner;
        GameObject _playerBase;
        GameObject _playerUnitObj;

        PlayerUnit _playerUnit;

        //temporary
        GameObject _playerBasePrefab;
        GameObject _playerUnitPrefab;

        List<GameObject> _playerBuildingPrefabs;
        List<GameObject> _playerBuildings;

        List<GameObject> _playerUnitPrefabs;
        List<GameObject> _playerUnits;
        
        List<GameObject> _monsterUnitPrefabs;
        List<GameObject> _monsterUnits;

        [SerializeField]
        Resource _resource;
        public Resource Resource => _resource;

        public Spawner Spawner { get { return _spawner; } set { _spawner = value; } }
        public GameObject PlayerBase { get { return _playerBase; } set { _playerBase = value; } }
        public GameObject PlayerUnitObj { get { return _playerUnitObj; } set { _playerUnitObj = value; } }
        public PlayerUnit PlayerUnit { get { return _playerUnit; } }
        public List<GameObject> PlayerBuildings { get { return _playerBuildings; } }
        public List<GameObject> PlayerUnits { get { return _playerUnits; } }
        public List<GameObject> MonsterUnits { get { return _monsterUnits; } }

        public override void Initialize()
        {
            base.Initialize();
            if (spawnPoint == Vector3.zero)
                spawnPoint = new Vector3(0.0f, Constant.GROUND_Y_POS, 90.0f);
            _waveStartCoroutine = _StartWaveCoroutine(5,1);

            _gameTimer = gameObject.AddComponent<ActionTime>();
            _phaseTimer = gameObject.AddComponent<ActionTime>();
            _refreshTimer = gameObject.AddComponent<ActionTime>();
            _playerBasePrefab = Resources.Load("Prefabs/Buildings/PlayerBase") as GameObject;
            _playerUnitPrefab = Resources.Load("Prefabs/Units/Player/PlayerUnit") as GameObject;
            _playerBuildingPrefabs = new List<GameObject>();
            _playerBuildings = new List<GameObject>();
            _playerUnitPrefabs = new List<GameObject>();
            _playerUnits = new List<GameObject>();
            _monsterUnitPrefabs = new List<GameObject>();
            _monsterUnitPrefabs.Add(Resources.Load("Prefabs/MonsterUnit") as GameObject);
            _monsterUnits = new List<GameObject>();
        }

        public void GameStart()
        {
            _isPlaying = true;
            _startPosition = Constant.VILLAGE_BASE_START_POS;
            _spawner = FindObjectOfType<Spawner>();

            _CreateStartBase();
            _CreatePlayerUnit();

            _PrepareResource();

            _StartGameTimer();
            _StartRefreshTimer();

            StartPhase(eGamePhase.TownBuild);

            _StartWave(5, 1);
        }

        public void GameOver()
        {

        }

        public void StartPhase(eGamePhase phase)
        {
            _gamePhase = phase;
            _StartPhaseTimer(phase);
            Logger.Log(phase.ToString());
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

        void _CreatePlayerUnit()
        {
            if(null == _playerUnitObj)
            {
                //임시 베이스 사이즈 계산식(에셋 적용 후 변경)
                float baseExtentsZ = 0.0f;
                if (_playerBase.gameObject.TryGetComponent<BoxCollider>(out BoxCollider comp))
                {
                    baseExtentsZ = comp.size.z + 5.0f;     //임시
                }
                Vector3 startPos = _playerBase.gameObject.transform.position + new Vector3(20.0f, 0.0f, /*-(baseExtentsZ + 1.0f)*/ 0.0f);

                _playerUnitObj = Instantiate(_playerUnitPrefab, startPos, Quaternion.identity);
                _playerUnit = _playerUnitObj.GetComponent<PlayerUnit>();
                _playerUnits.Add(_playerUnitObj);
            }
        }

        void _StartWave(int unitCountPerWave, float timeRate)
        {
            if (null != _waveStartCoroutine)
            {
                StopCoroutine(_StartWaveCoroutine(unitCountPerWave, timeRate));
                StartCoroutine(_StartWaveCoroutine(unitCountPerWave, timeRate));
            }
        }

        IEnumerator  _StartWaveCoroutine(int unitCountPerWave, float timeRate)
        {
            int count = unitCountPerWave;

            if (null != _spawner)
            {
                while (count > 0)
                {
                    _spawner.CreateObject(_monsterUnitPrefabs[0]);
                    //_CreateMonsterUnit(_monsterUnitPrefabs[0]);
                    count--;
                    yield return new WaitForSeconds(timeRate);
                }
            }
        }

        void _CreateMonsterUnit(GameObject monsterObj)
        {
            GameObject obj = Instantiate(monsterObj, spawnPoint, Quaternion.identity);
            _monsterUnits.Add(obj);
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
    }
}