using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrbitFollowTransform : MonoBehaviour {
  /// <summary>
  /// How far away from the target the enemy will try to follow in a circle
  /// </summary>
  public float followRadius = 4.0f;

  /// <summary>
  /// How high above the target the enemy will try to follow.
  /// </summary>
  public float followHeightOffset = 5.0f;

  /// <summary>
  /// Follow lerp rate per second for moving towards the current follow point.
  /// </summary>
  public float followLerpRate = 5.0f;

  /// <summary>
  /// How long the enemy will wait until they pick a new point to
  /// follow around the target.
  /// </summary>
  public float refollowMaxTimer = 6.0f;

  /// <summary>
  /// Time remaining until the enemy finds a new point to follow.
  /// </summary>
  private float m_refollowTimer = 0.0f;

  /// <summary>
  /// Point to aim to follow in local space around the target.
  /// </summary>
  private Vector3 m_targetLocalFollowOffset = Vector3.zero;

  // Cached component references
  private Transform m_followTransform = null;
  private Transform m_transform = null;

  /// <summary>
  /// Use this for initialization.
  /// </summary>
  void Start () {
    m_followTransform = GameObject.FindObjectOfType<TrainNode>().GetComponent<Transform>();
    if (!m_followTransform) {
      Debug.LogError("Could not find TrainNode Transform");
    }
    m_transform = GetComponent<Transform>();
    if (!m_transform) {
      Debug.LogError("Could not find Transform");
    }

    m_refollowTimer = 0;
  }

  /// <summary>
  /// Update is called once per frame.
  /// </summary>
  void Update () {
    if (isActiveAndEnabled) {
      if (m_refollowTimer <= 0.0f) {
        RefollowTarget();
      } else {
        m_refollowTimer -= Time.deltaTime;
      }

      // Lerp towards the target offset relative to the target
      Vector3 targetPosition = m_followTransform.position +
        (m_followTransform.rotation * m_targetLocalFollowOffset);
      m_transform.position = Vector3.Lerp(
        m_transform.position,
        targetPosition,
        followLerpRate * Time.deltaTime
      );

      // Look to face the target position
      m_transform.LookAt(m_followTransform.position);
    }
  }

  private void RefollowTarget() {
    m_refollowTimer = refollowMaxTimer;

    // Pick a random position around the target to follow
    float randomAngle = Random.Range(0.0f, 360.0f * Mathf.Deg2Rad);
    m_targetLocalFollowOffset = new Vector3(
      Mathf.Cos(randomAngle) * followRadius,
      followHeightOffset,
      Mathf.Sin(randomAngle) * followRadius
    );
  }
}
