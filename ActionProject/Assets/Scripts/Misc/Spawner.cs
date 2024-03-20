using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    GameObject _spawnObject;
    public GameObject SpawnObject { get { return _spawnObject; } set { _spawnObject = value; } }
    Collider col;

    private void Awake()
    {
        col = gameObject.GetComponent<Collider>();
    }

    public GameObject CreateObject()
    {
        if (null != _spawnObject)
        {
            Vector3 spawnPos = new Vector3(Random.Range(col.bounds.min.x, col.bounds.max.x),
            Random.Range(col.bounds.min.y, col.bounds.max.y),
            Random.Range(col.bounds.min.z, col.bounds.max.z));
            return Instantiate(_spawnObject, spawnPos, Quaternion.identity);
        }
        else
            return null;
    }

    public GameObject CreateObject(GameObject spawnObj)
    {
        Vector3 spawnPos = new Vector3(Random.Range(col.bounds.min.x, col.bounds.max.x),
            Random.Range(col.bounds.min.y, col.bounds.max.y),
            Random.Range(col.bounds.min.z, col.bounds.max.z));
        return Instantiate(spawnObj, spawnPos, Quaternion.identity);
    }
}
