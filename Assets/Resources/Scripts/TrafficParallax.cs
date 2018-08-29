using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficParallax : MonoBehaviour {
	public float speed;

	private Transform t;

	// Use this for initialization
	void Start () {
		t = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		t.Translate(new Vector3(0, speed, 0) * Time.deltaTime);
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Respawn") {
			Destroy(this.gameObject);
		}
	}
}
