using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This class is responsible for maintaining the main functions of the game.
/// </summary>
public class GameController : MonoBehaviour {

	public static GameController instance;

	private float spawnTimer;
	private float speed;
	private Vector2[] spawnLocation = new Vector2[5];
	private int rand;

	private GameObject queue;
	private GameObject job;


	public int totalJobs;
	public int queueJobs;
	public int completeJobs;
	public GameObject[] prefabToSpawn;
	public Text[] scales;
	public int countSpawn;

	
	// Use this for initialization
	void Start () {
		instance = this;

		spawnTimer = 6.0f;
		countSpawn = 0;
		spawnLocation [0] = new Vector2 (10.0f, 0.3f);
		spawnLocation [1] = new Vector2 (10.0f, -1.2f);
		spawnLocation [2] = new Vector2 (10.0f, -2.7f);
		spawnLocation [3] = new Vector2 (10.0f, -4.2f);
		spawnLocation [4] = new Vector2 (10.0f, -5.7f);

		totalJobs = 0;
		completeJobs = 0;
		queueJobs = 0;

		queue = new GameObject ();
		queue.name = "Queue";

		for (int x = 0; x < 5; x++) {
			rand = Random.Range (0, 8);
			job = Instantiate (prefabToSpawn [rand], spawnLocation [x], Quaternion.identity) as GameObject;
			job.gameObject.name = "Job " + totalJobs;
			job.transform.parent = queue.transform;
			job.GetComponent<JobController>().scaleText = scales[x];
			totalJobs++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnTimer <= 0) {
			if (countSpawn == 5) {
				countSpawn = 0;
			}

			rand = Random.Range (0, 8);

			job = Instantiate (prefabToSpawn [rand], spawnLocation [countSpawn], Quaternion.identity) as GameObject;
			job.gameObject.name = "Job " + totalJobs;
			job.transform.parent = queue.transform;
			job.GetComponent<JobController>().scaleText = scales[countSpawn];
			totalJobs++;
			spawnTimer = 1.5f;
			countSpawn++;
		}

		spawnTimer -= Time.deltaTime;
	}
}