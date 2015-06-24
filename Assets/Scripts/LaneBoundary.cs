using UnityEngine;
using System.Collections;

/// <summary>
/// This Class controls the variables and positioning of jobs within it's boundary collider.
/// </summary>
public class LaneBoundary : MonoBehaviour {

	void OnTriggerStay2D (Collider2D col){
		JobController job = col.gameObject.GetComponent<JobController> ();

		if (!job.dragging) {
			job.isSpawn = false;
		}
	}
}
