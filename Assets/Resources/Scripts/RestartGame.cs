using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour {
	public string nameOfSceneToLoad;
	public string axisName;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw(axisName) == 1 || Input.GetKeyDown(KeyCode.R)) {
			SceneManager.LoadScene(nameOfSceneToLoad);
			Time.timeScale = 1;
		}
	}
}
