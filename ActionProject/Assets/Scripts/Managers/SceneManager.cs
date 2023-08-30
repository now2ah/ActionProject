using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Action.Util;

namespace Action.Manager
{
    public class SceneManager : Singleton<SceneManager>
    {
        //�� ����Ʈ ����? (���� ����)

        public override void Initialize()
        {
            base.Initialize();
            base.SetName("SceneManager");
        }

        public void LoadScene(int sceneNumber, LoadSceneMode mode = LoadSceneMode.Single)
        {
            LoadScene(sceneNumber, mode);
        }

        public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            LoadScene(sceneName, mode);
        }
    }
}
