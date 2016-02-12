using UnityEngine;

public class Spider : MonoBehaviour {

	public float descendSpeed = 10;
	public float decendDistance = 10;
	public float ascendSpeed = 5;

	Damageable hitbox;
	State state = State.Hanging;
	Rigidbody2D body;

	void Awake() {
		hitbox = GetComponent<Damageable>();
		body = GetComponent<Rigidbody2D>();
	}
	
	void Update() {
		switch (state) {
			//hanging spider states
			case State.Hanging:
				body.isKinematic = true;
				RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
				if (hit.collider && hit.collider.tag == "Player") {
					state = State.Descending;
				}
				break;
			case State.Descending:
				break;
			case State.Ascending:
				break;
			case State.Grappling:
				break;
			//ground spider states
			case State.Crawling:
				break;
			case State.Crouching:
				break;
			case State.Leaping:
				break;
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.white;
		Gizmos.DrawLine(transform.position, transform.position + Vector3.down * decendDistance);
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