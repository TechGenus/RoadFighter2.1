using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// A script to activate the start button when the scene is loaded
public class StartButton : MonoBehaviour {

	Button thisOne;
	// Use this for initialization
	void Start () {
		thisOne = GetComponent<Button>();
		thisOne.Select();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
