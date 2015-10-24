using UnityEngine;
using System.Collections;

public class GrappleShot : MonoBehaviour {

    public GameObject whoShotThis;
    public float projectileSpeed;

    void OnEnable() {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(whoShotThis.transform.localScale.x * projectileSpeed, 0);
    }

    void Update() {
        if (Mathf.Abs(this.transform.position.x - whoShotThis.GetComponent<Grapple>().sightEnd.position.x) < 0.5f) {
            this.gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.gameObject.tag == "Player") {
            whoShotThis.GetComponent<Grapple>().OnPlayerHit(other.collider.gameObject);
        }
        this.gameObject.SetActive(false);
    }

    
}
