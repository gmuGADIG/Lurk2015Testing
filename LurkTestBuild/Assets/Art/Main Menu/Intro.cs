using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Intro : MonoBehaviour {

	public GameObject					menu;
	public GameObject					newGameText;
	public GameObject					loadGameText;
	public GameObject					optionsText;
	public GameObject					quitText;
	public bool							startFadeIn = false;
	public float						waitTime = 4.0f;

	void Start () {
		// Set the buttons to non-interactable
		newGameText.GetComponent<Button>().interactable = false;
		loadGameText.GetComponent<Button>().interactable = false;
		optionsText.GetComponent<Button>().interactable = false;
		quitText.GetComponent<Button>().interactable = false;

		// Set menu buttons alpha to transparent
		Color c = newGameText.GetComponent<Text>().color;

		c.a = 0f;

		newGameText.GetComponent<Text>().color = c;
		loadGameText.GetComponent<Text>().color = c;
		optionsText.GetComponent<Text>().color = c;
		quitText.GetComponent<Text>().color = c;

		StartCoroutine(WaitUntil());
	}

	void Update () {
		if(startFadeIn) {
			StartCoroutine(MenuFade());
		}
	}

	IEnumerator WaitUntil() {
		// Wait 5 seconds until the buttons fade in
		yield return new WaitForSeconds(waitTime);

		// Set the buttons to interactable
		newGameText.GetComponent<Button>().interactable = true;
		loadGameText.GetComponent<Button>().interactable = true;
		optionsText.GetComponent<Button>().interactable = true;
		quitText.GetComponent<Button>().interactable = true;

		startFadeIn = true;
	}

	// Coroutine that waits until after the title screen loads to fade in the menu buttons
	IEnumerator MenuFade() {
		for(float f = 0f; f <= 1f; f += 0.01f) {
			Color c = newGameText.GetComponent<Text>().color;
			c.a = f;
			newGameText.GetComponent<Text>().color = c;
			loadGameText.GetComponent<Text>().color = c;
			optionsText.GetComponent<Text>().color = c;
			quitText.GetComponent<Text>().color = c;
			yield return null;
		}
	}
}
