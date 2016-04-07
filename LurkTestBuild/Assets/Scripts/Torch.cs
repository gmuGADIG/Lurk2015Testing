using UnityEngine;
using System.Collections;

public class Torch : MonoBehaviour
{
    private bool lit = false;
    private bool playersFound = false;
    private Animator burning;
    private Respawn player1;
    private Respawn player2;
    public GameObject[] actionObjects;
    
    void Start()
    {
        lit = false;
        burning = gameObject.GetComponent<Animator>();
        burning.SetBool("isLit", false);
    }
    void Update()
    {

    }

    private void findPlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            player1 = players[0].GetComponent<Respawn>();
            if (players.Length > 1)
                player2 = players[1].GetComponent<Respawn>();
            playersFound = true;
            return;
        }

        /*GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (playersFound != null)
        {
            player1 = players[0].GetComponent<Respawn>();
            return;
        }*/
        Debug.Log("No GO with tag \"Player\"");
        playersFound = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Lantern" && !lit)
        {
            if (!playersFound)
                findPlayers();
            if (!playersFound)
            {
                Debug.LogError("Torch.cs - Players Not Found!");
                return;
            }
            lit = true;
            burning.SetBool("isLit", true);
            foreach (GameObject actionObj in actionObjects)
            {
                if (gameObject.activeSelf)
                {
                    actionObj.SendMessage("Activate");
                }
                else{
                    actionObj.SetActive(true);
                }
            }
            player1.setCheckpoint(this.gameObject);
            if (player2 != null)
                player2.setCheckpoint(this.gameObject);
        }
    }

    public void turnOffTorch()
    {
        lit = false;
        burning.SetBool("isLit", false);
    }
}
