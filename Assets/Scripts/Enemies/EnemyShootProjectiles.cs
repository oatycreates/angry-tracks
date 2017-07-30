using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootProjectiles : MonoBehaviour {
  /// <summary>
  /// Time to wait between shooting projectiles.
  /// </summary>
  public float shootMaxCooldown = 3.0f;

  /// <summary>
  /// Target to shoot at.
  /// </summary>
  public Transform targetTransform = null;

  /// <summary>
  /// Projectile to shoot at the player.
  /// </summary>
  public GameObject projectilePrefab = null;

  private float m_shootCooldown = 0.0f;

  // Cached component references
  private Transform m_transform = null;

  /// <summary>
  /// Use this for initialization.
  /// </summary>
  void Start () {
    m_transform = GetComponent<Transform>();
    if (!m_transform) {
      Debug.LogError("Could not find Transform");
    }

    if (!targetTransform) {
      Debug.LogError("Target transform not set");
    }

    // Start with a random cooldown so all enemies will have a variable shot time
    m_shootCooldown = shootMaxCooldown + Random.Range(0.0f, shootMaxCooldown);
  }

  /// <summary>
  /// Update is called once per frame
  /// </summary>
  void Update () {
    if (isActiveAndEnabled) {
      if (m_shootCooldown > 0.0f)
      {
        m_shootCooldown -= Time.deltaTime;
      } else {
        ShootProjectile();
      }
    }
  }

  private void ShootProjectile() {
    m_shootCooldown = shootMaxCooldown;

    GameObject spawnedProjectile = GameObject.Instantiate(
      projectilePrefab,
      m_transform.position,
      m_transform.rotation,
      null
    );

    // Give the spawned projectile a target
    HomingMisslie homingScript = spawnedProjectile.GetComponent<HomingMisslie>();
    homingScript.target = targetTransform;
  }
}
