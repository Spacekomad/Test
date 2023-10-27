using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CogPool : MonoBehaviour
{
    public static CogPool instance { get; private set; }

    public GameObject bulletPrefab;

    public int maxCogNum = 5;

    private Queue<Projectile> poolingObjectQueue = new Queue<Projectile>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            LayerMask.GetMask();
        }

        for (int i = 0; i < maxCogNum; ++i)
        {
            poolingObjectQueue.Enqueue(CreateCog());
        }
    }

    private Projectile CreateCog()
    {
        var newObj = Instantiate(bulletPrefab).GetComponent<Projectile>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public Projectile GetCog()
    {
        if (poolingObjectQueue.Count > 0)
        {
            var obj = poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = CreateCog();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    public void ReturnCog(Projectile obj)
    {
        if (poolingObjectQueue.Count >= maxCogNum)
        {
            Destroy(obj.gameObject);
        }
        else
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(transform);
            poolingObjectQueue.Enqueue(obj);
        }
    }
}
