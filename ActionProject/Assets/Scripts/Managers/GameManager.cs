using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Util;
using Action.UI;
using Action.Units;

namespace Action.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        public Vector3 spawnPoint;

        float _time;                //���� Ÿ�̸� �ð�
        float _startTime;           //���� ���� �ð�
        bool _isPlaying;            
        Vector3 _startPosition;     //���̽� ��ġ
        IEnumerator _waveStartCoroutine;

        GameObject _playerBase;
        GameObject _playerUnit;

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
        Resources _resources;

        public GameObject PlayerBase { get { return _playerBase; } set { _playerBase = value; } }
        public GameObject PlayerUnit { get { return _playerUnit; } set { _playerUnit = value; } }
        public ArrayList PlayerBuildings { get { return _playerBuildings; } }
        public ArrayList PlayerUnits { get { return _playerUnits; } }
        public ArrayList MonsterUnits { get { return _monsterUnits; } }

        public override void Initialize()
        {
            base.Initialize();
            if (spawnPoint == Vector3.zero)
                spawnPoint = new Vector3(0.0f, Constant.GROUND_Y_POS, 90.0f);
            _time = 0.0f;
            _waveStartCoroutine = _StartWaveCoroutine(5,1);

            _playerBasePrefab = Resources.Load("Prefabs/Buildings/PlayerBase") as GameObject;
            _playerUnitPrefab = Resources.Load("Prefabs/PlayerUnit") as GameObject;
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
            _StartTimer();
            _CreateStartBase();
            _CreatePlayerUnit();
            _StartWave(1, 1);
        }

        public void GameOver()
        {

        }


        void _StartTimer()
        {
            _startTime = Time.realtimeSinceStartup;
        }
        
        void _CalculateTime()
        {
            _time = Time.realtimeSinceStartup - _startTime;
            Logger.Log(_time.ToString());
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
            if(null == _playerUnit)
            {
                //�ӽ� ���̽� ������ ����(���� ���� �� ����)
                float baseExtentsZ = 0.0f;
                if (_playerBase.gameObject.TryGetComponent<BoxCollider>(out BoxCollider comp))
                {
                    baseExtentsZ = comp.size.z + 5.0f;     //�ӽ�
                }
                Vector3 startPos = _playerBase.gameObject.transform.position + new Vector3(20.0f, 0.0f, /*-(baseExtentsZ + 1.0f)*/ 0.0f);

                _playerUnit = GameObject.Instantiate(_playerUnitPrefab, startPos, Quaternion.identity);
                if (_playerUnit.TryGetComponent<PlayerUnit>(out PlayerUnit unit))
                {
                    unit.UnitName = "Commander";

                    UnitInfoPanel unitInfo = _playerUnit.GetComponentInChildren<UnitInfoPanel>();
                    if (null != unitInfo)
                    {
                        float testHP = 1000f;
                        unitInfo.SetName(unit.UnitName);
                        unitInfo.ApplyHPValue(testHP);
                    }
                }

                _playerUnits.Add(_playerUnit);
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

        void _PrepareResources()
        {
            if (null == _resources)
            {
                _resources = new Resources();

            }
        }

        private void Update()
        {
            if(_isPlaying)
                _CalculateTime();
        }
    }
}