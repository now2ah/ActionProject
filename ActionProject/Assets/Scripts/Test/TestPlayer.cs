using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Action.Manager.InputManager.Instance.IsKeyUp(Action.Manager.InputManager.Instance.GetActionKey("up")))
            gameObject.transform.Translate(transform.position + new Vector3(1.0f, 0.0f, 0.0f));
    }
}
