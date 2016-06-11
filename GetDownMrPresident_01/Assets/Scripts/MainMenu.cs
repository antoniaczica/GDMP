using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	public Button play;
	public Button controls;
	public Button exit;

	public Image controlsWindow;

	// Use this for initialization
	void Start () {
		controlsWindow.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Cancel") > 0){
			controlsCanceled ();
		}
	}

	public void playPressed(){
		Application.LoadLevel (1);
	}

	public void controlsPressed(){
		controlsWindow.enabled = true;
		play.enabled = false;
		controls.enabled = false;
		exit.enabled = false;
	}
	public void exitPressed(){
		Application.Quit ();
	}


	public void controlsCanceled(){
		controlsWindow.enabled = false;
		play.enabled = true;
		controls.enabled = true;
		exit.enabled = true;
	}
}
