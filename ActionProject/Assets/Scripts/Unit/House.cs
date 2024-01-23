using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;
using Action.UI;

namespace Action.Units
{
    public class House : Unit
    {
        GameObject _building;
        GameObject _uiPanelObject;
        HousePanel _housePanel;
        float _activeDistance;
        bool _isBuilt = false;

        public bool _CheckPlayerUnitDistance()
        {
            float dist = Vector3.Distance(GameManager.Instance.PlayerUnit.transform.position, transform.position);

            if (dist < _activeDistance)
                return true;
            else
                return false;
        }

        void _Initialize()
        {
            _uiPanelObject = UIManager.Instance.CreateUI("HousePanel", UIManager.Instance.InGameCanvas);
            _housePanel = _uiPanelObject.GetComponent<HousePanel>();
            _housePanel.Initialize(this);
            _activeDistance = 10.0f;
            _isBuilt = false;
        }

        private void Awake()
        {
            _building = gameObject.transform.GetChild(0).gameObject;
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            _Initialize();
        }

        // Update is called once per frame
        protected override void Update()
        {
            if (null != _housePanel)
            {
                if (_CheckPlayerUnitDistance())
                {
                    _uiPanelObject?.SetActive(true);
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
                    _uiPanelObject?.SetActive(false);
                }
            }
        }
    }
}