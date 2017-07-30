using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// From Spline Tutorial -  Make sure this class is Static
/// </summary>

public static class Bezier //: MonoBehaviour
{
  //The Basic Functionality
  /*
  public static Vector3 GetPoint (Vector3 p0, Vector3 p1, Vector3 p2, float t)
  {

    //Remember to Lerp Twice - so here we'll put a 2 lerps within a lerp
    return Vector3.Lerp( Vector3.Lerp(p0, p1, t), Vector3.Lerp(p1, p2, t), t);
  }


  //Quadratic Bezier Curve

  //For Reference, this kind of curve is a Quadratic Bezier Curve

  //The Linear Curve: 	B(t) = (1 - t) P0 + tP1

  //One Step Deeper: 		B(t) = (1-t)((1-t)P0 + tP1)+ t((1-t)P1 + tP2)

  //More Compact Form: 	B(t) = (1-t)Squared P0 + 2(1-t)tP1 + tSquaredP2

  public static Vector3 GetPoint (Vector3 p0, Vector3 p1, Vector3 p2, float t)
  {
    t = Mathf.Clamp01 (t);
    float oneMinusT = 1f - t;
    return
      oneMinusT * oneMinusT * p0 + 2f * oneMinusT * t * p1 + t * t * p2;
  }

  public static Vector3 GetFirstDerivative( Vector3 p0, Vector3 p1, Vector3 p2, float t)
  {
    return
        2f * (1f - t) * (p1 - p0) +
        2f * t * (p2 - p1);
  }
  */


  //Cubic Curves

  //Reference 		B(t) = (1-t)cubed P0 + 3(1-t)squared tP1 + 3(1-t)tsquaredP2 + tcubedP3
  //More Compact		B'(t) = 3(1-t)squared(P1 - P0) + 6(1-t)t(p2-P1) + 3tsquared(P3 - P2)

  public static Vector3 GetPoint( Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
  {
    t = Mathf.Clamp01(t);
    float oneMinusT = 1f -t;

    return
        oneMinusT * oneMinusT * oneMinusT * p0 +
        3f * oneMinusT * oneMinusT * t * p1 +
        3f * oneMinusT * t * t *p2 +
        t * t * t * p3;
  }

  public static Vector3 GetFirstDerivative( Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
  {
    t = Mathf.Clamp01(t);
    float oneMinusT = 1f - t;

    return
        3f * oneMinusT * oneMinusT * (p1 - p0) +
        6f * oneMinusT * t * (p2 - p1) +
        3f * t * t * (p3 - p2);
  }
}
