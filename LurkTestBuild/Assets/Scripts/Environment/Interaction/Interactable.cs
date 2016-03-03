using UnityEngine;
using System.Collections;

public abstract class Interactable : MonoBehaviour
{

    //Used as the maximun distance away from the object the player can be to interact with it
    public float circleRadius = 2f;
    //Used to hold the notification Sprite that will appear above the an interactable object
    public GameObject icon;

    //number of players inside the radius of the collider, needed so that when two players
    //are within the radius of the circle, the icon stays when one exits the cc2d
    protected float playersInCircle;
    //collider used to tell if a player is within the maxDistance of the object
    protected CircleCollider2D cc2d;

    protected void Start()
    {
        //create the icon just above the object
        icon = (GameObject) Instantiate(icon, new Vector2(transform.position.x, 
            transform.position.y + transform.lossyScale.y / 2 + 1.5f), Quaternion.identity);

        //child the icon to this object
        icon.transform.SetParent(transform);

        //make it invisible
        icon.SetActive(false);

        //give this gameobject a circle collider 2d
        cc2d = gameObject.AddComponent<CircleCollider2D>();
        //make it a trigger
        cc2d.isTrigger = true;
        //set its radius to that of the radius provided
        cc2d.radius = circleRadius;
    }

    //called when the player wants to interact with this object
    public abstract bool Interact(GameObject player);

    //called to stop the interaction
    protected abstract void End();

    void OnTriggerEnter2D(Collider2D other)
    {

        //if the circle touches a player
        if (other.gameObject.CompareTag("Player"))
        {
            //make the icon visible and increment the player counter
            icon.SetActive(true);
            playersInCircle++;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        //if a player leaves the circle
        if (other.gameObject.CompareTag("Player"))
        {
            //decrement the player counter
            playersInCircle--;

            //if there are no players in the circle,
            //stop the interaction and make the icon
            //invisible
            if (playersInCircle == 0)
            {
                icon.SetActive(false);
                End();
            }
        }
    }
}