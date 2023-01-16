using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsWindow : WindowsBase
{
    [SerializeField] private TextMeshProUGUI[] stats;

    protected override void onShow()
    {
        int i = 0;
        foreach (Question question in GameManager.instance.questions)
        {
            stats[i].text = question.question + " = " + question.value;
            stats[i].color = question.IsTrue ? Color.black : Color.red;
            i++;
        }
    }

    public void StartSelectLevel()
    {
        GameManager.instance.SetNextState();
    }
}
