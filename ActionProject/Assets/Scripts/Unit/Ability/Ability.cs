using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Units;


namespace Action.Game
{
    public class Ability : MonoBehaviour
    {
        bool _isActivated;
        int _level;
        [SerializeField]
        int _levelLimit;

        string _abilityName;
        string _description;

        Commander _commander;

        public bool IsActivated { get { return _isActivated; } set { _isActivated = value; } }
        public int Level { get { return _level; } set { _level = value; } }
        public int LevelLimit { get { return _levelLimit; } set { _levelLimit = value; } }
        public string AbilityName { get { return _abilityName; } set { _abilityName = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        public Commander Commander { get { return _commander; } set { _commander = value; } }

        public virtual void Initialize()
        {

        }

        public virtual void LevelUp(int level)
        {
        }

        public virtual void Activate(bool isOn)
        {
            _isActivated = isOn;
        }

        protected virtual void Awake()
        {
            _isActivated = false;
            _level = 0;
            _levelLimit = 5;
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            Initialize();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (!Manager.GameManager.Instance.IsLive)
                return;
        }
    }

}
