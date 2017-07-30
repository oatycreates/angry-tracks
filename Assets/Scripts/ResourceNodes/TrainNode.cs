using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Consumes: Speed
/// Generates: Motion
/// </summary>
public class TrainNode : MonoBehaviour {
  /// <summary>
  /// How quickly the Speed resource will be decreased.
  /// </summary>
  public float speedConsumeRate = -0.2f;

  /// <summary>
  /// How much each unit of 'Speed' converts into motion speed.
  /// </summary>
  public float speedToMotionMult = 1.0f;

  /// <summary>
  /// Estimate for how long the spline is as this may be difficult to calculate.
  /// </summary>
  public float splineLength = 100.0f;

  /// <summary>
  /// Time to wait before starting the speed decrease after the engine has been powered.
  /// </summary>
  public float speedDecreaseMaxGraceTime = 5.0f;

  /// <summary>
  /// Current time to wait before the speed can decrease again.
  /// </summary>
  private float m_speedDecreaseGraceTimer = 0.0f;

  // Cached component references
  private ResourceStore m_resourceStore = null;
  private SplineWalker m_splineWalker = null;

  /// <summary>
  /// Use this for initialization.
  /// </summary>
  void Start () {
    m_resourceStore = GameObject.FindObjectOfType<ResourceStore>();
    if (!m_resourceStore) {
      Debug.LogError("Could not find ResourceStore");
    }
    m_splineWalker = GameObject.FindObjectOfType<SplineWalker>();
    if (!m_splineWalker) {
      Debug.LogError("Could not find SplineWalker");
    }

    // Start with a small amount of speed
    m_resourceStore.SetResourceValue(EResource.Speed, Mathf.Abs(speedConsumeRate) * 10.0f);
  }

  /// <summary>
  /// Update is called once per frame.
  /// </summary>
  void Update () {
    if (isActiveAndEnabled) {
      float currentSpeed = m_resourceStore.GetResourceValue(EResource.Speed);
      if (m_speedDecreaseGraceTimer <= 0.0f) {
        if (currentSpeed > 0.0f) {
          // Consume Speed and use it to propel the train
          m_resourceStore.ChangeResourceValue(EResource.Speed, speedConsumeRate * Time.deltaTime);
          float speedAfter = m_resourceStore.GetResourceValue(EResource.Speed);
          float consumedSpeed = currentSpeed - speedAfter;
          float newSpeed = currentSpeed + (consumedSpeed * speedToMotionMult);
          UpdateTrainSpeed(newSpeed);
        }
      } else {
        m_speedDecreaseGraceTimer -= Time.deltaTime;
        // Update the speed to reflect the speed resource value
        UpdateTrainSpeed(currentSpeed);
      }
    }
  }

  /// <summary>
  /// Updates the train's spline walker speed.
  /// </summary>
  /// <param name="newSpeed">How much speed has been consumed this tick.</param>
  private void UpdateTrainSpeed(float newSpeed) {
    // s = d / t
    // t = d / s
    // t = splineLength / s
    m_splineWalker.duration = splineLength / newSpeed;
    m_splineWalker.duration = Mathf.Max(m_splineWalker.duration, 0.0f);
  }

  /// <summary>
  /// Tops up the speed decrease grace timer.
  /// </summary>
  public void SetSpeedDecreaseGraceTimer()
  {
    m_speedDecreaseGraceTimer = speedDecreaseMaxGraceTime;
  }
}
