using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ExGM : MonoBehaviour
{
    // �ڽ� Ŭ�������� �����ϴ��� ExGM�� ��ȯ��
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
