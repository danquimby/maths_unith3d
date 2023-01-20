using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevelWindow : WindowsBase
{
    [SerializeField] private LevelItem[] selectItems;

    protected override void onShow()
    {
        foreach (LevelItem item in selectItems)
        {
            item.OnShow(false);
        }
    }

    public void onClickMainMenu()
    {
        GameManager.instance.SetState(WindowsName.SELECT);
    }

}
