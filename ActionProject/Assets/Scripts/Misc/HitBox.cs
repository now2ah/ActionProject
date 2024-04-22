using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Units;

namespace Action.Game
{
    public class HitBox : MonoBehaviour
    {
        Constant.eHitBoxType _type;
        Unit _damager;
        float _damageAmount;
        Collider _collider;

        public HitBox(Constant.eHitBoxType type, Unit damager, float damageAmount)
        {
            _type = type;
            _damager = damager;
            _damageAmount = damageAmount;
        }

        public void ResizeTrigger(float amount)
        {
            if (null != _collider)
            {
                _collider.bounds.Expand(amount);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            switch(_type)
            {
                case Constant.eHitBoxType.ONLY_ENEMY:
                    if ("EnemyObject" == other.gameObject.tag)
                    {
                        if (other.TryGetComponent<Unit>(out Unit comp))
                        {
                            Unit.DamageMessage msg = new Unit.DamageMessage
                            {
                                damager = _damager,
                                amount = _damageAmount
                            };
                            comp.ApplyDamage(msg);
                        }
                    }
                    break;
                case Constant.eHitBoxType.ONLY_PLAYER:
                    break;
                case Constant.eHitBoxType.BOTH:
                    break;
            }
        }

        IEnumerator DestroyCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(DestroyCoroutine(0.3f));
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
