using UnityEngine;
using System.Collections;

public class PlatformJumping : MonoBehaviour {

    public GameObject jumpDownLeft;
    public GameObject jumpDownRight;
    public GameObject jumpUpLeft;
    public GameObject jumpUpRight;
    public float bottomOfObject;
    public float xCollideDist;

    void Start()
    {
        bottomOfObject = transform.position.y - GetComponent<Collider2D>().bounds.extents.y - .1f;
        xCollideDist = GetComponent<Collider2D>().bounds.extents.x;
        Vector2 rayLeft = new Vector2(transform.position.x - xCollideDist, bottomOfObject);
        Vector2 rayRight = new Vector2(transform.position.x + xCollideDist, bottomOfObject);
        RaycastHit2D hitLeft = Physics2D.Raycast(rayLeft, new Vector2(-1,-1), 7.0f, 1 << 8);
        RaycastHit2D hitRight = Physics2D.Raycast(rayRight, new Vector2(1,-1), 7.0f, 1 << 8);
        if (hitLeft.collider != null)
        {
            jumpDownLeft = hitLeft.collider.gameObject;
            hitLeft.collider.gameObject.GetComponent<PlatformJumping>().jumpUpRight = this.gameObject;
        }
        if (hitRight.collider != null)
        {
            jumpDownRight = hitRight.collider.gameObject;
            hitRight.collider.gameObject.GetComponent<PlatformJumping>().jumpUpLeft = this.gameObject;
        }
    }

}
