using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RayCastoAccross : MonoBehaviour {
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float sightDistance = 10f;
    [SerializeField] private float meleeDistance = 1f;
    [SerializeField] private float pushForce;

    public float damage = 2f;
    public float timeToDeletion = 10f;
    private float timer = 0f;
    private bool going = false;
    private Animator anim;
    private HashSet<GameObject> alreadyHit;
	// Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        alreadyHit = new HashSet<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
        Debug.DrawLine(transform.position, transform.position + transform.right * sightDistance);
	    if(Physics2D.Linecast(transform.position, transform.position+transform.right*sightDistance, playerLayer)) {
            anim.SetBool("Going", true);
            going = true;
        }
        if (going) {
            Run();
            DamagePlayers();
        }
	}

    void DamagePlayers() {
        RaycastHit2D[] players = Physics2D.LinecastAll(transform.position, transform.position + transform.right * meleeDistance, playerLayer);
        foreach(RaycastHit2D player in players) {
            if (!alreadyHit.Contains(player.collider.gameObject))
            {
                player.collider.gameObject.SendMessage("TakeDamage", damage);
                player.collider.gameObject.GetComponent<Rigidbody2D>().AddForce((player.collider.gameObject.transform.position - this.transform.position).normalized * pushForce);
                alreadyHit.Add(player.collider.gameObject);
            }
        }
    }

    void Run() {
        transform.position += transform.right * speed * Time.deltaTime;
        timer += Time.deltaTime;
        if(timer >= timeToDeletion) {
            Destroy(this.gameObject);   
        }
    }
}
