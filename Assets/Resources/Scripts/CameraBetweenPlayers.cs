﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraBetweenPlayers : MonoBehaviour {
	public Transform player1T;
	public Transform player2T;
	public Text winText;
	public Image winImage;


	private Transform t;
	private Vector3 player1StartingPos;
	private Vector3 player2StartingPos;
	private Camera cam;

	// Use this for initialization
	void Start () {
		t = GetComponent<Transform>();
		player1StartingPos = player1T.position;
		player2StartingPos = player2T.position;
		cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = new Vector3(t.position.x, (player1T.position.y + player2T.position.y) / 2, t.position.z);
		t.SetPositionAndRotation(newPos, Quaternion.identity);
		CheckWinOrLoseConditions();
	}

	void CheckWinOrLoseConditions() {
		if (player1T.position.y > t.position.y + cam.orthographicSize) {
			ShowWinningScreen("The winner is Blue!");
			Time.timeScale = 0f;
		} else if (player2T.position.y > t.position.y + cam.orthographicSize) {
			ShowWinningScreen("The winner is Red!");
			Time.timeScale = 0f;
		}
	}

	void ShowWinningScreen(string playerWinText) {
		winText.gameObject.SetActive(true);
		winText.text = playerWinText;
		winImage.gameObject.SetActive(true);
	}
}
