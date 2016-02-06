using UnityEngine;
using System.Collections;

public class RootController : MonoBehaviour {

	//time rising
	public float riseTime = .25f;
	//time descending
	public float descendTime = .25f;
	//time waiting at peak
	public float timeAtPeak = .25f;

	//time when this obect is instantiated
	private float startTime;
	//distance the root moves per fixed update
	private float moveDistance;

	void Start () {

		startTime = Time.time;
		//d = v * t so v = d / t therefor 
		//mD = v / (1 second / time between fixed upates (gives number of fixed updates per second)) 
		//NOTE: assumes riseTime == descendTime
		moveDistance = transform.lossyScale.y / riseTime / (1 / Time.fixedDeltaTime);
	}
	
	void FixedUpdate () {

		//if it is rising
		if (Time.time - startTime < riseTime) {

			//rise my minion
			float y = transform.position.y + moveDistance;
			transform.position = new Vector3(transform.position.x, y);
		
		//if it is at the peak
		} else if (Time.time - startTime < riseTime + timeAtPeak) {
			//do nothing
		
		//if it is descend
		} else if (Time.time - startTime < riseTime + timeAtPeak + descendTime) {

			//descend into the depths
			float y = transform.position.y - moveDistance;
			transform.position = new Vector3(transform.position.x, y);

		//after it is done
		} else {
			//bye bye
			Destroy(gameObject);
		}
	}
}