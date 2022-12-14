using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public bool empty;
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CarController>())
        {
            empty = false;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CarController>())
        {
            empty = true;
        }
    }
}
