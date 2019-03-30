using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tryMJcard
{
    public abstract class Singleton<T> where T :new()
    {
        private static T m_Instance;

        public static T Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    if (m_Instance == null)
                    {
                        m_Instance = new T();
                    }
                }
                return m_Instance;
            }
        }
        public int b;
    }
}
