using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	//This script should be attached to each controller (Controller Left or Controller Right)

	// Getting a reference to the controller GameObject
	private SteamVR_TrackedObject trackedObj;
	// Getting a reference to the controller Interface
	private SteamVR_Controller.Device Controller;
	float RaycastHitDistance;
	public LayerMask carRadioLayerMask;
	public bool isSeeingRadio = false;
	private bool isPointingAtRadio = false;
	float startingAngle = 0.0f;
	bool isChangingChannel = false;
	public float currentStationAngle = 0.0f;
	float newChannelAngle = 0.0f;
	public GameObject Radio;

	void Awake()
	{
		
		// initialize the trackedObj to the component of the controller to which the script is attached
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		RaycastHitDistance = 20.0f;
		carRadioLayerMask = LayerMask.GetMask ("carRadioLayerMask");

	}

	// Update is called once per frame
	void Update () {
		

		Raycasting ();


		Controller = SteamVR_Controller.Input((int)trackedObj.index);



		// Getting the Trigger press
		if (isPointingAtRadio && Controller.GetHairTriggerDown())
		{
			//Debug.Log ("initial");
			//Debug.Log (Controller.transform.rot.eulerAngles.z);
			startingAngle = Controller.transform.rot.eulerAngles.z;
			isChangingChannel = true;
		
		}

		if (isChangingChannel) {

			//Debug.Log ("displacement");

			float displacement = (startingAngle - Controller.transform.rot.eulerAngles.z) > 0 ? (startingAngle - Controller.transform.rot.eulerAngles.z): 360.0f + (startingAngle - Controller.transform.rot.eulerAngles.z);
			adjustRadioChannel (displacement); 	

		}



		// Getting the Trigger Release
		if (Controller.GetHairTriggerUp())
		{
			Debug.Log(gameObject.name + " Trigger Release");
			isChangingChannel = false;
			currentStationAngle = newChannelAngle;
		}


	}



	void Raycasting(){
		
		
		Vector3 fwd = transform.TransformDirection (Vector3.forward); //what is the direction in front of us
		RaycastHit hit = new RaycastHit ();

		if (Physics.Raycast (transform.position, fwd, out hit, RaycastHitDistance, carRadioLayerMask)) {
		
			isPointingAtRadio = true;
	
		} else {

			isPointingAtRadio = false;

		}
	}


	void adjustRadioChannel(float displacementValue){


		newChannelAngle = (currentStationAngle + displacementValue * 0.5f) % 180.0f ;
		//currentStationAngle = newChannelAngle;

		//update the value of playSong prefab by some value.
		Radio.GetComponent<audioScripts>().channelValue = newChannelAngle;


	}


}