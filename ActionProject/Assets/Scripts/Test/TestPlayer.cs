using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayer : MonoBehaviour
{
    Vector2 vec;

    void OnTestAction(InputAction.CallbackContext context)
    {
        vec = context.ReadValue<Vector2>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Action.Manager.InputManager.Instance.actionMove.performed += ctx => { OnTestAction(ctx); };
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(vec);
    }
}
