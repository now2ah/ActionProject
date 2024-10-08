using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Units;


namespace Action.Game
{
    public class Ability : MonoBehaviour
    {
        public AbilityData abilityData;

        int _levelLimit;
        bool _isAutoAttack;

        Commander _commander;

        public int LevelLimit { get { return _levelLimit; } set { _levelLimit = value; } }
        public bool IsAutoAttack { get { return _isAutoAttack; } set { _isAutoAttack = value; } }
        public Commander Commander { get { return _commander; } set { _commander = value; } }

        public virtual void Initialize()
        {

        }

        public virtual void LevelUp(int level)
        {
            if (abilityData.level < _levelLimit)
                abilityData.level++;
        }

        public virtual void Activate(bool isOn)
        {
            abilityData.isActivated = isOn;
        }

        protected virtual void Awake()
        {
            abilityData = new AbilityData();
            _levelLimit = 5;
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            //Initialize();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (!Manager.GameManager.Instance.IsLive)
                return;
        }
    }

}
