using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofCloses : MonoBehaviour {

	public float roofClosingDuration = 5.0F; // duration of the roof closing animation in seconds
	public float rotationAmount = 30.0F; // angle in degrees that each roof part is rotated by initially

	GameObject leftDome;
	GameObject rightDome; 

	// Use this for initialization
	void Start () {

		// store game objects in variables
		leftDome = GameObject.Find("dome_left");
		rightDome = GameObject.Find("dome_right");

		// set initial rotation of the two dome parts 
		leftDome.transform.rotation = Quaternion.Euler(-rotationAmount, 0, 0);
		rightDome.transform.rotation = Quaternion.Euler(rotationAmount, 0, 0);

		// animate the roof closing
		StartCoroutine (CloseRoof ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// this method animates the dome roof

	IEnumerator CloseRoof(){

		// set the current time for the closing animation
		float roofClosingRunningTime = 0.0F;

		// while the animation time is under 2 seconds, close the roof
		while (roofClosingRunningTime < roofClosingDuration) {

			// LEFT DOME

			// get rotation value in the form of eulerAngles
			Vector3 rot_left = leftDome.transform.rotation.eulerAngles;
			// change x rotation
			rot_left.x = Mathf.Lerp (-rotationAmount, 0, roofClosingRunningTime/roofClosingDuration);
			// transform rotation
			leftDome.transform.eulerAngles = rot_left;

			// RIGHT DOME

			// get rotation value in the form of eulerAngles
			Vector3 rot_right = rightDome.transform.rotation.eulerAngles;
			// change x rotation
			rot_right.x = Mathf.Lerp (rotationAmount, 0, roofClosingRunningTime/roofClosingDuration);
			// transform rotation
			rightDome.transform.eulerAngles = rot_right;

			// update running time
			roofClosingRunningTime += Time.deltaTime;


		
			yield return null;
		}

		
	}
}
