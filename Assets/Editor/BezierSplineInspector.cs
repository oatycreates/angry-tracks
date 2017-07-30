using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Taken from Spline Tutorial - This Script Must be in the Editor Folder under Unity Assets
/// </summary>

using UnityEditor;
[CustomEditor(typeof(BezierSpline))]
public class BezierSplineInspector : Editor //: MonoBehaviour 
{
	private const int stepsPerCurve = 10;
	private const float directionScale = 0.5f;

	private BezierSpline spline;
	private Transform handleTransform;
	private Quaternion handleRotation;

	//Make Buttons in the Scene so It's easier to see
	private const float handleSize = 0.04f;
	private const float pickSize = 0.06f;
	private int selectedIndex = -1;

	//Colours
	private static Color[] modeColours = 
	{
		Color.white,
		Color.yellow,
		Color.cyan
	};

	private void OnSceneGUI()
	{
		spline = target as BezierSpline;
		handleTransform = spline.transform;
		handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

		//Loop throught all Point Creations
		Vector3 p0 = ShowPoint(0);

		//for (int i = 1; i < spline.points.Length; i+= 3)
		for (int i = 1; i < spline.ControlPointCount; i+= 3)
		{
			Vector3 p1 = ShowPoint(i);
			Vector3 p2 = ShowPoint(i + 1);
			Vector3 p3 = ShowPoint(i + 2);

			Handles.color = Color.grey;
			Handles.DrawLine(p0, p1);
			Handles.DrawLine(p2, p3);


			Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);
			//Reset for the next time the Add Curve button is called
			p0 = p3;
		}
		ShowDirections();
	}

	private void ShowDirections()
	{
		Handles.color = Color.green;
		Vector3 point = spline.GetPoint(0f);
		Handles.DrawLine(point, point + spline.GetDirection(0f) * directionScale);

		int steps = stepsPerCurve * spline.CurveCount;

		//For the rest of the points -- Remember to do Less Than or Equal TO
		for (int i = 1; i <= steps; i++)
		{
			point = spline.GetPoint(i /  (float)steps);
			Handles.DrawLine(point, point + spline.GetDirection(i / (float)steps) * directionScale);
		}
	}

	private Vector3 ShowPoint(int index)
	{
		Vector3 point = handleTransform.TransformPoint( spline.GetControlPoint(index));

		float size = HandleUtility.GetHandleSize(point);
		//Double the size of the first node
		if (index == 0)
		{
			size *= 2f;
		}

		Handles.color = modeColours[(int)spline.GetControlPointMode(index)];
		if (Handles.Button(point, handleRotation, size *handleSize, size *pickSize, Handles.DotCap))
		{
			selectedIndex = index;
			//Repaint makes the Inspector Update
			Repaint();
		}

		//Do This So Only The Selected Handle need to Show Tooltips
		if (selectedIndex == index)
		{
			EditorGUI.BeginChangeCheck();
			point = Handles.DoPositionHandle(point, handleRotation);

			//Check for changes
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(spline, "Move Point");
				EditorUtility.SetDirty( spline );

				spline.SetControlPoint(index, handleTransform.InverseTransformPoint(point));
			}
		}

		return point;
	}

	public override void OnInspectorGUI()
	{
		//DrawDefaultInspector();
		spline = target as BezierSpline;

		EditorGUI.BeginChangeCheck();
		bool loop = EditorGUILayout.Toggle("Loop", spline.Loop);
		if (EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(spline, "Toggle Loop");
			EditorUtility.SetDirty(spline);
			spline.Loop = loop;
		}

		if (selectedIndex >= 0 && selectedIndex < spline.ControlPointCount)
		{
			DrawSelectedPointInspector();
		}

		if (GUILayout.Button("Add Curve"))
		{
			Undo.RecordObject(spline, "Add Curve");
			spline.AddCurve();
			EditorUtility.SetDirty(spline);
		}
	}

	private void DrawSelectedPointInspector()
	{
		GUILayout.Label("Selected Point");
		EditorGUI.BeginChangeCheck();

		Vector3 point = EditorGUILayout.Vector3Field("Position", spline.GetControlPoint( selectedIndex ));
		if (EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(spline, "Move Point");
			EditorUtility.SetDirty( spline );
			spline.SetControlPoint(selectedIndex, point);
		}

		EditorGUI.BeginChangeCheck();
		//Make the drop down menu appear in the inspector
		BezierControlPointMode mode = 
		(BezierControlPointMode) EditorGUILayout.EnumPopup("Mode", spline.GetControlPointMode(selectedIndex));

		if (EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(spline, "Change Point Mode");
			spline.SetControlPointMode(selectedIndex, mode);
			EditorUtility.SetDirty(spline);
		}
	}
}
