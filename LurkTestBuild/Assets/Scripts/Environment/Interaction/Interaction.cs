using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Interaction : Interactable
{
	
	//Used to describe the way the way the player can interact with the object
	public TextAsset interaction;
	//prefab gameobject which has a panel child and a text child objects
	public GameObject display;


	//component used to display the text
	private Text text;
	//text that still has to be displayed while cycling through the interaction text
	private string remaining;
	
	new void Start()
	{
        //use the Interactables Start
        base.Start();

        //make the display invisible
		display.SetActive(false);

		//get the text component from display
		text = display.transform.FindChild("Text").gameObject.GetComponent<Text>();
		
		//set the initial remaining text to be the same as interaction
		remaining = interaction.text;
	}

    //player not used in interaction
    public override bool Interact(GameObject player) {

        //if there are not players in range of the object, end the function
        //used as a fail safe during testing
        if (playersInCircle <= 0) {
            return false;
        }

        //if the interaction is invisible, start the interaction
        if (!display.activeSelf) {
            StartKey();
        }
        //if the interaction ha already been started
        else {
            CycleKey();
        }

        return true;
    }

	public void StartKey(){

        //make the icon invisible
    	icon.SetActive(false);

		//enable the display and set the text to be shown
		display.SetActive(true);
        text.text = remaining;
	}

    public void CycleKey()
    {
        //an index of a double line break signifies where panels are to be seperated
        int index = remaining.IndexOf("\r\n\r\n");

        //if there are no more double line breaks
        if (index < 0)
        {
            //the text to be displayed is the last of remaining
            text.text = remaining;
            //end the interaction
            End();

        //if there are more double line breaks
        }else {

            //take off the part before the end line characters
            remaining = remaining.Substring(index + 4);
            //set text to be the next panel of text
            text.text = remaining;
        }
    }

	protected override void End(){

        //disable the display and reset the remaining text to be the full
        //interaction text and turn the icon back on if there are players
        //in the circle

        display.SetActive(false);
        remaining = interaction.text;

        if(playersInCircle > 0) {
            icon.SetActive(true);
        }
    }
}