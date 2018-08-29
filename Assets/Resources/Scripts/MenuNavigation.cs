using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour {
	public GameObject controlsPanel;
	public void StartGame() {
		SceneManager.LoadScene("Scene0");
	}
	public void ShowControls() {
		controlsPanel.SetActive(!controlsPanel.activeSelf);
	}
	public void LeaveGame() {
		Debug.Log("leave game");
		Application.Quit();
	}

	void Start() {

	}
	void Update() {
		float joystickY = Input.GetAxis("Player1_Vertical");
		float joystickX = Input.GetAxis("Player1_Horizontal");
		//Debug.Log("Player1: ("+joystickX+", "+joystickY+")");
		joystickY = Input.GetAxis("Player2_Vertical");
		joystickX = Input.GetAxis("Player2_Horizontal");
		//	Debug.Log("Player2: ("+joystickX+", "+joystickY+")");
		if (Input.GetButtonDown("A")) {
			Debug.Log("A");
		}
		if (Input.GetButtonDown("B")) {
			Debug.Log("B");
		}
		if (Input.GetButtonDown("X")) {
			Debug.Log("X");
		}
		if (Input.GetButtonDown("Y")) {
			Debug.Log("Y");
		}
	}
}
