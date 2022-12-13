using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMover : MonoBehaviour
{
    public bool onStation;

    [SerializeField] float speed;
    [SerializeField] float speedBuff;
    [SerializeField] float speedBuffCooldown;
    [SerializeField] float smoothReset;

    float maxSpeed;
    float CdTimer;


    private void Start()
    {
        maxSpeed = speed;
    }
    private void Update()
    {
        if (onStation) { SmoothResetSpeed(GetCurrentSpeed()*25,0); return; }

        CooldownTimer();
    }
    private void CooldownTimer()
    {
        CdTimer += Time.deltaTime;
        if (CdTimer >= speedBuffCooldown)
        {
            SmoothResetSpeed(smoothReset, maxSpeed);
        }
    }
    public float GetSpeedBuff()
    {
        return speedBuff;
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
    public void SpeedBuff()
    {
        SetCurrentSpeed(GetSpeedBuff());
    }
    public float SetCoolDown(float value)
    {
        return CdTimer = value;
    }
    public float SmoothResetSpeed(float value, float equal)
    {
        if (speed > equal)
            return speed -= value * Time.deltaTime;

        return speed = equal;
    }
    public bool OnStation()
    {
        return onStation;
    }
}
