using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;

namespace Action.Scene
{
    public class InGameScene : MonoBehaviour
    {
        void _InitializeSingletons()
        {
            CameraManager.Instance.Initialize();
            InputManager.Instance.Initialize();
            GameManager.Instance.Initialize();
        }

        // Start is called before the first frame update
        void Start()
        {
            _InitializeSingletons();

            GameManager.Instance.GameStart();

            CameraManager.Instance.CreateVirtualCamera();

            UIManager.Instance.CreateTownStagePanel();
        }
    }
}
