using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using PathCreation.Examples;

public class CarController : MonoBehaviour
{
    #region VARIABLES
    public CarController forwardCar;
    public Animator animator;
    public AudioClip[] soundEffects;
    public int PassangersCount;

    AudioSource source;
    PathFollower pathFollower = null;
    CarMover carMover;
    CarType carType;
    #endregion

    #region METHODS
    private void Start()
    {
        References();
        PlaySound(0);
    }
    private void References()
    {
        pathFollower = GetComponent<PathFollower>();
        carMover = GetComponent<CarMover>();
        carType = GetComponent<CarType>();
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        animator.SetBool("Run", IsCarMoving());

        if (forwardCar != null) return;

        pathFollower.speed = carMover.GetCurrentSpeed();
    }
    private void FixedUpdate()
    {
        TrafficCalculator();
    }
    public void PlaySound(int value)
    {
        source.clip = soundEffects[value];
        source.Play();
    }
    private void TrafficCalculator()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 0.15f, Color.red);

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
        else
        {
            forwardCar = null;
        }
    }
    public void LevelUp()
    {
        PlaySound(2);
        PassangersCount = 0;
        carType.LevelUp();
    }
    public void DestroyCar()
    {
        carType.DestroyCar();
    }
    public bool IsCarMoving()
    {
        return !carMover.onStation;
    }
    #endregion 
}
