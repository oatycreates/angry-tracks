using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Rowan Donaldson
/// Date: 31st July 2017
/// Quickly Quick or Reload the Game
/// </summary>

public class QuickQuit : MonoBehaviour 
{

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}

		if (Input.GetKeyDown(KeyCode.Backspace))
		{
			SceneManager.LoadScene( SceneManager.GetActiveScene().name );
		}
	}
	
}
