using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PlayTouchPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,IDragHandler
{
    [SerializeField,Header("波紋生成オブジェクト")]
    private GameObject rippleCreator;

    /// <summary>
    /// マウスクリック時の処理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        SpawnRippleCreator(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    /// <summary>
    /// 波紋生成
    /// </summary>
    /// <param name="pos"></param>
    void SpawnRippleCreator(Vector2 pos)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(pos);
        Instantiate(rippleCreator, worldPos, Quaternion.identity);
    }
}
