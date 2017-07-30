using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Consumes: Power
/// Generates: Speed, Heat
/// </summary>
public class EngineNode : MonoBehaviour {
  /// <summary>
  /// How much Speed is added per second when the engine is running.
  /// </summary>
  public float engineSpeedRate = 0.1f;

  /// <summary>
  /// How much Heat is added per second when the engine is running.
  /// </summary>
  public float engineHeatRate = 0.2f;

  /// <summary>
  /// How much Heat is added per second when the engine is overheated.
  /// </summary>
  public float engineOverheatedHeatRate = 0.3f;

  /// <summary>
  /// How hot the engine can get before being 'Hot'.
  /// </summary>
  public float engineHeatThreshold = 8.0f;

  /// <summary>
  /// The engine must fall below this heat value to stop overheating.
  /// </summary>
  public float engineHeatCooldownThreshold = 4.0f;

  /// <summary>
  /// Particles to show when the engine has overheated.
  /// </summary>
  public ParticleSystem overheatParticles = null;

  /// <summary>
  /// Whether the engine is presently overheated.
  /// </summary>
  private bool m_isOverheated = false;

  /// <summary>
  /// Whether the engine was active last tick.
  /// </summary>
  private bool m_wasActive = false;

  // Cached component references
  private ResourceStore m_resourceStore = null;
  private TrainNode m_trainNode = null;
  private Renderer m_renderer = null;
  private Animator m_animator = null;
  private ClickableNode m_clickableNode = null;

  //Audio SFX
  public AudioSource engineAudio;
  public AudioSource jammedAudio;

  /// <summary>
  /// Use this for initialization.
  /// </summary>
  void Start () {
    m_resourceStore = GameObject.FindObjectOfType<ResourceStore>();
    if (!m_resourceStore) {
      Debug.LogError("Could not find ResourceStore");
    }
    m_trainNode = GameObject.FindObjectOfType<TrainNode>();
    if (!m_trainNode) {
      Debug.LogError("Could not find TrainNode");
    }
    m_renderer = GetComponent<Renderer>();
    if (!m_renderer) {
      Debug.LogError("Could not find Renderer");
    }
    m_animator = GetComponent<Animator>();
    if (!m_animator) {
      Debug.LogError("Could not find Animator");
    }
    m_clickableNode = GetComponent<ClickableNode>();
    if (!m_clickableNode) {
      Debug.LogError("Could not find ClickableNode");
    }

    if (!overheatParticles) {
      Debug.LogError("Could not find ParticleSystem overheatParticles");
    }
    overheatParticles.Stop();
  }

  /// <summary>
  /// Update is called once per frame.
  /// </summary>
  void Update () {
    if (isActiveAndEnabled) {
      // Make engine red while hot
      float heatValue = m_resourceStore.GetResourceValue(EResource.Heat);
      float engineHotPercent = Mathf.Min(heatValue / engineHeatThreshold, 1.0f);
      m_renderer.material.color = new Color(engineHotPercent, 1.0f - engineHotPercent, 1.0f - engineHotPercent);

      if (m_isOverheated || !m_clickableNode.isNodeActive) {
        // Stop the engine cylinder animation while overheated or not active
        m_animator.SetFloat("EngineCylinderSpeed", 0.0f);
      }

      if (m_clickableNode.isNodeActive) {
        if (!engineAudio.isPlaying) {
          engineAudio.Play();
        }
      } else {
        if (engineAudio.isPlaying) {
          engineAudio.Stop();
        }
      }

      if (m_isOverheated) {
        //Audio
        if (!jammedAudio.isPlaying) {
          jammedAudio.Play();
        }
        engineAudio.pitch = Mathf.MoveTowards(engineAudio.pitch, 0.5f, 0.05f);

        // Make the player fully cool down the engine before they can use it again
        if (m_resourceStore.GetResourceValue(EResource.Heat) <= engineHeatCooldownThreshold) {
          m_isOverheated = false;

          // Stop the overheat particles if cooled enough
          if (overheatParticles.isPlaying) {
            overheatParticles.Stop();
          }
        } else {
          if (m_resourceStore.GetResourceValue(EResource.Heat) < engineHeatThreshold) {
            // Passively add some heat to slow down the coolant when overheated
            m_resourceStore.ChangeResourceValue(EResource.Heat, engineOverheatedHeatRate * Time.deltaTime);
          }
        }
      } else {
        // Overheat the engine if it is too hot
        if (m_resourceStore.GetResourceValue(EResource.Heat) >= engineHeatThreshold) {
          OverheatEngine();
        }

        // Audio
        if (jammedAudio.isPlaying) {
          jammedAudio.Stop();
        }
        engineAudio.pitch = Mathf.MoveTowards(engineAudio.pitch, 1.0f, 0.05f);

        // Apply node update if active
        if (m_clickableNode.isNodeActive) {
          // Animate the engine with the speed of the train
          float speedAnimMult = m_resourceStore.GetResourceValue(EResource.Speed) / 10.0f;
          speedAnimMult = Mathf.Clamp(speedAnimMult, 0.0f, 5.0f);
          m_animator.SetFloat("EngineCylinderSpeed", speedAnimMult);

          m_resourceStore.ChangeResourceValue(EResource.Speed, engineSpeedRate * Time.deltaTime);
          m_resourceStore.ChangeResourceValue(EResource.Heat, engineHeatRate * Time.deltaTime);

          // Tell the train node to stop reducing speed for a bit to give the player some grace time
          m_trainNode.SetSpeedDecreaseGraceTimer();
        }
      }

      m_wasActive = m_clickableNode.isNodeActive;
    }
  }

  /// <summary>
  /// Prevents the Player from using the engine for a short time if it his a Heat threshold.
  /// </summary>
  private void OverheatEngine() {
    overheatParticles.Play();
    m_isOverheated = true;
  }
}
