using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Action.CameraSystem
{
    public class FixedVirtualCamera : MonoBehaviour
    {
        CinemachineVirtualCamera _virtualCamera;

        public void SetTarget(Transform target)
        {
            _virtualCamera.Follow = target;
            _virtualCamera.LookAt = target;

            //Setting vCam's Body/Aim
            CinemachineTransposer camBody = _virtualCamera.AddCinemachineComponent<CinemachineTransposer>(); //받아오지 못함 null 리턴
            camBody.m_BindingMode = CinemachineTransposer.BindingMode.LockToTarget;
            camBody.m_FollowOffset.Set(0.0f, Constant.GAMECAMERA_VERTICAL_DIST, Constant.GAMECAMERA_HORIZONTAL_DIST);
            CinemachineComposer camAim = _virtualCamera.AddCinemachineComponent<CinemachineComposer>();
        }

        // Start is called before the first frame update
        void Awake()
        {
            _virtualCamera = gameObject.AddComponent<CinemachineVirtualCamera>();
        }

    }
}

