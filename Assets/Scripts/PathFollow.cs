using UnityEngine;
using System.Collections;

public class PathFollow : MonoBehaviour {


	public GameObject Waypoint;
	public float speed = 1.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, Waypoint.transform.position, speed);
	}
}
