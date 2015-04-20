using UnityEngine;
using System.Collections;

public class DestroyEarth : MonoBehaviour {
	public GameObject BeamPrefab;

	public float TimeToDestroy = 3.0f;
	float Timer = 0.0f;
	bool Fired = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if ((Timer > TimeToDestroy)&& (!Fired)) {
			GameObject Bullet = (GameObject)Instantiate (BeamPrefab, transform.position+(transform.up*-0.5f), transform.rotation); 
			Bullet.rigidbody.AddForce (transform.up * -100, ForceMode.Impulse);
			Fired = true;
				}

		if (!Fired)
		Timer += Time.deltaTime;
	}
}
