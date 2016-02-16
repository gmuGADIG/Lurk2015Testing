using UnityEngine;
using System.Collections;

public class SwatSpawner : MonoBehaviour {

	//negative goes left, positive goes right
	public float direction = 1f;
	//the swatter gameobject, NOTE: must have SwatController attached to it
	public GameObject swatter;
	//the interval between swatter spawns
	public float timeToSpawn = 2f;

	//the starting position of the swatter
	private Vector2 startPos;
	//the time the player last touched the platform
	private float timePlatformTouch;
	//determines if the spawn
	private bool canSwat = false;

	void Start () {
	
		//set the starting position of the swatter to the propper side and height
		startPos = new Vector2(gameObject.transform.lossyScale.x / 2 * direction * -1, gameObject.transform.position.y + .8f);
	}
	
	void Update () {
		
		//if is has been timeToSpawn seconds and the swatter can spawn
		if(Time.time - timePlatformTouch > timeToSpawn && canSwat){

			SwatController sc = ((GameObject)Instantiate(swatter, startPos, Quaternion.identity)).GetComponent<SwatController>();

			//set the direction for the swatter to travel in
			sc.speed *= direction;
			//set the distance the swatter travels before it destroys itself
			sc.distance = gameObject.transform.lossyScale.x;

			//prevent the swatter from spawning until the player touches the platform again
			canSwat = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other){

		//if the player is touched the platform, start the time and allow the swatter to spawn
		if(other.CompareTag("Player")){
			timePlatformTouch = Time.time;
			canSwat = true;
		}
	}
}