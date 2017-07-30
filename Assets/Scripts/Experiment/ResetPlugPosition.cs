using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Rowan Donaldson
/// Date: 30th July 207
/// If the Physics Plug somehow gets too far away from the Train, reset it's position
/// </summary>

public class ResetPlugPosition : MonoBehaviour 
{
	private Rigidbody myRigid;
	private Vector3 startPos;
	public float maxDistanceFromTrainCenter = 10.0f;
	private float distance;
	private Transform train;

	void Awake()
	{
		myRigid = gameObject.GetComponent<Rigidbody>() as Rigidbody;

		train = gameObject.transform.root.gameObject.transform;

		startPos = gameObject.transform.localPosition;
	}

	void Update()
	{
		distance = Vector3.Distance(train.position, gameObject.transform.position);

		if (!myRigid.isKinematic)
		{
			if (distance > maxDistanceFromTrainCenter)
			{
				ResetPlugPos();
			}
		}
	}

	public void ResetPlugPos()
	{
		myRigid.velocity = Vector3.zero;
		myRigid.angularVelocity = Vector3.zero;
		gameObject.transform.localPosition = startPos;
	}

}
