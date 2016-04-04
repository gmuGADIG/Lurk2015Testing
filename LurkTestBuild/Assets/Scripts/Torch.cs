using UnityEngine;
using System.Collections;

public class Torch : MonoBehaviour
{
    bool lit;
    Animator burning;
    Respawn player1;
    Respawn player2;
    
    void Start()
    {
        lit = false;
        burning = gameObject.GetComponent<Animator>();
        burning.SetBool("isLit", false);

		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		player1 = players [0].GetComponent<Respawn> ();
		if (players.Length > 1)
			player2 = players [1].GetComponent<Respawn> ();

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
            player1.setCheckpoint(this.gameObject);
			if (player2 != null)
				player2.setCheckpoint (this.gameObject);
        }
    }

    public void turnOffTorch()
    {
        lit = false;
        burning.SetBool("isLit", false);
    }
}
