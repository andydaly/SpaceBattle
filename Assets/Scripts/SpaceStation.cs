using UnityEngine;
using System.Collections;

public class SpaceStation : MonoBehaviour {

	public GameObject Damage;

	private bool DamageActive = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if ((this.gameObject.GetComponent<Health>().healthPoints()<50.0f) && (!DamageActive))
		{
			Instantiate(Damage, transform.position,transform.rotation);
			DamageActive = true;
		}
		if (this.gameObject.GetComponent<Health> ().healthPoints () <= 0.0f) {
			audio.Play();
				}
	}

	void OnCollisionEnter(Collision collision) {
		
		if (collision.gameObject.tag == "ELaser")
		{
			this.gameObject.GetComponent<Health>().AdjustHealth(-4);
			
		}
	}

}
