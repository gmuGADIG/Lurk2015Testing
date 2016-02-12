using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// http://forum.unity3d.com/threads/projectile-prediction-line.143636/
public class Throw : MonoBehaviour
{
    #region Parameters
    public LayerMask mask;                  // Layer mask for throw prediction - (DO NOT CHANGE IF YOU DO NOT KNOW WHAT IT DOES)
	public Vector2 standardForce;			// Force to be added to obj after rotated by angle
	public float forceMult = 10;
	public float timeSeg = .05f;			// time per segment (in seconds) (resolution)
	public int maxVerts = 51;				// Maximum number of vertices to use for line (length)
	public float rotSpeed = 20f;            // Degrees to rotate/sec
    float angle = 0f;                       // Direction of throw

    #region LineProperties
    public Color startColor = Color.yellow;
	public Color endColor = Color.yellow;
	float startAlpha = .75f;
	float endAlpha = .75f;
	float startWidth = .1f;
	float endWidth = .1f;
	public Material mat;
	float z = -1f;					        // Z height to draw line
    List<Vector3> points = new List<Vector3>();
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
        inv = GetComponent<Inventory>();

	}

    private void setupLine()
    {
        line = gameObject.AddComponent<LineRenderer>();
        line.transform.parent = transform.parent;
        startColor.a = startAlpha;
        endColor.a = endAlpha;
        line.SetColors(startColor, endColor);
        line.SetWidth(startWidth, endWidth);
        line.material = mat;
    }
	#endregion

	#region Update
	void FixedUpdate()
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
            if (Input.GetAxisRaw("X") == 1)
            {
                MakeLineInvisable();
                // Throw instance of object
                GameObject obj = inv.RemoveItem();  // Object to be thrown (requires rigidBody)

                if (obj != null)
                {
                    obj.transform.position = transform.position;
                    obj.SendMessage("SetItemState", true);
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
        Vector2 position = transform.position;
        Vector2 nextPosition = position;

        Vector2 velocity = force;
        if(player.getDirection())
        {
            velocity.x = Mathf.Abs(velocity.x);
        }else
            velocity.x = -Mathf.Abs(velocity.x);

        int i = 0;
        do
        {
            //Debug.Log(i + " " + velocity);
            points.Add(new Vector3(position.x, position.y, z));
            nextPosition += velocity * timeSeg + 0.5f * Physics2D.gravity * timeSeg * timeSeg;
            // Raycast for collision
            RaycastHit2D hit = Physics2D.Linecast(position, nextPosition, mask);
            if (hit)
            {
                Vector2 point = hit.point;
                points.Add(new Vector3(point.x, point.y, z));
                drawLine();
                return;
            }
            position = nextPosition;

            velocity += Physics2D.gravity * timeSeg;
        } while (++i < maxVerts/* && !collision*/);

        drawLine();
    }

    private void drawLine()
    {

        if (line == null)
            setupLine();

        line.SetVertexCount(points.Count);
        line.SetPositions(points.ToArray());
        points.Clear();
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
