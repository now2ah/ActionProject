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

    public class Resource
    {
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
            _gold = GameManager.Instance.Constants.START_GOLD_AMOUNT;
        }
    }
}