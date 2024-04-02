using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Util;
using Action.CameraSystem;

namespace Action.Manager
{
    public class CameraManager : Singleton<CameraManager>
    {
        GameObject _mainCameraObj;
        MainCamera _mainCamera;
        GameObject _fixedVCamObj;
        FixedVirtualCamera _fixedVCam;

        float _vCamOffsetY;
        float _vCamOffsetZ;
        float _vCamFov;
        public float VCamOffsetY { get { return _vCamOffsetY; } set { _vCamOffsetY = value; } }
        public float VCamOffsetZ { get { return _vCamOffsetZ; } set { _vCamOffsetZ = value; } }
        public float VCamFOV { get { return _vCamFov; } set { _vCamFov = value; } }


        public MainCamera MainCamera => _mainCamera;

        public override void Initialize()
        {
            base.Initialize();
            _CreateMainCamera();
            _vCamOffsetY = Constant.GAMECAMERA_VERTICAL_DIST;
            _vCamOffsetZ = Constant.GAMECAMERA_HORIZONTAL_DIST;
            _vCamFov = Constant.GAMECAMERA_FOV;
        }

        public void CreateFixedVirtualCamera()
        {
            _fixedVCamObj = new GameObject("VirtualCamera");
            _fixedVCam = _fixedVCamObj.AddComponent<FixedVirtualCamera>();
            _fixedVCam.Initialize();
            _fixedVCam.SetBodyOffset(new Vector3(0.0f, _vCamOffsetY, _vCamOffsetZ));
            _fixedVCam.SetFov(Constant.GAMECAMERA_FOV);

            if (null == _fixedVCam.GetTarget())
                _fixedVCam.SetTarget(GameManager.Instance.CommanderObj.transform);
        }

        public void SetVCamOffset(float offsetY, float offsetZ)
        {
            if (null != _fixedVCam)
                _fixedVCam.SetBodyOffset(new Vector3(0.0f, offsetY, offsetZ));
        }

        public void SetVCamFOV(float fov)
        {
            if (null != _fixedVCam)
                _fixedVCam.SetFov(fov);
        }

        void _CreateMainCamera()
        {
            _mainCameraObj = new GameObject("MainCameraObj");
            _mainCameraObj.transform.SetParent(this.transform);
            _mainCamera = _mainCameraObj.AddComponent<MainCamera>();
            _mainCamera.InitializeMainCamera();
        }
    }
}
   
