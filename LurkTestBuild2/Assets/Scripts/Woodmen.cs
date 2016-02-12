using UnityEngine;
using System.Collections;

public class Woodmen : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Attack");
        }
    }
}
