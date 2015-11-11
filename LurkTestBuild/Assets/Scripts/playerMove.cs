using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic; // For List

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

    private int triggerCount;

    // Use this for initialization
    void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
		cam = Camera.main.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        // Get horizontal movement
		float horizontal = Input.GetAxis ("Horizontal");

        // Check if climbing ladder
		if (onLadder) {
			horizontal = 0;
            if(Input.GetAxis("Jump") > 0.01){
                // Jump off of ladder
                offLadder();
                jump();
            }
		}

        // Apply movement velocity
		rb.velocity = new Vector2(Mathf.Clamp(horizontal * accel, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.y, fallClamp, 9999));

        // Check for ground collision
        Collider2D[] colResults = new Collider2D[1];
		grounded = Physics2D.OverlapAreaNonAlloc(top_left.position, bottom_right.position, colResults, ground_layers);
        
        // Jump
		if(Input.GetAxis("Jump") > 0.01 && grounded > 0){
            jump();
		}
	}


    void OnTriggerEnter2D(Collider2D col) {
        // Keep track of trigger count
        triggerCount++;
    }

    void OnTriggerStay2D(Collider2D col){
        // Happens the first time the player gets on the ladder
		if (!onLadder && col.transform.tag == "Ladder" && Mathf.Abs(Input.GetAxis("Vertical")) > 0.01) {
			// Get on ladder
			Vector3 ladderPos = col.transform.position;
			ladderPos.y = transform.position.y;
			transform.position = ladderPos;
			onLadder = true;
            rb.gravityScale = 0;
            fallClamp = 0;
		}

        // Player is on the ladder currently
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
        // Keep track off all collisions
        triggerCount--;

        // Player leaves ladder from top or bottom
        if (onLadder && col.transform.tag == "Ladder" && triggerCount == 0 && rb.velocity.y <= 0) {
            // allColiders.Count == 0 prevents falling between leaving one ladder
            // and then entering another on the next frame
            // Essentially: we must be sure we aren't on ANY collider anymore
            offLadder();
		}else if(rb.velocity.y > 0 && onLadder && triggerCount == 0) {
            // Prevent "hop" at top of ladder
            // Stop further movement
            rb.velocity = Vector2.zero;
            // Snap to top of ladder
            float coliderExtentY = col.bounds.extents.y;
            float playerExtentY = rb.GetComponent<Collider2D>().bounds.extents.y;
            transform.position = new Vector3(transform.position.x, col.transform.position.y + coliderExtentY + playerExtentY);
        }
    }

    // Disconnect from ladder and reapply physics
    void offLadder() {
        onLadder = false;
        rb.gravityScale = 1;
        fallClamp = -9999;
    }

    // Jump
    void jump() {
        rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
    }
}
