using UnityEngine;
using System.Collections;

public class scratch_extinguish : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D col){
		scratch_flammable lighter = col.gameObject.GetComponent<scratch_flammable>();
		if(lighter && lighter.lit == true) {
			lighter.lit = false;
			return;
		}
	}
}
