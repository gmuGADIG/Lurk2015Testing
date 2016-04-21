using UnityEngine;
using System.Collections;

public class ActivateScript : MonoBehaviour {

    public bool startActive;

    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = startActive;
        GetComponent<BoxCollider2D>().enabled = startActive;
    }

    void Activate()
    {
        GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
        GetComponent<BoxCollider2D>().enabled = !GetComponent<BoxCollider2D>().enabled;

    }
}
