using UnityEngine;
using System.Collections;

public class CrawlAfter : MonoBehaviour {

    public float speed = 1f;
    public LayerMask layer;
    private GameObject player;

    private int direction;
    public bool onGround, onWallLeft, onWallRight, onCeiling;
    private ArrayList bestPath, testForBest;

	// Use this for initialization
	void Start () {
        bestPath = new ArrayList();
        testForBest = new ArrayList();
        testForBest.Add(this.transform.position);
	}

    // Update is called once per frame
    void Update()
    {
        player = this.GetComponent<Enemy>().aggro;
        Debug.DrawLine(this.transform.position, new Vector3(this.transform.position.x - 1f, this.transform.position.y, this.transform.position.z), Color.white);
        Debug.DrawLine(this.transform.position, new Vector3(this.transform.position.x + 1f, this.transform.position.y, this.transform.position.z), Color.white);
        Debug.DrawLine(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y - 1f, this.transform.position.z), Color.white);
        Debug.DrawLine(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z), Color.white);

        CheckForWalls();

        Move();
    }

    void CheckForWalls()
    {
        if (Physics2D.Linecast(this.transform.position, new Vector3(this.transform.position.x - .6f, this.transform.position.y, this.transform.position.z),layer))
            onWallLeft = true;
        else
            onWallLeft = false;
        if (Physics2D.Linecast(this.transform.position, new Vector3(this.transform.position.x + .6f, this.transform.position.y, this.transform.position.z),layer))
            onWallRight = true;
        else
            onWallRight = false;
        if (Physics2D.Linecast(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y-.6f, this.transform.position.z),layer))
            onGround = true;
        else
            onGround = false;
        if (Physics2D.Linecast(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y + .6f, this.transform.position.z),layer))
            onCeiling = true;
        else
            onCeiling = false;
    }

    void Move()
    {
        if (player.transform.position.x - this.transform.position.x < -2f)//if the player is to the left
        {
            if ((onGround || onCeiling) && !onWallLeft)
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
            }
            else if ((onWallLeft || onWallRight) && player.transform.position.y > this.transform.position.y)//player is above and this is on the wall
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            }
            if(onCeiling || onWallLeft || onWallRight)
                this.GetComponent<Rigidbody2D>().gravityScale = 0f;
            return;
        }
        else if (player.transform.position.x - this.transform.position.x > 2f)//if the player is to the right
        {
            if ((onGround || onCeiling) && !onWallRight)
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
            }
            else if ((onWallLeft || onWallRight) && player.transform.position.y > this.transform.position.y)//player is above and this is on the wall
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            }
            if (onCeiling)
                this.GetComponent<Rigidbody2D>().gravityScale = 0f;
            return;
        }
        if(onCeiling)
        {
            this.GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
    }
}
