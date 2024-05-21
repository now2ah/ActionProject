using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;
using Action.Units;

namespace Action.Game
{
    public class HitBox : MonoBehaviour
    {
        Constant.eHitBoxType _type;
        Unit _damager;
        float _damageAmount;
        Collider _collider;
        GameObject _hitEffectObj;

        public void Initialize(Constant.eHitBoxType type, Unit damager, float damageAmount)
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
            Unit unit = other.GetComponentInParent<Unit>();
            if (null == unit)
                return;

            switch (_type)
            {
                case Constant.eHitBoxType.ONLY_ENEMY:
                    if ("EnemyObject" == unit.gameObject.tag)
                    {
                        Unit.DamageMessage msg = new Unit.DamageMessage
                        {
                                damager = _damager,
                                amount = _damageAmount
                        };
                        unit.ApplyDamage(msg);
                        Vector3 pos = unit.transform.position + unit.transform.up * 2.0f;
                        Instantiate(_hitEffectObj, pos, Quaternion.identity);
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
            _hitEffectObj = GameManager.Instance.HitEffectPrefab;
            StartCoroutine(DestroyCoroutine(0.3f));
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
