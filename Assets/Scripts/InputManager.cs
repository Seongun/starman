using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	//This script should be attached to each controller (Controller Left or Controller Right)

	// Getting a reference to the controller GameObject
	private SteamVR_TrackedObject trackedObj;
	// Getting a reference to the controller Interface
	private SteamVR_Controller.Device Controller;

	private Interactions InteractionManager;

	public bool triggerPress = false;
	private bool handOnTheWheel;



	//radio interaction elements
	float RaycastHitDistance;
	public LayerMask carRadioLayerMask;
	public bool isSeeingRadio = false;
	private bool isPointingAtRadio = false;
	float startingAngle = 0.0f;
	bool isChangingChannel = false;
	public float currentStationAngle = 0.0f;
	float newChannelAngle = 0.0f;
	public GameObject Radio;
	public int numChannels;


	void Start(){

		numChannels= Radio.GetComponent<audioScripts> ().numChannels;

	}


	void Awake()
	{
		// initialize the trackedObj to the component of the controller to which the script is attached
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		InteractionManager = GameObject.Find ("GameManager").GetComponent<Interactions> ();

		RaycastHitDistance = 20.0f;
		carRadioLayerMask = LayerMask.GetMask ("carRadioLayerMask");

	}

	// Update is called once per frame
	void Update () {

		Raycasting ();


		Controller = SteamVR_Controller.Input ((int)trackedObj.index);

		// Getting the Touchpad Axis
		if (Controller.GetAxis() != Vector2.zero)
		{
			Debug.Log(gameObject.name + Controller.GetAxis());
		}

		// Getting the Trigger press
		if (Controller.GetHairTriggerDown())
		{
			triggerPress = true;	
//			Debug.Log(gameObject.name + " Trigger Press");
//			if (gameObject.name == "Controller (right)") {
//				InteractionManager.right_trigger_press = true;
//			} else if (gameObject.name == "Controller (left)") {
//				InteractionManager.left_trigger_press = true;
//			}
//
			//InteractionManager.ActiveHairlineTrigger(gameObject.name);

			//if we're on controller right
//			if(gameObject.name == "Controller (right)"){
//				Debug.Log ("both at the same time!!!");
//
//				//find the interaction manager and call the function we wrote
//
//				//GameObject.Find ("InteractionManager").GetComponent<Interactions> ().HandleRightTriggerPressed ();
//			}
		}


		//radio interaction
		// Getting the Trigger press
		if (isPointingAtRadio && Controller.GetHairTriggerDown())
		{
			//Debug.Log ("initial");
			//Debug.Log (Controller.transform.rot.eulerAngles.z);
			startingAngle = Controller.transform.rot.eulerAngles.z;
			isChangingChannel = true;

		}
		//radio interaction
		if (isChangingChannel) {

			//Debug.Log ("displacement");

			float displacement = (startingAngle - Controller.transform.rot.eulerAngles.z) > 0 ? (startingAngle - Controller.transform.rot.eulerAngles.z): 360.0f + (startingAngle - Controller.transform.rot.eulerAngles.z);
			adjustRadioChannel (displacement); 
			Debug.Log ("is changing channels");


		}





		// Getting the Trigger Release
		if (Controller.GetHairTriggerUp())
		{
			triggerPress = false;
//			if (gameObject.name == "Controller (right)") {
//				InteractionManager.right_trigger_press = false;
//			} else if (gameObject.name == "Controller (left)") {
//				InteractionManager.left_trigger_press = false;
//			}
//			Debug.Log(gameObject.name + " Trigger Release");


			//radio interaction

			Debug.Log(gameObject.name + " Trigger Release");
			isChangingChannel = false;
			currentStationAngle = newChannelAngle;
		}

		// Getting the Grip Press
		if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
		{
			Debug.Log(gameObject.name + " Grip Press");
		}

		// Getting the Grip Release
		if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
		{
			Debug.Log(gameObject.name + " Grip Release");
		}
	}


	public bool GetTriggerPress(){
		return triggerPress;
	}

	public void HandIsOnTheWheel(bool _newStatus){
		handOnTheWheel = _newStatus;
	}

	public bool GetHandOnTheWheel(){
		return handOnTheWheel;
	}


	void Raycasting(){


		Vector3 fwd = transform.TransformDirection (Vector3.forward); //what is the direction in front of us
		RaycastHit hit = new RaycastHit ();

		if (Physics.Raycast (transform.position, fwd, out hit, RaycastHitDistance, carRadioLayerMask)) {

			isPointingAtRadio = true;
			Debug.Log ("is pointing at radio");
		} else {

			isPointingAtRadio = false;

		}
	}


	void adjustRadioChannel(float displacementValue){


		newChannelAngle = (currentStationAngle + displacementValue * 0.5f / numChannels) % 180.0f ;
		//currentStationAngle = newChannelAngle;

		//update the value of playSong prefab by some value.
		Radio.GetComponent<audioScripts>().channelValue = newChannelAngle;


	}

//	public static 
}