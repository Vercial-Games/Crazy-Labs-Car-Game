using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarType : MonoBehaviour
{
    #region VARIABLES
    public int PassangerCapacity;
    public enum Type
    {
        Taxi,
        Uber,
        Bus
    }

    public Type carType;

    public GameObject taxi;
    public GameObject uber;
    public GameObject bus;
    #endregion

    #region METHODS
    private void Start()
    {
        GetCarType();
    }
    public void LevelUp()
    {
        if (carType == Type.Uber)
        {
            carType = Type.Bus;
        }
        else if (carType == Type.Taxi)
        {
            carType = Type.Uber;
        }

        GetCarType();
    }
    public void DestroyCar()
    {
        if (carType == Type.Taxi)
        {
            PlayerController.instance.TaxiCount--;
        }
        if (carType == Type.Uber)
        {
            PlayerController.instance.UberCount--;
        }
        if (carType == Type.Bus)
        {
            PlayerController.instance.UberCount--;
        }
        Destroy(this);
    }
    private void GetCarType()
    {
        if (carType == Type.Taxi)
        {
            PassangerCapacity = 1;
            ChangeCarSkin(Type.Taxi);
            PlayerController.instance.TaxiCount++;
            GetComponent<CarController>().animator = taxi.GetComponent<Animator>();
        }
        else if (carType == Type.Uber)
        {
            PassangerCapacity = 3;
            ChangeCarSkin(Type.Uber);
            PlayerController.instance.UberCount++;
            GetComponent<CarController>().animator = uber.GetComponent<Animator>();
        }
        else if (carType == Type.Bus)
        {
            PassangerCapacity = 5;
            ChangeCarSkin(Type.Bus);
            PlayerController.instance.BusCount++;
            GetComponent<CarController>().animator = bus.GetComponent<Animator>();
        }
    }
    void ChangeCarSkin(Type type)
    {
        if (type == Type.Taxi)
        {
            taxi.SetActive(true);
            uber.SetActive(false);
            bus.SetActive(false);
        }
        if (type == Type.Uber)
        {
            taxi.SetActive(false);
            uber.SetActive(true);
            bus.SetActive(false);
        }
        if (type == Type.Bus)
        {
            taxi.SetActive(false);
            uber.SetActive(false);
            bus.SetActive(true);
        }
    }
    #endregion
}
