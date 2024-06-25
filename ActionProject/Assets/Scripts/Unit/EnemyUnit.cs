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

        float _speed;
        float _attackDamage;
        float _attackSpeed;
        float _attackDistance;
        int _expAmount;

        protected GameObject _target;
        GameObject _nearestPlayerBuilding;
        GameObject _commanderUnit;
        Vector3 _targetPos;

        public float Speed { get { return _speed; } set { _speed = value; } }
        public NavMeshAgent NavMeshAgentComp { get { return _navMeshAgent; } set { _navMeshAgent = value; } }
        public float AttackDamage { get { return _attackDamage; } set { _attackDamage = value; } }
        public float AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }
        public float AttackDistance { get { return _attackDistance; } set { _attackDistance = value; } }
        public int ExpAmount { get { return _expAmount; } set { _expAmount = value; } }

        public GameObject Target { get { return _target; } set { _target = value; } }
        public Vector3 TargetPos { get { return _targetPos; } set { _targetPos = value; } }

        public new void Initialize()
        {
            base.Initialize();
            GameManager.Instance.OnRefresh.AddListener(RefreshTargetPosition);
            if (UnitPanel.TryGetComponent<RectTransform>(out RectTransform comp))
            {
                comp.localScale = new Vector3(0.5f, 0.5f, 1.0f);
            }
            UnitPanel.PanelPosition = UI.ePanelPosition.TOP;
        }

        public void SetSpeed(float speed)
        {
            if (null != _navMeshAgent)
                _navMeshAgent.speed = speed;
        }

        public void FindNearestPlayerBuilding()
        {
            if (1 == GameManager.Instance.PlayerBuildings.Count)
            {
                _nearestPlayerBuilding = GameManager.Instance.PlayerBase;
                return;
            }

            float nearest = Mathf.Infinity;
            for (int i = 0 ; i < GameManager.Instance.PlayerBuildings.Count ; i++)
            {
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

        public void StopMove()
        {
            if (null != _navMeshAgent)
            {
                SetDestination(transform.position);
                _rigidBody.velocity = Vector3.zero;
            }
        }

        public void SetDestination(Vector3 vec)
        {
            if (null != _navMeshAgent)
            {
                _targetPos = vec;
                _navMeshAgent.SetDestination(_targetPos);
            }
        }

        public void SetDestinationToTarget(GameObject target)
        {
            if (null != _navMeshAgent)
            {
                _target = target;
                _targetPos = target.transform.position;
                _navMeshAgent.SetDestination(_targetPos);
            }
        }

        public virtual void RefreshTargetPosition()
        {
            SetDestinationToTarget(_target);
        }

        protected void _ResetTarget()
        {
            if (!_navMeshAgent.isStopped)
                SetDestination(transform.position);

            _target = null;
        }

        protected override void _Dead(Unit damager)
        {
            base._Dead(damager);
            _DisableMove();
            _ApplyPhysicsEffect();
            _FreeObjectCoroutine();
            _GiveExp((PlayerUnit)damager, _expAmount);
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
            _rigidBody.drag = 0.0f;
            _rigidBody.angularDrag = 0.0f;
            Vector3 explosionPos = transform.position + new Vector3(Random.Range(-5.0f, 5.0f), 0f, Random.Range(-5.0f, 5.0f));
            _rigidBody.AddExplosionForce(500f, explosionPos, 5.0f, -1.0f);
        }

        protected void _FreeObject()
        {
            GameManager.Instance.EnemyUnits.Remove(this.gameObject);
            UnitPanel.Hide();
            Pool.Free(this);
        }

        protected void _FreeObjectCoroutine()
        {
            StopCoroutine(DeathCoroutine());
            StartCoroutine(DeathCoroutine());
        }

        IEnumerator DeathCoroutine()
        {
            yield return new WaitForSeconds(1.0f);
            _FreeObject();
        }

        protected void _GiveExp(PlayerUnit damager, int exp)
        {
            damager.GainExp(exp);
        }

        protected override void Awake()
        {
            base.Awake();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _rigidBody = GetComponent<Rigidbody>();
            _attackDamage = 1;
            _targetPos = Vector3.zero;
        }

        protected override void Start()
        {
            base.Start();
            Initialize();
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
