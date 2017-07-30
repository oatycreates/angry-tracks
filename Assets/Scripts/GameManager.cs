using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
  // Cached component references
  private ResourceStore m_resourceStore = null;
  private ValueToGameOver m_valueToGameOver = null;

  /// <summary>
  /// Use this for initialization.
  /// </summary>
  void Start () {
    m_resourceStore = GameObject.FindObjectOfType<ResourceStore>();
    if (!m_resourceStore) {
      Debug.LogError("Could not find ResourceStore");
    }
    m_valueToGameOver = GameObject.FindObjectOfType<ValueToGameOver>();
    if (!m_valueToGameOver) {
      Debug.LogError("Could not find ValueToGameOver");
    }
  }

  /// <summary>
  /// Update is called once per frame.
  /// </summary>
  void Update () {
    if (isActiveAndEnabled) {
      // Running out of speed is game over
      m_valueToGameOver.monitorValue = m_resourceStore.GetResourceValue(EResource.Speed);
    }
  }
}
