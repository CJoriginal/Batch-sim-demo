using UnityEngine;
using System.Collections;

/// <summary>
/// This class manages the transisitions between scenes
/// </summary>
public class SceneManager : MonoBehaviour {

	public static SceneManager instance;

	private string[] scenes;
	
	void Start(){
		instance = this;

		Debug.Log ("Main Menu Loaded");

		scenes = new string[5];

		scenes [0] = "Main Menu";
		scenes [1] = "Info Scene";
		scenes [2] = "Tutorial Menu";
		scenes [3] = "Game";
		scenes [4] = "End Scene";

	}
	
	public void NextScene(){
		StartCoroutine(ChangeLevel());
	}
	
	IEnumerator ChangeLevel() {
		int index = Application.loadedLevel;
		Debug.Log ("Current Level: " + Application.loadedLevelName);

		index++;

		if (index > 4) {
			index = 0;
		}


		float fadeTime = Fading.instance.BeginFade(1);
		yield return new WaitForSeconds(fadeTime);

		Debug.Log ("Loading Level: " + scenes [index]);

		Application.LoadLevel (scenes[index]);
	}
}
