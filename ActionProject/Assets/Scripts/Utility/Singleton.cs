using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.Util
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        static T _instance = null;
        static bool isQuitting = false;
        public static GameObject _singletonObject;

        public static T Instance
        {
            get
            {
                if (isQuitting)
                    return null;

                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                        return _instance;

                    var prefab = Resources.Load("Prefabs/Singleton/" + typeof(T).Name);
                    if (null != prefab)
                    {
                        _singletonObject = Instantiate(prefab) as GameObject;
                        _instance = _singletonObject.GetComponent<T>();
                    }
                    else
                    {
                        _singletonObject = new GameObject();
                        _instance = _singletonObject.AddComponent<T>();
                    }

                    _singletonObject.name = typeof(T).Name + "Object";
                    DontDestroyOnLoad(_singletonObject);
                }

                return _instance;
            }
        }

        public virtual void Initialize()
        {

        }

        private void OnDestroy()
        {
            isQuitting = true;
        }
    }
}