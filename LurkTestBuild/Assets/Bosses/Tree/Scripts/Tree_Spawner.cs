using UnityEngine;
using System.Collections;

public class Tree_Spawner : MonoBehaviour {
    public Tree_Torch boss;
    public Transform aggro;
    public float attackDelay;
    public Transform[] torches;
   
    public GameObject spider;
    public Transform[] spiderSpawners_Stage1;
    public Transform[] spiderSpawners_Stage2;
    public GameObject rootMan;
    public Transform[] rootManSpawners;
    public GameObject[] roots;
	public GameObject root;
    /*public GameObject woodMan;
    public Transform[] woodSpawn;
    public GameObject shadow;*/  
    float time;
    int randAttack1;
    int randAttack2;
	// Use this for initialization
	void Start () {
        time = 0.0f;
		roots = GameObject.FindGameObjectsWithTag ("Root");
	}
	
	// Update is called once per frame
	void Update () {
        if(attackDelay > time)
            time += Time.deltaTime;
        else
        {
            if (boss.getStage() == 1) //Stage 1
            {
                attackDelay = 10.0f;
                randAttack1 = Random.Range(0, 3);
                if (randAttack1 == 0) //Spiders
                {
                    for (int s = 0; s < spiderSpawners_Stage1.Length; s++)
                        Instantiate(spider, spiderSpawners_Stage1[s].position, transform.rotation);
                }
                else if (randAttack1 == 1) //rootMen
                {
                    for (int rm = 0; rm < rootManSpawners.Length; rm++)
                        Instantiate(rootMan, rootManSpawners[rm].position, transform.rotation);

                }
                else //Roots
                {
                    int r = Random.Range(0, roots.Length);
					//Debug.Log (r);
					root = roots [r].transform.GetChild (0).gameObject;
                    root.GetComponent<Animator>().SetBool("Rise", true);
                }
            }
            else if (boss.getStage() == 2) //Stage 2
            {
                attackDelay = 7.0f;
                randAttack1 = Random.Range(0, 8);
                if (randAttack1 == 0) //Spiders
                {
                    for (int s = 0; s < spiderSpawners_Stage1.Length; s++)
                        Instantiate(spider, spiderSpawners_Stage1[s].position, transform.rotation);
                }
                else if (randAttack1 == 1)
                {
                    for (int s = 0; s < spiderSpawners_Stage2.Length; s++)
                        Instantiate(spider, spiderSpawners_Stage2[s].position, transform.rotation);
                }
                else if (randAttack1 == 2) //rootMen
                {
                    for (int rm = 0; rm < rootManSpawners.Length; rm++)
                        Instantiate(rootMan, rootManSpawners[rm].position, transform.rotation);

                }
                else if (randAttack1 == 3)//Roots
                {
                    //for (int r = 0; r < numOfRoots; r++)
                    //{
                        
                   // }
                }
               
                /*if (randAttack1 == 3 || randAttack2 == 3)//Swat
                {
                    //StartCoroutine(boss.WhipRightBranch(aggro.position, 1.0f));
                }
                if (randAttack1 == 6 || randAttack2 == 6)
                {
                    //StartCoroutine(boss.WhipLeftBranch(torches[0].position, 1.0f));
                }
                if (randAttack1 == 5 || randAttack2 == 5)
                {
                    for (int w = 0; w < woodSpawn.Length; w++)
                        Instantiate(spider, woodSpawn[w].position, transform.rotation);
                }
                */
            }
            else if (boss.getStage() == 3)
                attackDelay = 6.0f;
            
            
            time = 0.0f;
        }
	}
}
