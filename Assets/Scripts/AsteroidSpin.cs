using UnityEngine;
using System.Collections;

public class AsteroidSpin : MonoBehaviour {

	public float rotationsPerMinute = 3.0f;
	int RotDirection = 0;
	// Use this for initialization
	void Start () {
		RotDirection = Random.Range (1, 7);
	}
	
	// Update is called once per frame
	void Update () {
		if (RotDirection == 1) {
			transform.Rotate (6.0f * rotationsPerMinute * Time.deltaTime, 0, 0);
		} 
		else if (RotDirection == 2) {
			transform.Rotate (0, 6.0f * rotationsPerMinute * Time.deltaTime, 0);
		}
		else if (RotDirection == 3) {
			transform.Rotate (0,0, 6.0f * rotationsPerMinute * Time.deltaTime);
		}
		else if (RotDirection == 4) {
			transform.Rotate (6.0f * rotationsPerMinute * Time.deltaTime, 6.0f * rotationsPerMinute * Time.deltaTime, 0);
		}
		else if (RotDirection == 5) {
			transform.Rotate (6.0f * rotationsPerMinute * Time.deltaTime, 0, 6.0f * rotationsPerMinute * Time.deltaTime);
		}
		else if (RotDirection == 6) {
			transform.Rotate (0, 6.0f * rotationsPerMinute * Time.deltaTime, 6.0f * rotationsPerMinute * Time.deltaTime);
		}
	}
}
