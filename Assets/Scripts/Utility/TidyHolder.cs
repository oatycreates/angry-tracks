using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Rowan Donaldson
/// Date: 30th July, 2017
/// Collect multiple instances of the same prefab and place them in a tidy holder in the Heirarchy
/// </summary>

[ExecuteInEditMode]
public class TidyHolder : MonoBehaviour 
{

	public string holderName = "Holder";
	private GameObject holder;

	void Awake()
	{
		holder = GameObject.Find( holderName );

		if (holder == null)
		{
			holder = new GameObject(holderName);
		}
	}

	void Start()
	{
		gameObject.transform.SetParent(holder.transform);
	}
	
}
