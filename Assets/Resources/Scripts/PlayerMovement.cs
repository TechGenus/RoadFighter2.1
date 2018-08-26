using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public Vector2 speed;
	public float knockBackTime = 1f;
	public GameObject otherPlayer;
	public string horizontalAxisName;
	public string verticalAxisName;
	public string playerAnimationStateName;

	private Vector2 deltaSpeed;
	private Animator anim;
	private Camera cam;
	private float controlFreezeTime;
	private float maxY;
	private Transform player1T;
	private float hAxis;
	private float vAxis;

	// Use this for initialization
	void Start() {
		cam = Camera.main;
		maxY = cam.orthographicSize;
		Debug.Log("maxY = " + maxY);

		player1T = GetComponent<Transform>();
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update() {
		deltaSpeed = speed * Time.deltaTime;
		hAxis = Input.GetAxisRaw(horizontalAxisName) * deltaSpeed.x;
		vAxis = Input.GetAxis(verticalAxisName) * deltaSpeed.y;

		if (Time.time <= controlFreezeTime) {
			if (anim.GetInteger(playerAnimationStateName) == 2) {
				player1T.Translate(new Vector3(-deltaSpeed.x, -deltaSpeed.y, 0));
			} else if (anim.GetInteger(playerAnimationStateName) == 3) {
				player1T.Translate(new Vector3(deltaSpeed.x, -deltaSpeed.y, 0));
			}
		} else {
			player1T.Translate(new Vector3(hAxis, vAxis, 0));

			if (vAxis != 0 || hAxis != 0) {
				anim.SetInteger(playerAnimationStateName, 1);
			} else {
				anim.SetInteger(playerAnimationStateName, 0);
			}
		}

		CheckWinOrLoseConditions();
	}

	void CheckWinOrLoseConditions() {
		if (player1T.position.y > maxY) {
			Debug.Log("You win");
		} else if (player1T.position.y < -maxY) {
			Debug.Log("You lose");
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.transform.position.x - player1T.position.x >= 0) { // slide right
			anim.SetInteger(playerAnimationStateName, 2);
		} else { // slide left
			anim.SetInteger(playerAnimationStateName, 3);
		}
		controlFreezeTime = Time.time + knockBackTime;
	}
}
