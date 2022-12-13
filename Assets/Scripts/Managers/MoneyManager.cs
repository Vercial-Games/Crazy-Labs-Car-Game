using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;
    float incomeValue = 5;
    float currentMoney = 10;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        currentMoney += Time.deltaTime;
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
        return currentMoney += value;
    }
    public float IncreaseCurrentMoney(float value)
    {
        return currentMoney += value;
    }

}
