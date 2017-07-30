using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Rowan Donaldson
/// Date: 30th July 2017
/// Much like the BarSpeedValue script, read the currentHeat, MaxHeat and fill this into the Image Fill
/// </summary>

public class BarHeatValue : MonoBehaviour 
{
	private Image myImage;
	[Range(0, 100)]
	public float percentage = 0;

	private ResourceStore gameResources;
	public float currentHeat;
	public float maxHeat;

	void Awake()
	{
		myImage = gameObject.GetComponent<Image>() as Image;

		//Find the ResourceStore
		gameResources = GameObject.FindObjectOfType<ResourceStore>();
	}

	void Update()
	{
		currentHeat = gameResources.readHeat;
		maxHeat = gameResources.maxHeat;

		percentage = (currentHeat/maxHeat);

		myImage.fillAmount = percentage;
	}
}
