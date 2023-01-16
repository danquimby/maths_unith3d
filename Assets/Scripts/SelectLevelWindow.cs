using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevelWindow : WindowsBase
{
    [SerializeField] private LevelItem[] selectItems;

    protected override void onShow()
    {
        bool previousBlocked = false;
        int previousStars = 0;
        for (var index = 0; index < selectItems.Length; index++)
        {
            LevelItem levelItem = selectItems[index];
            bool blocked = false;
            if (index > 0)
            {
                if (!previousBlocked)
                {
                    // предыдущий был не блокируемым
                    int stars = GameManager.instance.PlayerModel.GetByValue(levelItem.multioplicationValue).stars;
                    previousBlocked = stars == 0;
                    if (index == 1 && previousStars == 0)
                    {
                        blocked = true;
                    }
                }
                else
                {
                    blocked = true;    
                }
            }
            previousStars = GameManager.instance.PlayerModel.GetByValue(levelItem.multioplicationValue).stars;
            // TODO после совещания решили убрать замочки
            levelItem.OnShow(false);
        }
    }

    public void onClickMainMenu()
    {
        GameManager.instance.SetState(WindowsName.SELECT);
    }

}
