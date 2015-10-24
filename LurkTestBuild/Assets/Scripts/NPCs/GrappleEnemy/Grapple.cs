﻿using UnityEngine;
using System.Collections;

public class Grapple : MonoBehaviour {

    public Transform sightStart, sightEnd;
    public GameObject projectile;
    public float coolDown = 1.00f;
    public float xSight, ySight, reach;
    public bool flips;

    //private Vector2 Lerping;
    private float timeLeft = 0f;
    private GameObject player = null;

    void Start() {
        player = GetComponent<Enemy>().aggro;
    }

	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        
        if (player.transform.position.x < this.transform.position.x + xSight &&
            player.transform.position.x > this.transform.position.x - xSight &&
            player.transform.position.y < this.transform.position.y + ySight / 2 &&
            player.transform.position.y > this.transform.position.y - ySight / 2) {

            if (!projectile.activeSelf) {

                //trig stuff, aim past the player
                float x = this.transform.position.x - player.transform.position.x;
                float y = this.transform.position.y - player.transform.position.y;
                float angleModifier = Mathf.Tan(y / x);
                float yPos = this.transform.position.y + transform.localScale.x * (Mathf.Sin(angleModifier) * reach);
                float xPos = this.transform.position.x + transform.localScale.x * (Mathf.Cos(angleModifier) * reach);
                sightEnd.position = new Vector2(xPos, yPos);

                if (timeLeft <= 0f) {
                    projectile.transform.position = sightStart.position;
                    projectile.SetActive(true);
                    timeLeft = coolDown;
                }
            }
        }
        if (flips && !projectile.activeSelf) {
            if (player.transform.position.x < this.transform.position.x && transform.localScale.x > 0) {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            } else if (player.transform.position.x > this.transform.position.x && transform.localScale.x < 0) {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.yellow);
    }
}
