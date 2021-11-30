using System.Collections;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance = null;
    public static T Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;

            if (_instance == this)
                BeginPlay();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            EndPlay();
            _instance = null;
        }
    }

    protected virtual void BeginPlay() { }
    protected virtual void EndPlay() { }
}
