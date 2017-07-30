using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// From Spline Tutorial
/// </summary>
public class BezierCurve : MonoBehaviour
{
  public Vector3[] points;

  public void Reset()
  {
    points = new Vector3[]
        {
          new Vector3 (1f, 0f, 0f),
          new Vector3 (2f, 0f, 0f),
          new Vector3 (3f, 0f, 0f),
          new Vector3 (4f, 0f, 0f)
        };
  }

  //Get the point, and transform it to World Space
  public Vector3 GetPoint (float t)
  {
    return transform.TransformPoint( Bezier.GetPoint( points[0], points[1], points[2], points[3], t));
  }

  //The Speed at this point on the curve
  public Vector3 GetVelocity (float t)
  {
    //Use this in conjunction with the Bezier.GetFirstDerivate to produce a tanget along the curve
    return transform.TransformPoint(Bezier.GetFirstDerivative(points[0], points[1], points[2], points[3], t)) - transform.position;
  }

  public Vector3 GetDirection (float t)
  {
    return GetVelocity(t).normalized;
  }
}
