using UnityEngine;
using System.Collections;

public class AlienShipBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		
		if (collision.gameObject.tag == "Rocket")
		{
			this.gameObject.GetComponent<Health>().AdjustHealth(-30);
			Debug.Log (this.gameObject.name + " " + this.gameObject.GetComponent<Health>().healthPoints());
		}
	}
}
