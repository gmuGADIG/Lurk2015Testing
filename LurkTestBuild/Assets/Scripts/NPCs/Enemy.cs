using UnityEngine;
using System;
using System.Collections.Generic;

//base enemy script: all enemy prefabs should have this component
public class Enemy : MonoBehaviour {

	//the current aggro
	public GameObject aggro;

	//iterate over this to handle lantern behaviours
	public IEnumerable<GameObject> lanterns {
		get {
			foreach (GameObject lantern in lanternArray) {
				yield return lantern;
			}
		}
	}

	//the lanterns in the scene
	GameObject[] lanternArray;

	void Awake() {
		UpdateLanternArray();
	}

	public void UpdateLanternArray() {
		lanternArray = GameObject.FindGameObjectsWithTag("Lantern");
    }


}