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
}
