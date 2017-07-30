using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReactivateTimer : MonoBehaviour {
  /// <summary>
  /// How long until enemies respawn.
  /// </summary>
  public float enemyReactivateMaxTime = 5.0f;

  struct EnemyReactivate {
    public EnemyReactivate(GameObject enemyObject, float reactivateTimer) {
      this.enemyObject = enemyObject;
      this.reactivateTimer = reactivateTimer;
    }

    public GameObject enemyObject;
    public float reactivateTimer;
  }

  List<EnemyReactivate> enemyReactivateList = new List<EnemyReactivate>();

  private bool m_isReactivating = false;

  /// <summary>
  /// Use this for initialization.
  /// </summary>
  void Start () {

  }

  /// <summary>
  /// Update is called once per frame.
  /// </summary>
  void Update () {
    for (int i = 0; i < enemyReactivateList.Count;) {
      if (enemyReactivateList[i].reactivateTimer > 0.0f) {
        EnemyReactivate updatedEnemyReactivate = enemyReactivateList[i];
        updatedEnemyReactivate.reactivateTimer -= Time.deltaTime;
        enemyReactivateList[i] = updatedEnemyReactivate;

        // Next element
        ++i;
      } else {
        // Reactivate the object
        enemyReactivateList[i].enemyObject.SetActive(true);
        // Will mean 'i' is pointing to the next object
        enemyReactivateList.RemoveAt(i);
      }
    }
  }

  public void QueueEnemyReactivate(GameObject enemyObject)
  {
    // Queue reactivating the enemy
    enemyReactivateList.Add(new EnemyReactivate(enemyObject, enemyReactivateMaxTime));
  }
}
