using UnityEngine;

public class ProjectileDamager : Damager {

	//projectile only does damage if moving faster than this threshold
	public float velocityThreshold = 0;

	Rigidbody2D body;

	void Awake() {
		body = GetComponent<Rigidbody2D>();
	}

	public override int GetDamage(Damageable target) {
		if (body.velocity.sqrMagnitude > (velocityThreshold * velocityThreshold)) {
			return damage;
		}
		else {
			return 0;
		}
	}

}