using UnityEngine;

public class FollowPath : MonoBehaviour {

	public Path path;
	public float speed = 5;
	public EndBehaviour endBehaviour = EndBehaviour.DoNothing;

	float t = 0;

	void Update() {
		float length = path.length;
		float dt = (speed * Time.deltaTime) / path.length;
		t += dt;
		switch (endBehaviour) {
			case EndBehaviour.Loop:
				t = Mathf.Repeat(t, 1);
				break;
			case EndBehaviour.Reverse:
				if (t > 1 || t < 0) {
					speed = -speed;
				}
				break;
		}
		transform.position = path.positionAt(t);
	}

	public enum EndBehaviour {
		DoNothing, Loop, Reverse,
	}

}