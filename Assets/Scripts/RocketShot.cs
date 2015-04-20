using UnityEngine;
using System.Collections;

public class RocketShot : MonoBehaviour {

	public GameObject Explosion;


	[HideInInspector]
	public bool TempRockets = true;
	private float TimeAlive = 0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (TempRockets) {
						if (TimeAlive > 5.0f) {
								Destroy (this.gameObject);
						}
		
						TimeAlive += Time.deltaTime;
				}
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Alien") {

			Instantiate (Explosion, transform.position, transform.rotation);
			Destroy (this.gameObject);
			if (!TempRockets)
			{
				collision.gameObject.GetComponent<Boid>().groupMember = false;
				collision.gameObject.GetComponent<Health>().AdjustHealth(-120);
			}

		}

	}
}
