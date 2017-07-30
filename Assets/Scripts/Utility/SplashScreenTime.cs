using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Rowan Donaldson
/// Date: 29th June 2017
/// Countdown timer untill Scene Change
/// </summary>

public class SplashScreenTime : MonoBehaviour
{
  public float timer = 5.0f;
  private float rememberTimer;

  public string nextSceneName = " ";

  void Awake()
  {
    rememberTimer = timer;
  }

  void OnEnable()
  {
    timer = rememberTimer;
  }

  void Update()
  {
    timer -= Time.deltaTime;

    if (timer <= 0)
    {
      LoadScene();
      //Reset Timer
      timer = rememberTimer;
    }
  }

  void OnDisable()
  {
    timer = rememberTimer;
  }

  void LoadScene()
  {
    SceneManager.LoadScene( nextSceneName );
  }

  void OnGUI()
  {
    GUI.TextArea(new Rect(10, 10, 100, 20), "Time: " + ((int)timer + 1));
  }

}
