using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameCamera : MonoBehaviour
{
    public GameObject target;
    CinemachineVirtualCamera _virtualCamera;

    // Start is called before the first frame update
    void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _SettingGameCamera();
    }

    void _SettingGameCamera()
    {
        if (null == _virtualCamera)
            return;

        target = GameObject.FindGameObjectWithTag("Player");
        _virtualCamera.Follow = target.transform;
        _virtualCamera.LookAt = target.transform;
        CinemachineTransposer _camBody = _virtualCamera.AddCinemachineComponent<CinemachineTransposer>(); //받아오지 못함 null 리턴
        _camBody.m_BindingMode = CinemachineTransposer.BindingMode.LockToTarget;
    }
}
