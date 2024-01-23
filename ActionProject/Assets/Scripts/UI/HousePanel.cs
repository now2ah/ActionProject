using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Units;
using Action.Manager;

namespace Action.UI
{
    public class HousePanel : ControlUI
    {
        House _house;
        
        public void Initialize(House house)
        {
            _owner = house.gameObject;
            _house = house;
            
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            _FollowTargetPosition();
        }
    }

}