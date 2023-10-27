using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T instance { get { return _instance; } }
    
    // Start is called before the first frame update
    void Awake()
    {
        //Singleton
        if (null == instance)
        {
            _instance = this.GetComponent<T>();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
