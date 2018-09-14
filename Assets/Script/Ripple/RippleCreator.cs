using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleCreator : MonoBehaviour
{
    [SerializeField, Header("連続で生成する波紋の数")]
    private int rippleCount;
    [SerializeField, Header("波紋を生成する間隔")]
    private float createTime;
    [SerializeField, Header("波紋オブジェクト")]
    private GameObject ripple;

    private float createCount;//生成時間

    // Use this for initialization
    void Start()
    {
        Instantiate(ripple, transform.position, Quaternion.identity);
        createCount = createTime;
        rippleCount -= 1;
    }

    // Update is called once per frame
    void Update()
    {
        CreateRipple();
    }

    /// <summary>
    /// 波紋生成
    /// </summary>
    private void CreateRipple()
    {
        createCount -= Time.deltaTime;
        if (createTime <= 0.0f)
        {
            Instantiate(ripple, transform.position, Quaternion.identity);
            createCount = createTime;
            rippleCount -= 1;
        }

        if (rippleCount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
