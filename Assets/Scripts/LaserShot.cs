using UnityEngine;
using System.Collections;

public class LaserShot : MonoBehaviour {



	private float TimeAlive = 0.0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (TimeAlive > 5.0f) {
			Destroy(this.gameObject);
		}

		TimeAlive += Time.deltaTime;

	}

	void OnCollisionEnter(Collision collision) {

		if (collision.gameObject.tag == "Harrier") {
		
			Destroy (this.gameObject);
			
		}

		if (collision.gameObject.tag == "Station") {
			
			Destroy (this.gameObject);
			
		}

	}

}
