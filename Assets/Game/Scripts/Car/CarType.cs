using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarType : MonoBehaviour
{
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
    }
    private void GetCarType()
    {
        if (carType == Type.Taxi)
        {
            PassangerCapacity = 1;
            ChangeCarSkin(Type.Taxi);
            PlayerController.instance.TaxiCount++;
        }
        else if (carType == Type.Uber)
        {
            PassangerCapacity = 3;
            ChangeCarSkin(Type.Uber);
            PlayerController.instance.UberCount++;
        }
        else if (carType == Type.Bus)
        {
            PassangerCapacity = 5;
            ChangeCarSkin(Type.Bus);
            PlayerController.instance.BusCount++;
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

}
