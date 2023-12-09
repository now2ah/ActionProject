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
        GameObject _fixedVCamObj;
        FixedVirtualCamera _fixedVCam;

        public override void Initialize()
        {
            base.Initialize();
            //base.SetName("CameraManager");
            _CreateMainCamera();
        }

        public void CreateVirtualCamera()
        {
            _fixedVCamObj = new GameObject("VirtualCamera");
            _fixedVCam = _fixedVCamObj.AddComponent<Action.CameraSystem.FixedVirtualCamera>();

            if (null == _fixedVCam.GetTarget())
                _fixedVCam.SetTarget(GameManager.Instance.PlayerUnit.gameObject.transform);
        }

        void _CreateMainCamera()
        {
            _mainCameraObj = new GameObject("MainCameraObj");
            _mainCameraObj.transform.SetParent(this.transform);
            MainCamera mainCam = _mainCameraObj.AddComponent<MainCamera>();
            mainCam.InitializeMainCamera();
        }
    }
}
   
