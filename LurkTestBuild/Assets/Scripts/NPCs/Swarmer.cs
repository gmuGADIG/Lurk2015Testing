using UnityEngine;
using System.Collections.Generic;

//tgrehawi

//swarming flying enemy
public class Swarmer : MonoBehaviour {
	
	static List<Swarmer> swarm;

    public float speed = 2;
    public Weights weights;

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
	
    void FixedUpdate() {
        Vector2 sum = Vector2.zero;
        float count = 0;
        sum = rb.velocity * weights.velocity;
        count += weights.velocity;
        Vector2 away = Vector2.zero; 
        foreach (Swarmer s in swarm) {
            if (s != this) {
                sum += s.rb.velocity * weights.follow;
                count += weights.follow;
                away += (Vector2) (transform.position - s.transform.position);
            }
        }
        away /= swarm.Count - 1;
        sum += away * weights.avoid;
        count += weights.avoid;
        if (en.aggro != null) {
            sum += (Vector2) (en.aggro.transform.position - transform.position) * weights.aggro;
            count += weights.aggro;
        }
        sum /= count;
        rb.velocity = sum.normalized * speed;
    }

	void OnDisable() {
		swarm.Remove(this);
	}
	
    [System.Serializable]
    public class Weights {
        public float velocity = 1;
        public float aggro = 1;
        public float follow = 1;
        public float avoid = 1;
    }
	
}