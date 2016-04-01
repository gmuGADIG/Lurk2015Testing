using UnityEngine;
using System.Collections;

public class Lantern : Item {

    Transform player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void SetItemState(bool state)
    {
        this.transform.position = player.transform.position;
        col.enabled = state;
        rb2d.isKinematic = !state;
    }

    public void SetTransform(Transform p)
    {
        player = p;
    }
}
