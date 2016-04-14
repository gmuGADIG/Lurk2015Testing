using UnityEngine;

public class DamageOnCollision : MonoBehaviour {

	public int damage;

	public string noDamageTag = "Player";

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag != noDamageTag) {
			Damageable hitbox = collision.gameObject.GetComponent<Damageable>();
			if (hitbox != null) {
				hitbox.TakeDamage(damage);
			}
		}
	}

}