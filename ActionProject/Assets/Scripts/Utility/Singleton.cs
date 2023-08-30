using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.Util
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        static T _instance = null;
        public static GameObject _singletonObject;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    _singletonObject = go;
                    _instance = _singletonObject.AddComponent<T>();
                }

                return _instance;
            }
        }

        public virtual void Initialize()
        {

        }

        public void SetName(string name)
        {
            _singletonObject.name = name;
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}