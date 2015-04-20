using UnityEngine;
using System.Collections;

public class BattleManager : MonoBehaviour {

	private GameObject[] Harriers;
	private GameObject[] AlienShips;

	public float TimeTillCommence = 10.0f;
	public float TimeTillRefresh = 10.0f;
	private float Timer = 0.0f;
	private bool BattleCommence = false;
	private bool Started = false;
	private bool BattleRunthrough = false;
	private bool gameComplete =false;
	private bool TurnOnDestroy = false;
	// Use this for initialization
	void Start () {
		Harriers = GameObject.FindGameObjectsWithTag("Harrier") as GameObject[];
		AlienShips = GameObject.FindGameObjectsWithTag("Alien") as GameObject[]; 



	}
	
	// Update is called once per frame
	void Update () {

		if (!Started) {
			if (Timer > TimeTillCommence) {
				BattleCommence = true;
				Started = true;
			}
			Timer += Time.deltaTime;
		}

		if (BattleCommence) {

			RefreshShips();
			BattleCommence = false;
			BattleRunthrough = true;
		}

		if (BattleRunthrough)
		{
			if (!gameComplete)
			{
			for (int i = 0; i < Harriers.Length; i++)
			{

				if (Harriers[i] == null)
				{
					Harriers = GameObject.FindGameObjectsWithTag("Harrier") as GameObject[];
						if ((Harriers.Length==0)||(Harriers==null))
						{
							gameComplete = true;
							StopAllAliens();
						}
						else if (Harriers != null)
					{
						RefreshShips();
					}
					
					
					break;
				}
			}
			
			for (int i = 0; i < AlienShips.Length; i++)
			{
				if (AlienShips[i] == null)
				{
					AlienShips = GameObject.FindGameObjectsWithTag("Alien") as GameObject[];
						if ((AlienShips.Length==0)|| (AlienShips == null))
						{
							gameComplete = true;
							StopAllHarriers();
						}

						else if (AlienShips != null)
					{
						RefreshShips();
					}
					
					
					break;
				}
			}

			if (Timer > TimeTillRefresh)
			{
				RefreshShips();
				Debug.Log("refes");
				Timer =0.0f;
				}

				Timer += Time.deltaTime;
			}
			else
			{
				if (!TurnOnDestroy)
				{
				GameObject mothership = GameObject.FindGameObjectWithTag("Finish");
				mothership.GetComponent<DestroyEarth>().enabled = true;
					TurnOnDestroy = true;
				}
			}
		}


	}


	void StopAllAliens()
	{
		for (int i = 0; i < AlienShips.Length; i++) {
			AlienShips[i].GetComponent<Boid>().offsetPursueEnabled = false;
			AlienShips[i].GetComponent<Boid>().pursueEnabled = false;
		}
	}


	void StopAllHarriers()
	{
		for (int i = 0; i < Harriers.Length; i++) {
			Harriers[i].GetComponent<Boid>().offsetPursueEnabled = false;
			Harriers[i].GetComponent<Boid>().pursueEnabled = false;
				}
	}

	void RefreshShips()
	{
		Vector3 offset = new Vector3 (0,0,-5);

		for (int i = 0; i < Harriers.Length; i++)
		{
			if (Harriers[i] == null)
			{
				Harriers = GameObject.FindGameObjectsWithTag("Harrier") as GameObject[];
				break;
			}
			int PursueType = Random.Range(1,3);
			int ShipNum = Random.Range(0, AlienShips.Length);
			if (PursueType == 1)
			{
				Harriers[i].GetComponent<Boid>().pursueEnabled = true;
				Harriers[i].GetComponent<Boid>().offsetPursueEnabled = false;
				Harriers[i].GetComponent<Boid>().pursueTarget = AlienShips[ShipNum];
			}
			else
			{
				Harriers[i].GetComponent<Boid>().pursueEnabled = false;
				Harriers[i].GetComponent<Boid>().offsetPursueEnabled = true;
				Harriers[i].GetComponent<Boid>().offset =offset;
				Harriers[i].GetComponent<Boid>().offsetPursueTarget = AlienShips[ShipNum];
			}


		}
		
		for (int i = 0; i < AlienShips.Length; i++)
		{
			if (AlienShips[i] == null)
			{
				AlienShips = GameObject.FindGameObjectsWithTag("Alien") as GameObject[];
				break;
			}
			int PursueType = Random.Range(1,3);
			int ShipNum = Random.Range(0, Harriers.Length);
			AlienShips[i].GetComponent<Boid>().groupMember = false;
			AlienShips[i].GetComponent<Boid>().obstacleAvoidanceEnabled = true;
			if (PursueType == 1)
			{
				AlienShips[i].GetComponent<Boid>().pursueEnabled = true;
				AlienShips[i].GetComponent<Boid>().offsetPursueEnabled = false;
				AlienShips[i].GetComponent<Boid>().pursueTarget = Harriers[ShipNum];
			}
			else
			{
				AlienShips[i].GetComponent<Boid>().pursueEnabled = false;
				AlienShips[i].GetComponent<Boid>().offsetPursueEnabled = true;
				AlienShips[i].GetComponent<Boid>().offset =offset;
				AlienShips[i].GetComponent<Boid>().offsetPursueTarget = Harriers[ShipNum];
			}

		}
	}

	public bool GameComplete()
	{
		return gameComplete;
	}


}
