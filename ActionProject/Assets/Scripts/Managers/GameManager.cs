using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        public Vector3 spawnPoint;

        float _gameTime;                //게임 타이머 시간
        float _gameStartTime;           //게임 시작 시간
        eGamePhase _gamePhase;
        float _phaseTime;               //페이즈 타이머 시간
        public float PhaseTime => _phaseTime;
        float _phaseStartTime;          //페이즈 시작 시간
        float _phaseEndTime;            //페이즈 종료 시간
        bool _isPlaying;            
        
        Vector3 _startPosition;         //베이스 위치
        IEnumerator _waveStartCoroutine;


        GameObject _playerBase;
        GameObject _playerUnitObj;

        PlayerUnit _playerUnit;

        //temporary
        GameObject _playerBasePrefab;
        GameObject _playerUnitPrefab;

        List<GameObject> _playerBuildingPrefabs;
        ArrayList _playerBuildings;

        List<GameObject> _playerUnitPrefabs;
        ArrayList _playerUnits;
        
        List<GameObject> _monsterUnitPrefabs;
        ArrayList _monsterUnits;

        [SerializeField]
        Resource _resource;
        public Resource Resource => _resource;

        public GameObject PlayerBase { get { return _playerBase; } set { _playerBase = value; } }
        public GameObject PlayerUnitObj { get { return _playerUnitObj; } set { _playerUnitObj = value; } }
        public PlayerUnit PlayerUnit { get { return _playerUnit; } }
        public ArrayList PlayerBuildings { get { return _playerBuildings; } }
        public ArrayList PlayerUnits { get { return _playerUnits; } }
        public ArrayList MonsterUnits { get { return _monsterUnits; } }

        public override void Initialize()
        {
            base.Initialize();
            if (spawnPoint == Vector3.zero)
                spawnPoint = new Vector3(0.0f, Constant.GROUND_Y_POS, 90.0f);
            _gameTime = 0.0f;
            _phaseTime = 0.0f;
            _waveStartCoroutine = _StartWaveCoroutine(5,1);

            _playerBasePrefab = Resources.Load("Prefabs/Buildings/PlayerBase") as GameObject;
            _playerUnitPrefab = Resources.Load("Prefabs/Units/Player/PlayerUnit") as GameObject;
            _playerBuildingPrefabs = new List<GameObject>();
            _playerBuildings = new ArrayList();
            _playerUnitPrefabs = new List<GameObject>();
            _playerUnits = new ArrayList();
            _monsterUnitPrefabs = new List<GameObject>();
            _monsterUnitPrefabs.Add(Resources.Load("Prefabs/MonsterUnit") as GameObject);
            _monsterUnits = new ArrayList();
        }

        public void GameStart()
        {
            _isPlaying = true;
            _startPosition = Constant.VILLAGE_BASE_START_POS;
            
            _CreateStartBase();
            _CreatePlayerUnit();

            _PrepareResource();

            _StartGameTimer();

            StartPhase(eGamePhase.TownBuild);

            //_StartWave(1, 1);
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
            _gameStartTime = Time.time;
        }

        void _CalculateTime()
        {
            _gameTime = Time.time - _gameStartTime;
            _phaseTime = Time.time - _phaseStartTime;

            if (_phaseTime != Mathf.Floor(_phaseTime * 10.0f) / 10.0f)
            {
                _phaseTime = Mathf.Floor(_phaseTime * 10.0f) / 10.0f;
                UIManager.Instance.RefreshTownStageUI();
            }

            if (_phaseTime > _phaseEndTime)
            {
                _EndPhase();
                _phaseTime = 0.0f;
            }
        }

        void _StartPhaseTimer(eGamePhase phase)
        {
            _phaseStartTime = Time.time;
            _phaseTime = 0.0f;

            switch (phase)
            {
                case eGamePhase.TownBuild:
                    _phaseEndTime = Constant.TOWNBUILD_PHASE_TIME;
                    break;
                case eGamePhase.Hunt:
                    _phaseEndTime = Constant.HUNT_PHASE_TIME;
                    break;
                case eGamePhase.Defense:
                    _phaseEndTime = Constant.DEFENSE_PHASE_TIME;
                    break;
            }
        }

        void _EndPhase()
        {
            _phaseTime = 0.0f;

            if ((int)_gamePhase + 1 > 2)
                _gamePhase = 0;
            else
                _gamePhase++;

            StartPhase(_gamePhase);
        }

        void _CreateStartBase()
        {
            if (null == _playerBase)
            {
                _playerBase = GameObject.Instantiate(_playerBasePrefab, _startPosition, Quaternion.identity);
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

                _playerUnitObj = GameObject.Instantiate(_playerUnitPrefab, startPos, Quaternion.identity);
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

            while (count > 0)
            {
                _CreateMonsterUnit(_monsterUnitPrefabs[0]);
                count--;
                yield return new WaitForSeconds(timeRate);
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
                _CalculateTime();
            }
        }
    }
}