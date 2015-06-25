using UnityEngine;
using UnityEngine.UI;
using System.Collections;


/// <summary>
/// This class controls the functionality for the 'End Scene' scene.
/// </summary>
public class CreditScript : MonoBehaviour {

	public Text scoreText;
	public Text percentageText;
	public Text totalText;

	void Start(){
		Debug.Log ("Credits Loaded");

		int score = PlayerPrefs.GetInt ("Score");

		scoreText.text = score.ToString();

		int complete = PlayerPrefs.GetInt ("Complete Jobs");
		int total = PlayerPrefs.GetInt ("Total Jobs");

		double percentage = (double)(complete * 100) / total;

		percentageText.text = percentage.ToString("F2") + "%";

		totalText.text = "out of " + total + " jobs!";

	}
}
