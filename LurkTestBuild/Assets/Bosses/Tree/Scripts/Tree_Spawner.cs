using UnityEngine;
using System.Collections;

public class Tree_Spawner : MonoBehaviour {
    public Tree_Torch boss;
    public Transform aggro;
    public float attackDelay;
    public Transform[] torches;
   
    public GameObject spider;
	GameObject[] spiders;
	bool spawnedSpider;

	GameObject[] rootMen;
	bool spawnedRootMan;

	public Transform[] spiderSpawners;
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
		spiders = new GameObject[4];
		spawnedSpider = false;
		rootMen = new GameObject[2];
		spawnedRootMan = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(attackDelay > time)
            time += Time.deltaTime;
        else
        {
			
			if (boss.getStage () == 1) { //Stage 1
				attackDelay = 3.0f;
				randAttack1 = Random.Range (0, 3);
				//Debug.Log (randAttack1);
				if (randAttack1 == 0) { //Spiders
					spawnedSpider = false;
					for (int s = 0; s < spiderSpawners.Length / 2; s++) {
						//Debug.Log (spiders [s]);
						if (spiders [s] == null) {
							spiders [s] = (GameObject)Instantiate (spider, spiderSpawners [s].position, transform.rotation);
							spawnedSpider = true;
						}
					}
					if (!spawnedSpider)
						randAttack1 = Random.Range (1, 3);
				} 

				if (randAttack1 == 1) { //rootMen
					spawnedRootMan = false;
					for (int rm = 0; rm < rootManSpawners.Length; rm++){
						if (rootMen [rm] == null) {
							rootMen [rm] = (GameObject)Instantiate (rootMan, rootManSpawners [rm].position, transform.rotation);
							spawnedRootMan = true;
						}

					}
					if (!spawnedRootMan)
						randAttack1 = 2;
				} 

				if (randAttack1 == 2) { //Roots
					int r = Random.Range (0, roots.Length);
					root = roots [r].transform.GetChild (0).gameObject;
					root.GetComponent<Animator> ().SetBool ("Rise", true);
				}
			} 
			else if (boss.getStage () == 2) { //Stage 2
				attackDelay = 7.0f;
				randAttack1 = Random.Range (0, 7);
				
				if (randAttack1 == 0) { //Spiders
					spawnedSpider = false;
					for (int s = 0; s < spiderSpawners.Length / 2; s++) {
						if (spiders [s] == null) {
							spiders [s] = (GameObject)Instantiate (spider, spiderSpawners [s].position, transform.rotation);
							spawnedSpider = true;
						}
					}
					if (!spawnedSpider)
						randAttack1 = Random.Range (2, 7);
				} 
				else if (randAttack1 == 1) {
					spawnedSpider = false;
					for (int s = 2; s < spiderSpawners.Length; s++) {
						if (spiders [s] == null) {
							spiders [s] = (GameObject)Instantiate (spider, spiderSpawners [s].position, transform.rotation);
							spawnedSpider = true;
						}
					}
					if (!spawnedSpider)
						randAttack1 = Random.Range (2, 7);
				} 

				if (randAttack1 == 2) { //rootMen
					spawnedRootMan = false;
					for (int rm = 0; rm < rootManSpawners.Length; rm++){
						if (rootMen [rm] == null) {
							rootMen [rm] = (GameObject)Instantiate (rootMan, rootManSpawners [rm].position, transform.rotation);
							spawnedRootMan = true;
						}

					}
					if (!spawnedRootMan)
						randAttack1 = Random.Range (3, 7);
				} 

				if (randAttack1 == 3) {//Roots
					int r = Random.Range (0, roots.Length);
					root = roots [r].transform.GetChild (0).gameObject;
					root.GetComponent<Animator> ().SetBool ("Rise", true);

					int ro = Random.Range (0, roots.Length);
					while (r == ro)
						ro = Random.Range (0, roots.Length);
					
					root = roots [ro].transform.GetChild (0).gameObject;
					root.GetComponent<Animator> ().SetBool ("Rise", true);
				}
               
				else if (randAttack1 == 4) {//Swat
					boss.leftBranch.SetBool ("whip", true);
					StartCoroutine (boss.stopLeftWhip ());
				}
				else if (randAttack1 == 5) {
					boss.rightBranch.SetBool ("whip", true);
					StartCoroutine (boss.stopRightWhip ());
				}
				/*if (randAttack1 == 5 || randAttack2 == 5)
                {
                    for (int w = 0; w < woodSpawn.Length; w++)
                        Instantiate(spider, woodSpawn[w].position, transform.rotation);
                }*/
			} else if (boss.getStage () == 3) {
				attackDelay = 6.0f;

				randAttack1 = Random.Range (0, 7);

				if (randAttack1 == 0 || randAttack2 == 0) { //Spiders
					spawnedSpider = false;
					for (int s = 0; s < spiderSpawners.Length / 2; s++) {
						if (spiders [s] == null) {
							spiders [s] = (GameObject)Instantiate (spider, spiderSpawners [s].position, transform.rotation);
							spawnedSpider = true;
						}
					}
					if (!spawnedSpider) {
						randAttack1 = Random.Range (2, 7);
						randAttack2 = Random.Range (2, 7);
					}
				} 
				if (randAttack1 == 1 || randAttack2 == 1) {
					spawnedSpider = false;
					for (int s = 2; s < spiderSpawners.Length; s++) {
						if (spiders [s] == null) {
							spiders [s] = (GameObject)Instantiate (spider, spiderSpawners [s].position, transform.rotation);
							spawnedSpider = true;
						}
					}
					if (!spawnedSpider) {
						randAttack1 = Random.Range (2, 7);
						randAttack2 = Random.Range (2, 7);
					}
				} 

				if (randAttack1 == 2 || randAttack2 == 2) { //rootMen
					spawnedRootMan = false;
					for (int rm = 0; rm < rootManSpawners.Length; rm++){
						if (rootMen [rm] == null) {
							rootMen [rm] = (GameObject)Instantiate (rootMan, rootManSpawners [rm].position, transform.rotation);
							spawnedRootMan = true;
						}
					}
					if (!spawnedRootMan) {
						randAttack1 = Random.Range (3, 7);
						randAttack2 = Random.Range (3, 7);
					}
				}

				if (randAttack1 == 3 || randAttack2 == 3)//Roots
				{
					int r1 = Random.Range (0, roots.Length);
					root = roots [r1].transform.GetChild (0).gameObject;
					root.GetComponent<Animator> ().SetBool ("Rise", true);

					int r2 = Random.Range (0, roots.Length);
					while (r1 == r2)
						r2 = Random.Range (0, roots.Length);

					root = roots [r2].transform.GetChild (0).gameObject;
					root.GetComponent<Animator> ().SetBool ("Rise", true);

					int r3 = Random.Range (0, roots.Length);
					while (r1 == r3 || r2 == r3)
						r3 = Random.Range (0, roots.Length);

					root = roots [r3].transform.GetChild (0).gameObject;
					root.GetComponent<Animator> ().SetBool ("Rise", true);
				}

				if (randAttack1 == 4 || randAttack2 == 4)//Swat
				{
					boss.leftBranch.SetBool("whip", true);
					StartCoroutine(boss.stopLeftWhip());
				}
				if (randAttack1 == 5 || randAttack2 == 5)
				{
					boss.rightBranch.SetBool("whip", true);
					StartCoroutine(boss.stopRightWhip());
				}
			}
            
            
            time = 0.0f;
        }
	}
}
