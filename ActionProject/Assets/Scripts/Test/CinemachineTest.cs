using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Action.CameraSystem;

public class CinemachineTest : MonoBehaviour
{
    public CinemachineVirtualCamera vCam1;
    public CinemachineVirtualCamera vCam2;

    CinemachineBrain brain;

    // Start is called before the first frame update
    void Start()
    {
        brain = Camera.main.GetComponent<CinemachineBrain>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
