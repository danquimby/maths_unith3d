using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWindow : WindowsBase
{
    // Умножение
    public void StartMultiplication()
    {
        GameManager.instance.SetState(WindowsName.SELECT_LEVEL);
    }

    public void Reset()
    {
        GameManager.instance.CreateNewStats();
    }

    public void Quit()
    {
        Application.Quit(0);
    }
}
