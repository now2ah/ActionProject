using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.Util
{
    public static class Utility
    {
        /// <summary>
        /// Getting Near GameObejct between two Objects
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static GameObject GetNearerObject(Vector3 pos, GameObject obj1, GameObject obj2)
        {
            GameObject nearerObject = null;
            Vector3 dist1 = obj1.gameObject.transform.position - pos;
            Vector3 dist2 = obj2.gameObject.transform.position - pos;

            if (dist1.sqrMagnitude < dist2.sqrMagnitude)
                nearerObject = obj1;
            else
                nearerObject = obj2;
            return nearerObject;
        }
    }
}

