using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour {

	public int health;
	public string damagingTag = "Damaging";
	public bool triggersDealDamage = true;
	public DeathBehaviour deathBehaviour = DeathBehaviour.DoNothing;

	public DamageTakenEvent onTakeDamage;
	public UnityEvent onDeath;

	public void TakeDamage(int damage = 1) {
		TakeDamage(damage, Vector2.zero);
	}

	public void TakeDamage(int damage, Vector2 direction) {
		health -= damage;
		onTakeDamage.Invoke(damage, direction);
		SendMessage("OnTakeDamage", new Damage(damage, direction));
		if (health <= 0) {
			health = 0;
			Die();
		}
	}

	public void TakeDamageFrom(GameObject other) {
		if (other.tag == damagingTag) {
			Vector2 direction = transform.position - other.transform.position;
            Damager damager = other.GetComponent<Damager>();
			TakeDamage(damager == null ? 1 : damager.damage, direction);
			if (damager != null) {
				damager.onDealDamage.Invoke(this, direction);
				damager.SendMessage("OnDealDamage", this);
			}
		}
	}

	public void Die() {
		onDeath.Invoke();
		SendMessage("OnDeath");
		switch (deathBehaviour) {
			case DeathBehaviour.Destroy:
				Destroy(gameObject);
				break;
			case DeathBehaviour.Deactivate:
				gameObject.SetActive(false);
				break;
		}
	}

	void OnCollisonEnter2D(Collision2D collision) {
		TakeDamageFrom(collision.gameObject);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (triggersDealDamage) {
			TakeDamageFrom(collider.gameObject);
		}
	}

	public enum DeathBehaviour {
		DoNothing, Deactivate, Destroy
	}

}

[System.Serializable]
public class DamageTakenEvent : UnityEvent<int, Vector2> { }

public struct Damage {
	float damage;
	Vector2 direction;

	public Damage(float damage, Vector2 direction) {
		this.damage = damage;
		this.direction = direction;
	}
}