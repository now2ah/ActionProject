using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Action.Util
{
    public class StageSystem : MonoBehaviour
    {
        public GameObject _floor;
        Vector3 _startPosition;
        float _floorWidth;
        int[] x = { -2, -1, 0, 1, 2 };
        int[] z = { -2, -1, 0, 1, 2 };
        float _offset;

        NavMeshSurface _surface;

        Units.Unit _commander;

        public void Initialize(Units.Unit commander)
        {
            _SetUpStage();
            if (null == _commander)
            {
                _commander = commander;
                _commander.transform.position = _startPosition;
            }
        }

        void _SetUpStage()
        {
            if (null != _floor)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        Vector3 pos = new Vector3(_startPosition.x + (x[i] * _floorWidth * _offset), _startPosition.y, _startPosition.z + (z[j] * _floorWidth * _offset));
                        GameObject obj = Instantiate(_floor, pos, Quaternion.identity);
                        obj.SetActive(true);
                    }
                }

                _surface = _floor.AddComponent<NavMeshSurface>();
                _surface.BuildNavMesh();
            }
        }

        void _CheckCommanderPosition()
        {
            if (_commander.transform.position.x < _startPosition.x + _floorWidth / 2)
            {
                _startPosition = new Vector3(_startPosition.x + _floorWidth, _startPosition.y, _startPosition.z);
                for (int i=0; i<5; i++)
                {
                    Vector3 pos = new Vector3(_startPosition.x + 2 * _floorWidth * _offset, _startPosition.y, _startPosition.z + (z[i] * _floorWidth * _offset));
                    GameObject obj = Instantiate(_floor, pos, Quaternion.identity);
                    obj.SetActive(true);
                }
            }
        }

        private void Update()
        {
            _CheckCommanderPosition();
        }

        private void Start()
        {
            if (null != _floor)
            {
                Renderer col = _floor.GetComponentInChildren<MeshRenderer>();
                if (null != col)
                {
                    _floorWidth = col.bounds.extents.x * 2;
                }
            }
        }

        private void Awake()
        {
            _startPosition = Vector3.zero;
            _offset = 0.8f;
        }
    }
}

