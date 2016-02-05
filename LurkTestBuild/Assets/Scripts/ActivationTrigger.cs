using UnityEngine;
using UnityEngine.Events;

public class ActivationTrigger : MonoBehaviour {

	public UnityEvent onTriggerEnter;
	public UnityEvent onTriggerStay;
	public UnityEvent onTriggerExit;

	void OnTriggerEnter() {
		onTriggerEnter.Invoke();
	}

	void OnTriggerStay() {
		onTriggerStay.Invoke();
	}

	void OnTriggerExit() {
		onTriggerExit.Invoke();
	}

}