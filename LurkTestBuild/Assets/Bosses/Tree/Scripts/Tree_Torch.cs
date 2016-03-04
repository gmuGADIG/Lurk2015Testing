using UnityEngine;
using System.Collections;

public class Tree_Torch : MonoBehaviour
{
    public GameObject Boss;
    bool lit;
    public int health = 4;
    bool touched;
    public GameObject leftBranch;
    public GameObject rightBranch;
    public GameObject leftTorch;
    public GameObject rightTorch;
    Animator burning;
    public Animator fire;
    public int stage;
    float startTime;
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
        startTime = Time.time;
        stage = 1;
    }
    void Update()
    { 
        if (lit)
        {
            health -= 1;
            lit = false;
            StartCoroutine(stopBurning());
            //gameObject.GetComponent<Renderer>().material.color = Color.gray;
            stage++;
            StartCoroutine(WhipLeftBranch(new Vector3(leftTorch.transform.position.x, leftTorch.transform.position.y + 1.0f, leftTorch.transform.position.z), 1.0f));
            StartCoroutine(WhipRightBranch(new Vector3(rightTorch.transform.position.x, rightTorch.transform.position.y + 1.0f, rightTorch.transform.position.z), 1.0f));
           // Debug.Log(health);
        }
        if (health <= 0)
        {
            Destroy(transform.parent.gameObject);
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

    public IEnumerator WhipLeftBranch(Vector3 newPosition, float delay)
    {
        float elapsedTime = 0;
        Vector2 startingPos = leftBranch.transform.position;
        while (elapsedTime < delay)
        {
            leftBranch.transform.position = Vector3.Lerp(startingPos, newPosition, (elapsedTime / delay));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0;
        while (elapsedTime < delay)
        {
            leftBranch.transform.position = Vector3.Lerp(newPosition, startingPos, (elapsedTime / delay));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator WhipRightBranch(Vector3 newPosition, float delay)
    {
        float elapsedTime = 0;
        Vector2 startingPos = rightBranch.transform.position;
        while (elapsedTime < delay)
        {
            rightBranch.transform.position = Vector3.Lerp(startingPos, newPosition, (elapsedTime / delay));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0;
        while (elapsedTime < delay)
        {
            rightBranch.transform.position = Vector3.Lerp(newPosition, startingPos, (elapsedTime / delay));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    /*IEnumerator WhipBranch(char direction, float delay){
            yield return new WaitForSeconds(delay);
            float journeyLength;
            Vector2 slamAt;
            if (direction == 'l')
            {
                journeyLength = Vector2.Distance(leftBranch.position, leftTorch.position);
                slamAt = leftBranch.position + leftTorch.position;
            }
            else {
                journeyLength = Vector2.Distance(rightTorch.position, rightBranch.position);
                slamAt = rightBranch.position + rightTorch.position;
            }
            float distCovered = (Time.time - startTime) * 0.25f;
            float fracJourney = distCovered / journeyLength;
        if (direction == 'l')
            leftBranch.GetComponent<Rigidbody2D>().velocity += 10.0f;
        else
            rightBranch.position = Vector3.Lerp(rightBranch.position, rightTorch.position, fracJourney);

        }*/
}
