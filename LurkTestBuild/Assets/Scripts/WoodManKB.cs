using UnityEngine;
using System.Collections;

public class WoodManKB : MonoBehaviour {

	public float knockbackImpulse = 15;

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			Rigidbody2D rgbd = coll.gameObject.GetComponent<Rigidbody2D>();
            Debug.Log((rgbd.transform.position - transform.position).normalized * knockbackImpulse);
            rgbd.AddForce(((Vector2)(rgbd.transform.position - transform.position).normalized )* knockbackImpulse, ForceMode2D.Force);
		}

	}

}
