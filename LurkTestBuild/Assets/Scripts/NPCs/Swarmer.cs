using UnityEngine;
using System.Collections.Generic;

//author tgrehawi

//swarming flying enemy
public class Swarmer : MonoBehaviour {
	
	//the current swarm
	static List<Swarmer> swarm;

	//max speed of the swarmer
    public float speed = 20;
	//acceleration of the swarmer
	public float acceleration = 25;
	//boids parameters
    public Parameters parameters;

	//components;
	Enemy en;
	Rigidbody2D rb;

	//the local neighborhood of the swarmer
	public IEnumerable<Swarmer> neighborhood {
		get {
			foreach (Swarmer other in swarm) {
				if (other != this && (other.rb.position - rb.position).sqrMagnitude < parameters.sqrRadius) {
					yield return other;
				}
			}
		}
	}

	//init components
	void Awake() {
		en = GetComponent<Enemy>();
		rb = GetComponent<Rigidbody2D>();
	}
	
	//boid swarming behaviour
    void FixedUpdate() {
		int neighborCount = 0;
		//aggro force
		Vector2 aggro = Vector2.zero;
		if (en.aggro != null) {
            aggro = (Vector2) en.aggro.transform.position - rb.position;
		}
		//light fear stuff
		Vector2 light = Vector2.zero;
		foreach (GameObject lantern in en.lanterns) {
			Vector2 dir = rb.position - (Vector2) lantern.transform.position;
			dir = dir.normalized * (1 / dir.magnitude);
			light += dir;
		}
		//basic boids forces
		Vector2 seperation = Vector2.zero;
		Vector2 cohesion = Vector2.zero;
		Vector2 alignment = Vector2.zero;
		//summation
		foreach (Swarmer neighbor in neighborhood) {
			neighborCount ++;
			seperation += rb.position - neighbor.rb.position;
			cohesion += neighbor.rb.position;
			alignment += neighbor.rb.velocity;
		}
		//basic boids stuff
		if (neighborCount > 0) {
			//average
			seperation /= neighborCount;
			cohesion /= neighborCount;
			alignment /= neighborCount;
			//set up values
			seperation /= seperation.sqrMagnitude;
			cohesion -= rb.position;
			alignment = rb.velocity - alignment;
			//apply weights
			seperation = seperation * parameters.seperation;
			cohesion = cohesion * parameters.cohesion;
			alignment = alignment * parameters.alignment;
		}
		//more weights
		aggro = aggro * parameters.aggro;
		light = light * parameters.light;
		//apply acceleration
		Vector2 acc = (seperation + cohesion + alignment + aggro + light);
		acc = acc.normalized * acceleration;
		rb.AddForce(acc * rb.mass, ForceMode2D.Force);
		//clamp velocity
		if (rb.velocity.magnitude > speed) {
			rb.velocity = rb.velocity.normalized * speed;
		}
	}

	//add this swarmer to the swarm when it gets added to the scene
	void OnEnable() {
		if (swarm == null) {
			swarm = new List<Swarmer>();
		}
		swarm.Add(this);
	}

	//remove the swarmer from the swarm when it gets removed from the scene
	void OnDisable() {
		swarm.Remove(this);
	}
	
	//show neighborhood radius
	void OnDrawGizmosSelected() {
		Gizmos.DrawWireSphere(transform.position, parameters.radius);
	}

	//boid swarming parameters
	[System.Serializable]
    public class Parameters {
		//neighborhood radius
		public float radius = 5;
		//weights
		public float seperation = 1;
		public float cohesion = 1;
		public float alignment = 1;
		public float aggro = 1;
		public float light = 1;

		public float sqrRadius { get { return radius * radius; } }
    }
	
}