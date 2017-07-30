using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Node may be clicked on to enable/disable it.
/// </summary>
public class ClickableNode : MonoBehaviour {
  /// <summary>
  /// How far away the player can be for the node click to register.
  /// </summary>
  public float clickDistanceThreshold = 5.0f;

  /// <summary>
  /// Game object to spawn on node click.
  /// </summary>
  public GameObject clickSpawnObject = null;

  /// <summary>
  /// Whether the node should be considered 'Active'.
  /// </summary>
  private bool m_isNodeActive = false;
  public bool isNodeActive
  {
    get { return m_isNodeActive; }
  }

  private GameObject clickObject = null;
  private NodeActivateSphereAnim clickObjectAnimScript = null;

  // Cached component references
  private Camera m_camera = null;
  private Transform m_transform = null;
  private Collider m_collider = null;

  /// <summary>
  /// Use this for initialization.
  /// </summary>
  void Start () {
    m_camera = GameObject.FindObjectOfType<Camera>();
    if (!m_camera) {
      Debug.LogError("Could not find Camera");
    }
    m_transform = GetComponent<Transform>();
    if (!m_transform) {
      Debug.LogError("Could not find Transform");
    }
    m_collider = GetComponent<Collider>();
    if (!m_collider) {
      Debug.LogError("Could not find Collider");
    }

    // Spawn the click effect
    clickObject = GameObject.Instantiate(clickSpawnObject, m_transform.position, m_transform.rotation, m_transform);
    clickObjectAnimScript = clickObject.GetComponent<NodeActivateSphereAnim>();
    clickObject.SetActive(false);
  }

  /// <summary>
  /// Update is called once per frame.
  /// </summary>
  void Update () {
    if (isActiveAndEnabled) {
      // // See if the player clicked on this node
      // if (Input.GetButtonUp("Fire1")) {
      //   Vector3 camPos = m_camera.transform.position;
      //   Ray clickRay = new Ray(m_camera.transform.position, m_camera.transform.rotation * Vector3.forward);
      //   RaycastHit[] rayHits = Physics.RaycastAll(clickRay, 100.0f);
      //   foreach (RaycastHit rayHit in rayHits)
      //   {
      //     if (rayHit.distance <= clickDistanceThreshold &&
      //         rayHit.collider == m_collider) {
      //       SetNodeActive(true);
      //       break;
      //     }
      //   }
      // }
    }
  }

  /// <summary>
  /// Marks this node as 'Active'. Will turn off all other nodes.
  /// </summary>
  /// <param name="isActive">Whether to make the node active or not.</param>
  public void SetNodeActive (bool isActive) {
    m_isNodeActive = isActive;
    if (m_isNodeActive) {
      // Start the click object
      clickObject.SetActive(true);
      clickObjectAnimScript.ResetNodeActivateSphereAnim();
    }
  }
}
