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
	[HideInInspector] public float vAxis;

	private Vector2 deltaSpeed;
	private Animator anim;
	private Camera cam;
	private float controlFreezeTime;
	private float maxY;
	private float hSpeed;
	private float vSpeed;
	private Transform thisPlayerT;
	private Transform otherPlayerT;
	private PlayerMovement otherPlayerMovement;

	// Use this for initialization
	void Start() {
		cam = Camera.main;
		maxY = cam.orthographicSize;
		Debug.Log("maxY = " + maxY);

		thisPlayerT = GetComponent<Transform>();
		otherPlayerT = otherPlayer.GetComponent<Transform>();
		otherPlayerMovement = otherPlayer.GetComponent<PlayerMovement>();

		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update() {
		deltaSpeed = speed * Time.deltaTime;
		hSpeed = Input.GetAxisRaw(horizontalAxisName) * deltaSpeed.x;
		vAxis = Input.GetAxis(verticalAxisName);
		float rawVAxis = Input.GetAxisRaw(verticalAxisName);
		if (rawVAxis == -1) {
			vSpeed = rawVAxis * deltaSpeed.y;
		} else {
			vSpeed = vAxis * deltaSpeed.y;
		}

		InfluenceOtherPlayer(vAxis); 

		TranslatePlayer();
	}

	void InfluenceOtherPlayer(float vAxis) {
		if (vAxis == otherPlayerMovement.vAxis) {
			vSpeed = 0f;
		}
	}

	void TranslatePlayer() {
		if (Time.time <= controlFreezeTime) {
			if (anim.GetInteger(playerAnimationStateName) == 2) {
				thisPlayerT.Translate(new Vector3(-deltaSpeed.x, -deltaSpeed.y, 0));
			} else if (anim.GetInteger(playerAnimationStateName) == 3) {
				thisPlayerT.Translate(new Vector3(deltaSpeed.x, -deltaSpeed.y, 0));
			}
		} else {
			thisPlayerT.Translate(new Vector3(hSpeed, vSpeed, 0));

			if (vAxis != 0 || hSpeed != 0) {
				anim.SetInteger(playerAnimationStateName, 1);
			} else {
				anim.SetInteger(playerAnimationStateName, 0);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.transform.position.x - thisPlayerT.position.x >= 0) { // slide right
			anim.SetInteger(playerAnimationStateName, 2);
		} else { // slide left
			anim.SetInteger(playerAnimationStateName, 3);
		}
		controlFreezeTime = Time.time + knockBackTime;
	}
}
