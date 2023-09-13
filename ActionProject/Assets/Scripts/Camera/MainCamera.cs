using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Action.CameraSystem
{
    public class MainCamera : MonoBehaviour
    {
        Camera _camera;
        CinemachineBrain _cinemachineBrain;

        public void InitializeMainCamera()
        {
            _camera = gameObject.AddComponent<Camera>();
            _cinemachineBrain = gameObject.AddComponent<CinemachineBrain>();

            _SettingMainCamera();
            _SettingCinemachineBrain();
        }

        void _SettingMainCamera()
        {
            if (null == _camera) { Debug.LogError("Camera Component is null"); return; }

            _camera.tag = "MainCamera";
            //set fov, cliping plane near/far
        }

        void _SettingCinemachineBrain()
        {
            if (null == _cinemachineBrain) { Debug.LogError("CinemachineBrain Component is null"); return; }


        }
    }
}


