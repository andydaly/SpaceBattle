using UnityEngine;
using System.Collections;

public class StationSight : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.tag == "Station") {
			transform.parent.GetComponent<Boid>().groupMember = false;
			transform.parent.GetComponent<Boid>().offsetPursueEnabled = false;
			transform.parent.GetComponent<Boid>().pursueEnabled = true;
			transform.parent.GetComponent<Boid>().pursueTarget = collider.gameObject;
			transform.parent.GetComponent<AttackFromDistance>().enabled = true;
			transform.parent.GetComponent<AttackFromDistance>().distanceFromTarget = 1.01f;
			transform.parent.GetComponent<AttackFromDistance>().target = collider.gameObject;
				}
	}


}
