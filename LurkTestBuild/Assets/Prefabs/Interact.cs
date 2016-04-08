using UnityEngine;
using System.Collections;

public class Interact : MonoBehaviour {

    public GameObject buttonPrompt;
    public GameObject Conversation;
    public bool repeatable;

    private GameObject[] textObjects;
    private playerMove player;
    private float lastVerticalInput;
    private int current = 0;

    void Start() {
        int children = Conversation.transform.childCount;
        textObjects = new GameObject[children];
        for(int i = 0; i < children; i++) {
            textObjects[i] = Conversation.transform.GetChild(i).gameObject;
        }
    }

    void Update() {
        if (buttonPrompt.activeSelf) {
            if (player.verticalInput > 0 && lastVerticalInput <=0) {
                if (current > 0)
                    textObjects[current - 1].SetActive(false);
                if (current < textObjects.Length)
                    textObjects[current++].SetActive(true);
                else if(repeatable)
                    current = 0;
            }
        }
        if (player)
            lastVerticalInput = player.verticalInput;

    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            buttonPrompt.SetActive(true);
            player = other.GetComponent<playerMove>();
            if (repeatable)
                current = 0;
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            buttonPrompt.SetActive(false);
        }
    }
}
