using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.Game
{
    public class HitEffect : MonoBehaviour
    {
        ParticleSystem _particle;

        IEnumerator DestroyCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }

        private void Awake()
        {
            _particle = GetComponent<ParticleSystem>();
        }

        // Start is called before the first frame update
        void Start()
        {
            if (null != _particle)
            {
                StartCoroutine(DestroyCoroutine(_particle.main.duration));
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

