using UnityEngine;

public class DamageOnCollision : MonoBehaviour {

	public int damage;

	void OnCollisionEnter2D(Collision2D collision) {
		Damageable hitbox = collision.gameObject.GetComponent<Damageable>();
		if (hitbox != null) {
			hitbox.TakeDamage(damage);
		}
	}

}