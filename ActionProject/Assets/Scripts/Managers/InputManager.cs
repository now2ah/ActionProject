using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Action.Util;

namespace Action.Manager
{
    public class InputManager : Singleton<InputManager>
    {
        public InputAction testAction;

        private void OnEnable()
        {
            testAction.Enable();
        }

        public override void Initialize()
        {
            base.Initialize();
            base.SetName("InputManager");
            
        }

    }
}
