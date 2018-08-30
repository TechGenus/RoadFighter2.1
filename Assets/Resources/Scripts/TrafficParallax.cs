using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficParallax : MonoBehaviour {
	public float speed;
	
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rb.velocity = new Vector3(0, speed, 0);
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Respawn") {
			Destroy(this.gameObject);
		}
	}
}
