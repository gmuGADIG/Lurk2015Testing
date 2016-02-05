using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ArcherWindow : MonoBehaviour {

	public float range = 10;
	public float fireDelay = 1;
	public float projectileSpeed = 15;
	public LinearProjectile projectilePrefab;
	public DirectionFlags directions;

	IEnumerable<Vector2> lookDirections {
		get {
			if (directions.up)
				yield return Vector2.up;
			if (directions.down)
				yield return Vector2.down;
			if (directions.left)
				yield return Vector2.left;
			if (directions.right)
				yield return Vector2.right;
		}
	}

	void Start() {
		StartCoroutine(FireRoutine());
	}

	IEnumerator FireRoutine() {
		while(true) {
			Vector2 playerDirection = LookForPlayer();
			if (playerDirection == Vector2.zero) {
				yield return null;
			}
			else {
				FireProjectile(playerDirection);
				yield return new WaitForSeconds(fireDelay);
			}
		}
	}

	Vector2 LookForPlayer() {
		foreach (Vector2 direction in lookDirections) {
			RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range);
			if (hit.collider != null) {
				if (hit.collider.tag == "Player") {
					return direction;
				}
			}
		}
		return Vector2.zero;
	}

	void FireProjectile(Vector2 direction) {
		LinearProjectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as LinearProjectile;
		projectile.name = projectilePrefab.name;
		projectile.velocity = direction.normalized * projectileSpeed;
	}

	[Serializable]
	public class DirectionFlags {
		public bool left = true;
		public bool right = true;
		public bool up = true;
		public bool down = true;
	}

}