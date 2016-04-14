using UnityEngine;
using System;

public class Weapon : Item
{
    public float cooldown = 3f;
    public int dam = 5;
    public float dist = 1f; // Length of the weapon
    protected playerMove player;

    public override float UseItem()
    {
		Debug.Log ("Weapon");
        if (player == null)
            getPlayer();

        RaycastHit2D[] hit;
        hit = Physics2D.RaycastAll(player.transform.position, player.transform.right * (player.getDirection() ? 1 : -1), dist);
        for (int i = 0; i < hit.Length; i++)
        {
            RaycastHit2D hitOjb = hit[i];
            try
            {
                if (hitOjb.transform.tag == "Enemy" || hitOjb.transform.tag == "Boss")
                {
                    hitOjb.transform.gameObject.GetComponent<Damageable>().TakeDamage(dam, transform.right * (player.getDirection() ? 1 : -1));
                    Debug.Log("Delt " + dam + " weapon damage");
                }
            }
            catch (Exception e)
            {
                // No enemy to damage
            }
        }

        return cooldown;
    }

    protected void getPlayer()
    {
        player = GetComponentInParent<playerMove>();
    }
}

