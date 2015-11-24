using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	public Item[] inv = new Item[2];
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
		Item temp = inv [0];
		inv [0] = inv [1];
		inv [1] = temp;
		UpdateSprites ();
	}

	// Returns false if inventory slot 0 is full, true otherwise
	public bool Pickup(Item item)
	{
		if (inv [0] == null)
		{
			inv [0] = item;
			UpdateSprites();
		}
		else
			return false;

		return true;
	}

	// Should be called every time an item is changed, so it does not need to be called each frame
	public void UpdateSprites()
	{
		// Get object sprites from inv and set GUI images to them
		if(inv[0] != null)
			panels [0].GetComponent<Image> ().sprite = inv[0].sprite;
		else
			panels[0].GetComponent<Image>().sprite = defaultImage;

		if(inv[1] != null)
			panels [1].GetComponent<Image> ().sprite = inv[1].sprite;
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
