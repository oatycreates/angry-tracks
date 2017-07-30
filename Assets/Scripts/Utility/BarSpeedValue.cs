using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Rowan Donaldson
/// Date: 30th July 2017
/// Look at the Train's current speed, maximum speed and take the percentage to feed into Image Fil
/// </summary>

public class BarSpeedValue : MonoBehaviour 
{
	private Image myImage;
	[Range(0, 100)]
	public float percentage = 0;

	private ResourceStore gameResources;
	public float currentSpeed;
	public float maxSpeed;

	void Awake()
	{
		myImage = gameObject.GetComponent<Image>() as Image;

		//Find the ResourceStore
		gameResources = GameObject.FindObjectOfType<ResourceStore>();
	}

	void Update()
	{
		currentSpeed = gameResources.readSpeed;
		maxSpeed = gameResources.maxSpeed;

		percentage = (currentSpeed/maxSpeed);

		myImage.fillAmount = percentage;
	}
}
