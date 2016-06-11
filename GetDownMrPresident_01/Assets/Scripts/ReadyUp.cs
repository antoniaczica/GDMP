using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ReadyUp : MonoBehaviour {

	public Image redDPad;
	public Image greenDPad;
	public Image redA;
	public Image greenA;

	// Use this for initialization
	void Start () {
		redDPad.enabled = true;
		greenDPad.enabled = false;
		redA.enabled = true;
		greenA.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
