using UnityEngine;
using InControl;

public class PlayerInput : MonoBehaviour {

	public enum InputMethod { Keyboard, Controller }

	public InputMethod inputMethod;

	playerMove player;

	void Awake() {
		player = GetComponent<playerMove>();
	}

	void Update() {
		if (player != null) {
			switch (inputMethod){
				case InputMethod.Keyboard:
					player.horizontalInput = Input.GetAxis("Horizontal"); // Add the controls together so either can be used
					player.verticalInput = Input.GetAxis("Vertical"); // Add the controls together so either can be used
					player.zInput = Input.GetAxis("Z"); // Add the controls together so either can be used
					player.xInput = Input.GetAxis("X"); // Add the controls together so either can be used
					player.cInput = Input.GetAxis("C"); // Add the controls together so either can be used
					player.jumpInput = Input.GetAxis("Jump"); // Add the controls together so either can be used
					break;
				case InputMethod.Controller:
					InputDevice device = InputManager.ActiveDevice;
					player.horizontalInput = device.LeftStickX + device.DPadX; // Add the controls together so either can be used
					player.verticalInput = device.LeftStickY + device.DPadY; // Add the controls together so either can be used
					player.zInput = device.Action2; // Add the controls together so either can be used
					player.xInput = device.Action3; // Add the controls together so either can be used
					player.cInput = device.RightBumper; // Add the controls together so either can be used
					player.jumpInput = device.Action1; // Add the controls together so either can be used
					break;
			}
		}
		else {
			Debug.LogWarning("(PlayerInput) No player to control! Add a playerMove script to this GameObject");
		}
	}

}