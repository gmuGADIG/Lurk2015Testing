using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {

	Camera cam;							// Main Camera
	Transform player1;					// Player1
	Transform player2;					// Player2

	public float minDist = 5f;			// Minimum distance to zoom
	//public float maxDist = 10f;		// Maximum distance to zoom
	public float zoomScale = 1f;		// Zoom scale (multiplied to distance to get camera size)
	public float logBase = 2;			// Base of the log used for zoom (log base [logBase](distance between players * [zoomscale]) = size of camera
	public float lerpT = .5f;			// Lerp interpolant [0-1]
	public int damage = 1;				// Amount of damage to be given/sec when off camera

	// Use this for initialization
	void Start () {
		// Find all objects if not already linked
		if(cam == null)
			cam = FindObjectOfType<Camera>();
		if(player1 == null || player2 == null) {
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
			if(players.Length < 2){
				Debug.LogError("Only one player tagged!");
				return;
			}else if(players.Length > 2)
				Debug.LogWarning("More than two players tagged.");
			player1 = players[0].transform;
			player2 = players[1].transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
		Zoom ();
		if(!IsVisible())
			DoDamage();
	}

	void Move () {
		Vector3 middle = (player1.position + player2.position) / 2;	// Middle point between players
		middle.z -= 10;	// Move camera to not clip with scene
		transform.position = Vector3.Lerp(transform.position, middle, lerpT);	// Lerp to location
	}

	void Zoom () {
		float distance = Vector3.Distance(player1.position, player2.position);	// Distance between players

		// Check camera bounds
		if(distance < minDist)
			distance = minDist;
		/* Not really needed on a log scale. If used, uncomment decleration of maxDist
		else if(distance > maxDist)
			distance = maxDist;*/

		cam.orthographicSize = Mathf.Log(distance * zoomScale, logBase);	// Set size of camera
	}

	bool IsVisible () {
		Vector3 pos = cam.WorldToViewportPoint(player1.transform.position);	// If player1 is in/out of view, player2 should be also
		if((pos.x >= 0 && pos.x <= 1) && (pos.y >= 0 && pos.y <= 1))	// Check if player1 is in viewport (x and y between 0 and 1)
			return true;
		return false;
	}

	void DoDamage () {
		float dam = Time.deltaTime * damage;

		// deal dam to players

	}
}
