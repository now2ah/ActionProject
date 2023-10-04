using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;

public class TestScene : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.Instance.Initialize();
        UIManager.Instance.Initialize();
        CameraManager.Instance.Initialize();
        InputManager.Instance.Initialize();
        _CreateVirtualCamera();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void _CreateVirtualCamera()
    {
        GameObject virtualCameraObj = new GameObject("VirtualCamera");
        Action.CameraSystem.FixedVirtualCamera fixedVcam = virtualCameraObj.AddComponent<Action.CameraSystem.FixedVirtualCamera>();
        
        if(null != player)
        {
            fixedVcam.SetTarget(player.transform);
        }
            

        
    }
}
