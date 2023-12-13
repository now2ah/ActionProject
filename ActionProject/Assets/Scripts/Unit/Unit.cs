using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;
using Action.UI;

namespace Action.Units
{
    public class Unit : MonoBehaviour
    {
        string unitName = "none";
        public string UnitName { get { return unitName; } set { unitName = value; } }
        int hp = 0;
        public int HP { get { return hp; } set { hp = value; } }
        int fullHp = 0;
        public int FullHp { get { return fullHp; } set { fullHp = value; } }

        GameObject _unitNameTagPrefab;
        GameObject _unitNameTagObj;
        UnitNameTag _unitNameTag;

        StateMachine _stateMachine;
        public StateMachine StateMachine => _stateMachine;

        public void GetDamaged(int damage)
        {
            hp -= damage;
            if (hp < 0)
                _Death();
        }

        void _Death()
        {

        }

        public virtual void Start()
        {
            _unitNameTagPrefab = Resources.Load("Prefabs/UI/NamePanel") as GameObject;
            _unitNameTagObj = Instantiate(_unitNameTagPrefab);
            _unitNameTagObj.transform.SetParent(this.transform);
            _unitNameTag = _unitNameTagObj.GetComponent<UnitNameTag>();

            _stateMachine = new StateMachine();
        }

        public virtual void Update()
        {
            if(null != _stateMachine)
                _stateMachine.Update();
        }
    }
}