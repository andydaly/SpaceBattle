using UnityEngine;
using System.Collections;

public class SpawnRockets : MonoBehaviour {


	public GameObject SpawnPoint;
	public GameObject RocketPrefab;
	public GameObject SpaceStation;
	public int NumberOfRockets = 5;
	public float TimeBetweenRockets = 3.0f;

	private float Timer = 0.0f;
	private int RocketNum = 0;
	private GameObject[] AlienShips;
	private bool StartLanuch = false;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (SpaceStation == null) {
			if (!StartLanuch)
			{
				AlienShips = GameObject.FindGameObjectsWithTag("Alien") as GameObject[];
				StartLanuch = true;
			}
			if (RocketNum < NumberOfRockets)
			{
				if (Timer > TimeBetweenRockets)
				{
						
					GameObject rocket = (GameObject)Instantiate(RocketPrefab, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
					rocket.GetComponent<Boid>().pursueEnabled = true;
					rocket.GetComponent<Boid>().pursueTarget = AlienShips[RocketNum];
					rocket.GetComponent<RocketShot>().TempRockets = false;
					RocketNum++;
					Timer = 0.0f;

				}


				Timer += Time.deltaTime;
			}

		}
	}
}
