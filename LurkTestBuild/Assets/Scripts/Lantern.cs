using UnityEngine;
using System.Collections;

public class Lantern : Item {

    Transform player;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
    }
    override public void SetItemState(bool state)
    {
        this.transform.position = player.transform.position;
        col.isTrigger = !state;
        //col.enabled = state;
        rb2d.isKinematic = !state;
    }

    public void SetTransform(Transform p)
    {
        player = p;
    }
}
