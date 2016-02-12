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

	// Update is called once per frame
	void Update () {
        CheckForFlip();
        Move();
	}
    
    void CheckForFlip() {
        if (Physics2D.Linecast(sightStart.position, sightEnd.position, solidLayer)!=wantsCollider) {
            this.transform.localScale = new Vector3(-this.transform.localScale.x,
                this.transform.localScale.y, this.transform.localScale.z);
        }
    }

    void Move() {
        this.transform.position = new Vector2(this.transform.position.x + this.transform.localScale.x * speed, this.transform.position.y);
    }
}
