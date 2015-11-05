using UnityEngine;
using System.Collections;

public class scratch_flammable : MonoBehaviour {

	//Will the object be spawned lit or not?
	public bool lit = false;
	Collider2D[] sources;
	Collider2D hotspot;
	Light fireLight;

	void Start () {
		fireLight = (Light)this.gameObject.GetComponent<Transform>().Find ("FlameLight").GetComponent("Light");
		hotspot = this.GetComponent<Collider2D>();
	}
	
	void Update () {
		fireLight.enabled = this.lit;
		//Also make calls here to whatever graphical or instantiation is necessary (Animations, lights, etc).
	}

	void OnTriggerEnter2D(Collider2D col){
		scratch_extinguish snuff = col.gameObject.GetComponentInChildren<scratch_extinguish> ();
		if(snuff && this.lit == true){
			this.lit = false;
			fireLight.enabled = false;
			//This is so extinguishing takes precedence over being lit.
			return;
		}

		Debug.Log (col.gameObject);
		Debug.Log (col.gameObject.GetComponent<scratch_flammable>());
		scratch_flammable lighter = col.gameObject.GetComponent<scratch_flammable>();
		
		if(lighter && lighter.lit == true && this.lit != true) {
			this.lit = true;
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		scratch_extinguish snuff = col.gameObject.GetComponentInChildren<scratch_extinguish> ();
		if(snuff && this.lit == true){
			this.lit = false;
			fireLight.enabled = false;
			//This is so extinguishing takes precedence over being lit.
			return;
			Debug.Log ("This shit don't work right!");
		}

		scratch_flammable lighter = col.gameObject.GetComponent<scratch_flammable>();

		if(lighter && lighter.lit == true && this.lit != true) {
			this.lit = true;
			Debug.Log ("I changed light status! "+this.gameObject.ToString ());
		}
	}


}