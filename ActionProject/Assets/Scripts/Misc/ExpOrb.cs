using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Util;
using Action.Units;
using Action.Manager;
using DG.Tweening;

namespace Action.Game
{
    public class ExpOrb : MonoBehaviour, IPoolable<ExpOrb>
    {
        int _expAmount;
        float _absorbDistance;

        public int ExpAmount { get { return _expAmount; } set { _expAmount = value; } }

        public int PoolID { get; set; }
        public ObjectPooler<ExpOrb> Pool { get; set; }

        public void Initialize(int expAmount)
        {
            _expAmount = expAmount;
            
        }

        bool _IsNearCommander()
        {
            float dist = Vector3.Distance(GameManager.Instance.CommanderUnit.transform.position, transform.position);
            if (_absorbDistance < dist)
                return false;
            else
                return true;
        }

        void _Absorb()
        {
            //transform.DOMove(GameManager.Instance.CommanderUnit.transform.position, 1.0f).SetEase(Ease.OutSine);
            transform.LookAt(GameManager.Instance.CommanderUnit.transform);
            transform.position = transform.position + (transform.forward * Time.deltaTime * 10.0f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<PlayerUnit>(out PlayerUnit comp))
            {
                comp.GainExp(_expAmount);
                Pool.Free(this);
            }
        }

        void Awake()
        {
            _absorbDistance = 5;
        }

        void Update()
        {
            if (_IsNearCommander())
                _Absorb();

        }
    }
}

