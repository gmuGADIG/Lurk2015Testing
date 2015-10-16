using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {
	public Camera cam;					// Camera
	public Transform player1;			// Player1
	public Transform player2;			// Player2

	public float minDist = 5f;			// Minimum distance to zoom
	public float maxDist = 10f;			// Maximum distance to zoom
	public float zoomScale = 1f;		// Zoom scale (multiplied to distance to get camera size)
	public float lerpT = .5f;			// Lerp interpolant [0-1]

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
		else if(distance > maxDist)
			distance = maxDist;

		cam.orthographicSize = distance * zoomScale;	// Set size of camera
	}
}
