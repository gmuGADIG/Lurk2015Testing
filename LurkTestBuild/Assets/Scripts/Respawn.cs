using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour
{
	// Store Checkpoint
	public GameObject cp;

	public void setCheckpoint(GameObject go)
	{
		cp = go;
	}

	private void OnDeath()
	{
		Vector3 pos = cp.transform.position;
		pos.y += GetComponent<BoxCollider2D> ().size.y;
		transform.position = pos;
	}
}
