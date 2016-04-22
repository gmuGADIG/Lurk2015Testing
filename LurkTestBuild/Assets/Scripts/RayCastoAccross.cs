using UnityEngine;
using System.Collections;

public class RayCastoAccross : MonoBehaviour {

    [SerializeField]
    private float speed = 10f;
    [SerializeField] private float sightDistance = 10f;
    public LayerMask playerLayer;
    [SerializeField] private float pushForce;

    public float timeToDeletion = 10f;
    private float timer = 0f;
    private bool going = false;
    private Animator anim;
	// Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
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
        }
	}

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player") {
            other.gameObject.SendMessage("TakeDamage", 20);
            other.gameObject.GetComponent<Rigidbody2D>().AddForce((other.gameObject.transform.position - this.transform.position).normalized * pushForce);
            this.gameObject.layer = LayerMask.NameToLayer("no player collision");
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
