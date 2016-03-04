using UnityEngine;
using System.Collections;

public class Tree_Spawner : MonoBehaviour {
    public GameObject spider;
    public Transform[] spiderSpawners_Stage1;
    public Transform[] spiderSpawners_Stage2;
    public GameObject rootMan;
    public Transform[] rootManSpawners;
    public GameObject root;
    public int numOfRoots;
    public GameObject woodMan;
    public Transform[] woodSpawn;
    public GameObject shadow;
    public Tree_Torch boss;
    public Transform aggro;
    public Transform[] torches;
    public float attackDelay;
    float time;
    int randAttack1;
    int randAttack2;
	// Use this for initialization
	void Start () {
        attackDelay = 10.0f;
        time = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if(attackDelay > time)
            time += Time.deltaTime;
        else
        {
            if (boss.stage == 1)
            {
                randAttack1 = Random.Range(0, 2);
                randAttack2 = -1;
            }
            if (boss.stage >= 2) {
                randAttack1 = Random.Range(0, 7);
                attackDelay = 7.0f;
                randAttack2 = -1;
            }
            if (boss.stage == 3)
            {
                randAttack1 = Random.Range(0, 7);
                attackDelay = 6.0f;
                randAttack2 = Random.Range(0, 7);
                if(randAttack1 == randAttack2)
                {
                    if (randAttack2 > 0)
                        randAttack2--;
                    else
                        randAttack2++;
                }
            }
            //Stage 1
            if (randAttack1 == 0 || randAttack2 == 0) //Spiders
            {
                for (int s = 0; s < spiderSpawners_Stage1.Length; s++)
                    Instantiate(spider, spiderSpawners_Stage1[s].position, transform.rotation);
            }
            if (randAttack1 == 1 || randAttack2 == 1) //rootMen
            {
                for (int rm = 0; rm < rootManSpawners.Length; rm++)
                    Instantiate(rootMan, rootManSpawners[rm].position, transform.rotation);
            }
            if (randAttack1 == 2 || randAttack2 == 2)//Roots
            {
                for (int r = 0; r < numOfRoots; r++)
                {
                    float rootPos = Random.Range(-7.0f, 7.0f);
                    Instantiate(root, new Vector2(rootPos, -2.0f), transform.rotation);
                }
            }
            //Stage 2
            if(randAttack1 == 3 || randAttack2 == 3)//Swat
            {
                StartCoroutine(boss.WhipLeftBranch(aggro.position, 1.0f));
                StartCoroutine(boss.WhipRightBranch(aggro.position, 1.0f));
            }
            if(randAttack1 == 4 || randAttack2 == 4)
            {
                for (int s = 0; s < spiderSpawners_Stage2.Length; s++)
                    Instantiate(spider, spiderSpawners_Stage2[s].position, transform.rotation);
            }
            if (randAttack1 == 5 || randAttack2 == 5)
            {
                for (int w = 0; w < woodSpawn.Length; w++)
                    Instantiate(spider, woodSpawn[w].position, transform.rotation);
            }
            if (randAttack1 == 6 || randAttack2 == 6)
            {
                StartCoroutine(boss.WhipLeftBranch(torches[0].position, 1.0f));
                StartCoroutine(boss.WhipRightBranch(torches[1].position, 1.0f));
            }
        }
	}
}
