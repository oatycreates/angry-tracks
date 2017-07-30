using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Rowan Donaldson
/// Date: 29th July 2017
/// Expose a public function to open a webpage.
/// </summary>
public class OpenWebpage : MonoBehaviour
{
  public string webpage;

  public void OpenPage()
  {
    Application.OpenURL( webpage );
  }

}
