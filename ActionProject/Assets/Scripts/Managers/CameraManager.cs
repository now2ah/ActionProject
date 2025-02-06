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
        Cinemachine.CinemachineVirtualCameraBase _mainMenuVCam;
        GameObject _mainMenuVCamObj;
        GameObject _fixedVCamObj;
        FixedVirtualCamera _fixedVCam;
        AudioListener _audioListener;
        

        float _vCamOffsetY;
        float _vCamOffsetZ;
        float _vCamFov;
        public float VCamOffsetY { get { return _vCamOffsetY; } set { _vCamOffsetY = value; } }
        public float VCamOffsetZ { get { return _vCamOffsetZ; } set { _vCamOffsetZ = value; } }
        public float VCamFOV { get { return _vCamFov; } set { _vCamFov = value; } }


        public MainCamera MainCamera => _mainCamera;

        public override void Initialize()
        {
            _CreateMainCamera();
            _vCamOffsetY = GameManager.Instance.Constants.GAMECAMERA_VERTICAL_DIST;
            _vCamOffsetZ = GameManager.Instance.Constants.GAMECAMERA_HORIZONTAL_DIST;
            _vCamFov = GameManager.Instance.Constants.GAMECAMERA_FOV;
            
        }

        public void CreateMainMenuVirtualCamera()
        {
            if (null == _mainMenuVCamObj)
            {
                _mainMenuVCamObj = GameObject.Find("VirtualCamera");
                //_mainMenuVCam = _mainCameraObj.AddComponent<Cinemachine.CinemachineVirtualCameraBase>();
                _mainMenuVCamObj.transform.position = new Vector3(-110, 30, 0);
                _mainMenuVCamObj.transform.rotation = Quaternion.Euler(40, 90, 0);
            }
        }

        public void CreateFixedVirtualCamera()
        {
            if (null == _fixedVCamObj)
            {
                _fixedVCamObj = new GameObject("VirtualCamera");
                _fixedVCam = _fixedVCamObj.AddComponent<FixedVirtualCamera>();
                _fixedVCam.Initialize();
                _fixedVCam.SetBodyOffset(new Vector3(0.0f, _vCamOffsetY, _vCamOffsetZ));
                _fixedVCam.SetFov(GameManager.Instance.Constants.GAMECAMERA_FOV);
                _audioListener = _fixedVCamObj.AddComponent<AudioListener>();

                if (null == _fixedVCam.GetTarget())
                    _fixedVCam.SetTarget(GameManager.Instance.CommanderObj.transform);
            }
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

        public void SetVCamTarget(Transform target)
        {
            _fixedVCam.SetTarget(target);
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
   
