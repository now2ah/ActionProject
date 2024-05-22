using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.Game
{
    public class Projectile : MonoBehaviour, IPoolable<Projectile>
    {
        public int PoolID { get; set; }
        public ObjectPooler<Projectile> Pool { get; set; }
        float _speed;

        IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(5.0f);
            Pool.Free(this);
        }

        // Start is called before the first frame update
        void Start()
        {
            _speed = 0.2f;
            StartCoroutine(DestroyCoroutine());
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            transform.Translate(Vector3.forward * _speed);
        }
    }
}

