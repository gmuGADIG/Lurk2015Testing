using UnityEngine;
using System.Collections;

public class Torch : MonoBehaviour
{
    bool lit;
    Animator burning;
    //Respawn player1;
    //Respawn player2;
    
    void Start()
    {
        lit = false;
        burning = gameObject.GetComponent<Animator>();
        burning.SetBool("isLit", false);

    }
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Lantern" && !lit)
        {
            lit = true;
            burning.SetBool("isLit", true);
            //Respawn.setCheckpoint(this.gameObject());
        }
    }

    public void turnOffTorch()
    {
        lit = false;
        burning.SetBool("isLit", false);
    }
}
