using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
	public Vector2 speed;
	public float collisionSpeed;
	public float maxSpeed;
	public float knockBackTime = 1f;
	public string horizontalAxisName;
	public string verticalAxisName;
	public string playerAnimationStateName;
	public Text playerSpeedText;
	[HideInInspector] public float vAxis;

	private Vector2 deltaSpeed;
	private Animator anim;
	private float controlFreezeTime;
	private float hSpeed;
	private float vSpeed;
	private Transform thisPlayerT;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start() {
		thisPlayerT = GetComponent<Transform>();

		rb = GetComponent<Rigidbody2D>();

		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate() {
		deltaSpeed = speed * Time.deltaTime;
		hSpeed = Input.GetAxisRaw(horizontalAxisName) * deltaSpeed.x;
		vAxis = Input.GetAxis(verticalAxisName);
		
		vSpeed = vAxis * deltaSpeed.y;

		TranslatePlayer();
	}

	void TranslatePlayer() {
		float currVelocity = rb.velocity.y;
		float normalizedVelocityY = CalculateNormalizedVelocityY(currVelocity);
		if (Time.time <= controlFreezeTime) {
			
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

			if (currVelocity < maxSpeed && currVelocity > -maxSpeed) {
				rb.velocity += new Vector2(0, vSpeed - normalizedVelocityY * 5 * Time.deltaTime);
			} else {
				rb.velocity -= new Vector2(0, normalizedVelocityY * 5 * Time.deltaTime);
			}

			float displaySpeed = Mathf.Round(rb.velocity.y * 10);
			playerSpeedText.text = displaySpeed.ToString();

			if (rb.velocity.y != 0 || hSpeed != 0) {
				anim.SetInteger(playerAnimationStateName, 1);
			} else {
				anim.SetInteger(playerAnimationStateName, 0);
			}
		}
	}

	float CalculateNormalizedVelocityY(float currVelocity) {
		if (currVelocity != 0) {
			return currVelocity / Mathf.Abs(currVelocity);
		} else return 0;
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
