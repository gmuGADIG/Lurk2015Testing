using UnityEngine;
using System.Collections;

public class SwatController : MonoBehaviour {

	//the speed of the swatter
	public float speed = 8f;
	//the distance the swater travels, set in the swat spawner
	[HideInInspector] public float distance;

	//the initial x starting position of the swatter
	private float startX;

	void Start () {

		//set startX to the x starting float
		startX = transform.position.x;
		//set the velocity of the swatters rigidbody2d to speed
		GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
	}

	void Update () {
	
		//if it has travelled its assigned distance, destroy it
		if(Mathf.Abs(transform.position.x - startX) > distance){
			Destroy(gameObject);
		}
	}
}