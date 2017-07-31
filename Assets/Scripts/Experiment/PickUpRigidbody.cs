using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Rowan Donaldson
/// Date: 30th June 2017
/// Generously inspired by a script on the Unity Answers - Thanks ArchAngelus, MegaMagaretha and InfamousStudios
/// </summary>

public class PickUpRigidbody : MonoBehaviour
{
  public float catchingDistance = 3f;
  private bool m_isDragging = false;
  private Rigidbody m_draggingRigidbody = null;

  // Cached component references
  private Camera m_camera = null;

  void Start() {
    m_camera = GameObject.FindObjectOfType<Camera>();
    if (!m_camera) {
      Debug.LogError("Could not find Camera");
    }
  }

  void Update()
  {
    if (Input.GetButton("Fire1"))
    {
      // Start dragging if not already
      if (!m_isDragging)
      {
        m_draggingRigidbody = GetRigidbodyFromMouseRaycast();

        if (m_draggingRigidbody)
        {
          m_draggingRigidbody.isKinematic = true;
          m_isDragging = true;
        }
      }
    }
    else if (m_isDragging)
    {
      // Stop dragging
      if (m_draggingRigidbody != null)
      {
        m_draggingRigidbody.isKinematic = false;
      }

      m_isDragging = false;
    }

    if (m_isDragging && m_draggingRigidbody != null)
    {
      m_draggingRigidbody.transform.position = CalculateMouse3DVector();
    }
  }

  private Rigidbody GetRigidbodyFromMouseRaycast()
  {
    Rigidbody outRigidbody = null;
    RaycastHit hitInfo = new RaycastHit();

    // Check if the player is pointing at the pickup object with the crosshairs at the centre of the viewport
    if (Physics.Raycast(m_camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f)), out hitInfo, catchingDistance))
    {
      GameObject hitObject = hitInfo.collider.gameObject;
      Rigidbody hitRigidbody = hitObject.GetComponent<Rigidbody>();
      if (hitRigidbody && hitObject.CompareTag("Plug"))
      {
        outRigidbody = hitRigidbody;
      }
    }

    return outRigidbody;
  }

  private Vector3 CalculateMouse3DVector()
  {
    // Determine the position required to keep the object at the crosshairs at the centre of the viewport
    Vector3 v3 = new Vector3(0.5f, 0.5f, 0.0f);
    v3.z = catchingDistance;
    v3 = m_camera.ViewportToWorldPoint(v3);

    return v3;
  }
}
