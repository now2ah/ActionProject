using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;
using Action.UI;

namespace Action.Units
{
    public class House : Building
    {
        GameObject _building;
        GameObject _uiPanelObject;
        HousePanel _housePanel;

        public override void Activate()
        {
            base.Activate();
            //Logger.Log("House Activate");
        }

        public override void Initialize()
        {
            base.Initialize();
            _uiPanelObject = UIManager.Instance.CreateUI("HousePanel", UIManager.Instance.InGameCanvas);
            _housePanel = _uiPanelObject.GetComponent<HousePanel>();
            _housePanel.Initialize(this.gameObject);
            Logger.Log("House Init");
        }

        private void Awake()
        {
            _building = gameObject.transform.GetChild(0).gameObject;
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            FullHp = 1000;
            HP = FullHp;
            base.Start();
            Initialize();
        }

        // Update is called once per frame
        protected override void Update()
        {
            if (null != _housePanel)
            {
                if (_CheckPlayerUnitDistance())
                {
                    _uiPanelObject?.SetActive(true);
                    GameManager.Instance.PlayerUnit.InteractingBuilding = this.gameObject;

                    if (!_isBuilt)
                    {
                        _housePanel?.BuildPanel?.SetActive(true);
                        _housePanel?.ControlPanel?.SetActive(false);
                    }
                    else
                    {
                        _housePanel?.BuildPanel?.SetActive(false);
                        _housePanel?.ControlPanel?.SetActive(true);
                    }
                }
                else
                {
                    GameManager.Instance.PlayerUnit.InteractingBuilding = null;
                    _uiPanelObject?.SetActive(false);
                }
            }
        }
    }
}