using UnityEngine;

public class Path : MonoBehaviour {

	public Transform[] positions;

	public float length {
		get {
			float length = 0;
			for (int p = 1; p < positions.Length; p ++) {
				Transform a = positions[p - 1];
				Transform b = positions[p];
				if (a != null && b != null) {
					length += (a.position - b.position).magnitude;
				}
			}
			return length;
		}
	}

	public Vector3 positionAt(float t) {
		t = Mathf.Clamp01(t);
		float length = this.length;
		float dist = t * length;
		for (int p = 1; p < positions.Length; p++) {
			Transform a = positions[p - 1];
			Transform b = positions[p];
			if (a != null && b != null) {
				float nodeLength = (a.position - b.position).magnitude;
				if (nodeLength > dist) {
					return Vector3.Lerp(a.position, b.position, dist / nodeLength);
				}
				else {
					dist -= nodeLength;
				}
			}
		}
		string error = "Couldn't find point on path for t = " + t;
		Debug.LogError(error, this);
		throw new System.Exception(error);
	}

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