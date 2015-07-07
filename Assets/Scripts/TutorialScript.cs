using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This class controls the functionality for the 'Tutorial Scene' scene./// </summary>
public class TutorialScript : MonoBehaviour {

	public GameObject imageOne;
	public GameObject imageTwo;

	public Sprite[] game;
	public Sprite[] priority;

	private float timeGame;
	private float timePrior;
	private int gameSprite;
	private int prioritySprite;

	void Start(){
		Debug.Log ("Tutorial Menu Loaded");

		gameSprite = 0;
		prioritySprite = 0;


		timeGame = 0.0f;
		timePrior = 0.0f;
	}

	void Update(){

		if (timeGame > 15.0f) {
			if(gameSprite > 2){
				gameSprite = 0;
			}

			imageOne.GetComponent<Image>().sprite = game[gameSprite];
			gameSprite++;
			timeGame = 0;
		}

		if (timePrior > 10.0f) {
			if(prioritySprite > 2){
				prioritySprite = 0;
			}

			imageTwo.GetComponent<Image>().sprite = priority[prioritySprite];
			prioritySprite++;
			timePrior = 0;
		}

		timeGame += 0.02f;
		timePrior += 0.02f;
	}
}
