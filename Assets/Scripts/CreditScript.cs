using UnityEngine;
using UnityEngine.UI;
using System.Collections;


/// <summary>
/// This class controls the functionality for the 'End Scene' scene.
/// </summary>
public class CreditScript : MonoBehaviour {

	public Text scoreText;

	void Start(){
		Debug.Log ("Credits Loaded");

		int score = PlayerPrefs.GetInt ("Score");

		scoreText.text = score.ToString();
	}
}
