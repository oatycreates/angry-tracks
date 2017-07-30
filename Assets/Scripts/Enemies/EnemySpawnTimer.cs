using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTimer : MonoBehaviour {
  public float enemyMaxSpawnTime = 10.0f;

  public float maxSpawnRadius = 50.0f;

  public GameObject enemyPrefab = null;

  /// <summary>
  /// Target for the enemies to shoot at.
  /// </summary>
  public Transform targetTransform = null;

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

    InvokeRepeating("SpawnNewEnemy", enemyMaxSpawnTime, enemyMaxSpawnTime);
  }

  /// <summary>
  /// Update is called once per frame.
  /// </summary>
  void Update () {

  }

  private void SpawnNewEnemy() {
    float randomAngle = Random.Range(0, 360 * Mathf.Deg2Rad);
    float spawnRadius = Random.Range(0, maxSpawnRadius);
    Vector3 posOffset = new Vector3(
      Mathf.Cos(randomAngle) * spawnRadius,
      1.5f,
      Mathf.Sin(randomAngle) * spawnRadius
    );

    GameObject spawnedEnemy =
      GameObject.Instantiate(enemyPrefab, m_transform.position + posOffset, m_transform.rotation);
    EnemyShootProjectiles enemyProjectilesScript =
      spawnedEnemy.GetComponent<EnemyShootProjectiles>();
    enemyProjectilesScript.targetTransform = targetTransform;
  }
}
