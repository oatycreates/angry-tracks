using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Patrick Ferguson
/// Date: 31st July 2017
/// Look at target object with lerp
/// </summary>

public class LookAtLerp : MonoBehaviour
{
  public Transform target;

  /// <summary>
  /// Percentage to lerp the object rotation per second.
  /// </summary>
  public float lerpRate = 5.0f;

  // Cached component references
  private Transform m_transform = null;

  void Start()
  {
    m_transform = GetComponent<Transform>();
    if (!m_transform) {
      Debug.LogError("Could not find Transform");
    }
  }

  void Update()
  {
    if (target != null)
    {
      Quaternion newRot = Quaternion.LookRotation(target.position - m_transform.position, Vector3.up);
      m_transform.rotation =
        Quaternion.Slerp(m_transform.rotation, newRot, lerpRate * Time.deltaTime);
    }
  }

}
