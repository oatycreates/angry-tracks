using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileScript : MonoBehaviour {
  /// <summary>
  /// Heat to add on hit.
  /// </summary>
  public float hitHeatAdd = 1.0f;

  // Cached component references
  private ResourceStore m_resourceStore = null;

  // Use this for initialization
  void Start () {
    m_resourceStore = GameObject.FindObjectOfType<ResourceStore>();
    if (!m_resourceStore) {
      Debug.LogError("Could not find ResourceStore");
    }
  }

  // Update is called once per frame
  void Update () {

  }


  void OnCollisionEnter(Collision collisionInfo)
  {
    Transform other = collisionInfo.transform;
    if (other.CompareTag("Train")) {
      // Add heat to the engine
      m_resourceStore.ChangeResourceValue(EResource.Heat, hitHeatAdd);
    }
  }
}
