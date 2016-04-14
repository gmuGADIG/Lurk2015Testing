﻿using UnityEngine;
using System.Collections;

public class Lantern : Item {

    private Transform player;
    private bool held;
    public Vector3 lanternPos = new Vector3(0, 1, 0);
	Rigidbody2D rb;

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
        if(held)
        {
            Vector3 tar = player.transform.position + lanternPos;
            this.transform.position = Vector3.Lerp(transform.position, tar, .1f);
        }
	}
    public void SetItemState(bool state)
    {
        col.enabled = state;
        rb2d.isKinematic = !state;
        held = !state;
    }

    public void SetTransform(Transform p)
    {
        player = p;
    }
}
