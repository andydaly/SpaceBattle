using UnityEngine;
using System.Collections;

public class EarthExplodes : MonoBehaviour {

	public GameObject Explosion1;
	public GameObject Explosion2;
	public float TimeLimit = 3.0f;


	float Timer = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Timer > TimeLimit)
		{
			if (Explosion1!=null)
			Instantiate(Explosion1, transform.position,transform.rotation);

			if (Explosion2!=null)
			Instantiate(Explosion2, transform.position,transform.rotation);
			Destroy(this.gameObject);
		}

		Timer += Time.deltaTime;
	}
}
