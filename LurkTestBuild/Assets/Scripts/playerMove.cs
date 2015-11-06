using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerMove : MonoBehaviour {

	public float maxSpeed = 10;
	public float accel = 10;
	public float decel = 1.2f;
	public float jumpStrength = 20;
	public float ladderClimbSpeed = 2f;

	public int coins = 0;

	public Transform top_left;
	public Transform bottom_right;
	public LayerMask ground_layers;
	public int grounded = 0;

	private Rigidbody2D rb;
	private GameObject cam;
	private bool onLadder = false;
	private int fallClamp = -9999;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
		cam = Camera.main.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis ("Horizontal");
		if (onLadder) {
			horizontal = 0;
		}
		rb.velocity = new Vector2(Mathf.Clamp(horizontal * accel, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.y, fallClamp, 9999));
        Collider2D[] colResults = new Collider2D[1];
		grounded = Physics2D.OverlapAreaNonAlloc(top_left.position, bottom_right.position, colResults, ground_layers);
		if(Input.GetAxis("Jump") > 0.01 && grounded > 0){
			rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
		}
	}


	void OnTriggerStay2D(Collider2D col){
		if (!onLadder && col.transform.tag == "Ladder" && Mathf.Abs(Input.GetAxis("Vertical")) > 0.01) {
			// Get on ladder
			Vector3 ladderPos = col.transform.position;
			ladderPos.y = transform.position.y;
			transform.position = ladderPos;
			onLadder = true;
			rb.isKinematic = true;
			fallClamp = 0;
		}
		if (onLadder && col.transform.tag == "Ladder"){
			float climb = 0;
			if(Input.GetAxis("Vertical") > 0.01){
				// go up
				climb = ladderClimbSpeed;
			}else if (Input.GetAxis("Vertical") < -0.01){
				//go down
				climb = -ladderClimbSpeed;
				fallClamp = -9999;
			}
			rb.velocity = new Vector2(0, climb);
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (onLadder && col.transform.tag == "Ladder") {
			onLadder = false;
			rb.isKinematic = false;
			fallClamp = -9999;
		}
	}
}
