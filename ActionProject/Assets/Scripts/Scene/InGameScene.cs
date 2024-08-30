using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;

namespace Action.Scene
{
    public class InGameScene : MonoBehaviour
    {
        

        // Start is called before the first frame update
        void Start()
        {
            if (!GameManager.Instance.IsPlaying)
            {
                GameManager.Instance.GameStart();
                CameraManager.Instance.CreateFixedVirtualCamera();
                UIManager.Instance.CreateTownStagePanel();
            }
        }
    }
}
