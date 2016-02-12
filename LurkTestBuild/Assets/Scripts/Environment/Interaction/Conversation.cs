using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Conversation : MonoBehaviour {

	//The file with the conversation
	public TextAsset conversation;
	//canvas used for displaying UI elements
	public Canvas canvas;
	//GameObject used to hold the three child GameObjects
	public GameObject convoDisplay;
	//sets of animations for the talk sprite to show
	public RuntimeAnimatorController animation;

	//remaining amount of conversation that hasn't been shown
	private string remaining;
	//the UI element used to display text
	private Text text;
	//Animator componentn in the Talk Sprite child GameObject of convoDisplay
	private Animator talkSprite;

	void Start () {
	
		//rename GameObject so that it is easeir to understand
		convoDisplay.name = "Conversation for " + gameObject.name;
		//instantiate it
		convoDisplay = Instantiate(convoDisplay);
		//make it a child of the canvas
		convoDisplay.transform.SetParent(canvas.transform);
		//make it inactive
		convoDisplay.SetActive(false);

		//get the text component from the child of the display
		text = convoDisplay.transform.FindChild("Text").gameObject.GetComponent<Text>();

		//get the animator component of the talk sprite child from convoDisplay
		talkSprite = convoDisplay.transform.FindChild("Talk Sprite").gameObject.GetComponent<Animator>();
		//set its animation controller to the one provided
		talkSprite.runtimeAnimatorController = animation;

		//set the remaining amount of text to the text in conversation
		remaining = conversation.text;
	}

	public void StartConvo(){

		//parse the text and replace the necesary phrases
		Parse();
		//show the first page of text
		ShowNextText();

		//show the conversation
		convoDisplay.SetActive(true);

		//used for animation testing
		talkSprite.SetInteger("Anim_State", 0);
	}

	public void CycleConvo(){

		//if there is no more text to be displayed
		if (remaining.Equals("")) {

			//disable the display and reset the remaining text to be the full
			//interaction text and turn the icon back on
			Close ();
			GetComponent<Interact>().End();
		
		} else {

			//used for testing purposes
			talkSprite.SetInteger("Anim_State", 1);
			//show the next panel of text
			ShowNextText();
		}
	}

	//closes the conversation panel
	public void Close(){

		//sets all the conversation gameobjects to inactive
		convoDisplay.SetActive(false);
		//reset the remaining to the first amount
		remaining = conversation.text;
	}

	//replaces the special phrases with ones relative to the character speaking
	//NOT FINISHED YET
	void Parse(){

		remaining = remaining.Replace("*1*", "Character1");
		remaining = remaining.Replace("$", "he");
		remaining = remaining.Replace("%", "his");
	}

	//sets the next panel of text
	void ShowNextText(){

		//an index of a double line break signifies where panels are to be seperated
		int index = remaining.IndexOf("\n\n");

		//if there are no more double line breaks
		if (index < 0) {
			//index eqauls the last index of the string
			index = remaining.Length;
			//the text to be displayed is the last of remaining
			text.text = remaining.Substring (0, index);
			//remaining is set to an empty string
			remaining = "";

		//if there are more double line breaks
		} else {
			//set text to be the next panel of text
			text.text = remaining.Substring (0, index);
			//take off the panel of text just displayed from remaining and then 2 additional
			//characters, these 2 characters are the new line characters
			remaining = remaining.Substring (index + 2);
		}
	}
}
