using UnityEngine;
using System.Collections;

public class CrawlAfter : MonoBehaviour {

    public float speed = 1f;
    public LayerMask layer;

    private GameObject player;
    private float upDownLine, leftRightline;
    private int direction;
    public bool onGround, onWallLeft, onWallRight, onCeiling;
    private Vector2 upRight, downRight, upLeft, downLeft, down, up;

	// Use this for initialization
	void Start () {
        upDownLine = (this.transform.localScale.y / 2)+.1f;
        leftRightline = (this.transform.localScale.x / 2)+.1f;
    }

    // Update is called once per frame
    void Update()
    {
        upLeft = new Vector3(this.transform.position.x - leftRightline, this.transform.position.y+upDownLine-.15f);
        upRight = new Vector3(this.transform.position.x + leftRightline, this.transform.position.y+upDownLine-.15f);
        downLeft = new Vector2(this.transform.position.x - leftRightline, this.transform.position.y - upDownLine+.15f);
        downRight = new Vector2(this.transform.position.x + leftRightline, this.transform.position.y - upDownLine+.15f);
        down = new Vector2(this.transform.position.x, this.transform.position.y - (upDownLine));
        up = new Vector2(this.transform.position.x, this.transform.position.y + (upDownLine));
        player = this.GetComponent<Enemy>().aggro;

        CheckForWalls();
        Move();
    }

    void CornerMovement()
    {
        Debug.DrawLine(downLeft, new Vector2(downLeft.x, downLeft.y - 0.2f));
        Debug.DrawLine(downRight, new Vector2(downRight.x, downRight.y - 0.2f));
        if (Physics2D.Linecast(downRight, new Vector2(downRight.x, downRight.y - 0.2f), layer))
        {
            if(player.transform.position.x > this.transform.position.x)
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
            else
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
            this.GetComponent<Rigidbody2D>().gravityScale = 0f;

        }
        else if (Physics2D.Linecast(downLeft, new Vector2(downLeft.x, downLeft.y - 0.2f), layer))
        {
            if(player.transform.position.x < this.transform.position.x)
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
            else
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
            this.GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
    }

    void CheckForWalls()
    {
        if (Physics2D.Linecast(upLeft, downLeft, layer))
        {
            onWallLeft = true;
            Debug.DrawLine(upLeft, downLeft, Color.green);
        }
        else
        {
            Debug.DrawLine(upLeft, downLeft, Color.red);
            onWallLeft = false;
        }
        if (Physics2D.Linecast(this.transform.position, up, layer))
        {
            Debug.DrawLine(this.transform.position, up, Color.green);
            onCeiling = true;
        }
        else
        {
            Debug.DrawLine(this.transform.position, up, Color.red);
            onCeiling = false;
        }
        if (Physics2D.Linecast(upRight, downRight, layer))
        {
            Debug.DrawLine(upRight, downRight, Color.green);
            onWallRight = true;
        }
        else
        {
            Debug.DrawLine(upRight, downRight, Color.red);
            onWallRight = false;
        }
        if (Physics2D.Linecast(this.transform.position, down, layer))
        {
            Debug.DrawLine(this.transform.position, down, Color.green);
            onGround = true;
        }
        else
        {
            Debug.DrawLine(this.transform.position, down, Color.red);
            onGround = false;
        }
    }

    void Move()
    {
        if (!(onGround || onCeiling || onWallLeft || onWallRight))
        {
            CornerMovement();
        }
        if (player.transform.position.x - this.transform.position.x < -2f)//if the player is to the left
        {
            if ((onGround || onCeiling) && !onWallLeft)
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
            }
            else if ((onWallLeft || onWallRight) /*&& player.transform.position.y > this.transform.position.y*/)//player is above and this is on the wall
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
            else if ((onWallLeft || onWallRight))//player is above and this is on the wall
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            }
            if (onCeiling || onWallLeft || onWallRight)
                this.GetComponent<Rigidbody2D>().gravityScale = 0f;
            return;
        }
        if(onCeiling)
        {
            this.GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-100f, 100f), 10f));
        }
    }
}
