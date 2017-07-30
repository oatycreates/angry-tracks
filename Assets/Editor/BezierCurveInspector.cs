using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Taken form Spline Tutorial - Make sure this script is in the Editor Folder
/// </summary>
using UnityEditor;

[CustomEditor(typeof(BezierCurve))]
public class BezierCurveInspector : Editor //Make sure Editor is here instead of Monodevelop
{

	private BezierCurve curve;
	private Transform handleTransform;
	private Quaternion handleRotation;

	//Make it a curve
	private const int lineSteps = 10;

	//Easy way to Draw
	private const float directionScale = 0.5f;

	private void OnSceneGUI()
	{
		curve = target as BezierCurve;
		handleTransform = curve.transform;

		//Set pivot mode
		//Quaternion handleRotation = handleTransform.rotation;
		handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

		//Show the Points
		Vector3 p0 = ShowPoint(0);
		Vector3 p1 = ShowPoint(1);
		Vector3 p2 = ShowPoint(2);
		Vector3 p3 = ShowPoint(3);

		//Draw the Points
		Handles.color = Color.grey;
		Handles.DrawLine(p0, p1);
		Handles.DrawLine(p2, p3);

		//Draw Bezier
		ShowDirections();
		Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);

		//Make it a curve around 't' -- Also, get the Velocity/Speed of THAT POINT in the Curve
		Handles.color = Color.white;
		Vector3 lineStart = curve.GetPoint( 0f );

		Handles.color = Color.green;
		//Handles.DrawLine ( lineStart, lineStart + curve.GetVelocity(0f)); //Velocity
		Handles.DrawLine ( lineStart, lineStart + curve.GetDirection(0f));  //Direction = Velocity.normalized;

		//For every point after First -- Make sure the loop is "Less than AND Equal To"
		for (int i = 1; i <= lineSteps; i++)
		{
			Vector3 lineEnd = curve.GetPoint(i / (float) lineSteps);
			Handles.color = Color.white;
			Handles.DrawLine( lineStart, lineEnd);
			Handles.color = Color.green;
			//Handles.DrawLine(lineEnd, lineEnd + curve.GetVelocity(i / (float)lineSteps)); //Velocity
			Handles.DrawLine(lineEnd, lineEnd + curve.GetDirection(i / (float)lineSteps));  //Direction
			lineStart = lineEnd;
		}
	}

	//Because we're using this code a lot, we're putting it in an individual function
	private Vector3 ShowPoint (int index)
	{
		Vector3 point = handleTransform.TransformPoint( curve.points[index] );

		EditorGUI.BeginChangeCheck();

		point = Handles.DoPositionHandle( point, handleRotation );

		//If anything has changed
		if (EditorGUI.EndChangeCheck())
		{
			//Make Changes Undoable 
			Undo.RecordObject( curve, "Move Point");
			//Remind the user to Save When Quitting
			EditorUtility.SetDirty(curve);

			//Rest to World Space
			curve.points[index] = handleTransform.InverseTransformPoint( point );
		}

		return point;
	}

	private void ShowDirections()
	{	
		Handles.color = Color.green;
		Vector3 point = curve.GetPoint(0f);
		Handles.DrawLine(point, point + curve.GetDirection(0f) * directionScale);

		//For the remaining lines
		for (int i  = 1; i <= lineSteps; i++)
		{
			point = curve.GetPoint(i / (float)lineSteps);
			Handles.DrawLine(point, point + curve.GetDirection(i / (float)lineSteps) * directionScale);
		}
	}
}
