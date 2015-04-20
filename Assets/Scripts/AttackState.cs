using UnityEngine;
using System.Collections;

public class AttackState : MonoBehaviour {

	public GameObject BulletPrefab;
	public float bulletspeed = 100.0f;
	public float CoolDown = 3.0f;
	private float DamageTime = 0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ((this.GetComponent<Boid> ().pursueEnabled) || (this.GetComponent<Boid> ().offsetPursueEnabled)) {
			if (!this.GetComponent<Boid> ().groupMember)
			{
			if (DamageTime <= 0) {
				Transform child = transform.FindChild("Spawn");
				GameObject Bullet = (GameObject)Instantiate (BulletPrefab, child.position, transform.rotation); 
				Bullet.rigidbody.AddForce (transform.forward * bulletspeed, ForceMode.Impulse);
				DamageTime += CoolDown;
				audio.Play();
				
			}
			if (DamageTime > 0)
				DamageTime -= Time.deltaTime;
			else if (DamageTime <= 0)
				DamageTime = 0;

			}			
		}
	}
}
