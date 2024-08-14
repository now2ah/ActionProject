using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Action.State;
using Action.Manager;
using Action.SO;
using Action.Game;
using Action.Util;

namespace Action.Units
{
    public class Commander : PlayerUnit
    {
        UnitStatsSO _unitStats;
        [SerializeReference]
        float _dashDistant;

        [SerializeReference]
        float _dashCooltime;

        Vector2 _lookInput;
        Vector2 _moveInput;
        Vector3 _lookPos;

        CommanderIdleState _idleState;
        CommanderMoveState _moveState;
        CommanderAttackState _attackState;

        ActionTime _dashTimer;

        GameObject _interactingBuilding;

        int _animHashMoving;
        int _animHashAttacking;

        Ability[] _abilitySlots;
        AutoAttackAbility[] _autoAttackSlots;

        public UnityAction OnDamaged;
        public UnityEvent<int, int> OnGainExp;

        public CommanderIdleState IdleState => _idleState;
        public CommanderMoveState MoveState => _moveState;
        public CommanderAttackState AttackState => _attackState;
        public GameObject InteractingBuilding { get { return _interactingBuilding; } set { _interactingBuilding = value; } }
        public int AnimHashMoving => _animHashMoving;
        public int AnimHashAttacking => _animHashAttacking;

        public override void Initialize()
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
            _abilitySlots = new Ability[GameManager.Instance.Constants.ABILITY_COUNT];
            _autoAttackSlots = new AutoAttackAbility[GameManager.Instance.Constants.AUTOATTACK_TYPE_COUNT];
            _SetAbilities();
            _SetAutoAttackAbilities();
            DontDestroyOnLoad(this);
        }

        public void Interact()
        {
            if (null != _interactingBuilding)
                _interactingBuilding.GetComponent<Building>().Interact();
        }

        public override void Move()
        {
            Vector3 movePos = new Vector3(_moveInput.x, 0, _moveInput.y);

            if (!InputManager.Instance.Click.IsPressed())
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

        public void ActivateAbility(Enums.eAbility ability)
        {
            if (0 < _abilitySlots.Length)
                _abilitySlots[(int)ability].IsActivated = true;
        }

        public void ActivateAutoAttack(int index)
        {
            if (_autoAttackSlots.Length > 0)
                _autoAttackSlots[index].IsActivated = true;
        }

        public override void GainExp(int exp)
        {
            base.GainExp(exp);
            OnGainExp.Invoke(Exp, NextExp);
        }

        void OnMousePosition(InputAction.CallbackContext context)
        {
            Vector3 camPos = CameraManager.Instance.MainCamera.Camera.WorldToScreenPoint(transform.position);
            _lookInput = context.ReadValue<Vector2>();
            _lookPos = CameraManager.Instance.MainCamera.Camera.ScreenToWorldPoint(new Vector3(_lookInput.x, _lookInput.y, camPos.z));
        }

        void OnClick(InputAction.CallbackContext context)
        {
            Vector3 dir = _lookPos - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir.normalized), 0.15f);
        }

        void OnMove(InputAction.CallbackContext context)
        {
            if (!_isAttacking)
            {
                StateMachine.ChangeState(_moveState);
                _moveInput = context.ReadValue<Vector2>();
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
            if (_abilitySlots[(int)Enums.eAbility.PHYSICAL].IsActivated && 
                context.performed)
                StateMachine.ChangeState(_attackState);
        }

        void OnTeleport(InputAction.CallbackContext context)
        {
            if (!_dashTimer.IsStarted)
            {
                _Dash();
                _dashTimer.TickStart(_dashCooltime);
            }
        }

        void _CheckClick()
        {
            if (InputManager.Instance.Click.IsPressed())
            {
                Vector3 dir = _lookPos - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir.normalized), 0.15f);
            }
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

        void _SetAbilities()
        {
            PhysicalAttack physical = gameObject.AddComponent<PhysicalAttack>();
            _abilitySlots[0] = physical;
            DirectionalAttack directional = gameObject.AddComponent<DirectionalAttack>();
            _abilitySlots[1] = directional;
            GuidanceAttack guidance = gameObject.AddComponent<GuidanceAttack>();
            _abilitySlots[2] = guidance;
            DamageUpAbility damageUp = gameObject.AddComponent<DamageUpAbility>();
            _abilitySlots[3] = damageUp;
            HPUpAbility hpUp = gameObject.AddComponent<HPUpAbility>();
            _abilitySlots[4] = hpUp;
            SpeedUpAbility speedUpAbility = gameObject.AddComponent<SpeedUpAbility>();
            _abilitySlots[5] = speedUpAbility;
        }

        void _SetAutoAttackAbilities()
        {
            //test
            DirectionalAttack directional = gameObject.AddComponent<DirectionalAttack>();
            _autoAttackSlots[0] = directional;
            GuidanceAttack guidance = gameObject.AddComponent<GuidanceAttack>();
            _autoAttackSlots[1] = guidance;

            //ActivateAutoAttack(0);
        }

        void _Dash()
        {
            Vector3 newPos = transform.position + transform.forward * _dashDistant;
            transform.position = newPos;
        }

        protected override void Awake()
        {
            base.Awake();
            _unitStats = Resources.Load("ScriptableObject/UnitStats/CommanderStats") as UnitStatsSO;
            _dashDistant = 2.5f;
            _dashCooltime = 5.0f;
            _isMoving = false;
            _interactingBuilding = null;
            _animator = GetComponentInChildren<Animator>();
        }

        protected override void Start()
        {
            base.Start();
            //Initialize();
            InputManager.Instance.MousePosition.performed += ctx => { OnMousePosition(ctx); };
            InputManager.Instance.Click.performed += ctx => { OnClick(ctx); };
            InputManager.Instance.Move.performed += ctx => { OnMove(ctx); };
            InputManager.Instance.Move.canceled += ctx => { OnMoveCanceled(ctx); };
            InputManager.Instance.Action.performed += ctx => { OnActionPressed(ctx); };
            InputManager.Instance.PhysicalAttack.performed += ctx => { OnPhysicalAttackPressed(ctx); };
            InputManager.Instance.Teleport.performed += ctx => { OnTeleport(ctx); };
            _idleState = new CommanderIdleState(this);
            _moveState = new CommanderMoveState(this);
            _attackState = new CommanderAttackState(this);
            StateMachine.Initialize(_idleState);
            _dashTimer = gameObject.AddComponent<ActionTime>();
        }

        protected override void Update()
        {
            base.Update();
            _CheckUnableInteractBuilding();
            _CheckClick();
        }
    }
}
