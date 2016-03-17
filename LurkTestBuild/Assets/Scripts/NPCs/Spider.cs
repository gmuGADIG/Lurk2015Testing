using UnityEngine;
using UnityEditor;
using System;

public class Spider : MonoBehaviour {

	[Header("Hanging/Ascending/Descending")]
	public float descendSpeed = 10;
	public float descendDistance = 10;
	public float ascendSpeed = 5;
	[Header("Grapple")]
	public int grappleDamage = 1;
	public float grappleDamageInterval = 1;
	public string noPlayerCollisionLayerName = "no player collision";
	[Header("Crawling")]
	public float moveSpeed = 10;
	public float directionChangeRollInterval = 1;
	[Range(0, 1)]
	public float directionChangeChance = .5f;
	public float leapTriggerRange = 5;
	[Header("Crouching")]
	public float crouchTime = 2; //s time to crouch before leaping
	[Header("Leaping")]
	public float leapImpulse = 50;	//Ns impulse to leap with (strength of leap)
	public float leapAngle = 45; //degrees angle to leap

	Damageable hitbox;
	State state = State.Hanging;
	Rigidbody2D body;

	//the layer the spider started with
	int layer;

	//two ends of the up/down tween
	Vector3 home;
	Vector3 maxDecent;
	float t = 0;

	//the damageable component of the player we are grappled to
	Damageable grappleHitbox;
	//time since last damage dealt to grappled player
	float timeSinceLastGrappleDamage;

	//time since last direction change roll
	float timeSinceLastDirectionChangeRoll;
	//current direction
	float direction;

	//crouch timer
	float crouchTimer;

	void Awake() {
		layer = gameObject.layer;
		hitbox = GetComponent<Damageable>();
		body = GetComponent<Rigidbody2D>();
		direction = UnityEngine.Random.value > .5f ? 1 : -1;
		home = transform.position;
		maxDecent = home + Vector3.down * descendDistance;
		//state = State.Crawling;
	}

	void Update() {
		gameObject.layer = layer;
		switch (state) {
			//hanging spider states
			case State.Hanging:
				body.isKinematic = true;
				RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down);
				foreach (RaycastHit2D hit in hits) {
					if (hit.collider) {
						if (hit.collider.attachedRigidbody == body)
							continue;
						if (hit.collider.tag == "Player")
							state = State.Descending;
					}
					break;
				}
				break;
			case State.Descending:
				body.isKinematic = true;
				t += (descendSpeed / descendDistance) * Time.deltaTime;
				Tween();
				if (t >= 1) {
					state = State.Ascending;
				}
				break;
			case State.Ascending:
				body.isKinematic = true;
				t -= (ascendSpeed / descendDistance) * Time.deltaTime;
				Tween();
				if (t <= 0) {
					state = State.Hanging;
				}
				break;
			case State.Grappling:
				if (grappleHitbox) {
					gameObject.layer = LayerMask.NameToLayer(noPlayerCollisionLayerName);
					Vector3 pos = grappleHitbox.transform.position;
					pos.z = transform.position.z;
					transform.position = pos;
					timeSinceLastGrappleDamage += Time.deltaTime;
					while (timeSinceLastGrappleDamage >= grappleDamageInterval) {
						timeSinceLastGrappleDamage -= grappleDamageInterval;
						grappleHitbox.TakeDamage(grappleDamage);
					}
				}
				else {
					//if the thing we are grappling dies or we were sent to this state by mistake, switch to crawling
					state = State.Crawling;
				}
				break;
			//ground spider states
			case State.Crawling:
				body.isKinematic = false;
				timeSinceLastDirectionChangeRoll += Time.deltaTime;
				while (timeSinceLastDirectionChangeRoll >= directionChangeRollInterval) {
					timeSinceLastDirectionChangeRoll %= directionChangeRollInterval;
					if (UnityEngine.Random.value < directionChangeChance) {
						direction = - direction;
					}
				}
				Vector2 vel = body.velocity;
				vel.x = direction * moveSpeed;
				body.velocity = vel;
				Collider2D[] colliders = Physics2D.OverlapCircleAll(body.position, leapTriggerRange);
				foreach (Collider2D collider in colliders) {
					if (collider.tag == "Player" && Mathf.Sign(collider.transform.position.x - transform.position.x) == direction) {
						state = State.Crouching;
						crouchTimer = 0;
						//zero horizontal velocity
						vel = body.velocity;
						vel.x = 0;
						body.velocity = vel;
						break;
					}
				}
				break;
			case State.Crouching:
				body.isKinematic = false;
				crouchTimer += Time.deltaTime;
				if (crouchTimer >= crouchTime) {
					//leap
					state = State.Leaping;
					Vector2 impulse = new Vector2(
						leapImpulse * Mathf.Cos(leapAngle * Mathf.Deg2Rad) * direction,
						leapImpulse * Mathf.Sin(leapAngle * Mathf.Deg2Rad)
					);
					body.AddForce(impulse, ForceMode2D.Impulse);
				}
				break;
			case State.Leaping:
				body.isKinematic = false;
				break;
		}
	}

	void Tween() {
		t = Mathf.Clamp01(t);
		transform.position = Vector3.Lerp(home, maxDecent, t);
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.white;
		switch (state) {
			case State.Hanging:
				Gizmos.DrawLine(transform.position, transform.position + Vector3.down * descendDistance);
				break;
			case State.Descending:
			case State.Ascending:
				Gizmos.DrawLine(home, maxDecent);
				break;
		}
		Handles.Label(transform.position, Enum.GetName(typeof(State), state));
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			switch (state) {
				case State.Descending:
				case State.Leaping:
					state = State.Grappling;
					grappleHitbox = collision.gameObject.GetComponent<Damageable>();
					grappleHitbox.TakeDamage(grappleDamage);
					timeSinceLastGrappleDamage = 0;
					break;
			}
		}
		else {
			if (state == State.Leaping) {
				state = State.Crawling;
			}
		}
	}

	void OnTakeDamage() {
		state = State.Crawling;
	}

	enum State {
		Hanging,
		Descending,
		Ascending,
		Grappling,
		Crawling,
		Crouching,
		Leaping
	}

}
