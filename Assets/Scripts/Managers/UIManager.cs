using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TMP_Text money;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        money.text = ((int)MoneyManager.instance.GetCurrentMoney()).ToString();
    }
    


}
