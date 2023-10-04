using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Util;

namespace Action.Manager
{
    

    public class InputManager : Singleton<InputManager>
    {
        //¾×¼Ç
        Dictionary<string, KeyCode> _actionKeys;

        public override void Initialize()
        {
            base.Initialize();
            base.SetName("InputManager");
            _actionKeys = new Dictionary<string, KeyCode>();
            _InitializeBasicActionKeys();
        }

#if ENV_PC
        public KeyCode GetActionKey(string action)
        {
            if(null != action && _actionKeys.Count > 0)
            {
                for (int i = 0; i < _actionKeys.Count; i++)
                {
                    if (_actionKeys.ContainsKey(action))
                        return _actionKeys[action];
                }
            }
            return KeyCode.None;
        }

        public bool IsKeyUp(KeyCode key)
        {
            if (Input.GetKey(key))
                return true;

            return false;
        }

        void _InitializeBasicActionKeys()
        {
            for(int i = 0; i < Constant.ACTIONS.Length; i++)
            {
                _actionKeys.Add(Constant.ACTIONS[i], KeyCode.None);
            }

            _actionKeys["up"] = KeyCode.W;
            _actionKeys["left"] = KeyCode.A;
            _actionKeys["right"] = KeyCode.S;
            _actionKeys["down"] = KeyCode.D;
        }
#endif

        private void Update()
        {

        }
    }
}
