using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Action.Util;

namespace Action.Manager
{
    public class InputManager : Singleton<InputManager>
    {
        public InputAction actionMove;

        public override void Initialize()
        {
            base.Initialize();
            actionMove.Enable();
        }
    }
}
