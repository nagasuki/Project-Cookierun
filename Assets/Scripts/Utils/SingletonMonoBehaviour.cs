using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PugDev
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        public static T Instance;

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = (T)this;
            }
        }
    }
}