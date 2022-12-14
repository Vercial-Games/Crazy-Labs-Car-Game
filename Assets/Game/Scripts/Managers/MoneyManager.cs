using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    public float incomeSpeed = 0.5f;
    [SerializeField] float currentMoney = 5;
    [SerializeField] AudioSource source;

    float incomeValue = 3;


    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!GameManager.instance.gamePaused)
            currentMoney += Time.unscaledDeltaTime * incomeSpeed;
    }

    public float GetCurrentMoney()
    {
        return currentMoney;
    }
    public float GetIncomeValue()
    {
        return incomeValue;
    }

    public float SetCurrentMoney(int value)
    {
        return currentMoney = value;
    }

    public float IncreaseIncomeValue(float value)
    {
        source.Play();
        return currentMoney += value;
    }
    public float IncreaseCurrentMoney(float value)
    {
        return currentMoney += value;
    }
    public float DecreaseCurrentMoney(float value)
    {
        return currentMoney -= value;
    }

}
