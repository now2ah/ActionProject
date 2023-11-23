using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Util;

namespace Action.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        public Vector3 spawnPoint;

        float _time;                //게임 타이머 시간
        float _startTime;           //게임 시작 시간
        bool _isPlaying;            
        Vector3 _startPosition;     //베이스 위치
        IEnumerator _waveStartCoroutine;

        GameObject _playerBasePrefab;
        GameObject _playerUnitPrefab;
        GameObject _playerBase;
        GameObject _playerUnit;
        List<GameObject> _MonsterUnitPrefabs;
        ArrayList _MonsterUnits;

        public GameObject PlayerBase { get { return _playerBase; } set { _playerBase = value; } }
        public GameObject PlayerUnit { get { return _playerUnit; } set { _playerUnit = value; } }

        public override void Initialize()
        {
            base.Initialize();
            if (spawnPoint == Vector3.zero)
                spawnPoint = new Vector3(0.0f, 1.8f, 90.0f);
            _time = 0.0f;
            _waveStartCoroutine = _StartWaveCoroutine(5,1);

            _playerBasePrefab = Resources.Load("Prefabs/Test/TestBase") as GameObject;
            _playerUnitPrefab = Resources.Load("Prefabs/PlayerUnit") as GameObject;
            _MonsterUnitPrefabs = new List<GameObject>();
            _MonsterUnitPrefabs.Add(Resources.Load("Prefabs/MonsterUnit") as GameObject);
            _MonsterUnits = new ArrayList();
        }

        public void GameStart()
        {
            _isPlaying = true;
            _startPosition = new Vector3(0.0f, 0.1f, 0.0f);
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
            //Debug.Log(_time);
        }

        void _CreateStartBase()
        {
            if (null == _playerBase)
                _playerBase = GameObject.Instantiate(_playerBasePrefab, _startPosition, Quaternion.identity);
        }

        void _CreatePlayerUnit()
        {
            if(null == _playerUnit)
            {
                //임시 베이스 사이즈 계산식(에셋 적용 후 변경)
                float baseExtentsZ = 0.0f;
                if (_playerBase.gameObject.TryGetComponent<BoxCollider>(out BoxCollider comp))
                {
                    baseExtentsZ = comp.size.z + 5.0f;     //임시
                }
                Vector3 startPos = _playerBase.gameObject.transform.position + new Vector3(0.0f, 0.1f, -(baseExtentsZ + 1.0f));

                _playerUnit = GameObject.Instantiate(_playerUnitPrefab, startPos, Quaternion.identity);
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

        IEnumerator _StartWaveCoroutine(int unitCountPerWave, float timeRate)
        {
            int count = unitCountPerWave;

            while (count > 0)
            {
                _CreateMonsterUnit(_MonsterUnitPrefabs[0]);
                count--;
                yield return new WaitForSeconds(timeRate);
            }
        }

        void _CreateMonsterUnit(GameObject monsterObj)
        {
            GameObject obj = Instantiate(monsterObj, spawnPoint, Quaternion.identity);
            _MonsterUnits.Add(obj);
            Debug.Log("SPAWNED");
        }

        private void Update()
        {
            if(_isPlaying)
                _CalculateTime();
        }
    }
}
