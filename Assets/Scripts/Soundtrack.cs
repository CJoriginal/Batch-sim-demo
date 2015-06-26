using UnityEngine;
using System.Collections;

public class Soundtrack : MonoBehaviour {

	private static Soundtrack instance; 

	public static Soundtrack GetInstance() { 
		return instance; 
	}
	
	void Awake() { 
		if (instance != null && instance != this) { 
			Destroy(this.gameObject); return; 
		} else { 
			instance = this; 
		} 
		DontDestroyOnLoad(this.gameObject); 
	}

	void Update(){
		if (Application.loadedLevel == 3) {
			Destroy(this.gameObject);
		} 

		//instance.gameObject.GetComponent<AudioSource>().Play ();

	}
}