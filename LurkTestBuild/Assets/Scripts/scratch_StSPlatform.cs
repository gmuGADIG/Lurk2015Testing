using UnityEngine;
using System.Collections;

public class scratch_StSPlatform : MonoBehaviour {

    public bool isActive = true;
    public Rigidbody2D platform_body;
	public float cycle_time = 5;
	float last_check;
	float cur_check;
	int direction_val = 1;
	public float speed_val = 1;
	// Use this for initialization
	void Start () { 
		platform_body = GetComponent<Rigidbody2D>();
        if (isActive)
        {
            last_check = Time.time;
            platform_body.velocity = new Vector2(speed_val * direction_val, 0);
            if (cycle_time == 0)
            {
                platform_body.velocity = new Vector2(0, 0);
            }
        }
	}

	// Update is called once per frame
	void Update () {
        if(isActive)
        {
            cur_check = Time.time;
            if (cycle_time == 0)
            {

            }
            else
            {
                cur_check = Time.time;
                //Every y seconds, set the platform to move to the set position
                //Positions should swap every y seconds, in a cycle.
                if (cur_check - last_check >= cycle_time)
                {
                    direction_val *= -1;
                    platform_body.velocity = new Vector2(speed_val * direction_val, 0);
                    last_check = cur_check;
                }
            }
        }
	}
	
	//Methods to control destination points
	//An array of positions?
	public void Activate()
    {
        isActive = true;
    }
}
