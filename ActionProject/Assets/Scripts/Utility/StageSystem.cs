using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Action.Game;

namespace Action.Util
{
    public class StageSystem : MonoBehaviour
    {
        
        GameObject _groundObj;
        HuntStageGround _ground;
        Vector3 _startPosition;
        float _floorWidth;
        int[] x = { -2, -1, 0, 1, 2 };
        int[] z = { -2, -1, 0, 1, 2 };
        float _offset;

        [SerializeField]
        public List<List<HuntStageGround>> _groundGrid;

        ObjectPooler<HuntStageGround> _groundPool;

        GameObject _surfaceObj;
        NavMeshSurface _surface;

        Units.Commander _commander;

        public GameObject Ground { get { return _groundObj; } set { _groundObj = value; } }

        public void Initialize(Units.Commander commander)
        {
            _surfaceObj = new GameObject("HuntStageSurface");

            if (null != _groundObj)
            {
                Renderer col = _groundObj.GetComponentInChildren<MeshRenderer>();
                if (null != col)
                {
                    _floorWidth = col.bounds.extents.x * 2;
                }
            }

            _SetUpStage();

            if (null == _commander)
            {
                _commander = commander;
                _commander.NavMeshAgentComp.Warp(_startPosition);
                //_commander.gameObject.transform.position = _startPosition;
            }
        }

        void _SetUpStage()
        {
            if (null != _groundObj)
            {
                _groundPool.Initialize(_ground, 25, this.gameObject);

                for (int i = 0; i < 5; i++)
                {
                    List<HuntStageGround> tempList = new List<HuntStageGround>();
                    for (int j = 0; j < 5; j++)
                    {
                        Vector3 pos = new Vector3(_startPosition.x + (x[i] * _floorWidth * _offset), _startPosition.y, _startPosition.z + (z[j] * _floorWidth * _offset));
                        //GameObject obj = Instantiate(_groundObj, pos, Quaternion.identity);
                        HuntStageGround ground = _groundPool.GetNew();
                        tempList.Add(ground);
                        ground.transform.position = pos;
                        //obj.transform.SetParent(_surfaceObj.transform);
                        //obj.SetActive(true);
                    }
                    _groundGrid.Add(tempList);
                }
                _surface = _surfaceObj.AddComponent<NavMeshSurface>();
                _surface.BuildNavMesh();
            }
        }

        void _CheckCommanderPosition()
        {
            //Right End
            if (_commander.transform.position.x > _startPosition.x + _floorWidth / 2)
            {
                _startPosition = new Vector3(_startPosition.x + (_floorWidth * _offset), _startPosition.y, _startPosition.z);
                
                for (int i=0; i<5; i++)
                {
                    _groundGrid[0][i].Pool.Free(_groundGrid[0][i]);
                }

                _groundGrid.RemoveAt(0);

                List<HuntStageGround> tempList = new List<HuntStageGround>();
                for (int i=0; i<5; i++)
                {
                    Vector3 pos = new Vector3(_startPosition.x + (2 * _floorWidth * _offset), _startPosition.y, _startPosition.z + (z[i] * _floorWidth * _offset));
                    HuntStageGround ground = _groundPool.GetNew();
                    tempList.Add(ground);
                    ground.transform.position = pos;
                }
                _groundGrid.Add(tempList);
                _surface.UpdateNavMesh(_surface.navMeshData);
            } 
            //Left End
            else if (_commander.transform.position.x < _startPosition.x - _floorWidth / 2)
            {
                _startPosition = new Vector3(_startPosition.x - (_floorWidth * _offset), _startPosition.y, _startPosition.z);

                for (int i = 0; i < 5; i++)
                {
                    _groundGrid[4][i].Pool.Free(_groundGrid[4][i]);
                }

                _groundGrid.RemoveAt(4);

                List<HuntStageGround> tempList = new List<HuntStageGround>();

                for (int i = 0; i < 5; i++)
                {
                    Vector3 pos = new Vector3(_startPosition.x - (2 * _floorWidth * _offset), _startPosition.y, _startPosition.z + (z[i] * _floorWidth * _offset));
                    HuntStageGround ground = _groundPool.GetNew();
                    tempList.Add(ground);
                    ground.transform.position = pos;
                }
                _groundGrid.Insert(0, tempList);
                _surface.UpdateNavMesh(_surface.navMeshData);
            }
            //Top End
            else if (_commander.transform.position.z > _startPosition.z + _floorWidth / 2)
            {
                _startPosition = new Vector3(_startPosition.x, _startPosition.y, _startPosition.z + (_floorWidth * _offset));

                for (int i = 0; i < 5; i++)
                {
                    _groundGrid[i][0].Pool.Free(_groundGrid[i][0]);
                    _groundGrid[i].RemoveAt(0);
                }

                List<HuntStageGround> tempList = new List<HuntStageGround>();

                for (int i = 0; i < 5; i++)
                {
                    Vector3 pos = new Vector3(_startPosition.x + (x[i] * _floorWidth * _offset), _startPosition.y, _startPosition.z + (2 * _floorWidth * _offset));
                    HuntStageGround ground = _groundPool.GetNew();
                    tempList.Add(ground);
                    ground.transform.position = pos;
                }

                for(int i=0; i<5; i++)
                    _groundGrid[i].Add(tempList[i]);

                _surface.UpdateNavMesh(_surface.navMeshData);
            }
            //Bottom End
            else if (_commander.transform.position.z < _startPosition.z - _floorWidth / 2)
            {
                _startPosition = new Vector3(_startPosition.x, _startPosition.y, _startPosition.z - (_floorWidth * _offset));

                for (int i = 0; i < 5; i++)
                {
                    _groundGrid[i][4].Pool.Free(_groundGrid[i][4]);
                    _groundGrid[i].RemoveAt(4);
                }

                List<HuntStageGround> tempList = new List<HuntStageGround>();

                for (int i = 0; i < 5; i++)
                {
                    Vector3 pos = new Vector3(_startPosition.x + (x[i] * _floorWidth * _offset), _startPosition.y, _startPosition.z - (2 * _floorWidth * _offset));
                    HuntStageGround ground = _groundPool.GetNew();
                    tempList.Add(ground);
                    ground.transform.position = pos;
                }

                for (int i = 0; i < 5; i++)
                    _groundGrid[i].Insert(0, tempList[i]);

                _surface.UpdateNavMesh(_surface.navMeshData);
            }
        }

        private void Update()
        {
            if (Manager.GameManager.Instance.Phase == Manager.eGamePhase.Hunt)
                _CheckCommanderPosition();
        }

        private void Awake()
        {
            _startPosition = new Vector3(0, 0, 0);
            _offset = 0.8f;
            _groundObj = Resources.Load("Prefabs/Misc/HuntStageGround") as GameObject;
            _ground = _groundObj.GetComponent<HuntStageGround>();
            _groundPool = new ObjectPooler<HuntStageGround>();
            _groundGrid = new List<List<HuntStageGround>>();
        }
    }
}

