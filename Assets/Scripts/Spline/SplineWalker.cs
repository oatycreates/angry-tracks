using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Taken from the Spline tutorial on CatLikeCoding
/// </summary>

public enum SplineWalkerMode
{
  Once,
  Loop,
  PingPong
}

public class SplineWalker : MonoBehaviour
{
  public BezierSpline spline;

  public SplineWalkerMode mode;

  public float duration;

  private float progress;
  private bool goingForward = true;

  public bool lookForward;

  private void Update()
  {
    if (goingForward)
    {

      progress += Time.deltaTime / duration;

      if (progress > 1f)
      {
        if (mode == SplineWalkerMode.Once)
        {
          progress = 1f;
        }
        else
        if (mode == SplineWalkerMode.Loop)
        {
          progress -= 1f;
        }
        else
        {
          progress = 2f - progress;
          goingForward = false;
        }
      }
    }
    else
    {
      progress -= Time.deltaTime / duration;

      //For Ping Pong
      if (progress <0f)
      {
        progress = -progress;
        goingForward = true;
      }
    }

    Vector3 position = spline.GetPoint(progress);

    gameObject.transform.localPosition = position;

    if (lookForward)
    {
      gameObject.transform.LookAt(position + spline.GetDirection(progress));
    }
  }
}
