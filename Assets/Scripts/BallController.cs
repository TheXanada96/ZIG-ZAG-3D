using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

public GameObject particle;

[SerializeField]
	private float speed;

	Rigidbody rb; 

	bool started;
	bool gameOver;

    void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	// Use this for initialization
	void Start () {
	started = false;	
	}
	
	// Update is called once per frame
	void Update () {

		if(!started) {
			 if (Input.GetMouseButtonDown(0)) {
				rb.velocity= new Vector3 (speed,0,0);
				started = true;
				SpawnerPlatform.current.cominciaSpawn();
				GameManager.current.StartGame();
			 }
		}

if (!Physics.Raycast(transform.position, Vector3.down, 1f)) {
	gameOver= true;
	rb.velocity = new Vector3(0, -25f, 0);
	Camera.main.GetComponent<CameraController>().gameOver = true;

	SpawnerPlatform.current.gameOver = true;

	GameManager.current.GameOver();
}

	if (Input.GetMouseButtonDown(0) && !gameOver){
			DirectionalChange();
		}
	}

	void DirectionalChange () {
		if (rb.velocity.z > 0) { 
			rb.velocity= new Vector3 (speed,0,0);
		}
		else if (rb.velocity.x > 0) {
			rb.velocity = new Vector3(0, 0, speed);
		}
    }
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Diamond") {

		GameObject part =	Instantiate (particle, col.gameObject.transform.position, Quaternion.identity) as GameObject;

			Destroy (col.gameObject);
			Destroy (part, 2f);
		}
	}
}
