using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroMenu : MonoBehaviour {

	public Button					newGameButton;
	public Button					loadButton;
	public Button					optionsButton;
	public Button					quitButton;

	public Canvas					loadGameMenuCanvas;
	public Canvas					optionsMenuCanvas;
	public Canvas					gameSettingsMenuCanvas;
	public Canvas					videoSettingsMenuCanvas;
	public Canvas					audioSettingsMenuCanvas;

	public Canvas					quitMenuCanvas;
	public bool						quitMenuBool = false;

	void Start () {

		// Initialize all of the menus and turn them off
		newGameButton = newGameButton.GetComponent<Button>();
		loadButton = loadButton.GetComponent<Button>();
		optionsButton = optionsButton.GetComponent<Button>();
		quitButton = quitButton.GetComponent<Button>();

		loadGameMenuCanvas = loadGameMenuCanvas.GetComponent<Canvas>();
		loadGameMenuCanvas.enabled = false;

		optionsMenuCanvas = optionsMenuCanvas.GetComponent<Canvas>();
		optionsMenuCanvas.enabled = false;

		gameSettingsMenuCanvas = gameSettingsMenuCanvas.GetComponent<Canvas>();
		gameSettingsMenuCanvas.enabled = false;

		videoSettingsMenuCanvas = videoSettingsMenuCanvas.GetComponent<Canvas>();
		videoSettingsMenuCanvas.enabled = false;

		audioSettingsMenuCanvas = audioSettingsMenuCanvas.GetComponent<Canvas>();
		audioSettingsMenuCanvas.enabled = false;

		quitMenuCanvas = quitMenuCanvas.GetComponent<Canvas>();
		quitMenuCanvas.enabled = false;
	}

	void Update () {

		// If the player presses the "Escape" key, exit the current menu and go back to the previous menu.
		// If there are no menus open, open the Quit Menu
		if(Input.GetButtonDown("Cancel") && gameSettingsMenuCanvas.enabled == true) {
			CloseGameSettingsMenu();
		} else if(Input.GetButtonDown("Cancel") && videoSettingsMenuCanvas.enabled == true) {
			CloseVideoSettingsMenu();
		} else if(Input.GetButtonDown("Cancel") && audioSettingsMenuCanvas.enabled == true) {
			CloseAudioSettingsMenu();
		} else if(Input.GetButtonDown("Cancel") && optionsMenuCanvas.enabled == true) {
			CloseOptionsMenu();
		} else if (Input.GetButtonDown("Cancel")) {
			quitMenuBool = !quitMenuBool;
		}

		// Switch between opening and closing the Quit Menu based on the quitMenuBool
		if(quitMenuBool == false) {
			CloseQuitMenu();
		} else {
			OpenQuitMenu();
		}
	}


	// ===== START MENU =====

	// Start the game
	public void StartLevel() {
		SceneManager.LoadScene(1);

	}



	// ===== LOAD GAME MENU =====

	// Open the Load Game Menu
	public void OpenLoadGameMenu() {
		loadGameMenuCanvas.enabled = true;
		newGameButton.enabled = false;
		optionsButton.enabled = false;
		quitButton.enabled = false;
	}

	// Close the Options Menu
	public void CloseLoadGameMenu() {
		loadGameMenuCanvas.enabled = false;
		newGameButton.enabled = true;
		optionsButton.enabled = true;
		quitButton.enabled = true;
	}



	// ===== OPTIONS MENU =====

	// Open the Options Menu
	public void OpenOptionsMenu() {
		optionsMenuCanvas.enabled = true;
		newGameButton.enabled = false;
		optionsButton.enabled = false;
		quitButton.enabled = false;
	}

	// Close the Options Menu
	public void CloseOptionsMenu() {
		optionsMenuCanvas.enabled = false;
		newGameButton.enabled = true;
		optionsButton.enabled = true;
		quitButton.enabled = true;
	}



	// ===== GAME SETTINGS MENU =====

	// Open the Game Settings Menu
	public void OpenGameSettingsMenu() {
		gameSettingsMenuCanvas.enabled = true;
	}
		
	// Close the Game Settings Menu
	public void CloseGameSettingsMenu() {
		gameSettingsMenuCanvas.enabled = false;
	}



	// ===== VIDEO SETTINGS MENU =====


	// Open the Video Settings Menu
	public void OpenVideoSettingsMenu() {
		videoSettingsMenuCanvas.enabled = true;
	}

	// Close the Video Settings Menu
	public void CloseVideoSettingsMenu() {
		videoSettingsMenuCanvas.enabled = false;
	}



	// ===== AUDIO SETTINGS MENU =====

	// Open the Audio Settings Menu
	public void OpenAudioSettingsMenu() {
		audioSettingsMenuCanvas.enabled = true;
	}

	// Close the Audio Settings Menu
	public void CloseAudioSettingsMenu() {
		audioSettingsMenuCanvas.enabled = false;
	}



	// ===== QUIT MENU =====

	// Set the quitMenuBool true or false
	public void setQuitMenuBool() {
		quitMenuBool = !quitMenuBool;
	}

	// Open the Quit Menu
	public void OpenQuitMenu() {
		quitMenuCanvas.enabled = true;
	}

	// Close the Quit Menu
	public void CloseQuitMenu() {
		quitMenuCanvas.enabled = false;
	}

	// Exit the game
	public void ExitGame() {
		Application.Quit();
	}
}
