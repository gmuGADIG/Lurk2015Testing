using UnityEngine;
using System;

public class WoodcutterAxe : Weapon {

	public override float UseItem(){
		Debug.Log ("Axe");
		if (player == null)
			getPlayer();

		RaycastHit2D[] hit;
		//Vector3 weird = player.transform.position;
		hit = Physics2D.RaycastAll(player.transform.position, player.transform.right * (player.getDirection() ? 1 : -1), 5f);
		for (int i = 0; i < hit.Length; i++)
		{
			RaycastHit2D hitObj = hit[i];
			try
			{
				if (hitObj.transform.tag == "WoodMan" || hitObj.transform.tag == "RootMan")
				{
					Debug.Log("Chopped");
					hitObj.transform.gameObject.GetComponent<Damageable>().TakeDamage(dam * 2 + player.baseDamage, transform.right * (player.getDirection() ? 1 : -1));
					Debug.Log("Dealt " + dam*2 + player.baseDamage + " Axe damage");

					Vector2 target = hit[i].point;
					target.y = player.transform.position.y;

					return cooldown;
				}

				else if (hitObj.transform.tag == "Enemy" || hitObj.transform.tag == "Boss")
				{
					hitObj.transform.gameObject.GetComponent<Damageable>().TakeDamage(dam, transform.right * (player.getDirection() ? 1 : -1));
					Debug.Log("Dealt " + dam + " Axe damage");

					Vector2 target = hit[i].point;
					target.y = player.transform.position.y;

					return cooldown;
				}
			}
			catch (Exception e)
			{
				// No enemy to damage
			}
		}
		return cooldown;
	}
}
