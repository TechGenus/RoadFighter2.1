using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public Vector2 speed;
	public float knockBackTime = 2f;

	private Transform t;
	private Animator anim;

	private float controlFreezeTime;

	// Use this for initialization
	void Start()
	{
		t = GetComponent<Transform>();
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		float hAxis = Input.GetAxis("Horizontal") * speed.x;
		float vAxis = Input.GetAxis("Vertical") * speed.y;
		Vector3 translationDistance = new Vector3(hAxis, vAxis, 0) * Time.deltaTime;
		t.Translate(translationDistance);

		if (anim.GetInteger("playerOneState") > 1 && Time.time <= controlFreezeTime) {
			
		}
		else if (vAxis > 0 || hAxis != 0) {
			anim.SetInteger("playerOneState", 1);
		}
		else {
			anim.SetInteger("playerOneState", 0);
		}
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
}
