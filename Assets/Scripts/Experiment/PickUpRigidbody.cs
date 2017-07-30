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

    if (Physics.Raycast(m_camera.ScreenPointToRay(Input.mousePosition), out hitInfo, catchingDistance))
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
    Vector3 v3 = Input.mousePosition;
    v3.z = catchingDistance;
    v3 = m_camera.ScreenToWorldPoint(v3);

    return v3;
  }
}
