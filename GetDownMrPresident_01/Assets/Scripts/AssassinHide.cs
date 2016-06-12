using UnityEngine;
using System.Collections;

public class AssassinHide : MonoBehaviour {

	Vector3 lightSize;
    GameScore gameScore;
    public int playerNum;
    Rigidbody rb;

	void Start () {
        gameScore = GameObject.FindGameObjectWithTag("Environment").GetComponent<GameScore>();
        playerNum = gameScore.getPlayerNum(this.name);

        lightSize = GameObject.Find ("Cube").transform.localScale;
		rb = this.GetComponent<Rigidbody>();

	}

	void Update () {
		float rbVel = rb.velocity.magnitude;
		if (Input.GetAxis("LeftTrigger" + playerNum) == 1 && rbVel <0.5) {
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