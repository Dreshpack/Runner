using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
    public static T instance = null;
    public static T Instance
    {
        get
        {
            if(instance != null)
            {
                return instance;
            }
            T[] instances = FindObjectsOfType<T>();
            if(instances.Length > 0)
            {
                instance = instances[0];
                for(int i = 0; i < instances.Length; i++)
                {
                    Destroy(instances[i]);
                }
            }
            else
            {
                GameObject ST = new GameObject();
                ST.name = typeof(T).ToString();
                instance = ST.AddComponent<T>();
            }
            //DontDestroyOnLoad(instance);
            return instance;
        }
    }
}
