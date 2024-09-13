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
        int[] _resources;
        public int[] Resources => _resources;

        int _gold = 0;
        public int Gold { get { return _gold; } set { _gold = value; } }
        int _food = 0;
        public int Food { get { return _food; } set { _food = value; } }
        int _wood = 0;
        public int Wood { get { return _wood; } set { _wood = value; } }
        int _iron = 0;
        public int Iron { get { return _iron; } set { _iron = value; } }

        public void Initialize()
        {
            _resources = new int[4];
            _resources[(int)eResource.GOLD] = GameManager.Instance.Constants.START_GOLD_AMOUNT;
        }

        public bool IsValidSpend(int requireResource, eResource type)
        {
            if (_resources[(int)type] >= requireResource)
                return true;
            else
                return false;
        }

        public void Spend(int requireResource, eResource type)
        {
            _resources[(int)type] -= requireResource;
        }
    }
}