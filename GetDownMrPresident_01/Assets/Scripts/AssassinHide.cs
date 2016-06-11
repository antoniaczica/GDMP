using UnityEngine;
using System.Collections;

public class AssassinHide : MonoBehaviour {

	Vector3 lightSize;
	void Start () {
		lightSize = GameObject.Find ("Cube").transform.localScale;
	}

	void Update () {
		if (Input.GetKey ("space")) {
			HideOrShow(new Vector3(0,0,0));
		} else {
			HideOrShow(lightSize);
		}
	}

	// changes size of object for hiding or showing
	void HideOrShow(Vector3 size){
		GameObject.Find ("Cube").transform.localScale = size;
	}
}