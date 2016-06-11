using UnityEngine;
using System.Collections;

public class AssassinHide : MonoBehaviour {

	Vector3 lightSize;
    GameScore gameScore;
    public int playerNum;

	void Start () {
        gameScore = GameObject.FindGameObjectWithTag("Environment").GetComponent<GameScore>();
        playerNum = gameScore.getPlayerNum(this.name);
		lightSize = GameObject.Find ("Cube").transform.localScale;

	}

	void Update () {
		if (Input.GetButton("XButton" + playerNum)) {
			print ("Hiding");
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