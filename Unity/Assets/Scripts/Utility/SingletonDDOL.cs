using System.Collections;
using UnityEngine;

public abstract class SingletonDDOL<T> : Singleton<T> where T : Component
{
    protected override void BeginPlay()
    {
        base.BeginPlay();
        DontDestroyOnLoad(gameObject);
    }
}
