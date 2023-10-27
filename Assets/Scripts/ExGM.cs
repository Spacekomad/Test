using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ExGM : MonoBehaviour
{
    // 자식 클래스에서 접근하더라도 ExGM을 반환함
    private static ExGM _instance;
    public static ExGM instance { get { return _instance; } }


    void Awake()
    {
        //Singleton
        if (null == instance)
        {
            _instance = this.GetComponent<ExGM>();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
