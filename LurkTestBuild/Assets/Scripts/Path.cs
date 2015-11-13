using UnityEngine;

public class Path : MonoBehaviour {

	public Transform[] positions;

	void OnValidate() {
		for (int p = 0; p < positions.Length; p ++) {
			if (positions[p] == null) {
				positions[p] = new GameObject("position " + p).transform;
				positions[p].parent = transform;
				positions[p].position = transform.position;
			}
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = new Color(1, 1, .7f);
		for (int p = 1; p < positions.Length; p ++) {
			Gizmos.DrawLine(positions[p - 1].position, positions[p].position);
		}
	}


}