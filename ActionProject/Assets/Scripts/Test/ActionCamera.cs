using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.CameraSystem
{
    public class ActionCamera : MonoBehaviour
    {
        public SO.InputManagerSO inputManager;

        [SerializeField]
        float Speed = 2.0f;

        Vector2 _moveInput;
        bool _isMoving = false;
        

        void _Move(Vector2 direction)
        {
            transform.position += new Vector3(direction.x, 0f, direction.y) * Time.deltaTime * Speed;
        }

        private void InputManager_OnMoveActionDown(object sender, Vector2 e)
        {
            _isMoving = true;
            _moveInput = e;
        }

        private void InputManager_OnMoveAction(object sender, Vector2 e)
        {
            _moveInput = e;
        }

        private void InputManager_OnMoveActionUp(object sender, Vector2 e)
        {
            _moveInput = e;
            _isMoving = false;
        }

        // Start is called before the first frame update
        void Start()
        {
            inputManager.OnMoveActionDown += InputManager_OnMoveActionDown;
            inputManager.OnMoveAction += InputManager_OnMoveAction;
            inputManager.OnMoveActionUp += InputManager_OnMoveActionUp;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            _Move(_moveInput);
        }
    }
}

