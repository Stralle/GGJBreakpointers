using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField]
    bool m_DoNotDestroyOnLoad = false;

    private static T m_Instance = null;

    public static T GetInstance()
    {
        if (m_Instance)
        {
            return m_Instance;
        }

        Debug.LogError("Missing instance. ");
        return null;
    }

    private void Awake()
    {
        m_Instance = this as T;

        if (m_DoNotDestroyOnLoad)
        {
            DontDestroyOnLoad(this);
        }
    }
}
