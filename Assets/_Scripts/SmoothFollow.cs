﻿using UnityEngine;
using System.Collections;

public class SmoothFollow  : MonoBehaviour {

	/*
This camera smoothes out rotation around the y-axis and height.
Horizontal Distance to the target is always fixed.

There are many different ways to smooth the rotation but doing it this way gives you a lot of control over how the camera behaves.

For every of those smoothed values we calculate the wanted value and the current value.
Then we smooth it using the Lerp void .
Then we apply the smoothed values to the transform's position.
*/
	
	// The target we are following
	public Transform target ;
	// The distance in the x-z plane to the target
	public float distance = 5.0f;
	// the height we want the camera to be above the target
	public float height = 2.0f;
	// How much we 
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;
	public static int cameraSwitch;
	public float X = 0 , Y = 0;
	public Transform[] cameraSwitchView;
	public GameObject CivilianHelper;
	public Transform truck_camrotation,playercam,playercam1;
	// Place the script in the Camera-Control group in the component menu
//	@script AddComponentMenu("Camera-Control/Smooth Follow")
	void Start(){
		cameraSwitch = 0;

	}
	void Update(){


	}

	public void CameraSwitch(Transform setcam,float setdistance, float setheight)
	{
		target = setcam;
		distance = setdistance;
		height = setheight;
	}
	public void player_cams(){
		target = playercam;
		distance = 0f;
		height = 0f;
	}

	public void player_cams1(){
		target = playercam1;
		distance = 9f;
		height = 3f;
	}
	public void Truck_Cam_rotation()
	{
		target = truck_camrotation;
		distance = 4.52f;
		height = 1.79f;
	}



	void  LateUpdate () {

		if (!target)
			return;
		
		// Calculate the current rotation angles
		float wantedRotationAngle = target.eulerAngles.y+ X;
		float wantedHeight = target.position.y + height;
		
		float currentRotationAngle = transform.eulerAngles.y ;
		float currentHeight = transform.position.y;
		
		// Damp the rotation around the y-axis
		  currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
		
		// Damp the height
		  currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
		
		// Convert the angle into a rotation
		Quaternion  currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
		
		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = target.position;
		transform.position -= currentRotation * Vector3.forward * distance;
		
		
		transform.position = new Vector3(transform.position.x,currentHeight,transform.position.z);
		
		// Always look at the target
		transform.LookAt (target);
	}
}
