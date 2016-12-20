using UnityEngine;
using System.Collections;

public class TreeLiftTorch : MonoBehaviour {
    public bool lit;
    public bool touched;
    public bool treeMoved;
    public Animator tree;
    Animator burning;
	// Use this for initialization
	void Start () {
        touched = false;
		lit = false;
        treeMoved = false;
        burning = gameObject.GetComponent<Animator>();
        burning.SetBool("isLit", false);

    }
    void Update()
    {
        if (lit && !treeMoved)
        {
            liftTree(true);
        }

        if (!lit && treeMoved)
        {
            liftTree(false);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Boss")
            Debug.Log(other.name);
        if (other.tag == "Lantern" && !touched && !lit)
        {
            Debug.Log("Lantern");
            lit = true;
            burning.SetBool("isLit", true);
            touched = true;
        }
        if (other.tag == "Branch" && !touched && lit)
        {
            Debug.Log("branch");
            lit = false;
            burning.SetBool("isLit", false);
            touched = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Lantern" && touched)
        {
            touched = false;
            Debug.Log("Lantern gone");
        }
        if (other.tag == "Branch" && touched)
        {
            touched = false;
            Debug.Log("branch gone");
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
   
    public bool isLit()
    {
        return lit;
    }
}
