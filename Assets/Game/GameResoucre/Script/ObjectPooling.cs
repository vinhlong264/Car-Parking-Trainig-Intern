using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private static ObjectPooling instance;
    public static ObjectPooling Instance => instance;

    readonly Dictionary<GameObject , List<GameObject>> pools = new Dictionary<GameObject , List<GameObject>>();

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public GameObject GetObjToPools(GameObject keyObj)
    {
        List<GameObject> dumpPools = new List<GameObject>();

        if (pools.ContainsKey(keyObj))
        {
            dumpPools = pools[keyObj];
        }
        else
        {
            pools.Add(keyObj , dumpPools);
        }

        foreach(var g in dumpPools)
        {
            if (g.activeSelf) continue;

            g.SetActive(true);
            return g;
        }

        GameObject tmp = Instantiate(keyObj);
        dumpPools.Add(tmp);
        tmp.SetActive(true);
        return tmp;
    }
}
