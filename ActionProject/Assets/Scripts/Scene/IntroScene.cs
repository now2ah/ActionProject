using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Action.Manager;

namespace Action.Scene
{

    public class IntroScene : MonoBehaviour
    {
        GameObject _introPanel;

        void _InitializeSingletons()
        {
            UIManager.Instance.Initialize();
            SceneManager.Instance.Initialize();
        }

        IEnumerator IntroCoroutine()
        {
            SceneManager.Instance.Fade(SceneManager.eFade.FadeIn);
            yield return new WaitForSeconds(1.5f);
            SceneManager.Instance.Fade(SceneManager.eFade.FadeOut, () => 
            {
                SceneManager.Instance.LoadGameScene(1);
            });
        }

        private void Awake()
        {
            _InitializeSingletons();
        }

        void Start()
        {
            _introPanel = UIManager.Instance.CreateUI("IntroPanel");
            StartCoroutine(IntroCoroutine());
        }

        private void OnDestroy()
        {
            Destroy(_introPanel);
        }
    }
}