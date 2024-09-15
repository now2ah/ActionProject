using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.Units
{
    public class Fence : Building
    {
        public override void Initialize()
        {
            //if (_isActive)
            //    return;

            base.Initialize();

            RequireTextUI.Text.text = _requireGold.ToString();
        }

        protected override void Awake()
        {
            base.Awake();
            _requireGold = 5;
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            //Initialize();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }
}


