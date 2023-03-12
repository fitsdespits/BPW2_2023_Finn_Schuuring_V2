using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Director : MonoBehaviour
{
    public static int money;

    public TMP_Text moneyDisplay;

    public void Update()
    {
        moneyDisplay.text = money.ToString();
    }
}
