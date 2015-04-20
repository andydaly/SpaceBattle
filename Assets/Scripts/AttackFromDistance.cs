using UnityEngine;
using System.Collections;

public class AttackFromDistance : MonoBehaviour {

	public float distanceFromTarget = 10;
	public GameObject target;
	public GameObject BulletPrefab;
	public float bulletspeed = 100.0f;
	public float CoolDown = 3.0f;
	private float DamageTime = 0.0f;




	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
						Vector3 toTarget = target.transform.position - transform.position;
						float distance = toTarget.magnitude;

						if (distance < distanceFromTarget) {
								transform.GetComponent<Boid> ().pursueEnabled = false;

						}

						if (transform.GetComponent<Boid> ().pursueEnabled == false) {
								transform.LookAt (target.transform.position);
						}


						if (target.GetComponent<Health> ().healthPoints () >= 0f) {
								if (DamageTime <= 0) {

					GameObject Bullet = (GameObject)Instantiate (BulletPrefab, transform.position+(transform.forward*2), transform.rotation); 
										Bullet.rigidbody.AddForce (transform.forward * bulletspeed, ForceMode.Impulse);
										DamageTime += CoolDown;
					audio.Play();
				
								}
			
						}
		
						if (DamageTime > 0)
								DamageTime -= Time.deltaTime;
						else if (DamageTime <= 0)
								DamageTime = 0;
				} else {
		this.gameObject.GetComponent<Boid>().groupMember = true;
			this.gameObject.GetComponent<AttackFromDistance>().enabled = false;
				}

	}
}
