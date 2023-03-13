using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    private Dictionary<string, Pool> pools = new Dictionary<string, Pool>();

    private void Init(GameObject prefab)
    {
        if(prefab != null && pools.ContainsKey(prefab.name) == false)
        {
            pools[prefab.name] = new Pool(prefab);
        }
    }

    public GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        Init(prefab);
        return pools[prefab.name].Spawn(pos, rot);

    }

    public void Despawn(GameObject obj)
    {
        if(pools.ContainsKey(obj.name))
        {
            pools[obj.name].Despawn(obj);
        }
        else
        {
            Destroy(obj);
        }
    }

}
