using UnityEngine;

//base enemy script: all enemy scripts should extend this script
public class Enemy : MonoBehaviour {
	
	//the current aggro
	public GameObject aggro;
    public Vector3 hitbox;
    public float range;
    void Update()
    {
       RaycastHit2D line = Physics2D.Linecast(this.transform.position + hitbox, aggro.transform.position);
       Debug.Log("I see: " + line.collider.name);
       float adjRange = range;
       while (line.collider.name.Equals(aggro.name) == false && line.distance <= adjRange)
       {
           adjRange -= line.distance;
           Debug.Log("Range: " + adjRange);
           line = Physics2D.Linecast(line.transform.position + hitbox, aggro.transform.position);
           Debug.Log("I see: " + line.collider.name);
           
       }
       if (line.collider.name.Equals(aggro.name) && line.distance <= range)
       {
            //Debug.Log("found at: " + line.transform.position);

       }
        //else
            //Debug.Log("nada");
    }
	
}