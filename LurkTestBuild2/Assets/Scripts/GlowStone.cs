using UnityEngine;
using System.Collections;

public class GlowStone : MonoBehaviour {

	private bool goingUp = false;
	public Light thisLight;
	public float maxCharge = 5f;
	public float lightSpeed = 3f;

	// Use this for initialization
	void Start () {
		thisLight = GetComponentInChildren<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(goingUp);
		if (goingUp) {
			thisLight.intensity += lightSpeed*Time.deltaTime;
		} else {
			thisLight.intensity -= lightSpeed*Time.deltaTime;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Lantern") {
				goingUp = true;
				
			}
		}


	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Lantern") {
				goingUp = false;
		}
	}
}
