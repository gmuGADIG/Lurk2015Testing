using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerMove : MonoBehaviour {

	public float maxSpeed = 10;
	public float accel = 10;
	public float decel = 1.2f;
	public float jumpStrength = 20;

	public int coins = 0;

	public Transform top_left;
	public Transform bottom_right;
	public LayerMask ground_layers;
	public int grounded = 0;

	private Rigidbody2D rb;
	private GameObject cam;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
		cam = Camera.main.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis ("Horizontal");
		//rb.AddForce (new Vector2 (Mathf.Clamp(horizontal * accel, -maxSpeed, maxSpeed), 0));
        //rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed) / decel, rb.velocity.y);
        rb.velocity = new Vector2(Mathf.Clamp(horizontal * accel, -maxSpeed, maxSpeed), rb.velocity.y);
        Collider2D[] colResults = new Collider2D[1];
		grounded = Physics2D.OverlapAreaNonAlloc(top_left.position, bottom_right.position, colResults, ground_layers);
		if(Input.GetAxis("Vertical") > 0.01 && grounded > 0){
			rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
		}
	}

	/*void LateUpdate(){
		Vector3 targetPos = transform.position; //+ new Vector3(rb.velocity.x, rb.velocity.y, 0);
		targetPos.z = -10;
		cam.transform.position = Vector3.Lerp (cam.transform.position, targetPos, Time.deltaTime*8);
	}*/
}
