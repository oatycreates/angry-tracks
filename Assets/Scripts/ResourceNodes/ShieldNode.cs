using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Consumes: Power
/// Generates: Heat (decrease)
/// </summary>
public class ShieldNode : MonoBehaviour {
  /// <summary>
  /// Game object shield to use.
  /// </summary>
  public GameObject shieldObject = null;

  // Cached component references
  private ResourceStore m_resourceStore = null;
  private ClickableNode m_clickableNode = null;

  //Audio SFX
  private AudioSource optionalAudio;

  void Awake()
  {
    optionalAudio = gameObject.GetComponent<AudioSource>() as AudioSource;
    if (optionalAudio != null)
    {
      optionalAudio.Stop();
    }
  }

  /// <summary>
  /// Use this for initialization.
  /// </summary>
  void Start () {
    m_resourceStore = GameObject.FindObjectOfType<ResourceStore>();
    if (!m_resourceStore) {
      Debug.LogError("Could not find ResourceStore");
    }
    m_clickableNode = GetComponent<ClickableNode>();
    if (!m_clickableNode) {
      Debug.LogError("Could not find Renderer");
    }

    if (!shieldObject) {
      Debug.LogError("shieldObject not set");
    }
    shieldObject.SetActive(false);
  }

  /// <summary>
  /// Update is called once per frame.
  /// </summary>
  void Update () {
    if (isActiveAndEnabled) {
      // Apply node update if active
      if (m_clickableNode.isNodeActive) {
        if (!shieldObject.activeSelf) {
          shieldObject.SetActive(true);
          optionalAudio.Play();
        }
      } else {
        if (shieldObject.activeSelf) {
          shieldObject.SetActive(false);
          optionalAudio.Stop();
        }
      }
    }
  }
}
