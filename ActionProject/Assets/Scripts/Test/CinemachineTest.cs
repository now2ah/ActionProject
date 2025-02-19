using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Action.CameraSystem;
using Action.SO;

public class CinemachineTest : MonoBehaviour
{
    public CinemachineVirtualCamera vCam1;
    public CinemachineVirtualCamera vCam2;
    public InputManagerSO inputManager;

    CinemachineBrain brain;

    void _OnMoveAction(object o, Vector2 value)
    {
        Debug.Log(value);
    }

    // Start is called before the first frame update
    void Start()
    {
        brain = Camera.main.GetComponent<CinemachineBrain>();
        inputManager.OnMoveAction += _OnMoveAction;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
