using UnityEngine;
using System.Collections;
using PlayerPrefs;
//This script as it stands is meant to be a header file and used by any other script.
//Booleans can bite my shiny metal ass. 1 & 0 will be fine, comrade.
//This script should work for the project, and is quite malleable.
public class Save_Header_File : MonoBehaviour
{
	//void Start(){}//Skeleton if required.

	void SaveGame(string savepoint, int HasLantern)	//If needbe, we can use a struct. (ArrayList?)
	{												//^That is if we have a lot of import statements.
		if (savepoint == "Forest")					//We can save anything we want to PlayerPrefs.
			PlayerPrefs.SetString ("Save Point","Forest");
			//PlayerPrefs.SetInt("Save_Forest", 1);//This style, if we wish to teleport between points.
		if (savepoint == "Castle")
			PlayerPrefs.SetString ("Save Point","Castle");
		if (HasLantern == 1)
			PlayerPrefs.SetInt("HasLantern",1);
		PlayerPrefs.Save();
/*
		switch (savepoint)//We will likely use switch for sheer amount of save points.
		{
		case "Forest":
			PlayerPrefs.SetString("Save Point", "Forest");
			break;
		case "Castle":
			PlayerPrefs.SetString("Save Point", "Castle");
			break;
		}
		PlayerPrefs.Save();
*/
	}
	void DeleteSave()
	{										//Need a line for each saved "thing"
											//
		PlayerPrefs.DeleteKey("Save Point");//If we have too many, a struct w/ iterator? [Teleport Style]
		PlayerPrefs.DeleteKey("HasLantern");
	}
	void DeleteAll()//DANGER WILL ROBINSON. DANGER. NO, WILL ROBINSON.
	{//This will completely erase the preferences file. True Reset.
		PlayerPrefs.DeleteAll();
	}
	void LoadGame(out string savepoint, out int HasLantern)
	{
		savepoint = PlayerPrefs.GetString("Save Point");
		HasLantern = PlayerPrefs.GetInt ("HasLantern");
		//Using the alternate style, we might need a struct for all of the save points to set to 0.
	}
	void SaveGraphics(/*PARAMETERS GO HERE.*/)	//Probably should use some kind of struct.
	{											//
		//Save all those parameters.
		//???
		//Profit.
		PlayerPrefs.Save ();
	}
	void LoadGraphics(/*out String Option*/)
	{
		//Option = PlayerPrefs.GetString("<OPTION NAME>");
		//Load any gfx options and return them. If we're using some kind of arraylist, return that.
		//I would recommend struct since a standard graphics menu can have a lot.
	}
	void DefaultGraphics()//Delete the gfx preferences.
	{
		//Deletekeys or loop or iterate through struct and delete.
		//If anyone knows how to not have our preferences only be integers in PlayerPrefs.....
		//Yeah....., that'd be great.

		//If the gfx script has a default function, we can call it. Ideally, that script would be called
		//when installing/playing for the first time. We can also do it here if needed.
		//I believe Unity might have it's own options for saving graphics.
	}
}