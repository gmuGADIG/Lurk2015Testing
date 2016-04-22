using UnityEngine;
using System.Collections;


/* Use this script on a game object that you want to
pace back and forth --- wantsCollider = true if you want to aim this
at the ground to check for an edge, false to check for walls

place empty game objects at sightStart and sightEnd, respectively, this will create the
line that the script uses to check for a solid object.

fill in the layer that you want to check for, this will usually be default
*/
public class BackAndForth : MonoBehaviour {

    public float speed = 1f;
    public bool wantsCollider = false;
    public LayerMask solidLayer;
    public Transform sightStart, sightEnd;
	public float knockbackImpulse = 15;
	Animator animate;

	void Start(){
		animate = GetComponent<Animator> ();
		animate.SetInteger ("Action", 1);
		animate.Play ("ForestBeast Run");
	}
	// Update is called once per frame
	void Update () {
        CheckForFlip();
		if (animate.GetInteger("Action") == 1)
        	Move();
	}
    
    void CheckForFlip() {
        if (Physics2D.Linecast(sightStart.position, sightEnd.position, solidLayer)!=wantsCollider) {
            this.transform.localScale = new Vector3(-this.transform.localScale.x,
                this.transform.localScale.y, this.transform.localScale.z);
        }
    }

    void Move() {
		Debug.Log ("move");
        this.transform.position = new Vector2(this.transform.position.x + this.transform.localScale.x * speed, this.transform.position.y);
		//if (animate.GetInteger (0) == 0)
			//animate.Stop ();
		//animate.SetInteger ("Action", 1);
		if (animate.GetInteger("Action") != 1) {
			Debug.Log("Switch");
			animate.SetInteger ("Action", 1);
			animate.Play ("ForestBeast Run");
		}
    }

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			StartCoroutine (Attack (coll));
		}

	}

	IEnumerator Attack(Collider2D coll){
		//this.transform.position = this.transform.position;
		Debug.Log ("hit");
		animate.SetInteger ("Action", 2);
		animate.Play ("ForestBeast Attack");
		yield return new WaitForSeconds (0.5f);
		Rigidbody2D rgbd = coll.gameObject.GetComponent<Rigidbody2D>();
		Debug.Log((rgbd.transform.position - transform.position).normalized * knockbackImpulse);
		rgbd.AddForce(((Vector2)(rgbd.transform.position - transform.position).normalized )* knockbackImpulse, ForceMode2D.Force);
		yield return new WaitForSeconds (0.5f);
		Move ();
	}
}
