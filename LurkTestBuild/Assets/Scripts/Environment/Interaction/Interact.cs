using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
	
	//Used to describe the way the way the player can interact with the object
	public TextAsset interaction;
	//Used asthe maximun distance away from the object the player can interact with it
	public float maxDistance = 2f;
	//Used to see if the object has a conversation script
	public bool conversation;
	//Used to hold the notification Sprite that will appear above the an
	//interactable object
	public GameObject icon;
	//cavas used for displaying the UI Elements
	public Canvas canvas;
	//prefab gameobject which has a panel child and a text child objects
	public GameObject display;


	//collider used to tell if something is in the maxDistance of the object
	private CircleCollider2D cc2d;
	//number of players inside the radius of the collider, needed so that when two players
	//are within the radius of the circle, the sprite/chat doesn't disappear when one of them exits
	private int playersInCircle = 0;
	//component used to display the text
	private Text text;
	//text that still has to be displayed while cycling through the interaction text
	private string remaining;

	private Conversation conversationScript;
	
	void Start()
	{
		
		//intantiate icon
		icon = Instantiate(icon);
		//set its position to just above this object
		icon.transform.position = new Vector2(transform.position.x, transform.position.y + 1.5f);
		//set its parent to this object so that if the parent moves, it moves
		icon.transform.SetParent(transform);
		//set it to not be active so that it is not visible
		icon.SetActive(false);

		//change the name of the display object so that it is easier to tell
		//which object it is for
		display.name = "Interaction for " + gameObject.name;
		//instantiate it
		display = Instantiate(display);
		//make it a child of the canvas
		display.transform.SetParent(canvas.transform);
		//make it inactive
		display.SetActive(false);

		if (conversation == true) {
			conversationScript = GetComponent<Conversation>();
		}

		//get the text component from the child of the display
		text = display.transform.FindChild("Text").gameObject.GetComponent<Text>();
		
		//set the initial remaining text to be the same as interaction
		remaining = interaction.text;

		//adds the circle collider to this game object
		cc2d = gameObject.AddComponent<CircleCollider2D>();
		//makes it triggerable
		cc2d.isTrigger = true;
		//sets the radius of the circle to be the maxDistance
		cc2d.radius = maxDistance;
	}

    public void interact(){

        if(conversation == true){
            conversationScript.interact();
            return;
        }

        if (!display.activeSelf){
            StartKey();
        }
        else{
            CycleKey();
        }
    }

	public void StartKey(){

		//if the icon is being displayed
		if(icon.activeSelf){
			//make it invisible
			icon.SetActive(false);

			//if the object has a conversation script
			//enable the display and set the text to be shown
			display.SetActive(true);
            text.text = remaining;
		}
	}

	public void CycleKey(){

		//if there are still players in the circle and the icon is not being shown
		if(playersInCircle > 0 && icon.activeSelf == false){

			//find where the text gets cut off and reset remaining to the text that
			//still has to be displayed
			TextGenerator t = text.cachedTextGenerator;
			remaining = remaining.Substring(t.characterCountVisible - 1);
			text.text = remaining;
		
			//if there is no more text to be displayed
			if (remaining.Length<= 1){//remaining.Equals("")) {
				//disable the display and reset the remaining text to be the full
				//interaction text and turn the icon back on
				display.SetActive(false);
				remaining = interaction.text;
				End();
			}
		}
	}

	public void End(){
		icon.SetActive(true);
	}

	void OnTriggerEnter2D(Collider2D other){

		//if the circle touches a player
		if(other.gameObject.CompareTag("Player")){
			//make the icon visible and increment the player counter
			icon.SetActive(true);
			playersInCircle++;
		}
	}

	void OnTriggerExit2D(Collider2D other){

		//if a player leaves the circle
		if (other.gameObject.CompareTag("Player")) {
			//decrement the player counter
			playersInCircle--;

			//then check if there are no more players in the circle
			if(playersInCircle == 0){
				//make the icon invisible, make the display inactive, and reset
				//the remaining text back to the initial
				icon.SetActive(false);
				display.SetActive(false);
				remaining = interaction.text;

				if(conversation == true){
					conversationScript.Close();
				}
			}
		}
	}
}