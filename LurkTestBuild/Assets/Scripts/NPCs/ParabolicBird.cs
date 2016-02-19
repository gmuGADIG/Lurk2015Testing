using UnityEngine;
using System;
using System.Collections.Generic;

class ParabolicBird : MonoBehaviour {


	public Transform pathTransform = null;
	public float start = 0;
	public float speed = 5;
	public bool activateOnAwake = false;

	private float x; //current 'x' position (local to parabola's space)
	private bool activated = false;

	void Awake() {
		x = start;
		if (activateOnAwake) {
			Activate();
		}
	}

	//call this to activate the bird
	//(FOR USE WITH ACTIVATION TRIGGER COMPONENT)
	public void Activate() {
		activated = true;
	}

	void Update() {
		if (activated) {
			x += speed * Time.deltaTime / pathTransform.localScale.x;
			float y = x * x;
			Vector3 pos = pathTransform.TransformPoint(x, y, 0);
			transform.position = pos;
		}
	}

	//draw the curve so you know where its going
	void OnDrawGizmos() {
		if (pathTransform) {
			int samples = 200;	//samples to draw curve with
			float range = 50;	//distance over local x to draw curve
			Matrix4x4 gizmos_mat = Gizmos.matrix;
			Color gizmos_color = Gizmos.color;
			Gizmos.matrix = pathTransform.localToWorldMatrix;
			float x = -range / 2;
			float y = x * x;
			Vector3 last = new Vector3(x, y, 0);
			Gizmos.color = new Color(1, 1, .7f);
			for (int i = - samples / 2; i < samples / 2; i ++) {
				x = x + (range / samples);
				y = x * x;
				Vector3 point = new Vector3(x, y, 0);
				Gizmos.DrawLine(last, point);
				last = point;
			}
			x = start;
			y = x * x;
			Vector3 startPoint = new Vector3(x, y, 0);
			Gizmos.matrix = gizmos_mat;
			Gizmos.color = new Color(.7f, .7f, 1);
			Gizmos.DrawWireCube(pathTransform.TransformPoint(startPoint), Vector3.one);
			Gizmos.color = gizmos_color;
		}
	}

}
