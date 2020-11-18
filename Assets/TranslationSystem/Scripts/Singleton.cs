﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TranslationSystem
{

    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField]
        private bool dontDestroy = false;

        static T m_instance;

        /// <summary>
        /// The object static Instance
        /// </summary>
        public static T Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = GameObject.FindObjectOfType<T>();
                    if (m_instance == null)
                    {
                        GameObject singleton = new GameObject(typeof(T).Name);
                        m_instance = singleton.AddComponent<T>();
                    }
                }
                return m_instance;
            }
        }

        protected virtual void Awake()
        {
            if (m_instance == null)
            {
                m_instance = this as T;
                if (dontDestroy)
                {
                    transform.SetParent(null);
                    DontDestroyOnLoad(this.gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

    }
}