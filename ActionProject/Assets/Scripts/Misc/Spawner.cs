using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
            0,
            Random.Range(col.bounds.min.z, col.bounds.max.z));

        if (spawnObj.TryGetComponent<NavMeshAgent>(out NavMeshAgent comp))
        {
            comp.transform.rotation = Quaternion.identity;
            comp.Warp(spawnPos);
        }
    }
}
