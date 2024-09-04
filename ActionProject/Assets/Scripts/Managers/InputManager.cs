using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Action.Util;

namespace Action.Manager
{
    public class InputManager : Singleton<InputManager>
    {
        public InputAction MousePosition;
        public InputAction Click;
        public InputAction Move;
        public InputAction Action;
        public InputAction PhysicalAttack;
        public InputAction Teleport;
        public InputAction PauseMenu;

        public override void Initialize()
        {
            base.Initialize();
            MousePosition.Enable();
            Click.Enable();
            Move.Enable();
            Action.Enable();
            PhysicalAttack.Enable();
            Teleport.Enable();
            PauseMenu.Enable();
        }
    }
}
