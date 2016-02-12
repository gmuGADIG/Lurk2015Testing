using UnityEngine;
using System.Collections;

public class Respawnable : MonoBehaviour {

	//original position on the map
	private Vector3 originalPosition;
	//initial game time the object exited the screen
	private float StartTimeOffScreen;
	//width of the objects BoxCollider2D
	private float width;
	//height of the objects BoxCollider2D
	private float height;
	//boolean to tell whether the object is off screen
	private bool offScreen = false;
	//boolean used to tell if the object has been dropped by a player
	private bool dropped;
	//used to tell the amount of time the object has been dropped
	private float timeDropped;

	void Start () {
	
		//get the original position the object starts in
		originalPosition = new Vector3(transform.position.x, transform.position.y);
		//get the BoxCollider2D of the object
		BoxCollider2D bc2d = GetComponent<BoxCollider2D>();

		//get the width of the BoxCollider2D
		width = bc2d.bounds.size.x;
		//get the height of the BoxCollider2D
		height = bc2d.bounds.size.y;
	}
	
	void Update () {

		//get the upper right corner of the BoxCollider2D
		Vector3 uR = new Vector3(transform.position.x + width / 2, transform.position.y + height / 2);
		//get the lower left corner of the BoxCollier2D
		Vector3 dL = new Vector3(transform.position.x - width / 2, transform.position.y + height / 2);

		//if the object is not on the screen
		if (Camera.main.WorldToViewportPoint (uR).x < 0 ||
			Camera.main.WorldToViewportPoint (uR).y < 0 ||
			Camera.main.WorldToViewportPoint (dL).x > 1 ||
			Camera.main.WorldToViewportPoint (dL).y > 1) {

			//the first update called since the object has been off screen
			if (offScreen == false) {
				offScreen = true;
				//set the time off screen to the current game time
				StartTimeOffScreen = Time.time;
			}

			//if it has been off the screen for more than 10 seconds
			if (Time.time - StartTimeOffScreen >= 10 && offScreen == true) {
				//respawn it
				Respawn();
			}

		//if it on the screen
		} else {
			offScreen = false;
		}

		//if it has been droppped for 10 seconds
		if(dropped == true && Time.time - timeDropped >= 10){
			//repsawn it
			Respawn();
		}
	}

	/**
	 * Resets all the booleans to false and puts the object
	 * back at it original position on the map
	 */
	void Respawn(){
		//set the object back to its original position
		transform.position = originalPosition;
		//remove all forces form the object
		GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		//reset all booleans to false
		dropped = false;
		offScreen = false;
	}

	/**
	 * Called when the player drops the object
	 */ 
	void Dropped(){
		dropped = true;
		//set timeDropped to this second of the game
		timeDropped = Time.time;
	}

	/**
	 * Called when the player picks up the object
	 */
	void PickedUp(){
		dropped = false;
	}
}
