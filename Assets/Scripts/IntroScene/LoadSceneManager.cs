// Load an assetbundle which contains Scenes.
// When the user clicks a button the first Scene in the assetbundle is
// loaded and replaces the current Scene.

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LoadSceneManager : MonoBehaviour
{
//	private AssetBundle myLoadedAssetBundle;
//	private string[] scenePaths;


	Scene scene; // save the current scene so you can access its name

	// Use this for initialization
	void Start()
	{
		// get currently active scene
		scene = SceneManager.GetActiveScene();
//		Debug.Log("Active scene is '" + scene.name + "'.");
	}

	void OnGUI()
	{
		//create GUI button
		if (GUI.Button(new Rect(10, 10, 100, 30), "Change Scene"))
		{
			// start the change
			StartCoroutine(Change ());

		}
	}

	void LoadNewScene(){
		if (scene.name == "02_cube") {
//			Initiate.Fade("01_main",Color.white,0.2f);	
			SceneManager.LoadScene("03_sphere", LoadSceneMode.Single);
		} else {
			SceneManager.LoadScene("02_cube", LoadSceneMode.Single);
		}

	}

	IEnumerator Change(){
		Debug.Log ("Called");
		float fadeTime = GameObject.Find ("SceneLoadManager").GetComponent<SceneFadeBehavior>().BeginFade(1);
		yield return new WaitForSeconds (0.6f);	
		LoadNewScene ();
	}
}


//using UnityEngine;
//using UnityEngine.SceneManagement;
//
//public class LoadScene : MonoBehaviour
//{
//	void Start()
//	{
//		Debug.Log ("hey there");
//		// Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
//
//	}
//
//		void OnGUI()
//		{
//			if (GUI.Button(new Rect(10, 10, 100, 30), "Change Scene"))
//			{
////				Debug.Log("Scene2 loading: " + scenePaths[0]);
//				SceneManager.LoadScene("02_sphere", LoadSceneMode.Single);
//			}
//		}
//}
