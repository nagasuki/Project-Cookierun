using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PugDev
{
    public class SingletonMonoBehaviourDontDestroy<T> : MonoBehaviour where T : SingletonMonoBehaviourDontDestroy<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = (T)this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}