﻿using UnityEngine;
using System.Collections;

public class Torch : MonoBehaviour {
	bool lit;
    bool touched;
    bool treeMoved;
    public Animator tree;
    float startTime;
    float journeyLength;
    Vector2 up;
    Vector2 down;
    float moveTime = 0.0f;
    Animator burning;
	// Use this for initialization
	void Start () {
        touched = false;
		lit = false;
        treeMoved = false;
        burning = gameObject.GetComponent<Animator>();
        burning.SetBool("isLit", false);
        //startTime = Time.time;
        //up = new Vector2(tree.position.x, tree.position.y + 7.0f);
        //down = new Vector2(tree.position.x, tree.position.y);
        //journeyLength = Vector2.Distance(tree.position, up);
    }
    void Update()
    {
        Debug.Log(treeMoved);
        if (lit && !treeMoved)
        {
            liftTree(true);
        }

        if (!lit && treeMoved)
        {
            Debug.Log("move");
            liftTree(false);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Lantern" && !touched)
        {
            lit = true;
            burning.SetBool("isLit", true);
            //gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            touched = true;
        }
        //need to add animation for actually lighting the torch. Placeholder will just change the color
        if (other.tag == "Branch" && !touched)
        {
            lit = false;
            burning.SetBool("isLit", false);
            //gameObject.GetComponent<Renderer>().material.color = Color.gray;
            touched = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Lantern" && touched)
        {
            touched = false;
        }
        if (other.tag == "Branch" && touched)
        {
            touched = false;
        }
    }

    void liftTree(bool goUp)
    {
        //Vector3 start = tree.position;
        //moveTime += Time.deltaTime * 0.05f;
        if (goUp) {
            tree.SetInteger("treeMove", 1);
           // tree.position = Vector3.Lerp(start, up, moveTime);
            //if (tree.position.y >= up.y - 0.05f)
                treeMoved = true;
        }
        else
        {
            tree.SetInteger("treeMove", -1);
            //Debug.Log("Going down");           
            //tree.position = Vector3.Lerp(start, down, moveTime);
            //if (tree.position.y <= down.y + 0.05f)
            treeMoved = false;
        }
    }
   
}
