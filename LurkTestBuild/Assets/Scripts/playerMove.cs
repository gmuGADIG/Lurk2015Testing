using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InControl;

/* +----------------------------------------------------------------+
 * |                    Controls from design doc                    |
 * +----------------------------------------------------------------+
 * 
 * [Key]______________[Action]__________________[Controller]_________
 * 
 * Left/right arrow...move left/right...........Stick left/right
 * Space..............jump......................A
 * Down + X...........pick up/drop item.........Down + B
 * Z..................swap item.................B
 * X..................use item..................X
 * 
 * C (held)...........show aim line.............Left Trigger/Bumper
 * C + X..............throw item................Right trigger/Bumper
 * Down + Left/right..crouch....................Stick down
 * Up.................Interact..................Y
 * Up.................Climb (ladder)............Stick Up
 * */

public class playerMove : MonoBehaviour {

	public float speed = 1;
//	public float accel = 10;
//	public float decel = 1.2f;
	public float jumpStrength = 20;
	public float ladderClimbSpeed = 2f;
	public bool gender = true; //Male is true
	public enum Classes {Rogue, Warrior, Mage};
	public Classes pClass = Classes.Rogue;
	public string pName = "Bob";

	public int coins = 0;

	public Transform top_left;
	public Transform bottom_right;
	public LayerMask ground_layers;
	public int grounded = 0;

	private Rigidbody2D rb;
	private bool onLadder = false;
	private int fallClamp = -9999;


    private int triggerCount;

	private bool crouching = false;
	private float initialHeight = 1.5f;

	private bool direction = true;
    public bool getDirection() { return direction; }

	private Animator animator;
	private Inventory inventory;
	private float initialGravity;

	// How close to pick up objects
	public float pickupDistance = 1f;
	// Is space/jump down
	private bool jumpPressed = false;
	// Is x down
	private bool xPressed = false;
	// Is z down
	private bool zPressed = false;
	// Slow down when crouching
	public float crouchPenalty = 2;

	// Item cooldown counter
	private float lastItemUse = 0;



    // Controls to standardize inputs
    // This is important so we don't rely on hardcoded inputs
    // Only call an input directly if it is controller-specific (ie: Y button to interact which replaces Up key)

