using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Conversation : Interactable {

	//The file with the conversation
	public TextAsset conversation;
    //GameObject used to hold the three child GameObjects
    public GameObject convoDisplay;

	//remaining amount of conversation that hasn't been shown
	private string remaining;
	//the UI element used to display text
	private Text text;
    //all the dialog of the txt file that comes after listing all the
    //characters who speak in the interaction
    private string dialog;
    //string used to hold the npcs name if there is one
    private string npcName;
    //the current piece of dialog being displayed
    private string displaying;
    //used to tell if the interaction has been started
    private bool started;
    
    //give speaker and emotion coords to display talk sprite
    private TalkSpriteDisplay tsd;
    //the scripts containing the class, name and gender of the characters
    private playerMove[] playerScripts;
    //all the objects tagged with player
    private GameObject[] players;
    //specific use is to be determined
    private int[] characters;

    new void Start()
    {
        //initially run Interactables start
        base.Start();

        //get the text component from the conversation
        text = convoDisplay.transform.FindChild("Text").gameObject.GetComponent<Text>();

        tsd = convoDisplay.GetComponent<TalkSpriteDisplay>();

        //get the players in the game and set the length of the scripts equal to that of players
        players = GameObject.FindGameObjectsWithTag("Player");
        playerScripts = new playerMove[players.Length];

        //set the remaining amount of text to the text in conversation
        remaining = conversation.text;

        //do important stuff
        SetUp();
    }

    public override bool Interact(GameObject player)
    {
        
        //end the function if there are no players in the circle,
        //used as a fail safe
        if (playersInCircle <= 0){
            return false;
        }

        //make the icon invisible
        icon.SetActive(false);

        //if the conversation has not been started
        if (!started) {

            //determine which player started it and set stuff accordingly
            playerScripts[0] = player.GetComponent<playerMove>();

            if (players.Length == 2)
            {
                playerScripts[1] = (players[0] == player) ?
                    players[1].GetComponent<playerMove>() : players[0].GetComponent<playerMove>();
            }

        }

        //if it has not begun being displayed
        if (!convoDisplay.activeSelf)
        {
            //start the conversation
            StartConvo();
        }
        //if it has already started
        else {
            //continue it
            CycleConvo();
        }

        return true;
    }

    public void SetUp()
    {
        //get the index of the next double line break
        int index = remaining.IndexOf("\r\n\r\n");

        //set the length of people to the proper amount
        characters = (index >= 9) ? new int[3] : new int[2];

        //get the characters involved in the conversation
        for(int i = 0; i < characters.Length; i++)
        {
            characters[i] = (int)char.GetNumericValue(remaining[i * 3]);
        }

        //get the npcs name if there is an npc
        if (characters.Length == 3)
        {
            npcName = remaining.Substring(8, index - 8);
        }

        //set the remaing to after the list of characters involved
        remaining = remaining.Substring(index + 4);
        //this should be only dialog for the rest of the txt file
        dialog = remaining;
    }

	public void StartConvo(){

        //if there is an npc, set the right picture to the npc
        if (characters.Length > 2)
        {
            tsd.setRight(characters[2] - 1, 0);
        }

		//show the first page of text
		ShowNextText();

		//show the conversation
		convoDisplay.SetActive(true);

        //the conversation has started
        started = true;
    }

    public void CycleConvo(){

		//if there is no more text to be displayed
		if (remaining.Equals("")) {

			//end the conversation
			End ();
		
        //if there is still text to be displayed
		} else {

			//show the next panel of text
			ShowNextText();
		}
	}

    //closes the conversation panel
    protected override void End() {

        Destroy(this);

        //sets all the conversation gameobjects to inactive
        convoDisplay.SetActive(false);

        //reset the remaining to the dialog
        remaining = dialog;

        //if players are still in the circle, show the icon
        if (playersInCircle > 0) {
//            icon.SetActive(true);
        }

        //the convesation has ended
        started = false;
	}

    //replaces the special phrases with ones relative to the character speaking
    //NOT FINISHED YET
    void Parse() {

        //get the person who is currently speaking
        int speakerNum = (int)char.GetNumericValue(displaying[1]);
        //get the emotion of the person who is currently speaking
        int emotion = (int)char.GetNumericValue(displaying[3]);


        //order is rogue warrior mage
        //sub order is female male


        //if there is an npc in the conversation
        if ((characters.Length > 2 && speakerNum == 2) || speakerNum == 1)
        {
            tsd.setLeft(speakerNum - 1, emotion - 1);
        }
        //if it is between the players
        else
        {
            tsd.setRight(speakerNum - 1, emotion - 1);
        }

        //replace the character signifiers with immersive player stuff
        displaying = (speakerNum < 3) ? 
            displaying.Replace("*" + speakerNum + ":" + emotion + "*", playerScripts[speakerNum - 1].name + ":") :
            displaying.Replace("*" + speakerNum + ":" + emotion + "*", npcName + ":");
	}

	//sets the next panel of text
	void ShowNextText(){

		//an index of a double line break signifies where panels are to be seperated
		int index = remaining.IndexOf("\r\n\r\n");

		//if there are no more double line breaks
		if (index < 0) {

            //set displaying ot the last piece of the conversation
            displaying = remaining;

            //replace stuff
            Parse();

            //show the replaced stuff
            text.text = displaying;
			//remaining is set to an empty string
			remaining = "";

		//if there are more double line breaks
		} else {

            //get the current line of text
            displaying = remaining.Substring(0, index);

            //replace stuff in it
            Parse();

            //show it
            text.text = displaying;

			//take off the panel of text just displayed from remaining and then 2 additional
			//characters, these 2 characters are the new line characters
			remaining = remaining.Substring (index + 4);
		}
	}
}