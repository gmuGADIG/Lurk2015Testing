using UnityEngine;

public class LinearProjectile : MonoBehaviour {

	public Vector2 velocity;
	public float despawnTimer = 5;

	void Start() {
		Destroy(gameObject, despawnTimer);
	}

	void Update() {
		transform.position += (Vector3) velocity * Time.deltaTime;
		transform.right = velocity.normalized;
	}

}