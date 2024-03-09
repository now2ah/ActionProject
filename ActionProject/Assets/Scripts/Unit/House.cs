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

        public override void Initialize()
        {
            base.Initialize();
            //_housePanel = UIManager.Instance.CreateUI("HouseUI", UIManager.Instance.InGameCanvas);
            //_houseUI = _housePanel.GetComponent<HouseUI>();
            //_houseUI.Initialize(this.gameObject);
            //_houseUI.SetParent(_controlPanel.transform);
            //_houseUI.Hide();
            _constructTime = 2.0f;
        }

        protected override void Awake()
        {
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            MaxHp = 1000;
            HP = MaxHp;
            base.Start();
            Initialize();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }
}