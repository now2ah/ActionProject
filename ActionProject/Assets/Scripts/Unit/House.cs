using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;
using Action.UI;

namespace Action.Units
{
    public class House : Building
    {
        //GameObject _housePanel;
        //HouseUI _houseUI;

        public override void Interact()
        {
            base.Interact();
            //Logger.Log("House Activate");
        }

        public new void Initialize()
        {
            base.Initialize();
        }

        protected override void Awake()
        {
            base.Awake();
            MaxHp = 1000;
            HP = MaxHp;
            UnitName = "House";
            _constructTime = 2.0f;
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