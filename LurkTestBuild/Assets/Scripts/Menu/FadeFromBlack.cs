using UnityEngine;
using System.Collections;

public class FadeFromBlack : MonoBehaviour {

	public float						waitTime = 3.0f;
	public bool							startFade = false;
	public float						fadeTime = 0.01f;

	void Start () {
		// Set black screens alpha to non-transparent
		Color c = this.GetComponent<SpriteRenderer>().color;
		c.a = 1f;
		this.GetComponent<SpriteRenderer>().color = c;

		StartCoroutine(WaitToFade());
	}

	void Update () {

		// Wait until WaitToFade() is done to start Fade()
		if(startFade == true) {
			StartCoroutine(Fade());
		}
	}

	// Fade the screen from black
	IEnumerator Fade() {
		for(float f = 1f; f >= 0f; f -= fadeTime) {
			Color c = this.GetComponent<Renderer>().material.color;
			c.a = f;
			this.GetComponent<Renderer>().material.color = c;
			yield return null;
		}
	}

	// Wait a certain amount of time before fading from black
	IEnumerator WaitToFade() {
		yield return new WaitForSeconds(waitTime);
		startFade = true;
	}
}
