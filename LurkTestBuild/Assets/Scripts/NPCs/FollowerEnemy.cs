using UnityEngine;
using System.Collections;

public class FollowerEnemy : Enemy {

    Rigidbody body;
    [SerializeField]
    float aggroDistance = 10f;
    [SerializeField]
    float jump = 5000f;
	// Use this for initialization
	void Start ()
    {
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        //follow character jumping
        if (transform.position.y < aggro.transform.position.y && this.isGrounded())
        {
            RaycastHit hit;
            Vector3 diag = new Vector3(1,1,0);
            if (Physics.Raycast(transform.position, diag, out hit, 5.0f))
            {
                if (hit.collider.gameObject.tag == "Platform")
                {
                    body.velocity = Vector2.up * jump;
                    Debug.Log(body.velocity.y);
                }
            }
        }

        //follow character x 
        float distances = Mathf.Abs((aggro.transform.position.x - transform.position.x));
	    if(distances < aggroDistance)
        {
            if (distances < .000001f)
            {
                transform.position = aggro.transform.position;
            }
            else
            {
                Vector3 direction = new Vector3(aggro.transform.position.x - transform.position.x, 0, 0);
                direction.Normalize();
                transform.Translate(direction * Time.deltaTime);
            }
        }
	}

    bool isGrounded()
    {
        if(Mathf.Abs(body.velocity.y) < .01f)
        {
            return true;
        }
        return false;
    }
}
