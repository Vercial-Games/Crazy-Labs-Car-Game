using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject carPrefab;
    [SerializeField] List<GameObject> currentCars;

    private void Start()
    {
        AddCar();
    }
    public void AddCar()
    { 
        GameObject car = Instantiate(carPrefab);
        car.GetComponent<PathFollower>().pathCreator = FindObjectOfType<PathCreator>();
        currentCars.Add(car);
    }
}
