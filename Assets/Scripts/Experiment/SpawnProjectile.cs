using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Rowan Donaldson
/// Date: 30th June 2017
/// Spawn a Projectile of Prefab + Pass Some Values into it
/// </summary>

public class SpawnProjectile : MonoBehaviour
{
  public GameObject prefabType;
  public int noOfPooledObjects;
  private List<GameObject> pool = new List<GameObject>();
  public LookAtClosestEnemy turret;

  /// <summary>
  /// Time to wait between shots.
  /// </summary>
  public float spawnMaxCooldown = 0.25f;

  /// <summary>
  /// Time until the next allowed shot.
  /// </summary>
  private float m_spawnCooldown = 0.0f;

  // Cached component references
  private ClickableNode m_clickableNode = null;
  private Transform m_transform = null;

  void Awake()
  {
    m_transform = GetComponent<Transform>();
    if (!m_transform) {
      Debug.LogError("Could not find Transfrom");
    }

    for (int i = 0; i < noOfPooledObjects; i++)
    {
      GameObject g = GameObject.Instantiate( prefabType, m_transform.position, Quaternion.identity);
      g.SetActive(false);
      pool.Add(g);
    }
  }

  void Start()
  {
    m_clickableNode = GetComponent<ClickableNode>();
    if (!m_clickableNode) {
      Debug.LogError("Could not find ClickableNode");
    }
  }

  void Update()
  {
    if (isActiveAndEnabled) {
      // Apply node update if active
      if (m_clickableNode.isNodeActive) {
        if (m_spawnCooldown > 0) {
          m_spawnCooldown -= Time.deltaTime;
        } else {
          Spawn();
        }
      }
    }
  }

  public void Spawn()
  {
    m_spawnCooldown = spawnMaxCooldown;

    for (int i = 0; i < pool.Count; i++)
    {
      if (!pool[i].activeInHierarchy)
      {
        pool[i].transform.position = m_transform.position;
        pool[i].transform.rotation = m_transform.rotation;
        pool[i].SetActive(true);

        HomingMisslie homingMissileScript =
          pool[i].gameObject.GetComponent<HomingMisslie>();
        if (homingMissileScript)
        {
          homingMissileScript.SetTarget(turret.target);
        }
        break;
      }
    }
  }
}
