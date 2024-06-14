using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    GameObject _obj;
    public GameObject Obj { get { return _obj; } set { _obj = value; } }
    Collider col;

    private void Awake()
    {
        col = gameObject.GetComponent<Collider>();
    }

    public GameObject CreateObject()
    {
        if (null != _obj)
        {
            Vector3 spawnPos = new Vector3(Random.Range(col.bounds.min.x, col.bounds.max.x),
            Random.Range(col.bounds.min.y, col.bounds.max.y),
            Random.Range(col.bounds.min.z, col.bounds.max.z));
            return Instantiate(_obj, spawnPos, Quaternion.identity);
        }
        else
            return null;
    }

    public void SpawnObject(GameObject spawnObj)
    {
        Vector3 spawnPos = new Vector3(Random.Range(col.bounds.min.x, col.bounds.max.x),
            Random.Range(col.bounds.min.y, col.bounds.max.y),
            Random.Range(col.bounds.min.z, col.bounds.max.z));

        spawnObj.transform.position = spawnPos;
    }
}
