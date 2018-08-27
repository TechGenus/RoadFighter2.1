using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
	public float scrollSpeed;
	public Vector2 scrollDir;
	GameObject rectPrefab;
	bool first = true;
	Canvas canvas;
	//screen size in pixels
	Vector2 screenSize;
	// Use this for initialization
	void Start () {
		rectPrefab = gameObject;
		canvas = transform.parent.transform.parent.GetComponent<Canvas>();
		Debug.Log(transform.parent.transform.parent.name);
		Screen.SetResolution(1920,1080,true);
		screenSize = new Vector2(canvas.GetComponent<RectTransform>().rect.width, canvas.GetComponent<RectTransform>().rect.height);
	}
	
	// Update is called once per frame
	void Update () {
		//scroll dir: (x,y) st Max(x,y) == 1
		float max = Mathf.Max(Mathf.Abs(scrollDir.x), Mathf.Abs(scrollDir.y));
		scrollDir = scrollDir/max;
		//Get the position of the object relative to the parent in pixel soace (0,0) is the middle of screen
		Vector2 localPos = new Vector2(GetComponent<RectTransform>().localPosition.x, GetComponent<RectTransform>().localPosition.y);


		//if the image is past the middle
		if (first&&Vector2.Dot(localPos,scrollDir)>=0) {
			//the location of the newly instantiated image
			Vector2 newPos = -(new Vector2(screenSize.x*scrollDir.x, screenSize.y*scrollDir.y))+10*scrollSpeed*scrollDir;
			//instantiate a new image
			GameObject next = (GameObject) Instantiate(rectPrefab,transform.parent);
			next.transform.parent = transform.parent;
			next.GetComponent<RectTransform>().localPosition = newPos;
			next.transform.SetParent(this.transform.parent);
			next.name = gameObject.name;
			//next.GetComponent<RectTransform>().sizeDelta = GetComponent<RectTransform>().sizeDelta;
			first = false;

		}
		//if its off the screen
		else if (!first&&Mathf.Abs(localPos.x)>=Mathf.Abs(screenSize.x*scrollDir.x)&&Mathf.Abs(localPos.y)>=Mathf.Abs(screenSize.y*scrollDir.y) ) {
			Destroy(gameObject);
		}
		//The relative movement of the object
		Vector2 relativeDir = new Vector2(scrollDir.x*screenSize.x, scrollDir.y*screenSize.y).normalized;
		transform.Translate(relativeDir*scrollSpeed);

	}
}
