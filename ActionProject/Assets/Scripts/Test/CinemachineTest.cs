using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Action.CameraSystem;
using Action.SO;

public class CinemachineTest : MonoBehaviour
{
    public InputManagerSO inputManager;
    public GameObject _vCam1Obj;

    GameObject _target;
    CinemachineBrain _brain;
    CinemachineVirtualCamera _vCam1;

    void _OnMoveAction(object o, Vector2 value)
    {
        _target.transform.position += new Vector3(value.x, 0, value.y) * 50f * Time.deltaTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        _target = new GameObject("vCamTarget");
        _target.transform.position = Vector3.zero;
        _brain = Camera.main.GetComponent<CinemachineBrain>();
        //_vCam1Obj = new GameObject("vCam1");
        _vCam1 = _vCam1Obj.GetComponent<CinemachineVirtualCamera>();
        _vCam1.Follow = _target.transform;
        _vCam1.LookAt = _target.transform;
        inputManager.OnMoveAction += _OnMoveAction;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
