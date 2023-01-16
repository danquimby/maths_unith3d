using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsBase : MonoBehaviour
{
    [Header("панели для show/hide")]
    [SerializeField] protected GameObject[] panels;
    public void ShowPanel()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(true);
        }
        onShow();
    }

    public void HidePanel()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        onHide();
    }
    protected virtual void onShow(){}
    protected virtual void onHide(){}
}
