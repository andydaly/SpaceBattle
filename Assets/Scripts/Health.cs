using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public GameObject Explosion;

	private float healthNum = 100.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (healthNum <= 0.0f)
		{
			Instantiate(Explosion, transform.position,transform.rotation);

			Destroy(this.gameObject);

		}

	}

	public void AdjustHealth(float amount)
	{
		healthNum += amount;

	}


	public float healthPoints()
	{
		return healthNum;
	}
}
