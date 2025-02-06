using Action.Manager;
using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Action.Scene
{
    public class InitiateGameScene : MonoBehaviour
    {
        void _InitializeSingletons()
        {
            AssetManager.Instance.Initialize();
        }

        void Awake()
        {
            _InitializeSingletons();
        }
    }
}
