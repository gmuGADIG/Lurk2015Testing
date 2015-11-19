using UnityEngine;
using System.Collections;

public class scratch_flammable : MonoBehaviour {

	//Will the object be spawned lit or not?
	public bool lit = false;
	//This is whatever light object happens to be attached to your flammable object.
	Animator fireLight;

	void Start () {
		fireLight = GetComponent<Animator> ();
	}
	
	void Update () {
		//If your object is not lit, neither is the light, and vice versa.
		fireLight.enabled = this.lit;
		//Also make calls here to whatever graphical or instantiation is necessary (Animations, lights, etc).
	}

	/* Important shizz regarding how these scripts work:
	 * 1. Any object that you attach this script to needs to have a 2D Collider that is set to Trigger.
	 * 	This can be in addition to regular collider that has physics on it. I prefer that the Trigger 
	 *  collider be slightly bigger than the regular collider (just add .01 to each of the dimensions),
	 *  so any collision will also set off the trigger.
	 * 2. Remember the regular rules for triggers: At least one of the colliders needs a RigidBody to
	 *  set it off.
	 * 3. If you want an object that is only PARTIALLY flammable (ie. you want a lamppost where only 
	 * 	the lamp part catches fire, then you need to make the trigger collider fit over the important
	 * 	part of the object, again flaring a tiny bit out to make sure it catches collisions. If you
	 * 	have a situation where you need multiple trigger based scripts, then it might be a better
	 *  idea to create an empty gameObject, child it to the gameObject that is meant to be flammable,
	 * 	and give the empty child the flammable script and its own trigger collider.
	 *  Since this is usually tailored to the object in question, you need to judge for yourself
	 * 	what a good size is.
	 */
	void OnTriggerEnter2D(Collider2D col){
		scratch_extinguish snuff = col.gameObject.GetComponentInChildren<scratch_extinguish> ();
		if(snuff && this.lit == true){
			this.lit = false;
			fireLight.enabled = false;
			//This is so extinguishing takes precedence over being lit.
			return;
		}

		scratch_flammable lighter = col.gameObject.GetComponent<scratch_flammable>();
		
		if(lighter && lighter.lit == true && this.lit != true) {
			this.lit = true;
		}
	}
}