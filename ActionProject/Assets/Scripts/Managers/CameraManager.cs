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

        public override void Initialize()
        {
            base.Initialize();
            base.SetName("CameraManager");
            _CreateMainCamera();
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
   
