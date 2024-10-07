using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;

namespace Action.Game
{
    public enum eResource
    {
        GOLD,
        FOOD,
        WOOD,
        IRON
    }

    [System.Serializable]
    public class Resource
    {
        //int[] _resources;
        //List<int> _resources;
        //public int[] Resources => _resources;
        //public List<int> Resources => _resources;

        public int Gold = 0;
        public int Food = 0;
        public int Wood = 0;
        public int Iron = 0;

        public void Initialize()
        {
            //_resources = new int[4];
            //_resources[(int)eResource.GOLD] = GameManager.Instance.Constants.START_GOLD_AMOUNT;

            //_resources = new List<int>();
            //_resources.Add(GameManager.Instance.Constants.START_GOLD_AMOUNT);
            //_resources.Add(0);
            //_resources.Add(0);
            //_resources.Add(0);

            Gold = GameManager.Instance.Constants.START_GOLD_AMOUNT;
        }

        public bool IsValidSpend(int requireResource, eResource type)
        {
            int amount = 0;
            switch(type)
            {
                case eResource.GOLD:
                    amount = Gold;
                    break;
                case eResource.FOOD:
                    amount = Food;
                    break;
                case eResource.WOOD:
                    amount = Wood;
                    break;
                case eResource.IRON:
                    amount = Iron;
                    break;
            }

            if (amount >= requireResource)
                return true;
            else
                return false;
        }

        public void Spend(int requireResource, eResource type)
        {
            switch (type)
            {
                case eResource.GOLD:
                    Gold -= requireResource;
                    break;
                case eResource.FOOD:
                    Food -= requireResource;
                    break;
                case eResource.WOOD:
                    Wood -= requireResource;
                    break;
                case eResource.IRON:
                    Iron -= requireResource;
                    break;
            }
        }
    }
}