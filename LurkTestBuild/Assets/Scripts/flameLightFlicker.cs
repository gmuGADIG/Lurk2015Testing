using UnityEngine;
using System.Collections;

public class flameLightFlicker : MonoBehaviour {

	public float flickerStrength = 0.1f;

	private Light lightComponent;
	private float originalIntensity;

	// Use this for initialization
	void Start () {
		lightComponent = this.GetComponent<Light>();
		originalIntensity = lightComponent.intensity;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float randOffset = Random.Range(-flickerStrength, flickerStrength);
		lightComponent.intensity = Mathf.Lerp (lightComponent.intensity, originalIntensity + randOffset, Time.deltaTime*5);
	}
}
