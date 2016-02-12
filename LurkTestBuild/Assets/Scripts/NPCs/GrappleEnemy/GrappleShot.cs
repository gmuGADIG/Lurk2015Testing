using UnityEngine;
using System.Collections;

public class GrappleShot : MonoBehaviour {

    public GameObject whoShotThis;
    public float projectileSpeed, pullSpeed;
    public LineRenderer rope;

    private bool goBack = false;
    private GameObject player;
    private Transform sightStart, sightEnd;



    void OnEnable() {
        goBack = false;
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        rope.SetPosition(0, this.transform.position);
        rope.SetPosition(1, whoShotThis.transform.position);
        sightEnd = whoShotThis.GetComponent<Grapple>().sightEnd;
        sightStart = whoShotThis.GetComponent<Grapple>().sightStart;
    }

    void Update() {
        if (Mathf.Abs(this.transform.position.x - sightEnd.position.x) < 0.5f) {
            goBack = true;
        }
        if (goBack) {
            GetComponent<Rigidbody2D>().gravityScale = 0.5f;
            ComeBack();
        } else {
            this.transform.position = Vector3.Lerp(this.transform.position, sightEnd.position, projectileSpeed * Time.deltaTime);
        }
        rope.SetPosition(0, this.transform.position);
        rope.SetPosition(1, sightStart.position);
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.gameObject.tag == "Player") {
            player = other.collider.gameObject;
        }
        goBack = true;
    }

    void ComeBack() {
        this.transform.position = Vector3.Lerp(this.transform.position, sightStart.position, Time.deltaTime * pullSpeed); ;
        if(player != null) {
            player.transform.position = this.transform.position;
        }
        if (Mathf.Abs(this.transform.position.x - whoShotThis.GetComponent<Grapple>().sightStart.position.x) < 0.7f) {
            if (player != null) {
                player.GetComponent<playerMove>().enabled = true;
                player = null;
            }
            this.gameObject.SetActive(false);
        }  
    }
}
