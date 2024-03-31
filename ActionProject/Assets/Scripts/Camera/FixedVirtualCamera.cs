using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Action.CameraSystem
{
    public class FixedVirtualCamera : MonoBehaviour
    {
        CinemachineVirtualCamera _virtualCamera;
        CinemachineTransposer _camBody;
        CinemachineComposer _camAim;
        Transform _target;

        public void Initialize()
        {
            //Setting vCam's Body/Aim
            _camBody = _virtualCamera.AddCinemachineComponent<CinemachineTransposer>(); //받아오지 못함 null 리턴
            _camBody.m_BindingMode = CinemachineTransposer.BindingMode.LockToTargetOnAssign;
            _camAim = _virtualCamera.AddCinemachineComponent<CinemachineComposer>();
        }

        public Transform GetTarget() { return _target; }

        public void SetTarget(Transform target)
        {
            _target = target;
            _virtualCamera.Follow = _target;
            _virtualCamera.LookAt = _target;
        }

        public void SetFov(float value)
        {
            _virtualCamera.m_Lens.FieldOfView = value;
        }

        public void SetBodyOffset(Vector3 offset)
        {
            _camBody.m_FollowOffset.Set(offset.x, offset.y, offset.z);
        }

        public void SetAimOffset()
        {

        }

        // Start is called before the first frame update
        void Awake()
        {
            _virtualCamera = gameObject.AddComponent<CinemachineVirtualCamera>();
        }

    }
}

