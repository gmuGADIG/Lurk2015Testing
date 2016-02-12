using UnityEngine;
using System.Collections;

public class scratch_map_bounds_checker : MonoBehaviour {
	//What do you want to respawn from off of the map?
	public GameObject respawnable;
	//Coordinates to respawn at.
	public float spawnX, spawnY, spawnZ;
	//Time delay between object triggering a map_bounds trigger, and actually respawning.
	public float respawn_delay = 10;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void respawn(){
		Transform respawn_loc = respawnable.GetComponent<Transform>();
		respawn_loc.position = new Vector3(spawnX, spawnY, spawnZ);
		Rigidbody2D respawn_moves = respawnable.GetComponent<Rigidbody2D>();
		respawn_moves.velocity = new Vector2 (0, 0);
	}
}