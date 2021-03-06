using UnityEngine;
using UnityEngine.Events;

//optional component to specify how much damage something deals or other effects
public class Damager : MonoBehaviour {
	
	public int damage = 1;
	
	public DamageDealtEvent onDealDamage;

	public virtual int GetDamage(Damageable target) {
		return damage;
	}
	
}

[System.Serializable]
public class DamageDealtEvent : UnityEvent<Damageable, Vector2> { }