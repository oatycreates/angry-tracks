using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Rowan Donaldson
/// Date: 29th June 2017
/// Call generic Load Scene Functions
/// </summary>

public class OpenGameScene : MonoBehaviour
{
  public string sceneName = " ";

  public void LoadScene()
  {
    SceneManager.LoadScene( sceneName );
  }

  public void ResetScene()
  {
    SceneManager.LoadScene( SceneManager.GetActiveScene().name );
  }
}
