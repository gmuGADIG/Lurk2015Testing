using UnityEngine;
using System.Collections;

public class Grapple : MonoBehaviour {

    public Transform sightStart, sightEnd;
    public GameObject projectile;
    public float coolDown = 1.00f;

    //private Vector2 Lerping;
    private float timeLeft = 0f;
    private GameObject player = null;

	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        if (player == null) {
            Debug.DrawLine(sightStart.position, sightEnd.position, Color.red);
            LayerMask playerLayer = (1 << 0); 
            if (Physics2D.Linecast(sightStart.position, sightEnd.position, playerLayer) &&
                Physics2D.Linecast(sightStart.position, sightEnd.position, playerLayer).collider.gameObject.tag == "Player") {
                if (!projectile.activeSelf && timeLeft<= 0f) {
                    projectile.transform.position = sightStart.position;
                    projectile.SetActive(true);
                    timeLeft = coolDown;
                }
            }
        }
        //Lerping = Vector2.Lerp(projectile.transform.position, sightStart.position, Time.deltaTime * pullSpeed);
    }

    

   /* public Vector2 GetLerping() {
        return Lerping;
    }*/
}
