using UnityEngine;
using System.Collections;

public class scratch_gas_pocket : MonoBehaviour {

	public float bomb_radius = 3;
	public float bomb_recharge = 5;
	private float last_detonation_time;
	public bool active = true;
	//If one_blast is true, the object will destroy itself after detonating the first time.
	public bool one_blast = false;

	// Use this for initialization
	void Start () {
		last_detonation_time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (active == false) {
			if (Time.time - last_detonation_time >= bomb_recharge) {
				active = true;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (active && other.gameObject.GetComponent<scratch_flammable> ().lit) {
			this.ignite();
			active = false;
			last_detonation_time = Time.time;
		}
	}

	void ignite(){
		//Play the explosion animation

		//Find out what player objects are close by, and kill them.
		Vector3 thispos = this.gameObject.transform.position;
		Collider2D[] affected = Physics2D.OverlapCircleAll (new Vector2(thispos.x, thispos.y), bomb_radius);
		foreach (Collider2D target in affected) {
			if(target.gameObject.tag == "Player"){
				Debug.Log ("Player killed!");
			}
		}
		if (one_blast) {
			DestroyObject (this.gameObject);
		}
	}
}

//Gas explodes once, then slowly filters back in? Temporary hazard clearance?
	//Needs a recharge timer, and a way to indicate that it's back.
//Gas explodes once, and destroys the object?
//Should be able to pick one or the other.