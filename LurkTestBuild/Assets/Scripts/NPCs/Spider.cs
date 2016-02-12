using UnityEngine;

public class Spider : MonoBehaviour {

	Damageable hitbox;
	State state = State.Hanging;

	void Awake() {
		hitbox = GetComponent<Damageable>();
	}
	
	void Update() {
		switch (state) {
			//hanging spider states
			case State.Hanging:
				break;
			case State.Descending:
				break;
			case State.Ascending:
				break;
			case State.Grappling:
			//ground spider states
				break;
			case State.Crawling:
				break;
			case State.Crouching:
				break;
			case State.Leaping:
				break;
		}
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