using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFadeBehavior : MonoBehaviour {

	// fade parameters
	public Texture2D fadeOutTexture;
	public float fadeSpeed = 0.8F;

	private int drawDepth = -1000;
	private float alpha = 1; 
	private int fadeDir = -1;

	// Use this for initialization
	void Start () {
		// the scene 
//		BeginFade (-1);
		StartCoroutine(Fade(-1));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){
		//fade out alpha using direction, speed, Time.deltaTime to convert operation to seconds
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp01 (alpha);

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture(new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture);
	}

	public float BeginFade(int direction){
		fadeDir = direction;
		return fadeSpeed;
	}

	IEnumerator Fade(int direction){

//		float finalAlpha = 1 + direction;
		bool fadeTransition = true;

		while (fadeTransition == true) {
			
			alpha += fadeDir * fadeSpeed * Time.deltaTime;
			alpha = Mathf.Clamp01 (alpha);

			GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
			GUI.depth = drawDepth;
			GUI.DrawTexture(new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture);

			if (direction > 0 && alpha == 1) {
				fadeTransition = false;
			}else if(direction < 0 && alpha == 0){
				fadeTransition = false;
			}

			yield return null;
		
		}
			
		
	}
		
}
