using UnityEngine;
using System.Collections;

public class RootSpawner : MonoBehaviour {

	//root to jabbed up from below players
	public GameObject root;
	//interval between root times, NOTE: counting starts as soon as the previous root appears
	public float intervalBetweenRoots = 7f;
	//y level of the floor in unity
	public float floorLevel;

	//players in the game
	private GameObject[] players;
	//time since last root
	private float lastRootTime = 0f;

	void Start () {

		//get the players
		players = GameObject.FindGameObjectsWithTag("Player");
	}

	void Update () {

		//if it is time to root
		if(Time.time - lastRootTime > intervalBetweenRoots){

			//set the previous root time to now
			lastRootTime = Time.time;

			//create roots below the players
			for(int i = 0; i < players.Length; i++){
				Instantiate(root, new Vector3(players[i].transform.position.x, floorLevel - root.transform.lossyScale.y / 2),
					Quaternion.identity);
			}
		}
	}
}
