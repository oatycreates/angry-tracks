using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeActivateSphereAnim : MonoBehaviour {
  /// <summary>
  /// Time it should take for the animation to complete.
  /// </summary>
  public float animMaxTime = 1.0f;

  /// <summary>
  /// Scale value at the end of the animation.
  /// </summary>
  public float targetScale = 4.0f;

  /// <summary>
  /// Alpha value at the end of the animation.
  /// </summary>
  public float targetAlpha = 0.0f;

  /// <summary>
  /// How long the animation has run for.
  /// </summary>
  private float m_animRunTime = 0.0f;

  /// <summary>
  /// Scale of the object on Start().
  /// </summary>
  private float m_startingScale = 0.0f;
  /// <summary>
  /// Alpha of the object on Start().
  /// </summary>
  private float m_startingAlpha = 0.0f;

  // Cached component references
  private Transform m_transform = null;
  private Renderer m_renderer = null;

  void Awake() {
    m_transform = GetComponent<Transform>();
    if (!m_transform) {
      Debug.LogError("Could not find Transform");
    }
    m_renderer = GetComponent<Renderer>();
    if (!m_renderer) {
      Debug.LogError("Could not find Renderer");
    }
  }

  /// <summary>
  /// Use this for initialization.
  /// </summary>
  void Start () {
    m_startingScale = m_transform.localScale.magnitude;
    m_startingAlpha = m_renderer.material.color.a;

    ResetNodeActivateSphereAnim();
  }

  /// <summary>
  /// Update is called once per frame.
  /// </summary>
  void Update () {
    if (isActiveAndEnabled) {
      UpdateTick();
    }
  }

  public void ResetNodeActivateSphereAnim () {
    m_animRunTime = 0.0f;

    SetObjectScale(1.0f);
    SetObjectAlpha(1.0f);
  }

  private void UpdateTick() {
    if (m_animRunTime < animMaxTime) {
      m_animRunTime += Time.deltaTime;
      float animProgress = Mathf.Min(m_animRunTime / animMaxTime, 1.0f);
      float newScale = Mathf.Lerp(m_startingScale, targetScale, animProgress);
      float newAlpha = Mathf.Lerp(m_startingAlpha, targetAlpha, animProgress);

      SetObjectScale(newScale);
      SetObjectAlpha(newAlpha);
    } else {
      // Animation is over
      gameObject.SetActive(false);
    }
  }

  private void SetObjectScale(float scale) {
    m_transform.localScale = Vector3.one * scale;
  }

  private void SetObjectAlpha(float alpha) {
    m_renderer.material.color = new Color(
      m_renderer.material.color.r,
      m_renderer.material.color.g,
      m_renderer.material.color.b,
      alpha
    );
  }
}
