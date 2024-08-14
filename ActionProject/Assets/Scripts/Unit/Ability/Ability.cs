using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Action.Game
{
    public class Ability : MonoBehaviour
    {
        bool _isActivated;
        int _level;
        [SerializeField]
        int _levelLimit;

        public bool IsActivated { get { return _isActivated; } set { _isActivated = value; } }
        public int Level { get { return _level; } set { _level = value; } }
        public int LevelLimit { get { return _levelLimit; } set { _levelLimit = value; } }

        public virtual void LevelUp(int level)
        {
        }

        protected virtual void Awake()
        {
            _isActivated = false;
            _level = 1;
            _levelLimit = 5;
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {

        }

        // Update is called once per frame
        protected virtual void Update()
        {

        }
    }

}
