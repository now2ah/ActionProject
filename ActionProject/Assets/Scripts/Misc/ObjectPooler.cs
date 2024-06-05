using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.Util
{
    public interface IPoolable<T> where T : MonoBehaviour, IPoolable<T>
    {
        int PoolID { get; set; }
        ObjectPooler<T> Pool { get; set; }
    }

    public class ObjectPooler<T> where T : MonoBehaviour, IPoolable<T>
    {
        public List<T> instanceList;
        protected Stack<int> index;

        public void Initialize(T obj, int amount, GameObject parent)
        {
            instanceList = new List<T>();
            index = new Stack<int>();

            for (int i = 0; i < amount; i++)
            {
                T instance = Object.Instantiate(obj);
                instance.gameObject.transform.SetParent(parent.transform);
                instance.gameObject.SetActive(false);
                instance.PoolID = i;
                instance.Pool = this;
                instanceList.Add(instance);

                index.Push(i);
            }
        }

        public T GetNew()
        {
            int idx = index.Pop();
            instanceList[idx].gameObject.SetActive(true);

            return instanceList[idx];
        }

        public void Free(T obj)
        {
            index.Push(obj.PoolID);
            instanceList[obj.PoolID].gameObject.SetActive(false);
        }
    }
}
