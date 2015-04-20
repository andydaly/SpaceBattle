using UnityEngine;
using System.Collections;

public class Formation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ReturnToFormation()
	{
		this.gameObject.GetComponent<Boid>().groupMember = true;
	}

}
