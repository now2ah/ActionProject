using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.Game
{
    public class Projectile : MonoBehaviour
    {
        float _speed;

        // Start is called before the first frame update
        void Start()
        {
            _speed = 0.2f;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            transform.Translate(transform.forward * _speed);
        }
    }
}

