using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace tryMJcard
{
    /// <summary>
    /// 该方法只试用于unity中的脚本文件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        
        private static T m_Instance = null;

        public static T Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = GameObject.FindObjectOfType(typeof(T)) as T;
                    if (m_Instance == null)
                    {
                        m_Instance = new GameObject("Singleton of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();
                    }
                    m_Instance.Initialize();
                }
                return m_Instance;
            }
        }

        protected virtual void Awake()
        {
            if (m_Instance == null)
            {
                m_Instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
        }

        protected virtual void OnDestory()
        {
            DeInitialize();
        }

        /// <summary>
        /// 初始化所用方法
        /// </summary>
        public virtual void Initialize(){ }

        /// <summary>
        /// 消除时所调用方法
        /// </summary>
        public virtual void DeInitialize(){ }

        private void OnApplicationQuit()//该方法待开发
        {
            m_Instance = null;
        }
    }
}
