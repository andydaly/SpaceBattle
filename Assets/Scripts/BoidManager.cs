using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BoidManager : MonoBehaviour {

	static BoidManager instance;
	Boid[] boids;


	Boid[] members;


	public float radiusX = 6f;
	public float radiusZ = 2.9f;

	[HideInInspector]
	public Obstacle[] obstacles;


	// Use this for initialization
	void Start () {
		instance = this;
		obstacles = GameObject.FindObjectsOfType(typeof(Obstacle)) as Obstacle[];
		boids = GameObject.FindObjectsOfType(typeof(Boid)) as Boid[];



	}
	
	// Update is called once per frame
	void Update () {
		RefreshMembers ();
	}

	void RefreshMembers()
	{
		Vector3 leaderPosition = Vector3.zero;
		GameObject leader = null;

		int num = 0;
		members = new Boid[20];
		for (int i = 0; i < boids.Length; i++) {
			if (boids[i].GroupMember())
			{
				members[num] = boids[i];
				num++;
			}
		}

		for (int i = 0; i < boids.Length; i++) {
			if (boids[i].LeaderInGroup())
			{
				leader = boids[i].gameObject;
				leaderPosition = boids[i].currentPosition;
			}
		}


		for (int i = 0; i < num; i++) {

			members[i].offsetPursueEnabled = true;
			members[i].offsetPursueTarget = leader;
		}

		//Debug.Log (num + "    " + leaderPosition);

		int NumberOfMembers = num;

		float XAdd = 0.0f;
		float ZAdd = 0.0f;
		bool Switch = false;
		

		int RowCount = 0;
		int OddNumber = 0;



		XAdd = radiusX / (NumberOfMembers);
		ZAdd = radiusZ  / (NumberOfMembers);


		float XCount = XAdd;
		float ZCount = radiusZ;
		




		if (IsOdd (NumberOfMembers)) {
			members[0].offset = new Vector3(0,0, ZCount);
			members[0].maxSpeed =12;
			ZCount-=ZAdd;
			XCount+=XAdd;
			OddNumber =1;
		}

		for (int i = 0+OddNumber; i < num; i++) {

				if (Switch)
				{
					members[i].offset = new Vector3(0+XCount,0, ZCount);
					Switch = false;
				}
				else
				{
					members[i].offset = new Vector3(0-XCount,0, ZCount);
					Switch = true;
				}


				if (RowCount ==1)
				{
					XCount+=(XAdd+XAdd);
					ZCount-=(ZAdd+ZAdd);
					RowCount = 0;
				}
				else
				{
					RowCount++;
				}
				
		}



	}




	static bool IsOdd(int value)
	{
		return value % 2 != 0;
	}

	public static BoidManager Instance
	{
		get
		{
			return instance;
		}
	}
}
