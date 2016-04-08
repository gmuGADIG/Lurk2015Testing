using UnityEngine;
using System.Collections;

public class AnimateScene : MonoBehaviour {

	public Transform					road1;
	public Transform					road2;
	public Transform					frontWheel;
	public Transform					backWheel;

	void Update () {
		Animate();
	}

	// Move the background image and rotate the wheels
	void Animate() {
		road1.Translate(Vector3.left * (Time.deltaTime / 2));
		if(road1.position.x < -27) {
			Vector3 temp = road1.transform.position;
			temp.x = 43;
			road1.position = temp;
		}

		road2.Translate(Vector3.left * (Time.deltaTime / 2));
		if(road2.position.x < -27) {
			Vector3 temp = road2.transform.position;
			temp.x = 43;
			road2.position = temp;
		}

		frontWheel.Rotate(0,0,Time.deltaTime * -10);
		backWheel.Rotate(0,0,Time.deltaTime * -10);
	}
}
