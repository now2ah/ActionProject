using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Action.State;
using Action.Manager;
using Action.SO;
using Action.Game;

namespace Action.Units
{
    public class Commander : PlayerUnit
    {
        UnitStatsSO _unitStats;
        Vector2 inputVector;

        CommanderIdleState _idleState;
        CommanderMoveState _moveState;
        CommanderAttackState _attackState;

        GameObject _interactingBuilding;

        int _animHashMoving;
        int _animHashAttacking;

        AutoAttackAbilty[] _autoAttackSlots;

        public UnityAction OnDamaged;
        public UnityEvent<int, int> OnGainExp;

        public CommanderIdleState IdleState => _idleState;
        public CommanderMoveState MoveState => _moveState;
        public CommanderAttackState AttackState => _attackState;
        public GameObject InteractingBuilding { get { return _interactingBuilding; } set { _interactingBuilding = value; } }
        public int AnimHashMoving => _animHashMoving;
        public int AnimHashAttacking => _animHashAttacking;

        public new void Initialize()
        {
            base.Initialize();
            UnitName = _unitStats.unitName;
            MaxHp = _unitStats.maxHp;
            GrowthHp = _unitStats.growthMaxHp;
            HP = _unitStats.maxHp;
            Speed = _unitStats.speed;
            AttackDamage = _unitStats.attackDamage;
            GrowthAttackDamage = _unitStats.growthAttackDamage;
            Level = 1;
            Exp = 0;
            NextExp = 50;
            SetNameUI(UnitName);
            UnitPanel.Show();
            _animHashMoving = Animator.StringToHash("isMoving");
            _animHashAttacking = Animator.StringToHash("isAttacking");
            OnDamaged += UIManager.Instance.ShowDamagedEffect;
            OnGainExp.AddListener(UIManager.Instance.ExpBarUI.ApplyExpValue);
            _autoAttackSlots = new AutoAttackAbilty[GameManager.Instance.Constants.AUTOATTACK_TYPE_COUNT];
            _SetAutoAttackAbilities();
        }

        public void Interact()
        {
            if (null != _interactingBuilding)
                _interactingBuilding.GetComponent<Building>().Interact();
        }

        public override void Move()
        {
            Vector3 movePos = new Vector3(inputVector.x, 0, inputVector.y);

            //if (StateMachine.IsState(_moveState))
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movePos), 0.15f);

            transform.Translate(movePos * Time.deltaTime * Speed, Space.World);
        }

        public override void ApplyDamage(DamageMessage msg)
        {
            base.ApplyDamage(msg);
            OnDamaged.Invoke();
        }

        public void PhysicalAttack()
        {
            StopCoroutine(PhysicalAttackCoroutine());
            StartCoroutine(PhysicalAttackCoroutine());
        }

        public void ActivateAutoAttack(int index)
        {
            if (_autoAttackSlots.Length > 0)
            {
                _autoAttackSlots[index].IsActivated = true;
            }
        }

        public override void GainExp(int exp)
        {
            base.GainExp(exp);
            OnGainExp.Invoke(Exp, NextExp);
        }

        void OnMove(InputAction.CallbackContext context)
        {
            if (!_isAttacking)
            {
                StateMachine.ChangeState(_moveState);
                inputVector = context.ReadValue<Vector2>();
            }
        }

        void OnMoveCanceled(InputAction.CallbackContext context)
        {
            if (!_isAttacking)
                StateMachine.ChangeState(_idleState);
            //inputVector = Vector3.zero;
        }

        void OnActionPressed(InputAction.CallbackContext context)
        {
            if (context.performed)
                Interact();
        }

        void OnPhysicalAttackPressed(InputAction.CallbackContext context)
        {
            if (context.performed)
                StateMachine.ChangeState(_attackState);
        }

        void _CheckUnableInteractBuilding()
        {
            if (null == _interactingBuilding)
                return;
            else
            {
                float dist = Vector3.Distance(transform.position, _interactingBuilding.transform.position);
                if (_interactingBuilding.GetComponent<Building>().ActiveDistance < dist)
                    _interactingBuilding = null;
            }
        }

        void _CreateHitBox(float attackDamage)
        {
            //나중에 풀링으로 변경
            Vector3 pos = transform.position + transform.forward * 2.0f + transform.up * 1.5f;
            GameObject hitboxObj = Instantiate(GameManager.Instance.HitBoxPrefab, pos, Quaternion.identity);
            if (hitboxObj.TryGetComponent<Game.HitBox>(out Game.HitBox comp))
            {
                comp.Initialize(Enums.eHitBoxType.ONLY_ENEMY, this, attackDamage);
            }
        }

        IEnumerator PhysicalAttackCoroutine()
        {
            _isAttacking = true;
            _animator.SetBool(_animHashAttacking, _isAttacking);
            yield return new WaitForSeconds(0.2f);
            _CreateHitBox(AttackDamage);
            yield return new WaitForSeconds(0.3f);
            _isAttacking = false;
            _animator.SetBool(_animHashAttacking, _isAttacking);
            StateMachine.ChangeState(_idleState);
        }

        void _SetAutoAttackAbilities()
        {
            //test
            DirectionalAttack directional = gameObject.AddComponent<DirectionalAttack>();
            _autoAttackSlots[0] = directional;

            //ActivateAutoAttack(0);
        }

        protected override void Awake()
        {
            base.Awake();
            _unitStats = Resources.Load("ScriptableObject/UnitStats/CommanderStats") as UnitStatsSO;
            _isMoving = false;
            _interactingBuilding = null;
            _animator = GetComponentInChildren<Animator>();
        }

        protected override void Start()
        {
            base.Start();
            Initialize();
            InputManager.Instance.actionMove.performed += ctx => { OnMove(ctx); };
            InputManager.Instance.actionMove.canceled += ctx => { OnMoveCanceled(ctx); };
            InputManager.Instance.actionAction.performed += ctx => { OnActionPressed(ctx); };
            InputManager.Instance.actionPhysicalAttack.performed += ctx => { OnPhysicalAttackPressed(ctx); };
            _idleState = new CommanderIdleState(this);
            _moveState = new CommanderMoveState(this);
            _attackState = new CommanderAttackState(this);
            StateMachine.Initialize(_idleState);
        }

        protected override void Update()
        {
            base.Update();
            if (!_isAttacking && InputManager.Instance.actionMove.IsPressed())
            {
                inputVector = InputManager.Instance.actionMove.ReadValue<Vector2>();
                StateMachine.ChangeState(_moveState);
            }
                
            _CheckUnableInteractBuilding();
        }
    }
}
