using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Rowan Donaldson
/// Date: 30th June 2017
/// Create list of all gameObjects with Tag 'Enemy' and RotateTowards them.
/// </summary>

public class LookAtClosestEnemy : MonoBehaviour
{
  public List<GameObject> allEnemies = new List<GameObject>();
  //public float turnSpeed = 1.0f;
  public Transform target;

  // Cached component references
  private ClickableNode m_clickableNode = null;
  private Transform m_transform = null;

  void Awake()
  {
    InvokeRepeating("FindAllEnemies", 0, 2.5f);
  }

  void Start()
  {
    m_clickableNode = GetComponentInParent<ClickableNode>();
    if (!m_clickableNode) {
      Debug.LogError("Could not find ClickableNode");
    }
    m_transform = GetComponent<Transform>();
    if (!m_transform) {
      Debug.LogError("Could not find Transform");
    }

    FindAllEnemies();
  }

  void FindAllEnemies()
  {
    //Start by clearing the existing list
    allEnemies.Clear();
    //Now Reassign
    //allEnemies = GameObject.FindGameObjectsWithTag("Enemy") as List<GameObject>;
    allEnemies.AddRange( GameObject.FindGameObjectsWithTag("Enemy"));
  }

  void Update()
  {
    if (isActiveAndEnabled) {
      // Perform node update if active
      if (m_clickableNode.isNodeActive) {
        if (allEnemies.Count > 0)
        {
          GameObject closestGameObject = GetClosest(allEnemies);
          if (closestGameObject) {
            target = closestGameObject.transform;

            m_transform.LookAt( target.position );
          }
        }
      }
    }
  }

  GameObject GetClosest( List<GameObject> enemies)
  {
    GameObject g = null;

    float minDist = Mathf.Infinity;

    foreach (GameObject e in enemies)
    {
      if (e.activeInHierarchy)
      {
        float dist = Vector3.Distance(e.transform.position, m_transform.position);

        if (dist < minDist)
        {
          g = e;
          minDist = dist;
        }
      }
      else
      if (!e.activeInHierarchy)
      {
        g = null;
      }
    }

    return g;

  }
}
