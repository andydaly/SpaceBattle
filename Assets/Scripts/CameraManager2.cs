using UnityEngine;
using System.Collections;

public class CameraManager2 : MonoBehaviour {

	public Camera camera1;
	public Camera camera2;
	public Camera camera3;
	/*public Camera AlienCam1;
	public Camera AlienCam2;
	public Camera AlienCam3;
	public Camera AlienCam4;
	public Camera AlienCam5;
	public Camera AlienCam6;
	public Camera HarrierCam1;
	public Camera HarrierCam2;
	public Camera HarrierCam3;
	public Camera HarrierCam4;
	public Camera HarrierCam5;*/


	private float Timer = 0.0f;
	private bool ShipsView = false;
	private GameObject[] cameras;

	private int randNum = 0;

	// Use this for initialization
	void Start () {
		camera1.enabled = true;
		camera2.enabled = false;
		camera3.enabled = false;

		cameras = GameObject.FindGameObjectsWithTag("MainCamera") as GameObject[];
		for (int i = 0; i < cameras.Length; i++) {
			cameras[i].camera.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (camera1.enabled) {
			Timer += Time.deltaTime;
			if (Timer > 10.0f)
			{
				camera1.enabled = false;
				camera2.enabled = true;
				Timer = 0.0f;
			}
		}
		else if (camera2.enabled)
		{
			Timer += Time.deltaTime;
			if (Timer > 9.0f)
			{
				camera2.enabled = false;
				cameras[0].camera.enabled = true;
				ShipsView = true;
				Timer = 0.0f;
			}
		}

		if (ShipsView) {

			if (cameras[randNum] == null)
			{
				FixActiveCameras();
			}

			Timer += Time.deltaTime;
			if (Timer > 5.0f)
			{
				cameras[randNum].camera.enabled = false;
				randNum = Random.Range(0, cameras.Length);
				if (cameras[randNum] == null)
				{
					FixActiveCameras();


				}
				else
				{
					cameras[randNum].camera.enabled = true;
				}
				

				Timer = 0.0f;
			}



		}


		if ((this.GetComponent<BattleManager>().GameComplete()) && (!camera3.enabled))
		{
			ShipsView = false;
			camera3.enabled = true;
			for (int i = 0; i < cameras.Length; i++)
			{
				cameras[i].camera.enabled = false;
			}
		}


		if (camera3.enabled) {
			Timer += Time.deltaTime;
			if (Timer > 7.0f)
			{
				Application.LoadLevel("FinalScene");
			}
		}
	}


	void FixActiveCameras()
	{

		cameras = GameObject.FindGameObjectsWithTag("MainCamera") as GameObject[];
		Debug.Log ("num: " + cameras.Length);
		randNum = Random.Range(0, cameras.Length);
		if (cameras [randNum] == null) {
			FixActiveCameras();
		}
		cameras[randNum].camera.enabled = true;
		Timer = 0.0f;
	}
}
