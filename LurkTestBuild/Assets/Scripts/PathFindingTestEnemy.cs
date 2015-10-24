using UnityEngine;
using System.Collections;

public class PathFindingTestEnemy : MonoBehaviour {

    private float JumpTimer = 0.0f;
    private float JumpForce = 1200.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f, 0.0f);

        JumpTimer += Time.deltaTime;
        if(JumpTimer > 2.0f)
        {
            JumpTimer = 0.0f;

            GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpForce);
        }

	}
}
