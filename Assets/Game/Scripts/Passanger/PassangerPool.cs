using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassangerPool : MonoBehaviour
{
    public static PassangerPool instance;

    [SerializeField] List<GameObject> pooledObjects = new List<GameObject>();

    [SerializeField] private GameObject passangerPrefab;
    [SerializeField] int amount = 25;

    private void Awake()
    {
        instance = this;
        InstantiateNewPassangers();
    }

    private void InstantiateNewPassangers()
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(passangerPrefab);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}
