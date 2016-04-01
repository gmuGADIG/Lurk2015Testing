using UnityEngine;
using System.Collections;

public class Tree_Torch : MonoBehaviour
{
    bool lit;
    int health = 4;
    bool touched;
    Animator burning;
    public Animator leftBranch;
    public Animator rightBranch;
    public Animator fire;
    int stage;
    public TreeLiftTorch left;
    public TreeLiftTorch right;
    // Use this for initialization

    void Start()
    {
        lit = false;
        touched = false;
        burning = gameObject.GetComponent<Animator>();
        burning.SetBool("isLit", false);
        fire.SetBool("isLit", false);
        //gameObject.GetComponent<Renderer>().material.color = Color.gray;
        //Debug.Log(health);
        stage = 1;
    }

    void Update()
    {
        if (lit)
        {
            Debug.Log("lit");
            health--;
            lit = false;
            StartCoroutine(stopBurning());
            stage++;
            if (left.isLit())
            {
                leftBranch.SetBool("whip", true);
                StartCoroutine(stopLeftWhip());
                Debug.Log("whip");
            }
            if (right.isLit())
            {
                rightBranch.SetBool("whip", true);
                StartCoroutine(stopLeftWhip());
            }
            if (health <= 0)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }

        void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Lantern" && !touched)
        {
            lit = true;
            burning.SetBool("isLit", true);
            fire.SetBool("isLit", true);
            //gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            touched = true;
        }
        //need to add animation for actually lighting the torch. Placeholder will just change the color
        
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.tag == "Lantern" && touched)
        {
            touched = false;
        }
    }

    IEnumerator stopBurning()
    {
        yield return new WaitForSeconds(2.0f);
        burning.SetBool("isLit", false);
        fire.SetBool("isLit", false);
    }

    IEnumerator stopLeftWhip()
    {
        yield return new WaitForSeconds(2.1f);
        leftBranch.SetBool("whip", false);
    }

    IEnumerator stopRightWhip()
    {
        yield return new WaitForSeconds(2.1f);
        rightBranch.SetBool("whip", false);
    }

    public int getHealth()
    {
        return health;
    }

    public int getStage()
    {
        return stage;
    }
}
