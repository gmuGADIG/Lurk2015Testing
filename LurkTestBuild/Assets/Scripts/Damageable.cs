using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour {
	
	public int health;
	public string damagingTag = "Damaging";
	public bool triggersDealDamage = true;
	public DeathBehaviour deathBehaviour = DeathBehaviour.DoNothing;
	
	public DamageTakenEvent onTakeDamage;
	public UnityEvent onDeath;
	
	//have to overload; cant provide default values for non primitive-typed parameters! suck it Luke!
	public void TakeDamage(int damage = 1) {
		TakeDamage(damage, Vector2.zero);
	}
	
	public void TakeDamage(int damage, Vector2 direction) {
		TakeDamage(null, damage, direction);
	}
	
	void TakeDamage(GameObject source, int damage, Vector2 direction) {
		if (damage > 0) {
			health -= damage;
			onTakeDamage.Invoke(damage, direction);
			SendMessage("OnTakeDamage", new Damage(source, damage, direction), SendMessageOptions.DontRequireReceiver);
			if (health <= 0) {
				Die();
			}
		}
	}
	
	public void TakeDamageFrom(GameObject other) {
		if (other.tag == damagingTag) {
			Vector2 direction = transform.position - other.transform.position;
			Damager damager = other.GetComponent<Damager>();
			TakeDamage(other, damager == null ? 1 : damager.GetDamage(this), direction);
			if (damager != null) {
				damager.onDealDamage.Invoke(this, direction);
				damager.SendMessage("OnDealDamage", this, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
	
	public void Die() {
		health = 0;
		onDeath.Invoke();
		SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
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
	public GameObject source;
	public float damage;
	public Vector2 direction;
	
	public Damage(GameObject source, float damage, Vector2 direction) {
		this.source = source;
		this.damage = damage;
		this.direction = direction;
	}
}