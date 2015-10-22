using UnityEngine;
using System.Collections;

public class scratch_StSPlatform : MonoBehaviour {

	public Rigidbody2D platform_body;
	public float cycle_time = 5;
	float last_check;
	float cur_check;
	int direction_val = 1;
	// Use this for initialization
	void Start () {
		platform_body = GetComponent<Rigidbody2D>();
		last_check = Time.time;
		platform_body.velocity = new Vector2(1*direction_val, 0);
	}

	// Update is called once per frame
	void Update () {
		cur_check = Time.time;
		//Every y seconds, set the platform to move to the set position
		//Positions should swap every y seconds, in a cycle.
		if(cur_check - last_check >= cycle_time){
			direction_val *= -1;
			platform_body.velocity = new Vector2(1*direction_val, 0);
			last_check = cur_check;
		}
	}
	
	//Methods to control destination points
	//An array of positions?
	
}
