using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Util;

namespace Action.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        float _time;                //���� Ÿ�̸� �ð�
        float _startTime;           //���� ���� �ð�
        bool _isPlaying;            
        Vector3 _startPosition;     //���̽� ��ġ
        GameObject _playerBasePrefab;
        GameObject _playerUnitPrefab;
        GameObject _playerBase;
        GameObject _playerUnit;

        public GameObject GetPlayerBase() { return _playerBase; }
        public GameObject GetPlayerUnit() { return _playerUnit; }

        public override void Initialize()
        {
            base.Initialize();
            _time = 0.0f;
            _playerBasePrefab = Resources.Load("Prefabs/Test/TestBase") as GameObject;
            _playerUnitPrefab = Resources.Load("Prefabs/Test/TestPlayer") as GameObject;
        }

        public void GameStart()
        {
            _isPlaying = true;
            _startPosition = new Vector3(0.0f, 0.1f, 0.0f);
            _StartTimer();
            _CreateStartBase();
            _CreatePlayerUnit();
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
                //�ӽ� ���̽� ������ ����(���� ���� �� ����)
                float baseExtentsZ = 0.0f;
                if (_playerBase.gameObject.TryGetComponent<BoxCollider>(out BoxCollider comp))
                {
                    baseExtentsZ = comp.size.z + 5.0f;     //�ӽ�
                }
                Vector3 startPos = _playerBase.gameObject.transform.position + new Vector3(0.0f, 0.1f, -(baseExtentsZ + 1.0f));

                _playerUnit = GameObject.Instantiate(_playerUnitPrefab, startPos, Quaternion.identity);
            }
        }

        void _StartWave()
        {

        }

        private void Update()
        {
            if(_isPlaying)
                _CalculateTime();
        }
    }
}
