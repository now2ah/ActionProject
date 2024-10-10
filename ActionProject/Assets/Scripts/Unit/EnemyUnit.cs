using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Action.State;
using Action.Util;
using Action.Manager;

namespace Action.Units
{
    public class EnemyUnit : Unit, IMovable, IPoolable<EnemyUnit>
    {
        public int PoolID { get; set; }
        public ObjectPooler<EnemyUnit> Pool { get; set; }

        NavMeshAgent _navMeshAgent;
        Rigidbody _rigidBody;

        bool _isDead;
        protected bool _isMoving;
        protected bool _isAttacking;

        protected GameObject _target;
        GameObject _nearestPlayerBuilding;
        GameObject _commanderUnit;
        Vector3 _targetPos;

        public NavMeshAgent NavMeshAgentComp { get { return _navMeshAgent; } set { _navMeshAgent = value; } }
        public bool IsDead { get { return _isDead; } set { _isDead = value; } }
        public bool IsMoving { get { return _isMoving; } set { _isMoving = value; } }
        public bool IsAttacking { get { return _isAttacking; } set { _isAttacking = value; } }

        public GameObject Target { get { return _target; } set { _target = value; } }
        public Vector3 TargetPos { get { return _targetPos; } set { _targetPos = value; } }

        public override void Initialize()
        {
            base.Initialize();
            GameManager.Instance.OnRefresh.AddListener(RefreshTargetPosition);
            if (UnitPanel.TryGetComponent<RectTransform>(out RectTransform comp))
            {
                comp.localScale = new Vector3(0.5f, 0.5f, 1.0f);
            }
            UnitPanel.PanelPosition = UI.ePanelPosition.TOP;
            _isDead = false;
        }

        public void SetSpeed(float speed)
        {
            if (null != _navMeshAgent)
                _navMeshAgent.speed = speed;
        }

        public void FindNearestPlayerBuilding()
        {
            if (0 == GameManager.Instance.PlayerBuildings.Count)
                return;

            if (1 == GameManager.Instance.PlayerBuildings.Count)
            {
                _nearestPlayerBuilding = GameManager.Instance.PlayerBase;
                return;
            }

            float nearest = Mathf.Infinity;
            for (int i = 0 ; i < GameManager.Instance.PlayerBuildings.Count ; i++)
            {
                if (GameManager.Instance.PlayerBuildings[i].TryGetComponent<Building>(out Building comp))
                {
                    BuildingData buildingData = comp.UnitData as BuildingData;
                    if (!buildingData.isBuilt)
                        continue;
                }

                Vector3 dist = GameManager.Instance.PlayerBuildings[i].gameObject.transform.position - gameObject.transform.position;
                if (nearest > dist.sqrMagnitude)
                {
                    nearest = dist.sqrMagnitude;
                    _nearestPlayerBuilding = GameManager.Instance.PlayerBuildings[i];
                }
            }
        }

        public void FindNearestTarget(bool isIncludeCmd)  /*Commander 포함 여부*/
        {
            float nearest = Mathf.Infinity;
            for (int i = 0 ; i < GameManager.Instance.PlayerUnits.Count ; i++)
            {
                if (!isIncludeCmd && GameManager.Instance.PlayerUnits[i] == GameManager.Instance.CommanderUnit)
                    continue;

                Vector3 dist = GameManager.Instance.PlayerUnits[i].gameObject.transform.position - gameObject.transform.position;
                if (nearest > dist.sqrMagnitude)
                {
                    nearest = dist.sqrMagnitude;
                    _target = GameManager.Instance.PlayerUnits[i];
                }
            }

            //nearestBuilding이랑 거리 비교
            if (null != _nearestPlayerBuilding)
            {
                GameObject target = Utility.GetNearerObject(gameObject.transform.position, _target, _nearestPlayerBuilding);
                _target = target;
            }
        }

        public float GetTargetDistance()
        {
            if (null == _target)
                return 0;

            Vector3 distVector = _target.gameObject.transform.position - gameObject.transform.position;
            float dist = distVector.sqrMagnitude;
            return dist;
        }

        public void SetTargetPosition()
        {
            if(null != _target)
            {

            }
        }

        public void Look(GameObject target)
        {
            gameObject.transform.LookAt(target.transform);
        }

        public void StopAgent()
        {
            if (null != _navMeshAgent && _navMeshAgent.isOnNavMesh)
            {
                _navMeshAgent.velocity = Vector3.zero;
                _navMeshAgent.isStopped = true;
                _navMeshAgent.updateRotation = false;
            }
        }

        public void ResetPath()
        {
            if (null != _navMeshAgent && _navMeshAgent.isOnNavMesh)
            {
                _navMeshAgent.ResetPath();
            }
        }

        public void Stop()
        {
            StopAgent();
            ResetPath();
        }

        public void SetDestination(Vector3 vec)
        {
            if (null != _navMeshAgent && _navMeshAgent.isOnNavMesh)
            {
                _navMeshAgent.isStopped = false;
                _navMeshAgent.updateRotation = true;
                _targetPos = vec;
                _navMeshAgent.SetDestination(_targetPos);
            }
        }

