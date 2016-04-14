using UnityEngine;
using System.Collections;

public class WoodMan : MonoBehaviour {

	public string instantKillTag = "Hammer";
	public string randomDamageTag = "Ranged";
	public int minDamage = 1;
	public int maxDamage = 2;

	Damageable hitbox;

	void OnCollisionEnter2D(Collision2D collision) {
		GameObject source = collision.gameObject;
		if (source.tag == instantKillTag) {
			hitbox.Die();
		}
		else if (source.tag == randomDamageTag) {
			hitbox.TakeDamage(Random.Range(minDamage, maxDamage + 1), Vector2.zero);
		}
	}
	
	// Use this for initialization
	void Start () {
		hitbox = GetComponent<Damageable>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
