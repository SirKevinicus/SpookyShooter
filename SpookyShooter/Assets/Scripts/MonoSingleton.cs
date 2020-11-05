using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    public static T instance { get; protected set; }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
            throw new System.Exception("An instance of this singleton already exists.");
        }
        else
        {
            instance = (T)this;
            DontDestroyOnLoad(instance.gameObject);
        }
    }
}