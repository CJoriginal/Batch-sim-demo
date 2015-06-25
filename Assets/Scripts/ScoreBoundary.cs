using UnityEngine;
using UnityEngine.UI;
using System.Collections;


/// <summary>
/// This Class is responsible for the calculation and storing of the score variable in the game.
/// </summary>
public class ScoreBoundary : MonoBehaviour {

	public static ScoreBoundary instance;

	public int score;
	public int multiplier;

	public int largeValue;
	public int mediumValue;
	public int smallValue;
	public int highValue;
	public int normalValue;
	public int lowValue;

	public Text scoreText;
	public GameObject game;
	private float multiTime;

	void Start () {
		instance = this;

		score = 0;
		multiplier = 1;

		largeValue = 30;
		mediumValue = 20;
		smallValue = 10;
		highValue = 3;
		normalValue = 2;
		lowValue = 1;

		scoreText.text = "Score: " + score.ToString();

		multiTime = 0.0f;
	}

	void Update () {
		if(TimerScript.gameOver){
			Debug.Log ("GAME OVER");

			int totalJobs = GameController.instance.totalJobs;
			int completeJobs = GameController.instance.completeJobs;

			PlayerPrefs.SetInt ("Score", score);
			PlayerPrefs.SetInt ("Total Jobs", totalJobs);
			PlayerPrefs.SetInt ("Complete Jobs", completeJobs);

			SceneManager.instance.NextScene();
		}

		if (multiTime >= 6.0f) {
			MultiplierTimeOut ();
		}

		scoreText.text = "Score: " + score.ToString() + "   "
					   + "Multiplier: " + multiplier.ToString();

		multiTime += Time.deltaTime;
	}

	public void MultitimeStarter(){
		multiTime = 0;
	}

	private void MultiplierTimeOut() {
		if (multiplier != 1) {
			multiplier = 1;
		}
	}
}
