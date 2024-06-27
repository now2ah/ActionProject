using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Action.Util;

namespace Action.Manager
{
    public class InputManager : Singleton<InputManager>
    {
        public InputAction actionLook;
        public InputAction actionMove;
        public InputAction actionAction;
        public InputAction actionPhysicalAttack;

        public override void Initialize()
        {
            base.Initialize();
            actionLook.Enable();
            actionMove.Enable();
            actionAction.Enable();
            actionPhysicalAttack.Enable();
        }
    }
}
