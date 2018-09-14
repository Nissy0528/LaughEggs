using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetSliderValue : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField, Header("値を設定するInputField")]
    private InputField inputField;

    private Slider slider;//スライダー
    private bool isValueChange;//スライダーの値が変わるか

    // Use this for initialization
    void Start()
    {
        slider = GetComponent<Slider>();
        isValueChange = true;
    }

    void Update()
    {
        if (!isValueChange) return;

        float s_value = slider.value;
        if (!float.TryParse(inputField.text, out s_value))
        {
            if (inputField.text != "")
            {
                inputField.text = "0";
            }
            else
            {
                s_value = 0;
            }
        }
        slider.value = s_value;
    }

    /// <summary>
    /// マウスドラッグ中にInputFieldの値を変更
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        //Sliderの値をInputFieldに表示（小数第1まで）
        inputField.text = slider.value.ToString("f1");
        isValueChange = false;
    }

    /// <summary>
    /// マウスドラッグ終了時
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        isValueChange = true;
    }
}
