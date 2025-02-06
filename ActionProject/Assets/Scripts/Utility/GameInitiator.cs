using Action.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;


namespace Action.Util
{
    public class GameInitiator : MonoBehaviour
    {
        void _InitializeSingletons()
        {
            UIManager.Instance.Initialize();
            SceneManager.Instance.Initialize();
            CameraManager.Instance.Initialize();
            InputManager.Instance.Initialize();
            GameManager.Instance.Initialize();
            SaveSystem.Instance.Initialize();
            AudioManager.Instance.Initialize();
        }

        private void Awake()
        {
            _InitializeSingletons();
        }

        private void Start()
        {
            
        }
    }
}
