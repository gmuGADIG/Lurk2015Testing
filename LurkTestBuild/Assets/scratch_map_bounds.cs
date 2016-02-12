using UnityEngine;
using System.Collections;

public class scratch_map_bounds : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		scratch_map_bounds_checker respawnable = col.gameObject.GetComponent<scratch_map_bounds_checker> ();
		if (respawnable) {
			respawnable.respawn();
		}
	}
}