using UnityEngine;
using System.Collections;

public class scratch_gas_pocket : MonoBehaviour {

	public float bomb_radius = 3;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.GetComponent<scratch_flammable> ().lit) {
			this.ignite();
		}
	}

	void ignite(){
		//Play the explosion animation

		//Find out what player objects are close by, and kill them.
		Vector3 thispos = this.gameObject.transform.position;
		Collider2D[] affected = Physics2D.OverlapCircleAll (new Vector2(thispos.x, thispos.y), bomb_radius);
		foreach (Collider2D target in affected) {
			if(target.gameObject.GetComponent<playerMove>()){
				Destroy (target.gameObject);
			}
		}
		//Then destroy this object.
		Destroy(this);
	}
}

//Gas explodes once, then slowly filters back in? Temporary hazard clearance?
	//Needs a recharge timer, and a way to indicate that it's back.
//Gas explodes once, and destroys the object?
//Should be able to pick one or the other.