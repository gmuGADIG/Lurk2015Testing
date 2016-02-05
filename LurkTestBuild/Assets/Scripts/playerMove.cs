using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* +------------------------+
 * |Controls from design doc|
 * +------------------------+
 * Left/right arrow...move left/right
 * Space..............jump
 * Down + X...........pick up/drop item
 * Z..................use item/attack
 * 
 * C (held)...........show aim line
 * C + X..............throw item
 * Down + Left/right..crouch
 * Up.................Interact/climb ladder
 * */

public class playerMove : MonoBehaviour {

	public float maxSpeed = 10;
	public float accel = 10;
	public float decel = 1.2f;
	public float jumpStrength = 20;
	public float ladderClimbSpeed = 2f;
	public bool gender = true; //Male is true

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


	private Sprite defaultSprite;
	public Sprite crouchSprite;
	private bool crouching = false;

	private bool direction = true;
    public bool getDirection() { return direction; }

	private Animator animator;
	private Inventory inventory;
	private float initialGravity;

	// How close to pick up objects
	public float pickupDistance = 1f;
	// Is space/jump down
	private bool jumpPressed = false;
	// Slow down when crouching
	public float crouchPenalty = 2;

    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		inventory = GetComponent<Inventory> ();
		cam = Camera.main.gameObject;
		defaultSprite = GetComponent<SpriteRenderer> ().sprite;
		initialGravity = rb.gravityScale;
	}
	
	// Update is called once per frame
	void Update () {

		float horizontal = Input.GetAxis ("Horizontal");
		if (horizontal > 0) {
			direction = true;
			transform.localScale = new Vector3(1, 1, 1);
		} else if (horizontal < 0) {
			direction = false;
			transform.localScale = new Vector3(-1, 1, 1);
		}
		if (onLadder) {
			horizontal = 0;
			if(Input.GetAxis("Jump") > 0.01 && jumpPressed == false){
                // Jump off of ladder
                offLadder();
                jump();
            }
		}

		// Pickup/drop items
		if (Input.GetAxis ("Vertical") < -0.01 && Input.GetAxis ("Fire2") > 0.01) {
			// Get items
			GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
			GameObject closestItem = null;
			float closestDist = Mathf.Infinity;

			foreach(GameObject item in items){
				// If item is within reach
				float itemDistance = Vector3.Distance(transform.position, item.transform.position);

				if (itemDistance < pickupDistance){
					if(closestItem == null || itemDistance < closestDist){
						closestItem = item;
						closestDist = itemDistance;
					}
				}
			}

			if (closestItem){
				if(inventory.Pickup(closestItem)){
					Destroy(closestItem);
				}
			}
		}

        // Apply movement velocity
		rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + (horizontal * accel), -maxSpeed, maxSpeed)/((crouching ? crouchPenalty : 1)), Mathf.Clamp(rb.velocity.y, fallClamp, 9999));

        // Check for ground collision
        Collider2D[] colResults = new Collider2D[1];
		grounded = Physics2D.OverlapAreaNonAlloc(top_left.position, bottom_right.position, colResults, ground_layers);
		if (Input.GetAxis ("Jump") > 0.01) {
			if (grounded > 0 && jumpPressed == false) {
				jump ();
			}
		}

		if (grounded > 0 && Input.GetAxis ("Vertical") < -0.01) {
			// Crouch
			animator.SetBool("crouching", true);
			crouching = true;
			GetComponent<BoxCollider2D>().size = new Vector2(1, 0.6f);
			GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.2f);
		} else {
			animator.SetBool("crouching", false);
			crouching = false;
			GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
			GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
		}
		// Update jump button state
		if (Input.GetAxis ("Jump") > 0.01) {
			jumpPressed = true;
		} else {
			jumpPressed = false;
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
		rb.gravityScale = initialGravity;
		fallClamp = -9999;
	}
	
	// Jump
	void jump() {
		rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
	}

	bool getGender(){
		return gender;
	}

}
