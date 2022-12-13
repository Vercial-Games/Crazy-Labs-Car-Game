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
    private void GetCarType()
    {
        if (carType == Type.Taxi)
        {
            PassangerCapacity = 3;
            ChangeCarSkin(Type.Taxi);
        }
        if (carType == Type.Uber)
        {
            PassangerCapacity = 5;
            ChangeCarSkin(Type.Uber);
        }
        if (carType == Type.Bus)
        {
            PassangerCapacity = 10;
            ChangeCarSkin(Type.Bus);
        }
    }
    public void LevelUp()
    {
        if (carType == Type.Taxi)
        {
            carType = Type.Uber;
        }
        if (carType == Type.Uber)
        {
            carType = Type.Bus;
        }

        GetCarType();
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
