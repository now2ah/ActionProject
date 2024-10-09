using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;
using Action.Util;
using Action.SO;
using DG.Tweening;

namespace Action.Units
{
    public class Tower : Building
    {
        UnitStatsSO _unitStats;

        GameObject _target;
        ActionTime _attackTime;
        Vector3 _shootPosition;

        public override void Initialize()
        {
            base.Initialize();
            _SetUnitData();
            UnitPanel.ApplyHPValue(UnitData.hp, UnitData.maxHp);
            if ("Tower_Base_North" == gameObject.name)
                GameManager.Instance.GameData.towerBaseN = UnitData as BuildingData;
            else if ("Tower_Base_South" == gameObject.name)
                GameManager.Instance.GameData.towerBaseS = UnitData as BuildingData;
            GameManager.Instance.OnRefresh.AddListener(_FindTarget);
            if (UnitData is BuildingData)
                RequireTextUI.Text.text = ((BuildingData)UnitData).requireGold.ToString();
            SetNameUI(UnitData.name);
        }

        void _SetUnitData()
        {
            if ("Tower_Base_North" == gameObject.name)
            {
                if (null != GameManager.Instance.GameData.towerBaseN)
                {
                    if (UnitData is BuildingData)
                    {
                        UnitData.name = GameManager.Instance.GameData.towerBaseN.name;
                        UnitData.hp = GameManager.Instance.GameData.towerBaseN.hp;
                        UnitData.maxHp = GameManager.Instance.GameData.towerBaseN.maxHp;
                        ((BuildingData)UnitData).isBuilt = GameManager.Instance.GameData.towerBaseN.isBuilt;
                        ((BuildingData)UnitData).requireGold = GameManager.Instance.GameData.towerBaseN.requireGold;
                        ((BuildingData)UnitData).constructTime = GameManager.Instance.GameData.towerBaseN.constructTime;
                        ((BuildingData)UnitData).attackDamage = GameManager.Instance.GameData.towerBaseN.attackDamage;
                        ((BuildingData)UnitData).attackSpeed = GameManager.Instance.GameData.towerBaseN.attackSpeed;
                        ((BuildingData)UnitData).attackDistance = GameManager.Instance.GameData.towerBaseN.attackDistance;
                    }
                    if (GameManager.Instance.GameData.towerBaseN.isBuilt)
                        StateMachine.ChangeState(_doneState);
                }
                else
                {
                    UnitData.name = _unitStats.unitName;
                    UnitData.hp = _unitStats.maxHp;
                    UnitData.maxHp = _unitStats.maxHp;
                    ((BuildingData)UnitData).isBuilt = false;
                    ((BuildingData)UnitData).requireGold = _unitStats.requireGold;
                    ((BuildingData)UnitData).constructTime = _unitStats.constructTime;
                    ((BuildingData)UnitData).attackDamage = _unitStats.attackDamage;
                    ((BuildingData)UnitData).attackSpeed = _unitStats.attackSpeed;
                    ((BuildingData)UnitData).attackDistance = _unitStats.attackDistance;
                }
            }
            else if ("Tower_Base_South" == gameObject.name)
            {
                if (null != GameManager.Instance.GameData.towerBaseS)
                {
                    if (UnitData is BuildingData)
                    {
                        UnitData.name = GameManager.Instance.GameData.towerBaseS.name;
                        UnitData.hp = GameManager.Instance.GameData.towerBaseS.hp;
                        UnitData.maxHp = GameManager.Instance.GameData.towerBaseS.maxHp;
                        ((BuildingData)UnitData).isBuilt = GameManager.Instance.GameData.towerBaseS.isBuilt;
                        ((BuildingData)UnitData).requireGold = GameManager.Instance.GameData.towerBaseS.requireGold;
                        ((BuildingData)UnitData).constructTime = GameManager.Instance.GameData.towerBaseS.constructTime;
                        ((BuildingData)UnitData).attackDamage = GameManager.Instance.GameData.towerBaseS.attackDamage;
                        ((BuildingData)UnitData).attackSpeed = GameManager.Instance.GameData.towerBaseS.attackSpeed;
                        ((BuildingData)UnitData).attackDistance = GameManager.Instance.GameData.towerBaseS.attackDistance;
                    }
                    if (GameManager.Instance.GameData.towerBaseS.isBuilt)
                        StateMachine.ChangeState(_doneState);
                }
                else
                {
                    UnitData.name = _unitStats.unitName;
                    UnitData.hp = _unitStats.maxHp;
                    UnitData.maxHp = _unitStats.maxHp;
                    ((BuildingData)UnitData).isBuilt = false;
                    ((BuildingData)UnitData).requireGold = _unitStats.requireGold;
                    ((BuildingData)UnitData).constructTime = _unitStats.constructTime;
                    ((BuildingData)UnitData).attackDamage = _unitStats.attackDamage;
                    ((BuildingData)UnitData).attackSpeed = _unitStats.attackSpeed;
                    ((BuildingData)UnitData).attackDistance = _unitStats.attackDistance;
                }
            }
        }

        void _FindTarget()
        {
            float nearestDist = 10000f;
            for (int i=0; i<GameManager.Instance.EnemyUnits.Count; i++)
            {
                float dist = Vector3.Distance(transform.position, GameManager.Instance.EnemyUnits[i].transform.position);
                if (nearestDist > dist)
                {
                    _target = GameManager.Instance.EnemyUnits[i];
                    nearestDist = dist;
                }
            }
        }

        bool _IsInDistance()
        {
            if (null == _target)
                return false;

            if (((BuildingData)UnitData).attackDistance > Vector3.Distance(transform.position, _target.transform.position))
                return true;
            else
                return false;
        }

        void _ShootArrow()
        {
            Game.Projectile arrowProj = PoolManager.Instance.ArrowProjectilePool.GetNew();
            Game.Arrow arrow = arrowProj as Game.Arrow;
            arrow.Initialize(this, ((BuildingData)UnitData).attackDamage);
            arrow.Target = _target;
            arrow.transform.LookAt(_target.transform);
            arrow.transform.position = _shootPosition;
            arrow.transform.DOMoveX(_target.transform.position.x, 0.5f).SetEase(Ease.OutQuad);
            arrow.transform.DOMoveY(_target.transform.position.y, 0.5f).SetEase(Ease.InQuad);
            arrow.transform.DOMoveZ(_target.transform.position.z, 0.5f).SetEase(Ease.OutQuad);
        }

        void _Attack()
        {
            _ShootArrow();
            _attackTime.TickStart(((BuildingData)UnitData).attackSpeed);
        }

        protected override void Awake()
        {
            base.Awake();
            _unitStats = Resources.Load("ScriptableObject/UnitStats/TowerStats") as UnitStatsSO;
            _attackTime = gameObject.AddComponent<ActionTime>();
            _shootPosition = transform.position + new Vector3(0.0f, 5.0f, 0.0f);
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            if (StateMachine.CurState == _doneState &&
                _IsInDistance() && !_attackTime.IsStarted)
                _Attack();
        }
    }
}

