using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Consumes: Power
/// Generates: Heat (decrease)
/// </summary>
public class PlugRecepticle : MonoBehaviour {

  // Cached component references
  private ResourceStore m_resourceStore = null;
  private ClickableNode m_clickableNode = null;
  private Transform m_transform = null;

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
    m_clickableNode = GetComponentInParent<ClickableNode>();
    if (!m_clickableNode) {
      Debug.LogError("Could not find ClickableNode");
    }
    m_transform = GetComponent<Transform>();
    if (!m_transform) {
      Debug.LogError("Could not find Transform");
    }
  }

  /// <summary>
  /// Update is called once per frame.
  /// </summary>
  void Update () {
    if (isActiveAndEnabled) {

    }
  }

  void OnTriggerEnter(Collider other)
  {
    if (isActiveAndEnabled) {
      // Check if a plug has entered the trigger zone
      GameObject otherGameObject = other.gameObject;
      if (otherGameObject.CompareTag("Plug")) {
        if (!m_clickableNode.isNodeActive) {
          SetNodeActive(true);

          // Align plug to the slot
          otherGameObject.transform.rotation = m_transform.rotation;
        }
      }
    }
  }

  void OnTriggerStay(Collider other)
  {
    if (isActiveAndEnabled) {
      // Check if a plug has stayed in the trigger zone
      GameObject otherGameObject = other.gameObject;
      if (otherGameObject.CompareTag("Plug")) {
        // Lock the plug in place
        Rigidbody otherRigidbody = otherGameObject.GetComponent<Rigidbody>();
        otherRigidbody.isKinematic = true;
      }
    }
  }

  void OnTriggerExit(Collider other)
  {
    if (isActiveAndEnabled) {
      // Check if a plug has left the trigger zone
      GameObject otherGameObject = other.gameObject;
      if (otherGameObject.CompareTag("Plug")) {
        if (m_clickableNode.isNodeActive) {
          SetNodeActive(false);
        }
      }
    }
  }

  void SetNodeActive(bool isActive) {
    m_clickableNode.SetNodeActive(isActive);

    if (m_clickableNode.isNodeActive) {
      if (optionalAudio != null)
      {
        optionalAudio.Play();
      }
    } else {
      if (optionalAudio != null)
      {
        optionalAudio.Stop();
      }
    }
  }
}
