using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;

namespace Action.Scene
{

    public class IntroScene : MonoBehaviour
    {
        void _InitializeSingletons()
        {
            UIManager.Instance.Initialize();
            SceneManager.Instance.Initialize();
        }

        IEnumerator TestCoroutine()
        {
            yield return new WaitForSeconds(1.5f);
            SceneManager.Instance.Fade(SceneManager.eFade.FadeOut);
            yield return new WaitForSeconds(1.5f);
            SceneManager.Instance.Fade(SceneManager.eFade.FadeIn);
        }

        private void Awake()
        {
            _InitializeSingletons();
        }

        void Start()
        {
            UIManager.Instance.CreateUI("IntroPanel");
            StartCoroutine(TestCoroutine());
        }

    }
}