using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class Ripple : MonoBehaviour
{
    [SerializeField, Header("波紋オブジェクト")]
    private GameObject ripple;

    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    private Collision col;
    private ScreenPoint screenPoint;
    private float destroyTime;
    private float startTime;
    private List<Vector2> boundPoints;
    private List<GameObject> boundPointObjects;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        startTime = Time.timeSinceLevelLoad;
        col = new Collision();
        screenPoint = FindObjectOfType<ScreenPoint>();
        boundPoints = new List<Vector2>();
        boundPointObjects = new List<GameObject>();
    }

    // Use this for initialization
    void Start()
    {
        spriteRenderer.material.SetFloat("_StartTime", Time.time);

        float animationTime = spriteRenderer.material.GetFloat("_AnimationTime");
        destroyTime = animationTime;
        destroyTime -= spriteRenderer.material.GetFloat("_StartWidth") * animationTime;
        destroyTime += spriteRenderer.material.GetFloat("_Width") * animationTime;
        Destroy(transform.gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        float diff = Time.timeSinceLevelLoad - startTime;
        float rate = diff / destroyTime;

        float colliderRadius = circleCollider.radius;
        colliderRadius = Mathf.Lerp(0.0f, 0.5f, rate);
        circleCollider.radius = colliderRadius;

        Rebound(rate);
    }

    /// <summary>
    /// 波紋が跳ね返る
    /// </summary>
    private void Rebound(float rate)
    {
        Vector2 screen_topLeft = screenPoint.ScreenTopLeft();
        Vector2 screen_bottomRight = screenPoint.ScreenBottomRight();
        Vector2 screen_topRight = new Vector2(screen_bottomRight.x, screen_topLeft.y);
        Vector2 screen_bottomLeft = new Vector2(screen_topLeft.x, screen_bottomRight.y);

        float radius = transform.localScale.x * circleCollider.radius;

        if (col.IsCollisionCircle(screen_topLeft, screen_topRight, transform.position, radius)
            && transform.position.y <= screen_topLeft.y)
        {
            AddB_Point(col.PointX);
        }
        if (col.IsCollisionCircle(screen_topRight, screen_bottomRight, transform.position, radius)
            && transform.position.x <= screen_bottomRight.x)
        {
            AddB_Point(col.PointX);
        }
        if (col.IsCollisionCircle(screen_bottomRight, screen_bottomLeft, transform.position, radius)
            && transform.position.y >= screen_bottomRight.y)
        {
            AddB_Point(col.PointX);
        }
        if (col.IsCollisionCircle(screen_bottomLeft, screen_topLeft, transform.position, radius)
            && transform.position.x >= screen_topLeft.x)
        {
            AddB_Point(col.PointX);
        }

        if (boundPoints.Count > 0)
        {
            foreach (var b in boundPoints)
            {
                CreateBoundRipple(b, rate);
            }
        }
    }

    /// <summary>
    /// 跳ね返る座標をリストに追加
    /// </summary>
    private void AddB_Point(Vector2 boundPoint)
    {
        if (boundPoints.Count <= 0)
        {
            boundPoints.Add(boundPoint);
            return;
        }

        foreach (var b in boundPoints)
        {
            if (b == boundPoint)
            {
                return;
            }
        }

        boundPoints.Add(boundPoint);
    }

    /// <summary>
    /// 跳ね返る波紋生成
    /// </summary>
    /// <param name="boundPoint"></param>
    private void CreateBoundRipple(Vector2 boundPoint, float rate)
    {
        bool isCreate = true;
        foreach (var b_obj in boundPointObjects)
        {
            if ((Vector2)b_obj.transform.position == boundPoint)
            {
                isCreate = false;
                break;
            }
        }

        if (!isCreate) return;

        GameObject boundPointObj = new GameObject();
        boundPointObj.name = "BoundPoint";
        boundPointObj.transform.position = boundPoint;
        boundPointObj.AddComponent<BoundPoint>();
        boundPointObjects.Add(boundPointObj);

        GameObject boundRipple = Instantiate(ripple, boundPointObj.transform);
        boundRipple.transform.position = transform.position;

        SpriteRenderer rippleSprite = boundRipple.GetComponent<SpriteRenderer>();
        rippleSprite.material.SetFloat("_StartWidth", rate);

        boundPointObj.transform.eulerAngles = new Vector3(0, 0, 180);
    }
}
