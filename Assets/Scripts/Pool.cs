using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    //[SerializeField] private GameObject[] _prefab = new GameObject[_numberOfPrefabs];
    private GameObject _prefab;
    private const int _numberOfPrefabs = 5;

    private List<GameObject> _inActive = new List<GameObject>();

    public Pool(GameObject prefab)
    {
        //this._prefab[0] = prefab;
        this._prefab = prefab;
    }

    public GameObject Spawn(Vector3 pos, Quaternion rot)
    {
        GameObject obj;
        if(_inActive.Count == 0)
        {
           //GameObject currentObj = _prefab[Random.Range(1, _numberOfPrefabs)];
            //obj = Instantiate(currentObj, pos, rot);
            obj = Instantiate(_prefab, pos, rot);
            obj.name = _prefab.name;
        }
        else
        {
            obj = _inActive[_inActive.Count - 1];
            _inActive.RemoveAt(_inActive.Count - 1);
        }
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.SetActive(true);
        return obj;
    }

    public void Despawn(GameObject obj)
    {
        obj.SetActive(false);
        _inActive.Add(obj);
    }
}
