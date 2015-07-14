using UnityEngine;
using System.Collections;

/// <summary>
/// This class manages the transisitions between scenes
/// </summary>
public class SceneManager : MonoBehaviour {

	public static SceneManager instance;

	private string[] scenes;
	private int index;

	private float timestamp;

	void Start(){
		instance = this;
		timestamp = 30.0f;

		Debug.Log ("Main Menu Loaded");

		scenes = new string[5];

		index = 0;

		scenes [0] = "Demo Scene";
		scenes [1] = "Main Menu";
		scenes [2] = "Info Scene";
		scenes [3] = "Game";
		scenes [4] = "End Scene";

	}

	void Update () {
		if (timestamp < 0 && Application.loadedLevel != 0) {
			DemoScene();
		}

		if (Input.anyKeyDown && Application.loadedLevel == 0) {
			NextScene();
		}
		
		if (Input.anyKeyDown || Input.GetAxis("Mouse X") > 0 || Input.GetAxis("Mouse X") < 0){
			timestamp = 30.0f;
		}else{
			timestamp -= Time.deltaTime;
		}
	}

	public void NextScene(){
		StartCoroutine(ChangeLevel());
	}

	public void LoadScene(){
		StartCoroutine (LoadLevel ());
	}

	public void DemoScene(){
		index = Application.loadedLevel;

		Debug.Log ("No Input. Loading Demo.");

		Application.LoadLevel ("Demo Scene");
	}

	public void CreditScene() {
		Debug.Log ("Loading Credits");

		Application.LoadLevel("Credits");
	}
		
	public void Exit() {
		Application.Quit();
	}

	IEnumerator ChangeLevel() {
		index = Application.loadedLevel;
		Debug.Log ("Current Level: " + Application.loadedLevelName);

		index++;

		if (index > 4) {
			index = 1;
		}

		float fadeTime = Fading.instance.BeginFade(1);
		yield return new WaitForSeconds(fadeTime);

		Debug.Log ("Loading Level: " + scenes [index]);

		Application.LoadLevel (scenes[index]);
	}

	IEnumerator LoadLevel() {

		if (Application.loadedLevel == 5) {
			index = 1;
		}

		Debug.Log ("Loading Level: " + scenes [index]);
		
		float fadeTime = Fading.instance.BeginFade(1);
		yield return new WaitForSeconds(fadeTime);

		Application.LoadLevel (scenes[index]);
	}
}
