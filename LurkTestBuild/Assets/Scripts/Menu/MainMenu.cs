using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public Canvas					mainMenuCanvas;
	public Button					mainMenuResumeButton;
	public Button					mainMenuQuitButton;
	public bool						mainMenuBool = false;

	public Canvas					optionsMenuCanvas;
	public Canvas					gameSettingsMenuCanvas;
	public Canvas					videoSettingsMenuCanvas;
	public Canvas					audioSettingsMenuCanvas;

	public Canvas					quitMenuCanvas;

	void Start () {
		mainMenuCanvas = mainMenuCanvas.GetComponent<Canvas>();
		mainMenuCanvas.enabled = false;

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
		if(Input.GetButtonDown("Escape") && gameSettingsMenuCanvas.enabled == true) {
			gameSettingsMenuCanvas.enabled = false;
		} else if(Input.GetButtonDown("Escape") && videoSettingsMenuCanvas.enabled == true) {
			videoSettingsMenuCanvas.enabled = false;
		} else if(Input.GetButtonDown("Escape") && audioSettingsMenuCanvas.enabled == true) {
			audioSettingsMenuCanvas.enabled = false;
		} else if(Input.GetButtonDown("Escape") && optionsMenuCanvas.enabled == true) {
			optionsMenuCanvas.enabled = false;
		} else if(Input.GetButtonDown("Escape") && quitMenuCanvas.enabled == true) {
			quitMenuCanvas.enabled = false;
		} else if (Input.GetButtonDown("Escape")) {
			mainMenuBool = !mainMenuBool;
		}

		if(mainMenuBool == false) {
			CloseMainMenu();
			Time.timeScale = 1;
		} else {
			OpenMainMenu();
			Time.timeScale = 0;
		}
	}


	// ===== MAIN MENU =====

	public void OpenMainMenu() {
		mainMenuCanvas.enabled = true;
	}

	public void CloseMainMenu() {
		mainMenuCanvas.enabled = false;
	}

	public void setMainMenuBool() {
		mainMenuBool = !mainMenuBool;
	}



	// ===== OPTIONS MENU =====

	public void OpenOptionsMenu() {
		optionsMenuCanvas.enabled = true;
	}

	public void CloseOptionsMenu() {
		optionsMenuCanvas.enabled = false;
	}



	public void OpenGameSettingsMenu() {
		gameSettingsMenuCanvas.enabled = true;
	}

	public void CloseGameSettingsMenu() {
		gameSettingsMenuCanvas.enabled = false;
	}



	public void OpenVideoSettingsMenu() {
		videoSettingsMenuCanvas.enabled = true;
	}

	public void CloseVideoSettingsMenu() {
		videoSettingsMenuCanvas.enabled = false;
	}



	public void OpenAudioSettingsMenu() {
		audioSettingsMenuCanvas.enabled = true;
	}

	public void CloseAudioSettingsMenu() {
		audioSettingsMenuCanvas.enabled = false;
	}



	// ===== QUIT MENU =====

	public void OpenQuitMenu() {
		quitMenuCanvas.enabled = true;
	}

	public void CloseQuitMenu() {
		quitMenuCanvas.enabled = false;
	}

	public void ExitGame() {
		Application.Quit();
	}
}
