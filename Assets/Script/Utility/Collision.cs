using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision
{
    private Vector2 pointX;//円と線の接点

    /// <summary>
    /// 円と線のあたり判定
    /// </summary>
    /// <param name="pointA">線分の端A</param>
    /// <param name="pointB">線分の端B</param>
    /// <param name="pointP">円の中点</param>
    /// <param name="radius">円の半径</param>
    /// <returns></returns>
    public bool IsCollisionCircle(Vector2 pointA, Vector2 pointB, Vector2 pointP, float radius)
    {
        Vector2 vecAB = new Vector2(pointB.x - pointA.x, pointB.y - pointA.y);
        Vector2 vecAP = new Vector2(pointP.x - pointA.x, pointP.y - pointA.y);

        Vector2 normalAB = vecAB.normalized;

        float lenAX = Vector2.Dot(normalAB, vecAP);

        float shortesDistance;
        if (lenAX < 0)
        {
            shortesDistance = Vector2.Distance(pointA, pointP);
        }
        else if (lenAX > Vector2.Distance(pointA, pointB))
        {
            shortesDistance = Vector2.Distance(pointB, pointP);
        }
        else
        {
            shortesDistance = Mathf.Abs(Vector2Cross(normalAB, vecAP));
        }

        pointX = new Vector2(pointA.x + (normalAB.x * lenAX),
                             pointA.y + (normalAB.y * lenAX));

        bool hit = false;
        if (shortesDistance < radius)
        {
            hit = true;
        }
        return hit;
    }

    /// <summary>
    /// ベクトル同士の外積
    /// </summary>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    private float Vector2Cross(Vector2 lhs, Vector2 rhs)
    {
        return lhs.x * rhs.y - rhs.x * lhs.y;
    }

    /// <summary>
    /// 円と線の接点
    /// </summary>
    public Vector2 PointX
    {
        get { return pointX; }
    }
}
