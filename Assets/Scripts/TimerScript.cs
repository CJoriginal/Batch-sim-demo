using UnityEngine;
using UnityEngine.UI;
using System.Collections;


/// <summary>
/// This Class is responsible for managing the 'Timer' functionality for the game.
/// </summary>
public class TimerScript : MonoBehaviour {

	public float myTimer;
	public Text time;
	public static bool gameOver;


	void Start(){
		time.text = myTimer.ToString();
		gameOver = false;
		Debug.Log ("Game Loaded");
	}

	// Update is called once per frame
	void Update () {
		myTimer -= Time.deltaTime;
		time.text = "Time Left: " + myTimer.ToString ("F2");

		if (myTimer <= 0) {
			gameOver = true;
		}
	}
}
