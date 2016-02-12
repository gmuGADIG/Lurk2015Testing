using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour {

	public int health;
	public string damagingTag = "Damaging";
	public bool triggersDealDamage = true;
	public DeathBehaviour deathBehaviour = DeathBehaviour.DoNothing;

	public UnityEvent onTakeDamage;
	public UnityEvent onDeath;

	public void TakeDamage() {
		health --;
		onTakeDamage.Invoke();
		if (health <= 0) {
			Die();
		}
	}

	public void Die() {
		onDeath.Invoke();
		switch (deathBehaviour) {
			case DeathBehaviour.Destroy:
				Destroy(gameObject);
				break;
			case DeathBehaviour.Deactivate:
				gameObject.SetActive(false);
				break;
		}
	}

	void OnCollisonEnter(Collider other) {
		if (other.tag == damagingTag) {
			TakeDamage();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (triggersDealDamage && other.tag == damagingTag) {
			TakeDamage();
		}
	}

	public enum DeathBehaviour {
		DoNothing, Deactivate, Destroy
	}

}