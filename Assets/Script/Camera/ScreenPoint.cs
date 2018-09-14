using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenPoint : MonoBehaviour
{
    private Camera mainCamera;//カメラ

    // Use this for initialization
    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    /// <summary>
    /// 画面の左上
    /// </summary>
    /// <returns></returns>
    public Vector3 ScreenTopLeft()
    {
        Vector3 topLeft = mainCamera.ScreenToWorldPoint(Vector3.zero);
        topLeft.Scale(new Vector3(1f, -1f, 1f));
        return topLeft;
    }

    /// <summary>
    /// 画面の右下
    /// </summary>
    /// <returns></returns>
    public Vector3 ScreenBottomRight()
    {
        Vector3 bottomRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        bottomRight.Scale(new Vector3(1f, -1f, 1f));
        return bottomRight;
    }
}
