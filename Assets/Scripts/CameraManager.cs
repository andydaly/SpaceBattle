using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	public Camera camera1;
	public Camera camera2;
	public Camera camera3;
	public Camera camera4;
	public Camera camera5;


	float Timer = 0.0f;

	// Use this for initialization
	void Start () {
		camera1.enabled = true;
		camera2.enabled = false;
		camera3.enabled = false;
		camera4.enabled = false;
		camera5.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (camera1.enabled) {

			Timer += Time.deltaTime;
			if (Timer > 12.0f)
			{
				camera1.enabled =false;
				camera2.enabled =true;
				Timer =0.0f;
			}



		
		
		}
		else if (camera2.enabled) {

			if (camera3.transform.parent.GetComponent<AttackFromDistance>().enabled)
			{
				camera2.enabled =false;
				camera3.enabled =true;
			}
		}
		else if (camera3.enabled) {

			if (!camera3.transform.parent.GetComponent<AttackFromDistance>().enabled)
			{
				Timer += Time.deltaTime;
				if (Timer > 7.5f)
				{
				camera3.enabled =false;
				camera4.enabled =true;
					Timer = 0.0f;
				}


			}
		}
		else if (camera4.enabled) {
			
			Timer += Time.deltaTime;
			if (Timer > 14.0f)
			{
				camera4.enabled =false;
				camera5.enabled =true;
				Timer = 0.0f;
			}
				


		}
		else if (camera5.enabled) {
			
			Timer += Time.deltaTime;
			if (Timer > 22.0f)
			{
				Application.LoadLevel("ThirdScene");
				Timer = 0.0f;
			}
			
			
			
		}
	}
}
