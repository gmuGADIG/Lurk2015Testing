using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Requires Item.cs, Throw.cs, and playerMove.cs
/// Frequently referenced by playerMove.cs and Throw.cs
/// </summary>

public class Inventory : MonoBehaviour
{
	public GameObject[] inv = new GameObject[2];
	public GameObject[] panels = new GameObject[2];
	public Sprite defaultImage;

	public void Start()
	{
		if (!FindPanels ())
			Debug.LogError ("Item Panels Not Found");
		else
			UpdateSprites();
	}

	// Switches slot 0 and 1
	public void SwitchSlots()
	{
		GameObject temp = inv [0];
		inv [0] = inv [1];
		inv [1] = temp;
		UpdateSprites ();
	}
	
	// Returns false if inventory slot 0 is full, true otherwise
	public bool Pickup(GameObject item)
	{
		if (inv [0] == null)
		{
			inv [0] = item;
			UpdateSprites();
			return true;
		}
		
		return false;
	}

	// Returns false if inventory slot 0 is empty, true otherwise
	public bool Drop(GameObject item)
	{
		if (inv [0] != null)
		{
			inv [0].transform.position = transform.position;
			inv [0].SendMessage("setItemState", true);
			inv [0] = null;
			UpdateSprites();
			return true;
		}
		
		return false;
	}

	// Removes item0 and returns it
	public GameObject RemoveItem()
	{
		GameObject temp = inv [0];
		inv [0] = null;
		UpdateSprites ();
		return temp;
	}

	public GameObject GetItem(int i = 0)
	{
		return inv [i];
	}

	// Should be called every time an item is changed, so it does not need to be called each frame
	public void UpdateSprites()
	{
		// Get object sprites from inv and set GUI images to them
		if (inv [0] != null)
			panels [0].GetComponent<Image>().sprite = inv[0].GetComponent<SpriteRenderer> ().sprite;
		else
			panels[0].GetComponent<Image>().sprite = defaultImage;

		if(inv[1] != null)
			panels [1].GetComponent<Image>().sprite = inv[1].GetComponent<SpriteRenderer> ().sprite;
		else
			panels[1].GetComponent<Image>().sprite = defaultImage;
	}

	public bool FindPanels()
	{
		panels[0] = GameObject.Find("Item1");
		panels[1] = GameObject.Find("Item2");

		if (panels [0] == null || panels [1] == null)
			return false;

		return true;
	}
}
