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
            //MousePosition.Enable();
            //Click.Enable();
            //Move.Enable();
            //Action.Enable();
            //PhysicalAttack.Enable();
            //Teleport.Enable();
            //PauseMenu.Enable();
        }

        public void SetActiveActions(bool isOn)
        {
            if (isOn)
            {
                MousePosition.Enable();
                Click.Enable();
                Move.Enable();
                Action.Enable();
                PhysicalAttack.Enable();
                Teleport.Enable();
                PauseMenu.Enable();
            }
            else
            {
                MousePosition.Disable();
                Click.Disable();
                Move.Disable();
                Action.Disable();
                PhysicalAttack.Disable();
                Teleport.Disable();
                PauseMenu.Disable();
            }
        }

        public void DisposeAllActions()
        {
            MousePosition.Dispose();
            Click.Dispose();
            Move.Dispose();
            Action.Dispose();
            PhysicalAttack.Dispose();
            Teleport.Dispose();
            PauseMenu.Dispose();
        }

        public void AddListeners(Units.Commander commander)
        {
            MousePosition.performed += commander.OnMousePosition;
            Click.performed += commander.OnClick;
            Move.performed += commander.OnMove;
            Move.canceled += commander.OnMoveCanceled;
            Action.performed += commander.OnActionPressed;
            PhysicalAttack.performed += commander.OnPhysicalAttackPressed;
            Teleport.performed += commander.OnTeleport;
            SetActiveActions(true);
        }

        public void RemoveListeners(Units.Commander commander)
        {
            MousePosition.performed -= commander.OnMousePosition;
            Click.performed -= commander.OnClick;
            Move.performed -= commander.OnMove;
            Move.canceled -= commander.OnMoveCanceled;
            Action.performed -= commander.OnActionPressed;
            PhysicalAttack.performed -= commander.OnPhysicalAttackPressed;
            Teleport.performed -= commander.OnTeleport;
            SetActiveActions(false);
        }
    }
}
