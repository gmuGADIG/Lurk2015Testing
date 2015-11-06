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
	//interable object
	public GameObject icon;
	//cavas used for displaying the UI Elements
	public Canvas canvas;

	//Used to hold all the players in the game in order to check if the are
	//close enough to the object
	private GameObject[] players;
	//An instantiated copy of icon
	private GameObject instanceIcon;
	//component used to display the text
	private Text text;
	//text that still has to be displayed while cycling through the interaction text
	private string remaining;
	
	void Start()
	{
		
		//intantiate icon
		instanceIcon = Instantiate(icon);
		//set its position to just above this object
		instanceIcon.transform.position = new Vector2(transform.position.x, transform.position.y + 1.5f);
		//set its parent to this object so that if the parent moves, it moves
		instanceIcon.transform.SetParent(transform);
		//set it to not be active so that it is not visible
		instanceIcon.SetActive(false);

		//instantiate the canvas and make it invisble
		canvas = Instantiate(canvas);
		canvas.enabled = false;

		//get the text component from the child of the canvas
		text = canvas.transform.FindChild("Text").gameObject.GetComponent<Text>();

		//set the initial remaining text to be the same as interaction
		remaining = interaction.text;

		//gather all the players in the game
		players = GameObject.FindGameObjectsWithTag("Player");
	}
	
	void Update()
	{
		//loop through players
		foreach (GameObject player in players)
		{
			//if the player is within the maximun distance in the x direction
			//I don't know if there should be a y distance or not so I didn't make one
			if ((transform.position.x - player.transform.position.x <= maxDistance) &&
			    (transform.position.x - player.transform.position.x >= -maxDistance))
			{
				
				//if arrow key up is being pressed
				if (Input.GetKeyDown("up"))
				{
					
					//make the icon invisible
					instanceIcon.SetActive(false);
					
					//if the object has a conversation script
					if (conversation == true)
					{
						
						//I don't know how to run the conversation script nor do I have it
						//but I think it might be Invoke might work
						
						//Invoke("ConversationScript", 0);
						
					}
					else
					{
						//enable the canvas and set the text to be shown
						canvas.enabled = true;
						text.text = remaining;
					}
					
					//if the down button is being pressed and there is text to be displayed
				}
				else if (Input.GetKeyDown("down") && !text.text.Equals("")) {

					//find where the text gets cut off and reset remaining to the text that
					//still has to be displayed
					TextGenerator t = text.cachedTextGenerator;
					remaining = remaining.Substring(t.characterCountVisible);
					text.text = remaining;

					//if there is no more text to be displayed
					if (remaining.Equals("")) {
						//disable the canvas and reset the remaining text to be the full
						//interaction text
						canvas.enabled = false;
						remaining = interaction.text;
					}

					//if the icon is invisible and the Text has an empty string
				}
				else if (instanceIcon.activeSelf == false && text.text.Equals(""))
				{
					
					//make it visible
					instanceIcon.SetActive(true);
				}
				
				//if the player is not in range of the object   
			}
			else
			{
				
				//set the text to an empty string
				text.text = "";
				canvas.enabled = false;
				//make the icon invisible
				instanceIcon.SetActive(false);
			}
		}
	}
}