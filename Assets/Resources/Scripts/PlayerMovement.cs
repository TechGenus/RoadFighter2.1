using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public Vector2 speed;
	public float collisionSpeed;
	public float maxSpeed;
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
	private Rigidbody2D rb;
	private PlayerMovement otherPlayerMovement;

	// Use this for initialization
	void Start() {
		cam = Camera.main;
		maxY = cam.orthographicSize;
		Debug.Log("maxY = " + maxY);

		thisPlayerT = GetComponent<Transform>();
		otherPlayerT = otherPlayer.GetComponent<Transform>();
		otherPlayerMovement = otherPlayer.GetComponent<PlayerMovement>();

		rb = GetComponent<Rigidbody2D>();

		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update() {
		deltaSpeed = speed * Time.deltaTime;
		hSpeed = Input.GetAxisRaw(horizontalAxisName) * deltaSpeed.x;
		vAxis = Input.GetAxis(verticalAxisName);
		
		vSpeed = vAxis * deltaSpeed.y;

		TranslatePlayer();
	}

	void TranslatePlayer() {
		if (Time.time <= controlFreezeTime) {
			float normalizedVelocityY = rb.velocity.y / Mathf.Abs(rb.velocity.y);
			if (anim.GetInteger(playerAnimationStateName) == 2) {
				thisPlayerT.Translate(new Vector3(-deltaSpeed.x, 0, 0));
				rb.velocity -= new Vector2(0, normalizedVelocityY * collisionSpeed);
			} else if (anim.GetInteger(playerAnimationStateName) == 3) {
				thisPlayerT.Translate(new Vector3(deltaSpeed.x, 0, 0));
				rb.velocity -= new Vector2(0, normalizedVelocityY * collisionSpeed);
			}
		} else {
			//thisPlayerT.Translate(new Vector3(hSpeed, vSpeed, 0));
			thisPlayerT.Translate(new Vector3(hSpeed, 0, 0));
			rb.velocity = new Vector2(0, vSpeed);

			if (rb.velocity.y != 0 || hSpeed != 0) {
				anim.SetInteger(playerAnimationStateName, 1);
			} else {
				anim.SetInteger(playerAnimationStateName, 0);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		Vector3 collisionPos = col.transform.position;
		if (collisionPos.y > thisPlayerT.position.y) {
			if (collisionPos.x > thisPlayerT.position.x) { // slide right
				anim.SetInteger(playerAnimationStateName, 2);
			} else { // slide left
				anim.SetInteger(playerAnimationStateName, 3);
			}
			controlFreezeTime = Time.time + knockBackTime;
		}
	}
}
