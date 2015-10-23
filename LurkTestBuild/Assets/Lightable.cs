using UnityEngine;
using System.Collections;

public class Lightable : MonoBehaviour {

	GameObject light_sprite;
	public bool lit = false;
	//Make this an array of objects that will light it? Arrows, etc.

	// Use this for initialization
	void Start () {
		light_sprite = Instantiate();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collider other){
		Lantern lighter = other.gameObject.GetComponent<Lantern> ();
		if (lighter != null) {
			//Turn on the light

			//Instantiate a clone of the Lantern Light
			//set it to the right transform
		}
	}
}
