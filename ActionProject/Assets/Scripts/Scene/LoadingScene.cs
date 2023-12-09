using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Action.Manager;

namespace Action.Scene
{
    public class LoadingScene : MonoBehaviour
    {
        GameObject _loadingPanel;
        Image _fillImage;

        IEnumerator LoadGameSceneAsync(int sceneNumber)
        {
            AsyncOperation op = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneNumber);

            while (!op.isDone)
            {
                float progressValue = Mathf.Clamp01(op.progress / 0.9f);

                _fillImage.fillAmount = progressValue;

                yield return null;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            _loadingPanel = UIManager.Instance.CreateUI("LoadingPanel");
            _fillImage = _loadingPanel.transform.GetChild(1).GetChild(0).GetComponent<Image>();
            StartCoroutine(LoadGameSceneAsync(SceneManager.Instance.SceneNumToLoad));
        }

        private void OnDestroy()
        {
            Destroy(_loadingPanel);
        }
    }

}