using UnityEngine;
using System.Collections;

/// <summary>
/// Each item has this script (or an extended version of this script)
/// Weapons will extend this to have a specific action
/// </summary>

public class Item : MonoBehaviour
{
	// True when dropped, false when in inventory
	public bool isVisible = true;
	// Cache the sprite renderer
	SpriteRenderer sr;
	// Cache the collider
	Collider2D col;
	// Cache the rigidbody2d
	Rigidbody2D rb2d;

	public void Start (){
		sr = GetComponent <SpriteRenderer>();
		col = GetComponent <Collider2D>();
		rb2d = GetComponent <Rigidbody2D>();
	}

	public void Update(){
		if (isVisible) {
			// Item is on the ground and visible
			sr.material.color = new Color(1, 1, 1, 1);
		} else {
			// Item is in inventory
			sr.material.color = new Color(1, 1, 1, 0);
		}
	}

	public virtual bool UseItem(){
		// Does nothing by default.
		// Should be overridden in the extended script.
		// True = sucess, false = fail
		return true;
	}
	
	public void SetItemState(bool state){
		isVisible = state;
		col.enabled = state;
		rb2d.isKinematic = !state;
	}
}
