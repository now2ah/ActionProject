using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;

public class CharacterTestScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CameraManager.Instance.Initialize();
        GameManager.Instance.Initialize();
        GameManager.Instance.CreateCharacter();
        InputManager.Instance.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
