﻿using UnityEngine;
using System.Collections;


public class Interact : MonoBehaviour {

    //Used to describe the way the way the player can interact with the object
    public string interaction;
    //Used asthe maximun distance away from the object the player can interact with it
    public float maxDistance = 2f;
    //Used to see if the object has a conversation script
    public bool conversation;
    //Used to hold the notification Sprite that will appear above the an
    //interable object
    public GameObject icon;
    //Used to display the interaction text
    public TextMesh text;

    //Used to hold all the players in the game in order to check if the are
    //close enough to the object
    private GameObject[] players;
    //An instantiated copy of icon
    private GameObject instanceIcon;


    void Start () {

        //intantiate icon
        instanceIcon = Instantiate(icon);
        //set its position to just above this object
        instanceIcon.transform.position = new Vector2(transform.position.x, transform.position.y + 1.5f);
        //set its parent to this object so that if the parent moves, it moves
        instanceIcon.transform.SetParent(transform);
        //set it to not be active so that it is not visible
        instanceIcon.SetActive(false);

        //gather all the players in the game
        //I assume the characters will either be tagged as character or player
        players = GameObject.FindGameObjectsWithTag("Character");
	}
	
	void Update () {

        //loop through players
        foreach (GameObject player in players) { 

            //if the player is within the maximun distance in the x direction
            //I don't know if there should be a y distance or not so I didn't make one
            if ((transform.position.x - player.transform.position.x <= maxDistance) &&
                (transform.position.x - player.transform.position.x >= -maxDistance)) { 

                //if arrow key up is being pressed
                if (Input.GetKeyDown("up")) {

                    //make the icon invisible
                    instanceIcon.SetActive(false);

                    //if the object has a conversation script
                    if (conversation  == true) {

                        //I don't know how to run the conversation script nor do I have it
                        //but I think it might be Invoke might work
                        Invoke("ConversationScript", 0);

                    } else {
                        
                        //otherwise set the text of the MeshText object to display
                        //how to interact with this object
                        //NOTE: the MeshText object is always being displayed, it
                        //is just displaying an empty string when there is no text
                        text.text = interaction;
                   }
                    
                  //if the icon is invisible and the TextMesh has an empty string
                } else if (instanceIcon.activeSelf == false && text.text.Equals("")) {

                    //make it visible
                    instanceIcon.SetActive(true);
                }
               
              //if the player is not in range of the object   
            } else {

                //set the text to an empty string
                text.text = "";
                //make the icon invisible
                instanceIcon.SetActive(false);
            }
        }
    }
}