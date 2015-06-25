using UnityEngine;
using System.Collections;


/// <summary>
/// This class controls the properties and functionality of a 'Job' Prefab.
/// </summary>
public class JobController : MonoBehaviour {

	public bool dragging;
	private float distance;

	private Color mouseOverColor;
	private Color originalColor;
	private Vector3 position;
	private Vector3 location;

	private float speed;
	public float lifeTime;
	private float time;

	private bool work;
	private bool spawnMove;
	private bool touchWork;
	private bool spawnCheck;

	public float laneBoundsPos;
	public float laneBoundsSize;
	public float jobBoundsPosX;
	public float jobBoundsSizeX;
	public float jobBoundsPosY;
	public float jobBoundsSizeY;
	
	public bool startJob;
	public bool jobExit;
	public bool isWorking;
	public bool isSpawn;
	public bool outOfBounds;
	public bool inJob;

	public float speedMod;

	public GameObject game;

	public Transform jobTransform;
	public int value;
	public Vector3 size;
	public Vector3 exitLoc;

	public float qTop;
	public float jobTop;
	
	// Use this for initialization
	void Start () {
		isSpawn = true;
		jobExit = false;
		isWorking = false;
		dragging = false;

		mouseOverColor = Color.blue;
		originalColor = GetComponent<Renderer> ().material.color;

		speed = 0.05f;

		lifeTime = 5.0f;
		time = 0.0f;

		location = jobTransform.position;

		game = GameObject.Find ("Game Controller");

		startJob = false;
		spawnMove = false;
		touchWork = false;
		outOfBounds = false;
		inJob = false;
		spawnCheck = true;

		laneBoundsPos = -4.3f;
		laneBoundsSize = 6.1f;
		jobBoundsPosX = GetComponent<BoxCollider2D>().transform.position.x;
		jobBoundsSizeX = GetComponent<BoxCollider2D>().bounds.size.x * 0.5f;
		jobBoundsPosY = GetComponent<BoxCollider2D> ().transform.position.y;
		jobBoundsSizeY = GetComponent<BoxCollider2D>().bounds.size.y * 0.5f;

		jobTransform.localScale = new Vector3 (0.5f, 0.5f, 1f);
		size = new Vector3 (1, Random.Range (1, 6), 1);
	}
	
	// Update is called once per frame
	void Update () {
		if (time > 3.0f && !isSpawn) {
			spawnCheck = false;
		} else {
			time += Time.deltaTime;
		}

		if (isSpawn) {
			spawnMove = true;
			GetComponent<Rigidbody2D> ().WakeUp ();
			if (lifeTime <= 0) {
				Destroy (gameObject);
			}

			if (!dragging) {
				lifeTime -= Time.deltaTime;
			}
		} else if (work) {
			GetComponent<Rigidbody2D> ().isKinematic = true;
			GetComponent<BoxCollider2D> ().isTrigger = false;
		} else {
			GetComponent<Rigidbody2D> ().isKinematic = false;
			GetComponent<BoxCollider2D> ().isTrigger = false;
		}

		if (isWorking) {
			work = true;
			speed = 0.01f / speedMod;
		}

		if ((laneBoundsPos + laneBoundsSize) > (jobBoundsPosX + jobBoundsSizeX) && (laneBoundsPos - laneBoundsSize) < (jobBoundsPosX - jobBoundsSizeX)) {
			startJob = true;
			isSpawn = false;
		}

		GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
	}

