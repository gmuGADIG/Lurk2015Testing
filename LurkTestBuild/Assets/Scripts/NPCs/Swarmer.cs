using UnityEngine;
using System.Collections.Generic;

//tgrehawi

//swarming flying enemy
public class Swarmer : MonoBehaviour {
	
	static List<Swarmer> swarm;
	
	//components;
	Enemy en;
	Rigidbody2D rb;
	
	void Awake() {
		en = GetComponent<Enemy>();
		rb = GetComponent<Rigidbody2D>();
	}
	
	void OnEnable() {
		if (swarm == null)
			swarm = new List<Swarmer>();
		swarm.Add(this);
	}
	
	void OnDisable() {
		swarm.Remove(this);
	}
	
	
}