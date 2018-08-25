using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
	public float scrollSpeed;
	GameObject rectPrefab;
	bool first = true;
	Canvas canvas;
	// Use this for initialization
	void Start () {
		rectPrefab = gameObject;
		canvas = transform.parent.GetComponent<Canvas>();
		Debug.Log(canvas.enabled);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 localPos = GetComponent<RectTransform>().localPosition;

		//if the image is past the middle
		if (first&&localPos.x>0) {
			//the location of the newly instantiated image
			Vector2 pos = new Vector2(-(GetComponent<RectTransform>().rect.width), 0);
			//instantiate a new image
			GameObject next = (GameObject) Instantiate(rectPrefab,transform.parent);
			next.GetComponent<RectTransform>().localPosition = pos;
			next.transform.SetParent(this.transform.parent);
			next.name = gameObject.name;
			//next.GetComponent<RectTransform>().sizeDelta = GetComponent<RectTransform>().sizeDelta;
			first = false;

		}
		//if its off the screen
		else if (!first&&localPos.x>GetComponent<RectTransform>().rect.width ) {
			Destroy(gameObject);
		}
		transform.Translate(Vector3.right*scrollSpeed);

	}
}
