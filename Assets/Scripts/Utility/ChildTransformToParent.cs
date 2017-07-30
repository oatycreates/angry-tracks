using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Attaches this game object to the input parent transform.
/// </summary>
public class ChildTransformToParent : MonoBehaviour {
  public float maxHitDistance = 5f;

  /// <summary>
  /// Where to place the child relative to the parent on initialisation.
  /// This helps ensure a clean attachment.
  /// </summary>
  public Vector3 startAttachOffset = new Vector3(0, 2.0f, -0.82f);

  public Transform newParentTransform = null;

  // Cached component references
  private Transform m_transform = null;

  //OptionalAudio
  public AudioSource secondarySource;

  void Awake()
  {
  	if (secondarySource != null)
  	{
  		secondarySource.Stop();
  	}
  }

  /// <summary>
  /// Use this for initialization.
  /// </summary>
  void Start () {
    m_transform = GetComponent<Transform>();
    if (!m_transform) {
      Debug.LogError("Could not find Transform");
    }

    if (!newParentTransform) {
      Debug.LogError("newParentTransform not set! " + gameObject.name + " can't be attached.");
    }

    AttachToParent(false);
    // Start in the middle of the parent
    m_transform.localPosition = startAttachOffset;
  }

  /// <summary>
  /// Update is called once per frame.
  /// </summary>
  void Update ()
  {
    if (isActiveAndEnabled) {
      // Check whether the player has fallen out of their parent
      RaycastHit[] rayHits = Physics.RaycastAll(m_transform.position, Vector3.down, maxHitDistance);
      if (transform.parent)
      {
        // Check for falling off the train, static objects will mostly be the level
        bool onlyHitStatic = rayHits.All((hit) => {
          return hit.collider.gameObject.isStatic == true;
        });
        if (onlyHitStatic)
		{
							    //Optional Audio
							    if (secondarySource != null)
							    {
							    	secondarySource.PlayOneShot( secondarySource.clip );
							    }
          Unparent();
        }
      }
      else
      {
        // Check for reattaching the player if they are back on the train
        bool rayHitTrain = rayHits.Any((hit) => {
          return hit.collider.gameObject.CompareTag("Train");
        });
        if (rayHitTrain)
        {
          AttachToParent(true);
        }
      }
    }
  }

  void Unparent()
  {
    m_transform.SetParent(null, true);
  }

  void AttachToParent(bool worldPositionStays)
  {
    m_transform.SetParent(newParentTransform, worldPositionStays);
  }
}
