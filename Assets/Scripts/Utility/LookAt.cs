using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Rowan Donaldson
/// Date: 30th July 2017
/// Look at target object
/// </summary>

public class LookAt : MonoBehaviour 
{
	public Transform target;

	void Update()
	{
		if (target != null)
		{
			gameObject.transform.LookAt(target);
		}
	}
	
}
