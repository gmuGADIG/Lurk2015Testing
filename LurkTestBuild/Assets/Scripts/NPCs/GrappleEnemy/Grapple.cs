using UnityEngine;
using System.Collections;

public class Grapple : MonoBehaviour {

    public Transform sightStart, sightEnd;
    public GameObject projectile;
    public float pullSpeed;

    private GameObject player = null;

	// Update is called once per frame
	void Update () {
        if (player == null) {
            Debug.DrawLine(sightStart.position, sightEnd.position, Color.red);
            LayerMask playerLayer = (1 << 0); 
            if (Physics2D.Linecast(sightStart.position, sightEnd.position, playerLayer) &&
                Physics2D.Linecast(sightStart.position, sightEnd.position, playerLayer).collider.gameObject.tag == "Player") {
                if (!projectile.activeSelf) {
                    projectile.transform.position = sightStart.position;
                    projectile.SetActive(true);
                }
            }
        } else {
            OnPlayerHit(player);
        }
	}

    public void OnPlayerHit(GameObject player) {
        this.player = player;
        player.GetComponent<playerMove>().enabled = false;
        player.transform.position = Vector2.Lerp(player.transform.position, sightStart.position, Time.deltaTime*pullSpeed);
        if(Mathf.Abs(player.transform.position.x - sightStart.position.x) < 1f) {
            player.GetComponent<playerMove>().enabled = true;
            this.player = null;
        }
    }
}