    // [Generic inputs]_____________________[Key Mapping]__________[Controller Mapping (on 360 controller)]
    public float horizontalInput = 0;   // Left/Right keys........Left analog stick/D pad
    public float verticalInput = 0;      // Up/Down keys...........Left analog stick/D pad
    public float zInput = 0;            // Z......................B
    public float xInput = 0;            // X......................X
    public float cInput = 0;            // C......................Right bumper
    public float jumpInput = 0;         // Space..................A
    // These are public so that other scripts can retrieve the input without getting/setting

    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody2D> ();
		animator = GameObject.Find("Sprite").GetComponent<Animator> ();
		inventory = GetComponent<Inventory> ();
		initialGravity = rb.gravityScale;
		initialHeight = GetComponent<BoxCollider2D> ().size.y;
    }
	
	// Update is called once per frame
	void Update () {
        InputDevice device = InputManager.ActiveDevice;
        InputControl control = device.GetControl(InputControlType.Action1);

        horizontalInput = device.LeftStickX + device.DPadX + Input.GetAxis("Horizontal"); // Add the controls together so either can be used
        verticalInput = device.LeftStickY + device.DPadY + Input.GetAxis("Vertical"); // Add the controls together so either can be used
        zInput = device.Action2 + Input.GetAxis("Z"); // Add the controls together so either can be used
        xInput = device.Action3 + Input.GetAxis("X"); // Add the controls together so either can be used
        cInput = device.RightBumper + Input.GetAxis("C"); // Add the controls together so either can be used
        jumpInput = device.Action1 + Input.GetAxis("Jump"); // Add the controls together so either can be used

        if (horizontalInput > 0) {
			direction = true;
			transform.localScale = new Vector3(1, 1, 1);
		} else if (horizontalInput < 0) {
			direction = false;
			transform.localScale = new Vector3(-1, 1, 1);
		}
		if (onLadder) {
			if(jumpInput > 0.01 && jumpPressed == false){
                // Jump off of ladder
                offLadder();
                jump();
            }
		}

		// Pickup/drop items
		if (verticalInput < -0.01 && xInput > 0.01 && !xPressed) {
			// Get items
			GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
			GameObject closestItem = null;
			float closestDist = Mathf.Infinity;

			foreach(GameObject item in items){
				// If item is within reach
				float itemDistance = Vector3.Distance(transform.position, item.transform.position);

				if (itemDistance < pickupDistance){
					if((closestItem == null || itemDistance < closestDist) && item.GetComponent<Item>().isVisible){
						closestItem = item;
						closestDist = itemDistance;
					}
				}
			}

			if (closestItem){
				//Try to pick up item
				if(inventory.Pickup(closestItem)){
					closestItem.SendMessage("SetItemState", false);
                    closestItem.SendMessage("SetTransform", this.transform, SendMessageOptions.DontRequireReceiver);
				}else{
					// Need to switch items
					GameObject droppedItem = inventory.Drop();
					inventory.Pickup(closestItem);
					closestItem.SendMessage("SetItemState", false);
                    closestItem.SendMessage("SetTransform", this.transform, SendMessageOptions.DontRequireReceiver);

                    droppedItem.SendMessage("SetItemState", true);
				}
			}else{
				// Not near an item, just drop primary item
				GameObject droppedItem = inventory.Drop();
				if(droppedItem){
					droppedItem.SendMessage("SetItemState", true);
				}
			}
		}else if(xInput > 0 && !xPressed && Time.time > lastItemUse){
			// Use item
			float cooldown = inventory.UseItem();
			if(cooldown >= 0){
				// Add the cooldown to prevent spamming weapons
				lastItemUse = Time.time + cooldown;
			}else{
				lastItemUse = Time.time + 0.2f;
			}
		}

		// Swap items between inventory slots
		if (zInput > 0 && !zPressed) {
			inventory.Swap ();
		}

        // Check for ground collision
        Collider2D[] colResults = new Collider2D[1];
		grounded = Physics2D.OverlapAreaNonAlloc(top_left.position, bottom_right.position, colResults, ground_layers);
		if (grounded > 0) {
			//Player is on the gorund
			animator.SetBool ("jumping", false);
			animator.SetBool ("falling", false);
			if (jumpInput > 0.01 && jumpPressed == false) {
				jump ();
			}

		}else if(rb.velocity.y < 0){
			//Player is not on the ground and is falling
			animator.SetBool ("falling", true);
		}
			

		if (grounded > 0 && verticalInput < -0.01) {
			// Crouch
			animator.SetBool("crouching", true);
			crouching = true;
			GetComponent<BoxCollider2D>().size = new Vector2(1, initialHeight * 0.6f);
			GetComponent<BoxCollider2D>().offset = new Vector2(0, initialHeight * -0.2f);
		} else {
			animator.SetBool("crouching", false);
			crouching = false;
			GetComponent<BoxCollider2D>().size = new Vector2(1, initialHeight);
			GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
		}
		animator.SetFloat ("horizontalSpeed", Mathf.Abs(horizontalInput));
		// Update jump button state
		if (jumpInput > 0.01) {
			jumpPressed = true;
		} else {
			jumpPressed = false;
		}
		// Update z state
		if (zInput > 0) {
			zPressed = true;
		} else {
			zPressed = false;
		}
		// Update x state
		if (xInput > 0) {
			xPressed = true;
		} else {
			xPressed = false;
		}
	}

	void FixedUpdate(){
		// Apply movement velocity
		rb.velocity = new Vector2(horizontalInput*speed/(crouching ? crouchPenalty : 1), Mathf.Clamp(rb.velocity.y, fallClamp, 9999));
	}


	void OnTriggerEnter2D(Collider2D col) {
		// Keep track of trigger count
		triggerCount++;
	}
	
	
	void OnTriggerStay2D(Collider2D col){
		// Happens the first time the player gets on the ladder
		if (!onLadder && col.transform.tag == "Ladder" && Mathf.Abs(verticalInput) > 0.01) {
			// Get on ladder
			Vector3 ladderPos = col.transform.position;
			ladderPos.y = transform.position.y;
			//transform.position = ladderPos;
			onLadder = true;
			rb.gravityScale = 0;
			fallClamp = 0;
		}
		if (onLadder && col.transform.tag == "Ladder"){
			float climb = 0;
			if(verticalInput > 0.01){
				// go up
				climb = ladderClimbSpeed;
			}else if (verticalInput < -0.01){
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
		animator.SetBool ("jumping", true);
		grounded = 0; // Not grounded for a minimum of one tick
	}

	bool getGender(){
		return gender;
	}

}
