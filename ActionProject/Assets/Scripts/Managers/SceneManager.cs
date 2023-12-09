using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using Action.Util;

namespace Action.Manager
{
    public class SceneManager : Singleton<SceneManager>
    {
        public enum eFade
        {
            FadeIn,
            FadeOut,
        }

        GameObject _fadeUI;
        Image _fadeImage;
        float _fadeSpeed;

        public override void Initialize()
        {
            base.Initialize();
            //base.SetName("SceneManager");
            _LoadFadeImage();
            _fadeSpeed = 0.5f;
        }

        public void LoadGameScene(int sceneNumber, LoadSceneMode mode = LoadSceneMode.Single)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneNumber, mode);
        }

        public void LoadGameScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, mode);
        }

        public void Fade(eFade fade, UnityAction action = null)
        {
            StopCoroutine("FadeCoroutine");
            StartCoroutine(FadeCoroutine(fade, action));
        }

        void _LoadFadeImage()
        {
            _fadeUI = UIManager.Instance.CreateUI("FadeInNOutPanel");
            _fadeImage = _fadeUI.GetComponent<Image>();
            _fadeUI.SetActive(false);
        }

        IEnumerator FadeCoroutine(eFade fade, UnityAction action = null)
        {
            _fadeUI?.SetActive(true);
            _fadeUI.transform.SetSiblingIndex(UIManager.Instance.MainCanvas.transform.childCount - 1);
            float startValue = (fade == eFade.FadeIn) ? 1f : 0f;
            float endValue = (fade == eFade.FadeIn) ? 0f : 1f;
            float alpha = startValue;

            if (fade == eFade.FadeIn)
            {
                while(alpha >= endValue)
                {
                    alpha += Time.deltaTime * (1.0f / _fadeSpeed) * ((fade == eFade.FadeIn) ? -1 : 1);
                    _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, alpha);
                    yield return null;
                }
            }
            else if (fade == eFade.FadeOut)
            {
                while (alpha <= endValue)
                {
                    alpha += Time.deltaTime * (1.0f / _fadeSpeed) * ((fade == eFade.FadeIn) ? -1 : 1);
                    _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, alpha);
                    yield return null;
                }
            }
            action?.Invoke();
            yield return new WaitForSeconds(0.5f);
            _fadeUI?.SetActive(false);
        }
    }
}
