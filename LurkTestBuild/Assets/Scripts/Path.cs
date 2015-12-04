using UnityEngine;

public class Path : MonoBehaviour {

	public Transform[] positions;

	void OnDrawGizmos() {
		Gizmos.color = new Color(1, 1, .7f);
		for (int p = 1; p < positions.Length; p ++) {
			Transform a = positions[p - 1];
			Transform b = positions[p];
			if (a != null && b != null) {
				Gizmos.DrawLine(a.position, b.position);
			}
		}
	}


}