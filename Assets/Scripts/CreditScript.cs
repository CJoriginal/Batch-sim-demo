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
	public Text queuedText;
	public Text infoText;

	void Start(){
		Debug.Log ("Credits Loaded");

		int score = PlayerPrefs.GetInt ("Score");

		scoreText.text = score.ToString();

		int complete = PlayerPrefs.GetInt ("Complete Jobs");
		int total = PlayerPrefs.GetInt ("Total Jobs");
		int queue = PlayerPrefs.GetInt ("Queued Jobs");

		double percentage = (double)(complete * 100) / total;

		percentageText.text = percentage.ToString("F2") + "%";

		totalText.text = "jobs completed out of " + total + " jobs!";

		percentage = (double)(queue * 100) / total;

		queuedText.text = percentage.ToString("F2") + "%";

		infoText.text = "jobs were queued!";

	}
}
