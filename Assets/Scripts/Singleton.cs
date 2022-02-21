using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton <T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject tempGameObject = new GameObject();
                    _instance = tempGameObject.AddComponent<T>();
                    _instance.name = typeof(T).ToString();
                    

                }
            }
            return _instance;
        }
    }


    // Start is called before the first frame update
    protected virtual void Awake()
    {
        _instance = this as T;
    }

    public void DestroyInstance()
    {
        Destroy(gameObject);
        _instance = null;
    }

}
