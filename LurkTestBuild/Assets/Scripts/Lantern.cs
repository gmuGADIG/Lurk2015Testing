using UnityEngine;
using System.Collections;

public class Lantern : Item {

    private Transform player;
    private bool held;
    public Vector3 lanternPos = new Vector3(0, 1, 0);
	Rigidbody2D rb;
	private Vector2 lastVel;

	// Use this for initialization
	void Start ()
    {
        base.Start();
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update ()
    {
        base.Update();
        if (held) {
			Vector3 tar = player.transform.position + lanternPos;
			rb.position = Vector3.Lerp (transform.position, tar, .5f);
		}
		if (rb.velocity.y > 0 && lastVel.y < 0) {
			StopRotation();
		}
		lastVel = rb.velocity;
	}
    public void SetItemState(bool state)
    {
        col.enabled = state;
        rb2d.isKinematic = !state;
        held = !state;
		if(state){
			StopRotation();
		}
    }

	private void StopRotation(){
		foreach (Transform child in transform.parent) 
		{
			if (child.name == "Lantern") // Find the physical lantern
			{
				// Stop rotation
				child.transform.rotation = Quaternion.identity;
				child.GetComponent<Rigidbody2D>().angularVelocity = 0;
			}
		}
	}

    public void SetTransform(Transform p)
    {
        player = p;
    }
}
