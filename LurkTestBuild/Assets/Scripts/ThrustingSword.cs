using UnityEngine;
using System;

public class ThrustingSword : Item
{
    protected float cooldown = 3f;
    protected int dam = 5;
    private playerMove player;
    private LineRenderer lr;
    private Color lrStartC = new Color(152, 152, 152, .5f);
    private Color lrEndC = new Color(255, 255, 255, 1f);
    private float lrStartW = 1f;
    private float lrEndW = 1f;
    private float lrFadeTime = .1f;
    public Material lrMat;

    public override float UseItem()
    {
        if (player == null)
            getPlayer();

        RaycastHit2D[] hit;
        hit = Physics2D.RaycastAll(transform.position, transform.right * (player.getDirection() ? 1 : -1), 5f);
        for (int i = 0; i < hit.Length; i++)
        {
            RaycastHit2D hitOjb = hit[i];
            try
            {
                if (hitOjb.transform.tag == "Enemy" || hitOjb.transform.tag == "Boss")
                {
                    hitOjb.transform.gameObject.GetComponent<Damageable>().TakeDamage(dam, transform.right * (player.getDirection() ? 1 : -1));
                    Debug.Log("Delt " + dam + " thrusting sword damage");
                    
                    Vector2 target = hit[i].point;
                    target.y = player.transform.position.y;

                    // Set line renderer points for dash effect
                    setupLR();
                    lr.SetPositions(new Vector3[] { transform.parent.position, target });

                    // Move player to pos
                    transform.parent.position = target;

                    return cooldown;
                }
            }
            catch (Exception e)
            {
                // No enemy to damage
            }
        }
        return base.UseItem();
    }

    private void setupLR()
    {
        //lr = gameObject.AddComponent<LineRenderer>();
        //lr.transform.parent = transform.parent;
        GameObject go = new GameObject("ThrustingSword LR");
        go.transform.parent = player.transform;
        lr = go.AddComponent<LineRenderer>();
        lr.gameObject.AddComponent<FadeTimer>().Setup(lrFadeTime);
        lr.SetColors(lrStartC, lrEndC);
        lr.SetWidth(lrStartW, lrEndW);
        lr.material = lrMat;
    }

    private void getPlayer()
    {
        player = GetComponentInParent<playerMove>();
    }
}
