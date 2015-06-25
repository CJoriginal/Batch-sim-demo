using UnityEngine;
using System.Collections;

public class Fading : MonoBehaviour {

	public static Fading instance;

	public Texture2D fadeOutTexture;	// Overlay Texture
	public float fadeSpeed = 0.8f;		// The Fading Speed

	private int drawDepth = -1000;		// The Texture's order in the draw hierarchy
	private float alpha = 1.0f;			// The Texture's Alpha value
	private int fadeDir = -1;			// The direction to fade


	void Start() {
		instance = this;
	}

	void OnGUI () {

		alpha += fadeDir * fadeSpeed * Time.deltaTime;

		alpha = Mathf.Clamp01 (alpha);


		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture);
	}

	public float BeginFade (int direction) {
		fadeDir = direction;
		return (fadeSpeed);
	}

	void OnLevelWasLoaded () {
		BeginFade (-1);
	}
}
