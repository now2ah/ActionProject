using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Util;
using Action.Manager;

namespace Action.Game
{
    public class Coin : MonoBehaviour
    {
        int _gold;
        public int Gold => _gold;

        public void Initialize(int gold)
        {
            _gold = gold;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "PlayerObject")
            {
                if (other.TryGetComponent<Units.Commander>(out Units.Commander comp))
                {
                    AudioManager.Instance.PlaySFX(AudioManager.eSfx.COINPICKUP);
                    Manager.GameManager.Instance.GameData.resource.Gold += _gold;
                    Destroy(gameObject);
                }
            }
        }

        private void Awake()
        {
            _gold = 1;
        }

        private void FixedUpdate()
        {
            transform.Rotate(Vector3.up, 5.0f);
        }
    }
}
