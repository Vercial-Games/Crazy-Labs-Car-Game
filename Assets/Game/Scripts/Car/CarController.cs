using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class CarController : MonoBehaviour
{
    public int PassangersCount;
    public CarController forwardCar;

    PathFollower pathFollower = null;
    CarMover carMover;
    CarType carType;

    private void Start()
    {
        pathFollower = GetComponent<PathFollower>();
        carMover = GetComponent<CarMover>();
        carType= GetComponent<CarType>();
    }

    private void Update()
    {
        if (forwardCar != null) return;

        pathFollower.speed = carMover.GetCurrentSpeed();
        BuffControl();
    }
    private void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 0.65f, Color.red);

            if (hit.transform.gameObject.GetComponent<CarController>())
            {
                forwardCar = hit.transform.gameObject.GetComponent<CarController>();
                pathFollower.speed = forwardCar.pathFollower.speed;
            }
            else
            {
                forwardCar = null;
            }
        }
    }
    private void BuffControl()
    {
        if (Input.GetMouseButtonDown(0) && !carMover.OnStation())
        {
            carMover.SpeedBuff();
            carMover.SetCoolDown(0);
        }
    }
    public void LevelUp()
    {
        carType.LevelUp();
    }
    public void DestroyCar()
    {
        carType.DestroyCar();
    }

}
