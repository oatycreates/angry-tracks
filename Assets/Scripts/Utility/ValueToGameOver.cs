using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// Author: Rowan Donaldson
/// Date: 29th June 2017
/// Monitor a Resource Value - when it reaches zero, change scene (to game over)
/// </summary>

public class ValueToGameOver : MonoBehaviour
{
  public string nextScene;

  public float monitorValue = 9999.0f;

  /// <summary>
  /// Time that the monitor value must be at zero before triggering the game over.
  /// </summary>
  public float monitorValueZeroMaxTime = 4.0f;

  /// <summary>
  /// Current time until the monitor value has been at zero for long enough to
  /// trigger 'Game over'.
  /// </summary>
  private float m_monitorValueZeroTimer = 0.0f;

  // Cached component references
  private FirstPersonController m_firstPersonController = null;

  void Start()
  {
    m_firstPersonController = GameObject.FindObjectOfType<FirstPersonController>();
    if (!m_firstPersonController) {
      Debug.LogError("Could not find FirstPersonController");
    }
  }

  void Update()
  {
    if (isActiveAndEnabled) {
      if (monitorValue <= 0.0f)
      {
        if (m_monitorValueZeroTimer <= 0.0f) {
          // Game over
          LoadScene();
        } else {
          m_monitorValueZeroTimer -= Time.deltaTime;
        }
      } else {
        // The monitor value must stay at zero for a few seconds, if it goes
        // above zero, the timer will reset
        m_monitorValueZeroTimer = monitorValueZeroMaxTime;
      }
    }
  }

  void LoadScene()
  {
    // Re-enable the cursor
    m_firstPersonController.mouseLook.SetCursorLock(false);

    SceneManager.LoadScene( nextScene );
  }

}
