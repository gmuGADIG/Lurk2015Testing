using UnityEngine;
using System.Collections;

public class scratch_flammable : MonoBehaviour {

	//Will the object be spawned lit or not?
	public bool lit = false;

	void Start () {
		
	}
	
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D col){

		//Are the two hotspots touching?
		//Is the other object lit?
		scratch_flammable lighter = col.gameObject.GetComponent<scratch_flammable>();
		if(lighter && lighter.lit == true) {
			this.lit = true;
			//Also make calls here to whatever graphical or instantiation is necessary (Animations, lights, etc).
			return;
		}
		//Else, if it collides with an extinguisher, it is no longer lit
		scratch_extinguish snuff = col.gameObject.GetComponent<scratch_extinguish>();
		if (snuff) {
			Debug.Log ("Yo!");
		}
		if(snuff && this.lit == true){
			Debug.Log ("Also Yo!");
			this.lit = false;
			//Same as above, make graphical adjustments here.
		}
	}
}
