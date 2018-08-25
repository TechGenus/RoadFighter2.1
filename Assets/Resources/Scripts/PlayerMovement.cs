using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public Vector2 speed;
	public float knockBackTime = 2f;

	private Transform t;
	private Animator anim;
	private Camera cam;

	private float controlFreezeTime;
	private float maxY;

	// Use this for initialization
	void Start()
	{
		cam = Camera.main;
		maxY = cam.orthographicSize;
		Debug.Log("maxY = " + maxY);

		t = GetComponent<Transform>();
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		if (anim.GetInteger("playerOneState") == 2 && Time.time <= controlFreezeTime) {
			t.Translate(new Vector3(-speed.x, -speed.y, 0) * Time.deltaTime);
		}
		else if (anim.GetInteger("playerOneState") == 3 && Time.time <= controlFreezeTime) {
			t.Translate(new Vector3(speed.x, -speed.y, 0) * Time.deltaTime);
		}
		else {
			DriveNormally();
		}
		CheckWinOrLoseConditions();
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		Debug.Log(col.transform.position.x - t.position.x);
		if (col.transform.position.x - t.position.x >= 0) { // slide right
			anim.SetInteger("playerOneState", 2);
		}
		else { // slide left
			anim.SetInteger("playerOneState", 3);
		}
		controlFreezeTime = Time.time + knockBackTime;
		Debug.Log(controlFreezeTime);
	}

	void DriveNormally()
	{
		float hAxis = Input.GetAxis("Horizontal") * speed.x;
		float vAxis = Input.GetAxis("Vertical") * speed.y;
		Vector3 translationDistance = new Vector3(hAxis, vAxis, 0) * Time.deltaTime;
		t.Translate(translationDistance);

		if (vAxis > 0 || hAxis != 0) {
			anim.SetInteger("playerOneState", 1);
		}
		else {
			anim.SetInteger("playerOneState", 0);
		}
	}

	void CheckWinOrLoseConditions()
	{
		if (t.position.y > maxY) {
			Debug.Log("You win");
		}
		else if (t.position.y < -maxY) {
			Debug.Log("You lose");
		}
	}
}
