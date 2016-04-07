using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

	public float					startDialogue = 5f;
	public Canvas					oldManDialogueCanvas;
	public GameObject				speechBubble;

	public float					textSpeed = 0.08f;
	public GameObject				oldManText;
	public string					oldManFullText = "Greetings traveler! My name is old man.";
	public string[]					dialogueList = {"Greetings traveler! My name is old man.", "Are you a male or a female?"};
	public bool						question = false;

	public GameObject				maleChoice;
	public GameObject				femaleChoice;

	public bool						player1Bool = true;
	public bool						player2Bool = true;

	public GameObject				redArrow;
	public GameObject				blueArrow;

	void Start () {
		oldManDialogueCanvas.GetComponent<Canvas>().enabled = false;
		speechBubble.GetComponent<SpriteRenderer>().enabled = false;

		maleChoice.GetComponent<Text>().enabled = false;
		femaleChoice.GetComponent<Text>().enabled = false;

		redArrow.GetComponent<SpriteRenderer>().enabled = false;
		blueArrow.GetComponent<SpriteRenderer>().enabled = false;

		StartCoroutine(WaitForDialogue());
	}

	void Update () {
		if(question == true) {
			
			// Player 1
			if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0) {
				Vector3 temp = redArrow.transform.localPosition;
				temp.y = 0.03f;
				redArrow.transform.localPosition = temp;

				player1Bool = true;
			} else if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0) {
				Vector3 temp = redArrow.transform.localPosition;
				temp.y = -0.4f;
				redArrow.transform.localPosition = temp;

				player1Bool = false;
			}

			if((player1Bool == true) && Input.GetButtonDown("Submit")) {
				Debug.Log("Player 1 chooses male.");
			}
			if((player1Bool == false) && Input.GetButtonDown("Submit")) {
				Debug.Log("Player 1 chooses female.");
			}

			// Player 2
			if(Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") > 0) {
				Vector3 temp = blueArrow.transform.localPosition;
				temp.y = 0.08f;
				blueArrow.transform.localPosition = temp;

				player2Bool = true;
			} else if (Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") < 0) {
				Vector3 temp = blueArrow.transform.localPosition;
				temp.y = -0.39f;
				blueArrow.transform.localPosition = temp;

				player2Bool = false;
			}

			if((player2Bool == true) && Input.GetButtonDown("Z")) {
				Debug.Log("Player 2 chooses male.");
			}
			if((player2Bool == false) && Input.GetButtonDown("Z")) {
				Debug.Log("Player 2 chooses female.");
			}
		}
	}

	IEnumerator WaitForDialogue() {
		yield return new WaitForSeconds(startDialogue);

		oldManDialogueCanvas.GetComponent<Canvas>().enabled = true;
		speechBubble.GetComponent<SpriteRenderer>().enabled = true;

		StartCoroutine(StartDialogue());
	}

	IEnumerator StartDialogue() {
		for(int i = 0; i < dialogueList.Length; i++) {
			oldManText.GetComponent<Text>().text = "";
			yield return StartCoroutine(StartTypewriter(i));
		}

		if(question == true) {
			maleChoice.GetComponent<Text>().enabled = true;
			femaleChoice.GetComponent<Text>().enabled = true;

			redArrow.GetComponent<SpriteRenderer>().enabled = true;
			blueArrow.GetComponent<SpriteRenderer>().enabled = true;
		}
	}

	IEnumerator StartTypewriter(int i) {
		foreach (char letter in dialogueList[i].ToCharArray()) {
			if(letter == '?') {
				question = true;
			}
			oldManText.GetComponent<Text>().text += letter;
			yield return new WaitForSeconds(textSpeed);
		}
		yield return new WaitForSeconds(1.0f);
	}
}
