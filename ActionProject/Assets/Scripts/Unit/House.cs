using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;
using Action.UI;

namespace Action.Units
{
    public class House : Building
    {
        GameObject _housePanel;
        HouseUI _houseUI;

        public override void Interact()
        {
            base.Interact();
            //Logger.Log("House Activate");
        }

        public override void Initialize()
        {
            base.Initialize();
            _housePanel = UIManager.Instance.CreateUI("HouseUI", UIManager.Instance.InGameCanvas);
            _houseUI = _housePanel.GetComponent<HouseUI>();
            _houseUI.Initialize(this.gameObject);
            _houseUI.SetParent(_controlPanel.transform);
            _houseUI.Hide();
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
            if (null != _houseUI)
            {
                if (_IsNearPlayerUnit())
                {
                    _houseUI?.Show();
                    GameManager.Instance.PlayerUnit.InteractingBuilding = this.gameObject;
                }
                else
                {
                    GameManager.Instance.PlayerUnit.InteractingBuilding = null;
                    _houseUI?.Hide();
                }
            }
            //if (null != _housePanel)
            //{
            //    if (_CheckPlayerUnitDistance())
            //    {
            //        _uiPanelObject?.SetActive(true);
            //        GameManager.Instance.PlayerUnit.InteractingBuilding = this.gameObject;

            //        if (StateMachine.CurState != _idleState)
            //        {
            //            _housePanel?.BuildPanel?.SetActive(true);
            //            _housePanel?.ControlPanel?.SetActive(false);
            //        }
            //        else
            //        {
            //            _housePanel?.BuildPanel?.SetActive(false);
            //            _housePanel?.ControlPanel?.SetActive(true);
            //        }
            //    }
            //    else
            //    {
            //        GameManager.Instance.PlayerUnit.InteractingBuilding = null;
            //        _uiPanelObject?.SetActive(false);
            //    }
            //}
        }
    }
}