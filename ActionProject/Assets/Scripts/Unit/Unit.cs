using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;
using Action.UI;
using Action.Manager;

namespace Action.Units
{
    public partial class Unit : MonoBehaviour
    {
        GameObject _unitPanelObject;
        UnitPanel _unitPanel;
        Collider _unitCollider;
        MeshRenderer _unitMeshRenderer;
        SkinnedMeshRenderer _unitSkinnedMeshRenderer;
        Material _unitMaterial;

        UnitData _unitData;

        [SerializeReference]
        float _maxHp;
        float _growthHp;
        string _unitName;

        bool _canDamaged;
        float _infoActiveDistant;

        //UnitPanel 표시하지 않는 유닛은 false
        bool _isOnUnitPanel;

        protected bool _isActive;

        public GameObject UnitPanelObject { get { return _unitPanelObject; } set { _unitPanelObject = value; } }
        public UnitPanel UnitPanel { get { return _unitPanel; } set { _unitPanel = value; } }
        public string UnitName { get { return _unitName; } set { _unitName = value; } }
        public Collider UnitCollider { get { return _unitCollider; } set { _unitCollider = value; } }
        public MeshRenderer UnitMeshRenderer { get { return _unitMeshRenderer; } set { _unitMeshRenderer = value; } }
        public SkinnedMeshRenderer UnitSkinnedMeshRenderer { get { return _unitSkinnedMeshRenderer; } set { _unitSkinnedMeshRenderer = value; } }
        public Material UnitMaterial { get { return _unitMaterial; } set { _unitMaterial = value; } }
        public UnitData UnitData { get { return _unitData; } set { _unitData = value; } }
        public float InfoActiveDistant { get { return _infoActiveDistant; } set { _infoActiveDistant = value; } }
        public bool IsOnUnitPanel { get { return _isOnUnitPanel; } set { _isOnUnitPanel = value; } }

        StateMachine _stateMachine;
        public StateMachine StateMachine => _stateMachine;

        public virtual void Initialize()
        {
            if (!_isActive)
                _isActive = true;

            _infoActiveDistant = GameManager.Instance.Constants.INGAMEUI_VISIBLE_DISTANT;
            if (null == _unitPanelObject)
                _unitPanelObject = UIManager.Instance.CreateUI("UnitPanel", UIManager.Instance.InGameCanvas);
            _unitPanel = _unitPanelObject.GetComponent<UnitPanel>();
            _unitPanel.Initialize(gameObject);
            _unitPanel.Hide();
            _unitCollider = GetComponentInChildren<Collider>();
            _unitMeshRenderer = GetComponentInChildren<MeshRenderer>();
            _unitSkinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
            if (null != _unitMeshRenderer)
                _unitMaterial = new Material(_unitMeshRenderer.material);
            else if (null != _unitSkinnedMeshRenderer)
                _unitMaterial = new Material(_unitSkinnedMeshRenderer.material);

            if (null == _stateMachine)
                _stateMachine = new StateMachine();
        }


        public virtual void ApplyDamage(DamageMessage msg)
        {
            if (_CheckDead())
                return;

            _unitData.hp -= msg.amount;
            _HitMaterialEffect();
            UnitPanel.ApplyHPValue(_unitData.hp, _unitData.maxHp);

            if (_CheckDead())
                _Dead(msg.damager);
        }

        public void SetNameUI(string name)
        {
            if (null != _unitPanel)
                _unitPanel.SetNameText(name);
        }

        bool _CheckDead()
        {
            if (0 >= _unitData.hp)
                return true;
            else
                return false;
        }

        protected virtual void _Dead(Unit damager)
        {

        }

        void _HitMaterialEffect()
        {
            StopCoroutine("ChangeMaterialCoroutine");
            StartCoroutine(ChangeMaterialCoroutine());
        }

        IEnumerator ChangeMaterialCoroutine()
        {
            if (null != _unitMeshRenderer)
                _unitMeshRenderer.material = GameManager.Instance.HitMaterial;
            else if (null != _unitSkinnedMeshRenderer)
                _unitSkinnedMeshRenderer.material = GameManager.Instance.HitMaterial;

            yield return new WaitForSeconds(0.1f);

            if (null != _unitMeshRenderer)
                _unitMeshRenderer.material = _unitMaterial;
            else if (null != _unitSkinnedMeshRenderer)
                _unitSkinnedMeshRenderer.material = _unitMaterial;
        }

        protected bool _IsNearPlayerUnit()
        {
            float dist = Vector3.Distance(GameManager.Instance.CommanderObj.transform.position, transform.position);

            if (dist < _infoActiveDistant)
                return true;
            else
                return false;
        }

        void _VisualizeUnitPanel()
        {
            if (!_isOnUnitPanel || this.gameObject == GameManager.Instance.CommanderObj)
                return;

            if (_IsNearPlayerUnit())
                _unitPanel.Show();
            else
                _unitPanel.Hide();
        }

        void _SetDefaultValue()
        {
            _unitData.name = "default_name";
            _unitData.hp = 10;
            _unitData.maxHp = 10;
        }

        protected virtual void Awake()
        {
            _unitData = new UnitData();
            _SetDefaultValue();
            _isOnUnitPanel = true;
            _isActive = false;
        }

        protected virtual void Start()
        {
            Initialize();
        }

        protected virtual void Update()
        {
            if (!Manager.GameManager.Instance.IsLive)
                return;

            _VisualizeUnitPanel();
            if (null != _stateMachine)
                _stateMachine.Update();
        }
    }
}