        public void SetDestinationToTarget(GameObject target)
        {
            if (null != _navMeshAgent && _navMeshAgent.isOnNavMesh)
            {
                _navMeshAgent.isStopped = false;
                _navMeshAgent.updateRotation = true;
                _target = target;
                if (_target.TryGetComponent<Building>(out Building comp))
                {
                    if (_target.TryGetComponent<Collider>(out Collider col))
                    {
                        _targetPos = col.bounds.ClosestPoint(transform.position);
                    }
                }
                else
                    _targetPos = target.transform.position;

                if (_navMeshAgent.isOnNavMesh)
                    _navMeshAgent.SetDestination(_targetPos);
            }
        }

        public virtual void RefreshTargetPosition()
        {
            if (!gameObject.activeSelf)
                return;

            //SetDestinationToTarget(_target);
        }

        protected override void _Dead(Unit damager)
        {
            base._Dead(damager);
            _DisableMove();
            _ApplyPhysicsEffect();
            _FreeObjectCoroutine();
            _GenerateExpOrb();
            if (damager.TryGetComponent<PlayerUnit>(out PlayerUnit playerUnit))
            {
                _CreateFloatingUI(((EnemyUnitData)UnitData).goldAmount);
                _GiveGold(((EnemyUnitData)UnitData).goldAmount);
            }
            int randNum = Random.Range(0, 100);
            if (randNum > 50)
                _GenerateCoin();
        }

        protected void _DisableMove()
        {
            GameManager.Instance.OnRefresh.RemoveListener(RefreshTargetPosition);
            _navMeshAgent.enabled = false;
            StateMachine.IsRunning = false;
        }

        protected void _ApplyPhysicsEffect()
        {
            _rigidBody.useGravity = true;
            //_rigidBody.drag = 0.0f;
            _rigidBody.angularDrag = 0.0f;
            Vector3 explosionPos = transform.position + new Vector3(Random.Range(-5.0f, 5.0f), 0f, Random.Range(-5.0f, 5.0f));
            _rigidBody.AddExplosionForce(500f, explosionPos, 5.0f, -1.0f);
        }

        protected void _FreeObject()
        {
            UnitPanel.Hide();
            _rigidBody.useGravity = false;
            //_rigidBody.drag = 0.0f;
            _rigidBody.angularDrag = 0.5f;
            _navMeshAgent.enabled = true;
            GameManager.Instance.EnemyUnits.Remove(this.gameObject);
            Pool.Free(this);
        }

        protected void _FreeObjectCoroutine()
        {
            //StopCoroutine(DeathCoroutine());
            StartCoroutine(DeathCoroutine());
        }

        IEnumerator DeathCoroutine()
        {
            _isDead = true;
            yield return new WaitForSeconds(0.5f);
            _FreeObject();
        }

        protected void _GiveExp(PlayerUnit damager, int exp)
        {
            damager.GainExp(exp);
        }

        protected void _GiveGold(int gold)
        {
            GameManager.Instance.GameData.resource.Gold += gold;
        }

        protected void _GenerateExpOrb()
        {
            Game.ExpOrb expOrb = PoolManager.Instance.ExpOrbPool.GetNew();
            expOrb.Initialize(((EnemyUnitData)UnitData).expAmount);
            float angle = Random.Range(0.0f, 360.0f);
            Vector3 rotation = transform.rotation.eulerAngles + new Vector3(0.0f, angle, 0.0f);
            Vector3 position = new Vector3(transform.position.x, (GameManager.Instance.Constants.HUNT_PROJECTILE_Y_POS) * 1.0f, transform.position.z) + Vector3.forward * 1.0f;

            expOrb.transform.rotation = Quaternion.Euler(rotation);
            expOrb.transform.position = position;
        }

        protected void _GenerateCoin()
        {
            GameObject coinObj = Instantiate(GameManager.Instance.CoinPrefab, new Vector3(transform.position.x, (GameManager.Instance.Constants.HUNT_PROJECTILE_Y_POS) * 1.0f, transform.position.z), Quaternion.identity);
            if (coinObj.TryGetComponent<Game.Coin>(out Game.Coin comp))
                comp.Initialize(((EnemyUnitData)UnitData).goldAmount);
        }

        void _CreateFloatingUI(int gold)
        {
            GameObject floatingUI = UIManager.Instance.CreateUI("FloatingPanel", UIManager.Instance.InGameCanvas);
            floatingUI.transform.position = CameraManager.Instance.MainCamera.Camera.WorldToScreenPoint(transform.position);
            if (floatingUI.TryGetComponent<UI.FloatingPanelUI>(out UI.FloatingPanelUI comp))
            {
                comp.Initialize(gameObject);
                comp.Text.text = "+" + gold.ToString();
                comp.Text.color = Color.yellow;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _rigidBody = GetComponent<Rigidbody>();
            UnitData = new EnemyUnitData();
            _targetPos = Vector3.zero;
            _navMeshAgent.acceleration = 60.0f;
            _navMeshAgent.autoBraking = false;
        }

        protected override void Start()
        {
            base.Start();
            //Initialize();
            _target = null;
            _nearestPlayerBuilding = null;
            _commanderUnit = GameManager.Instance.CommanderObj;
        }

        protected override void Update()
        {
            base.Update();
        }
    }

}
