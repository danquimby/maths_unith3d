using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Answer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private int number;

    public int Number
    {
        get => number;
        set
        {
            number = value;
            text.text = number.ToString();
        }
    }
    public void onClick()
    {
        GameManager.instance.MainGame.SetState(number);
    }
}
