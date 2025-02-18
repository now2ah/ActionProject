using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Action.SO
{
    [CreateAssetMenu(fileName = "InputManager", menuName = "ScriptableObject/InputManagerSO")]
    public class InputManagerSO : ScriptableObject
    {
        InputAction moveAction;
    }
}

