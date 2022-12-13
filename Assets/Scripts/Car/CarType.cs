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

    private void Start()
    {
        if (carType == Type.Taxi)
        {
            PassangerCapacity = 3;
        }
        if (carType == Type.Uber)
        {
            PassangerCapacity = 5;
        }
        if (carType == Type.Bus)
        {
            PassangerCapacity = 10;
        }
    }
}
