using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {


	public GameObject				dialogue1Box;
	public GameObject				dialogue1Text;

	public string[]					dialogueList = {};

	public bool 					question = true;

	// TypewriterEffect
	private float					textSpeed = 0.08f;

	IEnumerator Start () {
		dialogue1Box.SetActive(false);

		yield return new WaitForSeconds(5f);

		StartCoroutine(RunDialogue(0, 2));

		Debug.Log("Hello");
	}

	void Update () {
		if(question == true) {

			/*
			maleChoice.GetComponent<Text>().enabled = true;
			femaleChoice.GetComponent<Text>().enabled = true;

			redArrow.GetComponent<SpriteRenderer>().enabled = true;
			blueArrow.GetComponent<SpriteRenderer>().enabled = true;

			// Player 1
			if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0) {
				Vector3 temp = redArrow.transform.localPosition;
				temp.y = 0.03f;
				redArrow.transform.localPosition = temp;

				player1Male = true;
			} else if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0) {
				Vector3 temp = redArrow.transform.localPosition;
				temp.y = -0.4f;
				redArrow.transform.localPosition = temp;

				player1Male = false;
			}

			if((player1Male == true) && Input.GetButtonDown("Submit")) {
				Debug.Log("Player 1 chooses male.");
			}
			if((player1Male == false) && Input.GetButtonDown("Submit")) {
				Debug.Log("Player 1 chooses female.");
			}

			// Player 2
			if(Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") > 0) {
				Vector3 temp = blueArrow.transform.localPosition;
				temp.y = 0.08f;
				blueArrow.transform.localPosition = temp;

				player2Male = true;
			} else if (Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") < 0) {
				Vector3 temp = blueArrow.transform.localPosition;
				temp.y = -0.39f;
				blueArrow.transform.localPosition = temp;

				player2Male = false;
			}

			if((player2Male == true) && Input.GetButtonDown("Z")) {
				Debug.Log("Player 2 chooses male.");
			}
			if((player2Male == false) && Input.GetButtonDown("Z")) {
				Debug.Log("Player 2 chooses female.");
			}
			*/
		}
	}

	IEnumerator RunDialogue(int firstString, int lastString) {
		dialogue1Box.SetActive(true);

		for(int i = firstString; i <= lastString; i++) {
			yield return StartCoroutine(RunTypewriter(i));
		}

		dialogue1Box.SetActive(false);
	}

	IEnumerator RunTypewriter(int i) {
		dialogue1Text.GetComponent<Text>().text = "";

		yield return new WaitForSeconds(1f);

		foreach (char letter in dialogueList[i].ToCharArray()) {
			dialogue1Text.GetComponent<Text>().text += letter;
			yield return new WaitForSeconds(textSpeed);
		}

		yield return new WaitForSeconds(1f);
	}
}
