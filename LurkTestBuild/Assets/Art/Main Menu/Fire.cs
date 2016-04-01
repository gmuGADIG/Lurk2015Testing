using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

	private float				minIntensity = 0.1f;
	private float				maxIntensity = 7.0f;
	private float				random;
	public float				speed = 1.0f;

	void Start () {
		random = Random.Range(0.0f, 65535.0f);
	}

	void Update () {
		//StartCoroutine(Flicker());
		float noise = Mathf.PerlinNoise(random, Time.time * speed);
		GetComponent<Light>().intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
	}

	IEnumerator Flicker() {
		Debug.Log(Time.time);
		GetComponent<Light>().intensity = Random.Range(minIntensity,maxIntensity);
		yield return new WaitForSeconds(1.0f);
	}
}
