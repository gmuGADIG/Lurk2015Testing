using UnityEngine;
using System.Collections;

public class DistanceAggro : MonoBehaviour {

    public float radius = 10f;
    public float aggroShiftDelay;

    private Enemy enemyScript;
    private GameObject player1, player2;
    private float timer;

    // Use this for initialization
    void Start () {
	    GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player");
        player1 = players[0];
        player2 = players[1];
        enemyScript = this.GetComponent<Enemy>();
        timer = aggroShiftDelay;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(timer);
        float player1distance = Mathf.Abs((player1.transform.position - this.transform.position).magnitude);
        float player2distance = Mathf.Abs((player2.transform.position - this.transform.position).magnitude);
        GameObject target;
        if (player2distance < radius || player1distance < radius) {
            if (player2distance < radius && player1distance < radius && timer <= 0) {
                if (player1distance > player2distance) {
                    target = player2;
                } else {
                    target = player1;
                }
            }else if(player1distance > player2distance) {
                target = player2;
            } else {
                target = player1;
            }
        } else {
            target = null;
        }
        if (enemyScript.aggro != target) {
            timer -= Time.deltaTime;
            if(timer <= 0) {
                enemyScript.aggro = target;
                timer = aggroShiftDelay;
            }
        }

    }
}

