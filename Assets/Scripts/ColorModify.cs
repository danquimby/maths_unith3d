using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorModify : MonoBehaviour
{
    [SerializeField] private Image _image;
    private float R = 1;
    private float G = 0;
    [Range(0,1f)]
    [SerializeField] private float value = 1f;

    private float width;
    private Rect rect;
    [SerializeField]private float step = 0f;
    [SerializeField] private float second;
    void Start()
    {
        StartCoroutine(ChangeColor());
        rect = _image.rectTransform.rect;
        width = rect.width;
        step = (width / 100) * 2;
    }

    private IEnumerator ChangeColor()
    {
        float _second = 0;
        while (value > 0)
        {
            value -= 0.02f;
            width -= step;
            
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Update()
    {
        _image.rectTransform.sizeDelta = new Vector2(width, rect.height);
        float rValue = R - value;
        float gValue = G + value;
        //Debug.Log("R: " + rValue + " v:" + value);
        //Debug.Log("G: " + gValue);
        _image.color = new Color(rValue,gValue,0);
    }
}
