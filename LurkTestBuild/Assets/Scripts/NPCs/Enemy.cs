using UnityEngine;
using System;
using System.Collections.Generic;

//base enemy script: all enemy prefabs should have this component
public class Enemy : MonoBehaviour {

	//iterate over this to handle player behaviours/aggro/etc
	public IEnumerable<GameObject> players {
		get {
			foreach (GameObject player in playerArray) {
				yield return player;
			}
		}
	}

	//iterate over this to handle lantern behaviours
	public IEnumerable<GameObject> lanterns {
		get {
			foreach (GameObject lantern in lanternArray) {
				yield return lantern;
			}
		}
	}

	//the players in the scene
	GameObject[] playerArray;
	//the lanterns in the scene
	GameObject[] lanternArray;

	void Awake() {
		UpdatePlayersAndLanterns();
	}

	public void UpdatePlayersAndLanterns() {
		playerArray = GameObject.FindGameObjectsWithTag("Player");
		lanternArray = GameObject.FindGameObjectsWithTag("Lantern");
	}

#if UNITY_EDITOR
	void Update() {
		if (Input.GetKeyDown(KeyCode.F5)) {
			UpdatePlayersAndLanterns();
		}
	}
#endif
}