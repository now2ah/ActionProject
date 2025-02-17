using Action.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;


namespace Action.Util
{
    public class GameInitiator : MonoBehaviour
    {
        void _InitializeSingletons(UnityAction callback)
        {
            UIManager.Instance.Initialize();
            SceneManager.Instance.Initialize();
            CameraManager.Instance.Initialize();
            //InputManager.Instance.Initialize();
            //GameManager.Instance.Initialize();
            //SaveSystem.Instance.Initialize();
            //AudioManager.Instance.Initialize();
            callback.Invoke();
        }

        private void Awake()
        {
            _InitializeSingletons(() =>
            {
                SceneManager.Instance.LoadNextScene();
            });
        }
    }
}
