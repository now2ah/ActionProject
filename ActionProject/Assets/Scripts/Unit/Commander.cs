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
        GameObject _indicator;

        int _animHashMoving;
        int _animHashAttacking;

        Ability[] _abilitySlots;

        public UnityAction OnDamaged;
        public UnityEvent<int, int> OnGainExp;
        public UnityEvent<List<Ability>> OnLevelUp;

        public CommanderIdleState IdleState => _idleState;
        public CommanderMoveState MoveState => _moveState;
        public CommanderAttackState AttackState => _attackState;
        public GameObject InteractingBuilding { get { return _interactingBuilding; } set { _interactingBuilding = value; } }
        public GameObject Indicator { get { return _indicator; } set { _indicator = value; } }
        public int AnimHashMoving => _animHashMoving;
        public int AnimHashAttacking => _animHashAttacking;
        public Ability[] AbilitySlots { get { return _abilitySlots; } }

        public override void Initialize()
        {
            base.Initialize();
            _SetUnitData();
            SetNameUI(UnitData.name);
            UnitPanel.Show();
            UnitPanel.ApplyHPValue(UnitData.hp, UnitData.maxHp);
            _indicator = Instantiate(GameManager.Instance.BuildingIndicatorPrefab);
            _indicator.transform.SetParent(transform);
            _indicator.SetActive(false);
            _animHashMoving = Animator.StringToHash("isMoving");
            _animHashAttacking = Animator.StringToHash("isAttacking");
            OnDamaged += UIManager.Instance.ShowDamagedEffect;
            OnGainExp.AddListener(UIManager.Instance.ExpBarUI.ApplyExpValue);
            OnLevelUp.AddListener(UIManager.Instance.AbilityUpgradeUI.Initialize);
            _abilitySlots = new Ability[GameManager.Instance.Constants.ABILITY_COUNT];
            _SetAbilities();
            if (null == GameManager.Instance.GameData.commanderData)
            {
                _abilitySlots[(int)Enums.eAbility.PHYSICAL].LevelUp(1);
                ActivateAbility(Enums.eAbility.PHYSICAL);
                _abilitySlots[(int)Enums.eAbility.DIRECTIONAL].LevelUp(1);
                ActivateAbility(Enums.eAbility.DIRECTIONAL);
            }
            else
                _LoadAbilities();
            
            SetEnableAutoAttacks(false);

            GameManager.Instance.GameData.commanderData = UnitData as CommanderData;
            
            DontDestroyOnLoad(this);
        }

        public void Interact()
        {
            if (null != _interactingBuilding)
            {
                if (_interactingBuilding.TryGetComponent<Building>(out Building comp))
                {
                    BuildingData buildingData = comp.UnitData as BuildingData;
                    if (comp.StateMachine.CurState == comp.IdleState &&
                        GameManager.Instance.GameData.resource.IsValidSpend(buildingData.requireGold, eResource.GOLD))
                    {
                        GameManager.Instance.GameData.resource.Spend(buildingData.requireGold, eResource.GOLD);
                        comp.Interact();
                    }
                }
            }
        }

        public override void Move()
        {
            Vector3 movePos = new Vector3(_moveInput.x, 0, _moveInput.y);

            if (!InputManager.Instance.Click.IsPressed())
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movePos), 0.15f);

            transform.Translate(movePos * Time.deltaTime * ((PlayerUnitData)UnitData).speed, Space.World);
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
                _abilitySlots[(int)ability].Activate(true);
        }

        public override void GainExp(int exp)
        {
            base.GainExp(exp);
            OnGainExp.Invoke(((PlayerUnitData)UnitData).exp, ((PlayerUnitData)UnitData).nextExp);
        }

        public override void ModifyLevel(int level)
        {
            base.ModifyLevel(level);
            List<Ability> abilities = _SetUpAbilityUpgrade();
            OnLevelUp.Invoke(abilities);
        }

        public void SetEnableAutoAttacks(bool isOn)
        {
            if (false == isOn)
            {
                foreach (var autoAttack in _abilitySlots)
                    autoAttack.Activate(isOn);
            }
            else if (true == isOn)
            {
                foreach (var autoAttack in _abilitySlots)
                {
                    if (0 < autoAttack.abilityData.level)
                        autoAttack.Activate(isOn);
                }
            }
        }

        public void OnMousePosition(InputAction.CallbackContext context)
        {
            _CheckMousePosition(context.ReadValue<Vector2>());
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            Vector3 dir = _lookPos - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir.normalized), 0.15f);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (!_isAttacking)
            {
                StateMachine.ChangeState(_moveState);
                _moveInput = context.ReadValue<Vector2>();
            }
        }

        public void OnMoveCanceled(InputAction.CallbackContext context)
        {
            if (!_isAttacking)
                StateMachine.ChangeState(_idleState);
            //inputVector = Vector3.zero;
        }

        public void OnActionPressed(InputAction.CallbackContext context)
        {
            if (context.performed)
                Interact();
        }

        public void OnPhysicalAttackPressed(InputAction.CallbackContext context)
        {
            if (context.performed)
                _PhysicalAttackCheck();
        }

        public void OnTeleport(InputAction.CallbackContext context)
        {
            if (!_dashTimer.IsStarted)
            {
                _Dash();
                _dashTimer.TickStart(_dashCooltime);
                UIManager.Instance.SkillIconUI.DashImage.gameObject.SetActive(false);
            }
        }

        protected override void _Dead(Unit damager)
        {
            GameManager.Instance.Stop();
            UIManager.Instance.CreateUI("GameOverPanel", UIManager.Instance.MainCanvas);
        }

        void _PhysicalAttackCheck()
        {
            if (_abilitySlots[(int)Enums.eAbility.PHYSICAL].abilityData.isActivated)
            {
                PhysicalAttack ability = _abilitySlots[(int)Enums.eAbility.PHYSICAL] as PhysicalAttack;
                if (!ability.Timer.IsStarted)
                {
                    PhysicalAttack();
                    ability.Timer.TickStart(ability.abilityData.attackSpeed);
                    UIManager.Instance.SkillIconUI.AttackImage.gameObject.SetActive(false);
                    AudioManager.Instance.PlaySFX(AudioManager.eSfx.SLASH);
                }
            }
        }

        void _CheckMousePosition(Vector2 lookInput)
        {
            Vector3 camPos = CameraManager.Instance.MainCamera.Camera.WorldToScreenPoint(this.transform.position);
            _lookInput = lookInput;
            _lookPos = CameraManager.Instance.MainCamera.Camera.ScreenToWorldPoint(new Vector3(_lookInput.x, _lookInput.y, camPos.z));
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
                {
                    _interactingBuilding = null;
                    _indicator.SetActive(false);
                }
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
            _CreateHitBox(((PlayerUnitData)UnitData).attackDamage);
            _isAttacking = false;
            _animator.SetBool(_animHashAttacking, _isAttacking);
        }

        void _SetAbilities()
        {
            PhysicalAttack physical = gameObject.AddComponent<PhysicalAttack>();
            physical.Initialize();
            _abilitySlots[0] = physical;
            DirectionalAttack directional = gameObject.AddComponent<DirectionalAttack>();
            directional.Initialize();
            _abilitySlots[1] = directional;
            GuidanceAttack guidance = gameObject.AddComponent<GuidanceAttack>();
            guidance.Initialize();
            _abilitySlots[2] = guidance;
            DamageUpAbility damageUp = gameObject.AddComponent<DamageUpAbility>();
            damageUp.Initialize();
            _abilitySlots[3] = damageUp;
            HPUpAbility hpUp = gameObject.AddComponent<HPUpAbility>();
            hpUp.Initialize();
            _abilitySlots[4] = hpUp;
            SpeedUpAbility speedUpAbility = gameObject.AddComponent<SpeedUpAbility>();
            speedUpAbility.Initialize();
            _abilitySlots[5] = speedUpAbility;
        }

        void _LoadAbilities()
        {
            foreach (var ability in GameManager.Instance.GameData.commanderData.abilityDatas)
            {
                switch (ability.type)
                {
                    case Enums.eAbility.PHYSICAL:
                        if (gameObject.TryGetComponent<PhysicalAttack>(out PhysicalAttack physical))
                        {
                            physical.Initialize();
                            physical.abilityData = ability;
                            _abilitySlots[0] = physical;
                        }
                        break;
                    case Enums.eAbility.DIRECTIONAL:
                        if (gameObject.TryGetComponent<DirectionalAttack>(out DirectionalAttack directional))
                        {
                            directional.Initialize();
                            directional.abilityData = ability;
                            _abilitySlots[1] = directional;
                        }
                        break;
                    case Enums.eAbility.GUIDANCE:
                        if (gameObject.TryGetComponent<GuidanceAttack>(out GuidanceAttack guidance))
                        {
                            guidance.Initialize();
                            guidance.abilityData = ability;
                            _abilitySlots[2] = guidance;
                        }
                        break;
                    case Enums.eAbility.DAMAGEUP:
                        if (gameObject.TryGetComponent<DamageUpAbility>(out DamageUpAbility dmg))
                        {
                            dmg.Initialize();
                            dmg.abilityData = ability;
                            _abilitySlots[3] = dmg;
                        }
                        break;
                    case Enums.eAbility.HPUP:
                        if (gameObject.TryGetComponent<HPUpAbility>(out HPUpAbility hp))
                        {
                            hp.Initialize();
                            hp.abilityData = ability;
                            _abilitySlots[4] = hp;
                        }
                        break;
                    case Enums.eAbility.SPEEDUP:
                        if (gameObject.TryGetComponent<SpeedUpAbility>(out SpeedUpAbility spd))
                        {
                            spd.Initialize();
                            spd.abilityData = ability;
                            _abilitySlots[5] = spd;
                        }
                        break;
                }
            }
        }

        List<Ability> _SetUpAbilityUpgrade()
        {
            List<Ability> abilityList = new List<Ability>();
            for (int i = 0; i < 3; i++)
            {
                int num = Random.Range(0, _abilitySlots.Length - 1);
                if (-1 < abilityList.IndexOf(_abilitySlots[num]) ||
                    _abilitySlots[num].LevelLimit == _abilitySlots[num].abilityData.level)
                    i--;
                else
                    abilityList.Add(_abilitySlots[num]);
            }
            return abilityList;
        }

        void _Dash()
        {
            Vector3 newPos = transform.position + transform.forward * _dashDistant;
            transform.position = newPos;
        }

        void _SetUnitData()
        {
            if (null != GameManager.Instance.GameData.commanderData)
            {
                if (UnitData is CommanderData)
                {
                    ((CommanderData)UnitData).name = GameManager.Instance.GameData.commanderData.name;
                    ((CommanderData)UnitData).hp = GameManager.Instance.GameData.commanderData.maxHp;
                    ((CommanderData)UnitData).maxHp = GameManager.Instance.GameData.commanderData.maxHp;
                    ((CommanderData)UnitData).growthHp = GameManager.Instance.GameData.commanderData.maxHp;
                    ((CommanderData)UnitData).speed = GameManager.Instance.GameData.commanderData.speed;
                    ((CommanderData)UnitData).attackDamage = GameManager.Instance.GameData.commanderData.attackDamage;
                    ((CommanderData)UnitData).growthAttackDamage = GameManager.Instance.GameData.commanderData.growthAttackDamage;
                    ((CommanderData)UnitData).level = GameManager.Instance.GameData.commanderData.level;
                    ((CommanderData)UnitData).exp = GameManager.Instance.GameData.commanderData.exp;
                    ((CommanderData)UnitData).nextExp = GameManager.Instance.GameData.commanderData.nextExp;
                    ((CommanderData)UnitData).abilityDatas = GameManager.Instance.GameData.commanderData.abilityDatas;
                }
            }
            else
            {
                ((CommanderData)UnitData).name = _unitStats.unitName;
                ((CommanderData)UnitData).hp = _unitStats.maxHp;
                ((CommanderData)UnitData).maxHp = _unitStats.maxHp;
                ((CommanderData)UnitData).growthHp = _unitStats.growthMaxHp;
                ((CommanderData)UnitData).speed = _unitStats.speed;
                ((CommanderData)UnitData).attackDamage = _unitStats.attackDamage;
                ((CommanderData)UnitData).growthAttackDamage = _unitStats.growthAttackDamage;
                ((CommanderData)UnitData).level = 1;
                ((CommanderData)UnitData).exp = 0;
                ((CommanderData)UnitData).nextExp = _unitStats.nextExp;
            }
        }

        void _AddListener()
        {
            InputManager.Instance.MousePosition.performed += ctx => { OnMousePosition(ctx); };
            InputManager.Instance.Click.performed += ctx => { OnClick(ctx); };
            InputManager.Instance.Move.performed += ctx => { OnMove(ctx); };
            InputManager.Instance.Move.canceled += ctx => { OnMoveCanceled(ctx); };
            InputManager.Instance.Action.performed += ctx => { OnActionPressed(ctx); };
            InputManager.Instance.PhysicalAttack.performed += ctx => { OnPhysicalAttackPressed(ctx); };
            InputManager.Instance.Teleport.performed += ctx => { OnTeleport(ctx); };
            InputManager.Instance.SetActiveActions(true);
        }

        void _RemoveListener()
        {
            InputManager.Instance.MousePosition.performed -= OnMousePosition;
            InputManager.Instance.Click.performed -= OnClick;
            InputManager.Instance.Move.performed -= OnMove;
            InputManager.Instance.Move.canceled -= OnMoveCanceled;
            InputManager.Instance.Action.performed -= OnActionPressed;
            InputManager.Instance.PhysicalAttack.performed -= OnPhysicalAttackPressed;
            InputManager.Instance.Teleport.performed -= OnTeleport;
            InputManager.Instance.SetActiveActions(false);
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
            UnitData = new CommanderData();
            ((CommanderData)UnitData).abilityDatas = new List<AbilityData>();
        }

        protected override void Start()
        {
            base.Start();
            //Initialize();

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
            if (_dashTimer.IsFinished)
                UIManager.Instance.SkillIconUI.DashImage.gameObject.SetActive(true);
        }

        protected void OnEnable()
        {
            //_AddListener();
        }

        protected void OnDisable()
        {
            //_RemoveListener();
        }
    }
}
