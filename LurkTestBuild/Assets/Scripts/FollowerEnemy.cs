﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowerEnemy : Enemy {

    Rigidbody2D body;
    //how far away can the enemy detect aggro
    public float aggroDistance = 100f;
    //size of enemy and aggro collider
    Vector3 collideYDist;
    Vector3 collideXDist;
    Vector3 aggroCollideDist;
    //platform layer
    LayerMask platform = 1 << 8;

    //current platforms
    GameObject currentAggroPlatform;
    GameObject currentEnemyPlatform;
    //target platform and jumpPos and direction to reach them
    GameObject targetPlatform;
    Vector2 direction;
	Vector3 jumpPos;
    bool isJumping = false;


    List<GameObject> path;
	Hashtable platforms;


	// initialize all variables
	void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        collideYDist = new Vector3(0, GetComponent<Collider2D>().bounds.extents.y + .1f, 0);
        collideXDist = new Vector3(GetComponent<Collider2D>().bounds.extents.x, 0, 0);
        aggroCollideDist = new Vector3(0, aggro.GetComponent<Collider2D>().bounds.extents.y + .1f, 0);
        path = new List<GameObject>();
		platforms = new Hashtable();
        targetPlatform = null;
    }
	
	// Update is called once per frame
	void Update () {
        //check that player is within range
        float distances = Mathf.Abs((aggro.transform.position - transform.position).magnitude);
	    if(distances < aggroDistance)
        {
            //find platform of aggro
            RaycastHit2D hit = Physics2D.Raycast(aggro.transform.position - aggroCollideDist, -transform.up, .1f, platform);
            if (hit.collider != null)
            {
                //get current aggro and enemy platforms
                currentAggroPlatform = hit.collider.gameObject;
                currentEnemyPlatform = getPlatform();
                //check that path is empty and enemy and player are on a platform
                if (currentAggroPlatform != null && currentEnemyPlatform != null && (path.Count == 0 || currentAggroPlatform != path[path.Count-1]) && currentAggroPlatform != currentEnemyPlatform)
                {
                    //clear the platforms map
                    platforms.Clear();
                    path.Clear();
                    //get the path
                    path = GetPath(currentAggroPlatform);
                    //add aggro's platform at end of path, if a path was found
                    if (path != null)
                    {
                        path.Insert(path.Count, currentAggroPlatform);
                    }
                }
            }

            //player movement
            //check for path to follow
            if (path.Count > 0)
            {
                //check if enemy currently has a target position
                if (targetPlatform == null && isGrounded())
                {
                    //get next target position in path and remove it
                    targetPlatform = path[0];
                    path.Remove(targetPlatform);
                    if (targetPlatform == currentEnemyPlatform)
                    {
                        targetPlatform = null;
                    }
					else
					{
                        //check that enemy on same platform as aggro
                        if (currentEnemyPlatform != currentAggroPlatform)
                        {
                            //get jumpPos based on targetPlatform
                            currentEnemyPlatform = getPlatform();
                            if (targetPlatform == currentEnemyPlatform.GetComponent<PlatformJumping>().jumpDownRight || targetPlatform == currentEnemyPlatform.GetComponent<PlatformJumping>().jumpUpRight)
                            {
                                jumpPos = new Vector3(currentEnemyPlatform.transform.position.x + currentEnemyPlatform.GetComponent<Collider2D>().bounds.extents.x - .2f, transform.position.y, 0);
                            }
                            else
                            {
                                jumpPos = new Vector3(currentEnemyPlatform.transform.position.x - currentEnemyPlatform.GetComponent<Collider2D>().bounds.extents.x + .2f, transform.position.y, 0);
                            }
                            Debug.Log(jumpPos);
                        }
					}
                }
                //enemy has a target position
                else
                {
					//enemy has a target platform
                    if (targetPlatform != null)
                    {
                        //enemy is not on same platform as aggro
                        if (currentAggroPlatform != currentEnemyPlatform)
                        {
                            //enemy is close to jumpPos
                            if (isGrounded())
                            {
                                //enemy is at jump position
                                if (Mathf.Abs(jumpPos.x - transform.position.x) < .25f && targetPlatform.transform.position.y > transform.position.y)
                                {
                                    //get the force needed to jump and jump
                                    Vector2 force = CalculateForce();
                                    Debug.DrawLine(transform.position, force);
                                    isJumping = true;
                                    body.velocity = force;
                                }
                                //enemy is moving towards target along current platform
                                else
                                {
                                    //get direction to jumpPos and move enemy
                                    direction = new Vector2(targetPlatform.transform.position.x - transform.position.x, 0);
                                    direction.Normalize();
                                    transform.Translate(direction * 5 * Time.deltaTime);
                                }
                            }
                            else
                            {
                                //target is moving from platform above target and didn't jump
                                if (targetPlatform.transform.position.y < transform.position.y && !isJumping)
                                {
                                    //get direction to targetPlatform and move enemy
                                    direction = new Vector2(targetPlatform.transform.position.x - transform.position.x, 0);
                                    direction.Normalize();
                                    transform.Translate(direction * 15 * Time.deltaTime);
                                }
                            }
                        }
                        else
                        {
                            //get direction to aggro and move enemy
                            direction = new Vector2(aggro.transform.position.x - transform.position.x, 0);
                            direction.Normalize();
                            transform.Translate(direction * 5 * Time.deltaTime);
                        }
                    }
                }
            }

        }
	}

    //player has hit new platform and gets ready for new target
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == targetPlatform)
        {
            targetPlatform = null;
            jumpPos = Vector2.zero;
            isJumping = false;
        }
    }

    //get force for jumping
    Vector2 CalculateForce()
    {
        Vector3 targetPos = targetPlatform.transform.position;
        targetPos.y += .6f;
        Vector3 dir = targetPos - transform.position;
        if (dir.x > 0)
            dir.x += 1f;
        else
            dir.x -= 1f;
        Vector3 dirFlat = dir;
        dirFlat.y = 0;

        float height = dir.y;
        float xz = dirFlat.magnitude;

        float v0y = height / 2 + 1f* Physics.gravity.magnitude * 4;
        float v0xz = xz / 2 + xz;

        Vector3 result = dirFlat.normalized;
        result *= v0xz;
        result.y = v0y;


        return result;
    }

    //gets current platform for enemy
    GameObject getPlatform()
    {
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - collideYDist - collideXDist, -transform.up - transform.right * 1.1f, .1f, platform);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position - collideYDist + collideXDist, -transform.up + transform.right * 1.1f, .1f, platform);
        if (hitLeft.collider != null)
        {
            return hitLeft.collider.gameObject;
        }
        else if (hitRight.collider != null)
        {
            return hitRight.collider.gameObject;
        }
        return null;
    }

    //check if enemy is grounded and return it 
    bool isGrounded()
    {
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - collideYDist - collideXDist, -transform.up - transform.right * 1.1f, .1f, platform);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position - collideYDist + collideXDist, -transform.up + transform.right * 1.1f, .1f, platform);
        if (hitLeft.collider != null)
        {
            return hitLeft.collider.gameObject;
        }
        else if (hitRight.collider != null)
        {
            return hitRight.collider.gameObject;
        }
        return false;
    }
	
	//reconstruct path to aggro's platform
	List<GameObject> ReconstructPath(GameObject finish)
	{
		List<GameObject> returnPath = new List<GameObject>();
		while (platforms[finish] != null)
		{
			returnPath.Add(finish);
            finish = (GameObject)platforms[finish];
		}
		return returnPath;
	}
	
	//find optimal path to aggro's platform
	List<GameObject> GetPath(GameObject finish)
	{
        //gameobjects to check
        Queue<GameObject> openList = new Queue<GameObject>();
        //gameobjects checked
        List<GameObject> closedList = new List<GameObject>();
        //add finish to queue to check
        openList.Enqueue(finish);
        //while there is a gameobject left to check
        while (openList.Count > 0)
        {
        //get the first gameobject from openlist
            GameObject checking = openList.Dequeue();
            //add it to the closed list
            closedList.Add(checking);
            //base case, check for target platform
            if (checking == currentEnemyPlatform)
            {
                //rebuild path and return the list
                return ReconstructPath(checking);
            }
            else
            {
				//check each jump path from checking
                for (int i = 0; i < 4; i++)
                {
                    if (i==0)
                    {
						//go up left
                        if (checking.GetComponent<PlatformJumping>().jumpUpLeft != null)
                        {
							//get the platform if not null
                            GameObject next = checking.GetComponent<PlatformJumping>().jumpUpLeft;
							//if not already in closedlist, add next to map with value of checking, add next to openlist
                            if (!closedList.Contains(next))
                            {
                                platforms[next] = checking;
                                openList.Enqueue(next);
                            }
                        }
                    }
                    else if (i == 1)
                    {
                        //go up right
                        if (checking.GetComponent<PlatformJumping>().jumpUpRight != null)
                        {
                            GameObject next = checking.GetComponent<PlatformJumping>().jumpUpRight;
                            if (!closedList.Contains(next))
                            {
                                platforms[next] = checking;
                                openList.Enqueue(next);
                            }
                        }
                    }
                    else if (i == 2)
                    {
                        //go down left
                        if (checking.GetComponent<PlatformJumping>().jumpDownLeft != null)
                        {
                            GameObject next = checking.GetComponent<PlatformJumping>().jumpDownLeft;
                            if (!closedList.Contains(next))
                            {
                                platforms[next] = checking;
                                openList.Enqueue(next);
                            }
                        }
                    }
                    else
                    {
                        //go down right
                        if (checking.GetComponent<PlatformJumping>().jumpDownRight != null)
                        {
                            GameObject next = checking.GetComponent<PlatformJumping>().jumpDownRight;
                            if (!closedList.Contains(next))
                            {
                                platforms[next] = checking;
                                openList.Enqueue(next);
                            }
                        }
                    }
                }
            }
        }
        //hasn't been found return empty path
        return path;
	}

}
