using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// http://forum.unity3d.com/threads/projectile-prediction-line.143636/
public class Throw : MonoBehaviour
{
	#region Parameters
	public Vector2 standardForce;			// Force to be added to obj after rotated by angle
	public float forceMult = 10;
	public float timeSeg = .05f;			// time per segment (in seconds)
	public int maxVerts = 51;				// Maximum number of vertices to use for line
	public float rotSpeed = 20f;            // Degrees to rotate/sec
    float angle = 0f;                       // Direction of throw

    #region LineProperties
    Color startColor = Color.red;
	Color endColor = Color.magenta;
	float startAlpha = .5f;
	float endAlpha = .5f;
	float startWidth = .1f;
	float endWidth = .1f;
	public Material mat;
	float z = -1f;					// Z height to draw line
	#endregion

	LineRenderer line;
	Vector2 force;
	bool pressed = false;
	GameObject thrown;
	Inventory inv;
    playerMove player;
	#endregion

	#region Awake
	void Awake()
	{
		standardForce = transform.right * forceMult;
		force = transform.right * forceMult;
        player = GetComponent<playerMove>();

		// Setup line
		line = gameObject.AddComponent<LineRenderer>();
		line.transform.parent = transform.parent;
		startColor.a = startAlpha;
		endColor.a = endAlpha;
		line.SetColors(startColor, endColor);
		line.SetWidth(startWidth, endWidth);
		line.material = mat;
		inv = GetComponent<Inventory> ();
	}
	#endregion

	#region Update
	void Update()
	{
		if(Input.GetAxisRaw ("C") == 1 && inv.GetItem() != null)
		{
			UpdateTrajectory();
			UpdateRotation();
			MakeLineVisable();
			pressed = true;
		}
        if (pressed == true)
		{
            if (Input.GetAxisRaw("V") == 1)
            {
                MakeLineInvisable();
                // Throw instance of object
                GameObject obj = inv.RemoveItem();  // Object to be thrown (requires rigidBody)

                if (obj != null)
                {
                    obj.transform.position = transform.position;
                    obj.SendMessage("setItemState", true);
                    if (player.getDirection())
                        obj.GetComponent<Rigidbody2D>().velocity = force;
                    else
                    {
                        force.x = -force.x;
                        obj.GetComponent<Rigidbody2D>().velocity = force;
                    }
                }
            }
            else if (Input.GetAxisRaw("C") <= 0)
            {
                MakeLineInvisable();
                angle = 0;
                force = transform.right * forceMult;
                pressed = false;
            }
        }
        else {}
	}
	#endregion

	#region UpdateTrajectory
	void UpdateTrajectory()
	{
		if(line == null)
			line = gameObject.AddComponent<LineRenderer>();
		line.SetVertexCount(maxVerts);
		
		Vector2 position = transform.position;
		Vector2 velocity = force;
        if(player.getDirection())
        {
            velocity.x = Mathf.Abs(velocity.x);
        }else
            velocity.x = -Mathf.Abs(velocity.x);
        for (int i = 0; i < maxVerts; ++i)
		{
			line.SetPosition(i, new Vector3(position.x, position.y, z));
			
			position += velocity * timeSeg + 0.5f * Physics2D.gravity * timeSeg * timeSeg;
			velocity += Physics2D.gravity * timeSeg;
		}
	}
	#endregion

	#region UpdateRotation
	void UpdateRotation()
	{
		angle += rotSpeed * Time.deltaTime; // Change direction by rotSpeed * time since last update

        if (angle >= 90)
            rotSpeed *= -1;
        else if (angle <= 0)
            rotSpeed *= -1;


        force = Quaternion.Euler (0, 0, angle) * standardForce;
	}
	#endregion

	#region MakeLine(In)Visable
	void MakeLineVisable()
	{
		line.SetColors(startColor, endColor);
	}

	void MakeLineInvisable()
	{
		line.SetColors(Color.clear, Color.clear);
	}
	#endregion
}
