using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Rowan Donaldson
/// Date: 30th June 2017
/// Homing Missile Rigidbody - recycled from Scourge Game
/// </summary>

public class HomingMisslie : MonoBehaviour
{
  private Rigidbody myRigid;
  public Transform target;
  public float moveSpeed = 5;
  public float turnSpeed = 30;
  public TrailRenderer trail;
  public bool looseLockOnAtCloseRange = false;
  public float closeRange = 5.0f;

  /// <summary>
  /// The missile will be disabled when it hits/touches an entity with these flags.
  /// </summary>
  public List<string> disableOnHitWithFlags = new List<string>();

  /// <summary>
  /// The missile will disable the target entity when it hits one matching these tags.
  /// </summary>
  public List<string> disableOtherOnHitWithFlags = new List<string>();

  public float missileLifetime = 5.0f;

  void Awake()
  {
    myRigid = gameObject.GetComponent<Rigidbody>() as Rigidbody;
    myRigid.useGravity = false;
  }

  public void SetTarget( Transform a_trans)
  {
    if (target == null)
    {
      target = a_trans;
    }
  }

  void OnEnable()
  {
    trail.time = 0.2f;

    Invoke("SetSleep", missileLifetime);
  }

  void OnDisable()
  {
    myRigid.velocity = Vector3.zero;
    myRigid.angularVelocity = Vector3.zero;
    trail.time = 0.0f;
  }

  void Update()
  {
    if (isActiveAndEnabled) {
      if (looseLockOnAtCloseRange && target != null)
      {
        CloseRangeCheck();
      }
    }
  }

  void FixedUpdate()
  {
    //Prevent Angular Velocity
    myRigid.angularVelocity = Vector3.zero;

    if (isActiveAndEnabled) {
      myRigid.velocity = gameObject.transform.forward;

      if (target != null)
      {
        Quaternion targetDirection = Quaternion.LookRotation(target.position - gameObject.transform.position);

        myRigid.MoveRotation( Quaternion.RotateTowards(gameObject.transform.rotation, targetDirection, turnSpeed));
      }

      // Always move forward
      myRigid.AddRelativeForce(Vector3.forward * moveSpeed, ForceMode.VelocityChange);
    }
  }

  void OnCollisionEnter(Collision collisionInfo)
  {
    Transform other = collisionInfo.transform;

    if (disableOtherOnHitWithFlags.Contains(other.tag)) {
      // Disable other
      other.gameObject.SetActive(false);
    }

    if (disableOnHitWithFlags.Contains(other.tag)) {
      // Disable missile
      gameObject.SetActive(false);
    }
  }

  void OnTriggerEnter(Collider other)
  {
    if (disableOtherOnHitWithFlags.Contains(other.tag)) {
      // Queue reactivate if Enemy
      if (other.CompareTag("Enemy")) {
        EnemyReactivateTimer enemyReactivateScript =
          GameObject.FindObjectOfType<EnemyReactivateTimer>();
        enemyReactivateScript.QueueEnemyReactivate(other.gameObject);
      }

      // Disable other
      other.gameObject.SetActive(false);
    }

    if (disableOnHitWithFlags.Contains(other.tag)) {
      gameObject.SetActive(false);
    }
  }

  void CloseRangeCheck()
  {
    float dist = Distance(target.transform.position, gameObject.transform.position);

    if (dist < closeRange)
    {
      target = null;
    }
  }

  public float Distance(Vector3 pointA, Vector3 pointB)
  {
    float d = Vector3.Distance(pointA, pointB);

    return d;
  }

  void SetSleep()
  {
    gameObject.SetActive(false);
  }
}
