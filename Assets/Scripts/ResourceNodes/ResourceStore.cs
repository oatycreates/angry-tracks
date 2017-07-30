using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Types of resources.
/// </summary>
public enum EResource {
  Power,
  Speed,
  Heat
}

public class ResourceStore : MonoBehaviour {
  /// <summary>
  /// Current value of each resource type. Key is the type, value is the resource value.
  /// </summary>
  private Dictionary<EResource, float> resourceValues = new Dictionary<EResource, float>();

  /// <summary>
  /// Whether the component should be presently powered. This should be set to
  /// zero in the Update tick if the Power resource is zero.
  /// </summary>
  public bool isPowered = true;

  /// <summary>
  /// Maximum allowed speed resource.
  /// </summary>
  public float maxSpeed = 10.0f;
  public float readSpeed;

  /// <summary>
  /// Maximum allowed speed resource.
  /// </summary>
  public float maxHeat = 8.0f;
  public float readHeat;

  /// <summary>
  /// Use this for initialization.
  /// </summary>
  void Start () {

  }

  /// <summary>
  /// Update is called once per frame.
  /// </summary>
  void Update () {
    if (isActiveAndEnabled) {
      // Cap maximum Speed
      float currentSpeed = GetResourceValue(EResource.Speed);
      readSpeed = currentSpeed;
      if (currentSpeed > maxSpeed) {
        SetResourceValue(EResource.Speed, maxSpeed);
      }

      // Cap maximum Heat
      float currentHeat = GetResourceValue(EResource.Heat);
      readHeat = currentHeat;
      if (currentHeat > maxHeat) {
        SetResourceValue(EResource.Heat, maxHeat);
      }
    }
  }

  // void OnGUI()
  // {
  //   if (isActiveAndEnabled) {
  //     // Log current resource state
  //     string logMessage = "Values -> ";
  //     foreach (var resourceKvp in resourceValues)
  //     {
  //       logMessage += resourceKvp.Key.ToString() + ": " + resourceKvp.Value.ToString() + ", ";
  //     }

  //     GUI.color = Color.white;
  //     GUI.TextArea(new Rect(10, 10, 400, 22), logMessage);
  //   }
  // }

  /// <summary>
  /// Returns the current value for a particular resource.
  /// </summary>
  /// <param name="resourceType">Resource type to retrieve the value for.</param>
  /// <returns>
  /// Current value of that resource type on this node.
  /// Returns 0.0 if the resource type isn't found.
  /// </returns>
  public float GetResourceValue(EResource resourceType) {
    // Return 0.0 if the resource type isn't found
    float outValue = 0.0f;
    resourceValues.TryGetValue(resourceType, out outValue);
    return outValue;
  }

  /// <summary>
  /// Modifies the input resource type by the input value.
  /// </summary>
  /// <param name="resourceType">Resource type to modify.</param>
  /// <param name="amount">Amount to change the resource's value by, will be lower bounded to zero.</param>
  public void ChangeResourceValue(EResource resourceType, float amount) {
    SetResourceValue(resourceType, GetResourceValue(resourceType) + amount);
  }

  /// <summary>
  /// Sets the input resource type to the input value.
  /// </summary>
  /// <param name="resourceType">Resource type to modify.</param>
  /// <param name="newValue">Amount to set the resource's value to, will be lower bounded to zero.</param>
  public void SetResourceValue(EResource resourceType, float newValue) {
    if (resourceValues.ContainsKey(resourceType)) {
      resourceValues[resourceType] = Mathf.Max(newValue, 0.0f);
    } else {
      // Resource type not found, add the resource.
      resourceValues.Add(resourceType, Mathf.Max(newValue, 0.0f));
    }
  }
}
