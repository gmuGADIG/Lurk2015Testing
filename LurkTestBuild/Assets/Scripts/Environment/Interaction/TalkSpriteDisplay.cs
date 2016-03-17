using UnityEngine;
using System.Collections;

public class TalkSpriteDisplay : MonoBehaviour {

    //the sprite sheet to be programmatically cut up
    public Texture2D t2d;

    //the cut up sprites
    private Sprite[,] playerSprites;

    private Sprite[] npcSprites;

    //left picture to be displayed
    private SpriteRenderer leftSprite;
    //right picture to be displayed
    private SpriteRenderer rightSprite;

    void Start () {

        //get the left and right sprite from the conversation
        leftSprite = transform.FindChild("Left Sprite").gameObject.GetComponent<SpriteRenderer>();
        rightSprite = transform.FindChild("Right Sprite").gameObject.GetComponent<SpriteRenderer>();

        //testing size
        playerSprites = new Sprite[2, 2];
        npcSprites = new Sprite[2];

        //testing cut
        for (int y = 0; y < playerSprites.GetLength(1); y++)
        {
            for (int x = 0; x < playerSprites.GetLength(0); x++)
            {
                Sprite newSprite = Sprite.Create(t2d, new Rect(x * 128, y * 128, 128, 128), new Vector2(0.5f, 0.5f));
                playerSprites[x, y] = newSprite;
            }
        }

        //get the npcs
        for(int i = 0; i < npcSprites.Length; i++)
        {
            Sprite newSprite = Sprite.Create(t2d, new Rect(i * 128 + (playerSprites.GetLength(0) ) * 128, 128, 128, 128),
                new Vector2(0.5f, 0.5f));
            npcSprites[i] = newSprite;
        }

        //testing stuff
        leftSprite.sprite = playerSprites[0, 0];
        rightSprite.sprite = playerSprites[1, 1];

        //make the conversation invisible
        gameObject.SetActive(false);
    }

    //set the left sprite to the x y coord in the player sprites
    public void setLeft(int x, int y)
    {
        leftSprite.sprite = playerSprites[x, y];
    }

    //determine if it is a player or npc then set is acoordingly
    public void setRight(int x, int y)
    {
        rightSprite.sprite = (x < 2) ? playerSprites[x, y] : npcSprites[x - 2];
    }
}