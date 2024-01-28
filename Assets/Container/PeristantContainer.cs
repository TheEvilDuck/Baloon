using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Containers
{
    public class PeristantContainer : MonoBehaviour
    {
        private Dictionary<Type,object>_registeredObjects;
        
        public bool Initilizied {get; private set;} = false;

        public void Init()
        {
            _registeredObjects = new Dictionary<Type, object>();
            DontDestroyOnLoad(this);
            Initilizied = true;
        }

        public bool TryRegister(object obj)
        {
            if (_registeredObjects.ContainsKey(obj.GetType()))
                return false;

            _registeredObjects.Add(obj.GetType(),obj);
            return true;
        }

        public bool TryGet<T>(out object obj)
        {
            obj =null;

            if (!_registeredObjects.ContainsKey(typeof(T)))
                return false;

            obj = _registeredObjects.GetValueOrDefault(typeof(T));
            return true;
        }
    }
}
