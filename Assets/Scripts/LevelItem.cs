using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelItem : MonoBehaviour
{
    [SerializeField] public int multioplicationValue;
    [SerializeField] private Image[] im_stars;
    [SerializeField] private TextMeshProUGUI textValue;
    [SerializeField] private Image lockImage;
    [SerializeField] private Button _button;

    public void OnShow(bool blocked)
    {
        int stars = GameManager.instance.PlayerModel.GetByValue(multioplicationValue).stars;
        foreach (Image star in im_stars)
        {
            star.sprite = GameManager.instance.starsImage[stars == 0 ? 0 : 1];
            if (stars>0)
                stars--;
        }
        textValue.text = multioplicationValue.ToString();

        _button.enabled = !blocked;
        lockImage.enabled = blocked;
    }

    public void onClick()
    {
        GameManager.instance.MultiplicationValue = multioplicationValue;
        GameManager.instance.SetNextState();
    }
    
}
