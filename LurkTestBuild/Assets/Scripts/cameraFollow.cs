using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cameraFollow : MonoBehaviour {

	Camera cam;							// Main Camera
	Transform player1;					// Player1
	Transform player2;					// Player2
    Transform boss;
    GameObject distanceAlert;           // Alert for when players go off screen

    public GameObject background;
	public float minDist = 5f;			// Minimum distance to zoom
	//public float maxDist = 10f;		// Maximum distance to zoom
	public float zoomScale = 1f;		// Zoom scale (multiplied to distance to get camera size)
	public float logBase = 2;			// Base of the log used for zoom (log base [logBase](distance between players * [zoomscale]) = size of camera
	public float lerpT = 8f;			// Lerp factor for camera movement
	public int damage = 1;				// Amount of damage to be given/sec when off camera
    public float distanceAlertLerp = 8f;// Lerp factor for distance alert

	bool setup = false;					// If setup is complete
    bool bossFocus = false;
	// Use this for initialization
	void Start () {
		FindObjects ();
	}
	
	void Update () {
        setup = FindBoss();
        bossFocus = setup;
        if (!bossFocus)
            setup = FindObjects(); // Check for player updates/initial objects

        if(setup)
        {
            if (player1 && !bossFocus) {
                // At least one player, move/zoom the camera
                Move(player1, player2);
                Zoom(player1, player2);
                RectTransform distanceAlertRect = distanceAlert.GetComponent<RectTransform>();
                if (!IsVisible()) {
                    DoDamage();
                    distanceAlertRect.offsetMax = new Vector2(0, Mathf.Lerp(distanceAlertRect.offsetMax.y, -64, Time.deltaTime* distanceAlertLerp));
                } else {
                    distanceAlertRect.offsetMax = new Vector2(0, Mathf.Lerp(distanceAlertRect.offsetMax.y, 100, Time.deltaTime * distanceAlertLerp));
                }
            }
            else if (player1 && bossFocus)
            {
                Move(boss, null);
                Zoom(boss, null);
                RectTransform distanceAlertRect = distanceAlert.GetComponent<RectTransform>();
                if (!IsVisible())
                {
                    DoDamage();
                    distanceAlertRect.offsetMax = new Vector2(0, Mathf.Lerp(distanceAlertRect.offsetMax.y, -64, Time.deltaTime * distanceAlertLerp));
                }
                else {
                    distanceAlertRect.offsetMax = new Vector2(0, Mathf.Lerp(distanceAlertRect.offsetMax.y, 100, Time.deltaTime * distanceAlertLerp));
                }
            }
            else {
                // Player must have left the game
                setup = false;
            }
        }
        
	}

	bool FindObjects () {
        // Find all objects if not already linked
        cam = Camera.main;

        if (!distanceAlert) {
            distanceAlert = GameObject.FindGameObjectWithTag("DistanceAlert");
        }
        if (!distanceAlert) {
            // Scene missing the distance alert?
            return false;
        }

        // Find players
        if (player1 == null || player2 == null)
        {
            int playerCount = FindPlayers();
            if (playerCount == 0)
            {
                return false;
            }
        }
        return true;
	}

    bool FindBoss()
    {
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");
        if (bosses.Length == 0)
        {
            return false;
        }
        else
        {
            boss = bosses[0].transform;
            return true;
        }
    }

    int FindPlayers() {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 0) {
            // No players yet
        } else if (players.Length == 1) {
            player1 = players[0].transform;
            player2 = null;
        } else if (players.Length == 2) {
            player1 = players[0].transform;
            player2 = players[1].transform;
        } else if (players.Length > 2)
            // Not good, more than 2 players in room
            // THIS SHOULD NEVER HAPPEN - WE NEED TO MAKE SURE OF THIS
            Debug.LogWarning("More than two players tagged.");
        

        return players.Length;
    }


    void Move (Transform target1, Transform target2 = null) {
        Vector3 middle = target1.position;
        if (target2 != null && !bossFocus) {
            middle = (target1.position + target2.position) / 2;	// Middle point between players
        }
        middle.z -= 10;	// Move camera to not clip with scene
		transform.position = Vector3.Lerp(transform.position, middle, Time.deltaTime*lerpT);	// Lerp to location
	}

	void Zoom (Transform target1, Transform target2 = null) {
        if (target2 && !bossFocus) {
            float distance = Vector3.Distance(target1.position, target2.position);  // Distance between players

            // Check camera bounds
            if (distance < minDist)
                distance = minDist;
            /* Not really needed on a log scale. If used, uncomment decleration of maxDist
            else if(distance > maxDist)
                distance = maxDist;*/

            cam.orthographicSize = Mathf.Log(distance * zoomScale, logBase);    // Set size of camera
        } else {
            cam.orthographicSize = Mathf.Log(50 * zoomScale, logBase);    // Set size of camera
        }
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