	void FixedUpdate() {
		jobBoundsPosX = GetComponent<BoxCollider2D>().transform.position.x;
		jobBoundsPosY = GetComponent<BoxCollider2D> ().transform.position.y;

		if (dragging) {

			speed = 0.05f;

			if (!work) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				Vector3 rayPoint = ray.GetPoint (distance);

				if(isSpawn || !startJob){
					transform.position = rayPoint;
					jobTransform.localScale = size;
				}
				else {
					float lLeft = laneBoundsPos - laneBoundsSize;
					float lRight = laneBoundsPos + laneBoundsSize;
					float jLeft = jobBoundsPosX - jobBoundsSizeX;
					float jRight = jobBoundsPosX + jobBoundsSizeX;

					if(lLeft >= jLeft || lRight <= jRight){
						transform.position = jobTransform.localPosition;

						outOfBounds = true;
					}
					else{
						transform.position = rayPoint;
						jobTransform.localScale = size;
					}
				}
			}
		} else {
			if (spawnMove) {
				if(!spawnCheck){
					location = new Vector2 (10.0f, -7.2f);
				}
				if (isSpawn) {
					transform.position = location;
					jobTransform.localScale = new Vector3 (0.5f, 0.5f, 1);
					GetComponent<Rigidbody2D>().Sleep();
					spawnMove = false;
					isSpawn = false;
					speed = 0f;
				}
			}

			if (!isSpawn && !isWorking) {

				position = jobTransform.localPosition;
				position.y -= speed;
				transform.position = position;
			}

			if (jobExit && !isSpawn && !isWorking) {
				position = jobTransform.localPosition;

				position = exitLoc;

				transform.position = position;

				jobExit = false;
			}

			if(outOfBounds){
				float lLeft = laneBoundsPos - laneBoundsSize;
				float lRight = laneBoundsPos + laneBoundsSize;
				float jLeft = jobBoundsPosX - jobBoundsSizeX;
				float jRight = jobBoundsPosX + jobBoundsSizeX;

				if(lLeft > jLeft || lRight < jRight){
					Vector3 vec = transform.position;
					
					if(lLeft > jLeft){
						vec.x = vec.x + 0.01f;
					} 
					if(lRight < jRight){
						vec.x = vec.x - 0.01f;
					}
					
					transform.position = vec;
				}
				else {
					outOfBounds = false;
				}
			}

			if (work) {			

				GetComponent<Rigidbody2D>().isKinematic = true;

				position = jobTransform.position;
			
				position.y -= speed;
			
				jobTransform.position = position;
			}
		}

		if (jobTransform.position.z != 0) {

			Vector3 pos = jobTransform.position;

			pos.z = 0;

			jobTransform.position = pos;
		}
	}


	void OnMouseEnter()
	{
		GetComponent<Renderer>().material.color = mouseOverColor;
	}
	
	void OnMouseExit()
	{
		GetComponent<Renderer>().material.color = originalColor;
	}
	
	void OnMouseDown()
	{
		distance = Vector3.Distance(transform.position, Camera.main.transform.position);
		exitLoc.y = jobTransform.position.y;
		exitLoc.x = jobTransform.position.x;
		exitLoc.z = 0f;

		dragging = true;
	}
	
	void OnMouseUp()
	{
		dragging = false;
	}


	void OnTriggerEnter2D(Collider2D col){
		if (!dragging) {
			if (col.gameObject.tag == "Queue End") {
				touchWork = true;
				isWorking = true;

				speed = 0.01f;
	
				if (this.gameObject.layer == 12) {
					value = 10;
					speedMod = 1.0f;
				} else if (this.gameObject.layer == 11) {
					value = 20;
					speedMod = 1.5f;
				} else if (this.gameObject.layer == 10) {
					value = 30;
					speedMod = 2.0f;
				}

				speed = speed / speedMod;
			}
			if (col.gameObject.tag == "Job"){
				if(!isSpawn && !isWorking) {

					if(col.gameObject.GetComponent<JobController>().isWorking || col.gameObject.GetComponent<JobController>().touchWork){
						touchWork = true;
						speed = col.gameObject.GetComponent<JobController>().speed;
					}
				}
			}
		}
	}

	void OnTriggerStay2D (Collider2D col){
		if (col.gameObject.tag == "Queue End") {

			float queueY = col.gameObject.GetComponent<BoxCollider2D> ().transform.position.y;
			float jobY = GetComponent<BoxCollider2D> ().transform.position.y;

			float queueSize = col.gameObject.GetComponent<BoxCollider2D> ().bounds.size.y * 0.5f;
			float jobSize = GetComponent<BoxCollider2D> ().bounds.size.y * 0.5f;

			qTop = queueY + queueSize;
			jobTop = jobY + jobSize;

			if (qTop > jobTop) {
				ScoreBoundary.instance.score += value * ScoreBoundary.instance.multiplier;
				ScoreBoundary.instance.multiplier += 1;
				ScoreBoundary.instance.MultitimeStarter ();

				GameController.instance.completeJobs += 1;
				
				Destroy (gameObject);
			}
		}

		if(col.gameObject.tag == "Respawn"){
			isSpawn = true;
		}


		if (col.gameObject.tag == "Job" && dragging) {
			inJob = true;
		}
	}

	void OnTriggerExit2D (Collider2D col){
		if (col.gameObject.tag == "Job") {
			inJob = false;
			touchWork = false;
		}
	}
}
