using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMover : MonoBehaviour
{
    #region VARIABLES
    public bool onStation;

    [SerializeField] float speed;
    [SerializeField] float smoothReset;

    float maxSpeed;
    float CdTimer;
    #endregion

    #region METHODS
    private void Start()
    {
        maxSpeed = speed;
    }
    private void Update()
    {
        if (onStation) { SmoothResetSpeed(GetCurrentSpeed()*25,0); return; }

        SetCurrentSpeed(GetMaxSpeed());
    }
    public float GetCurrentSpeed()
    {
        return speed;
    }
    public float IncreaseCurrentSpeed(float value)
    {
        return speed + value;
    }
    public float SetCurrentSpeed(float value)
    {
        return speed = value;
    }
    public float GetMaxSpeed()
    {
        return maxSpeed;
    }
    public float SmoothResetSpeed(float value, float equal)
    {
        if (speed > equal)
            return speed -= value * Time.deltaTime;

        return speed = equal;
    }
    public float SetCoolDown(float value)
    {
        return CdTimer = value;
    }
    public bool OnStation()
    {
        return onStation;
    }
    #endregion
}
