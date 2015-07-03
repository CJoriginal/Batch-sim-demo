using UnityEngine;
using System.Collections;

public class DemoScript : MonoBehaviour {

	private float spawnTimer;
	private float speed;
	private Vector2[] spawnLocation = new Vector2[3];
	private int rand;
	
	private GameObject queue;
	private GameObject job;
	
	public GameObject[] prefabToSpawn;
	public int countSpawn;
	
	
	// Use this for initialization
	void Start () {
		spawnTimer = 3.0f;
		countSpawn = 0;
		spawnLocation[0] = new Vector2(-4.5f, 14f);
		spawnLocation[1] = new Vector2(-1f, 14f);
		spawnLocation[2] = new Vector2(2f, 14f);

		queue = new GameObject ();
		queue.name = "Queue";

		rand = Random.Range(0, 8);
		job = Instantiate(prefabToSpawn[rand], spawnLocation[countSpawn], Quaternion.identity) as GameObject;
		job.transform.parent = queue.transform;
		job.transform.localScale = new Vector3(1,1,1);
		job.GetComponent<JobController>().SetSpeed(0.03f);

		countSpawn++;
	}
	
	// Update is called once per frame
	void Update () {

		if(spawnTimer <= 0){
			if(countSpawn == 3){
				countSpawn = 0;
			}

			rand = Random.Range(0, 8);
			job = Instantiate(prefabToSpawn[rand], spawnLocation[countSpawn], Quaternion.identity) as GameObject;
			job.transform.parent = queue.transform;
			spawnTimer = 3.0f;
			countSpawn++;
		}

		job.GetComponent<JobController>().SetSpeed(0.03f);
		job.transform.localScale = new Vector3(1,1,1);

		spawnTimer -= Time.deltaTime;
	}
}
