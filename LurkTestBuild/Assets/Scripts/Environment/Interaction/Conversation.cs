using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Conversation : Interactable {

	//The file with the conversation
	public TextAsset conversation;
    //GameObject used to hold the three child GameObjects
    public GameObject convoDisplay;

    //the sprite sheet to be programmatically cut up
    public Texture2D t2d;

	//remaining amount of conversation that hasn't been shown
	private string remaining;
	//the UI element used to display text
	private Text text;
    //all the dialog of the txt file that comes after listing all the
    //characters who speak in the interaction
    private string dialog;
    //the current piece of dialog being displayed
    private string displaying;
    //used to tell if the interaction has been started
    private bool started;

    //the scripts containing the class, name and gender of the characters
    private playerMove[] playerScripts;
    //all the objects tagged with player
    private GameObject[] players;
    //specific use is to be determined
    private int[] people;

    //the cut up sprites
    private Sprite[,] sr;

    //left picture to be displayed
    private SpriteRenderer leftSprite;
    //right picture to be displayed
    private SpriteRenderer rightSprite;

    new void Start()
    {
        //initially run Interactables start
        base.Start();

        //make the conversation invisible
        convoDisplay.SetActive(false);

        //get the text component from the conversation
        text = convoDisplay.transform.FindChild("Text").gameObject.GetComponent<Text>();

        //get the players in the game and set the length of the scripts equal to that of players
        players = GameObject.FindGameObjectsWithTag("Player");
        playerScripts = new playerMove[players.Length];

        //get the left and right sprite from the conversation
        leftSprite = convoDisplay.transform.FindChild("Left Sprite").gameObject.GetComponent<SpriteRenderer>();
        rightSprite = convoDisplay.transform.FindChild("Right Sprite").gameObject.GetComponent<SpriteRenderer>();

        //set the remaining amount of text to the text in conversation
        remaining = conversation.text;

        //testing size
        sr = new Sprite[2, 2];

        //testing cut
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Sprite newSprite = Sprite.Create(t2d, new Rect(j * 128, i * 128, 128, 128), new Vector2(0.5f, 0.5f));
                sr[j, i] = newSprite;
            }
        }

        //testing stuff
        leftSprite.sprite = sr[0, 0];
        rightSprite.sprite = sr[1, 1];

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
        people = new int[index / 3 + 1];

        //get the characters involved in the conversation
        for(int i = 0; i < index; i += 3)
        {
            people[i / 3] = (int)char.GetNumericValue(remaining[i]);
        }

        //set the remaing to after the list of characters involved
        remaining = remaining.Substring(index + 4);
        //this should be only dialog for the rest of the txt file
        dialog = remaining;
    }

	public void StartConvo(){

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

        //sets all the conversation gameobjects to inactive
        convoDisplay.SetActive(false);

        //reset the remaining to the dialog
        remaining = dialog;

        //if players are still in the circle, show the icon
        if (playersInCircle > 0) {
            icon.SetActive(true);
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

        //if there is an npc in the conversation
        if ((people.Length > 2 && speakerNum == 2) || speakerNum == 1)
        {
            leftSprite.sprite = sr[speakerNum - 1, emotion - 1];
        }
        //if it is between the players
        else
        {
            rightSprite.sprite = sr[speakerNum - 1, emotion - 1];
        }

        //replace the character signifiers with immersive player stuff
        displaying = displaying.Replace("*"+speakerNum+":"+emotion+"*", "Player "+speakerNum+":");

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