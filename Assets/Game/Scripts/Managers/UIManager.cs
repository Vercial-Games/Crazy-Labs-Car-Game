using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TMP_Text money;

    public TMP_Text carPrice;
    public TMP_Text mergePrice;
    public TMP_Text incomePrice;


    public Button addCarButton;
    public Button mergeButton;
    public Button incomeButton;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        money.text = ((int)MoneyManager.instance.GetCurrentMoney()).ToString();

        carPrice.text = PlayerController.instance.carPrice.ToString();
        mergePrice.text = PlayerController.instance.mergePrice.ToString();
        incomePrice.text = PlayerController.instance.incomePrice.ToString();
    }



}
