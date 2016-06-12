using UnityEngine;
using System.Collections;

public class BodyguardTakedown : MonoBehaviour {

	GameScore gameScore;

	public float minDistance = 1.5f;
	public int playerNum;

	void Start()
	{
		gameScore = GameObject.FindGameObjectWithTag("Environment").GetComponent<GameScore>();
		playerNum = gameScore.getPlayerNum("Dev_Player_01 (Bodyguard)");
	}

	void Update() {
		// Assuming that player 1 is assassin, will need to be changed when player switching is implemented
		if(Input.GetAxis("LeftTrigger1") == 1){

			//print(Input.GetJoystickNames().ToString());

			print("Trigger : " + Input.GetAxis ("LeftTrigger1"));

		}
		if (Input.GetButtonDown("AButton" + playerNum)) {
			//print("button!" + playerNum);
			Takedown();
		}
	}
	public void Takedown() {
		//Get Assassin
		GameObject assassin = GameObject.Find("Dev_Player_01 (Assassin)");
		//if in range
		if (Vector3.Distance(assassin.transform.position, this.transform.position) < minDistance) {
			//takedown
			RoundManager.main.PresidentSaved();
			assassin.gameObject.GetComponent<Rigidbody>().isKinematic = false;
			assassin.gameObject.GetComponent<Rigidbody>().useGravity = true;
			assassin.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			assassin.gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, 80));
//			Destroy(assassin.gameObject.GetComponent<PlayerTakedown>());
//			Destroy(assassin.gameObject.GetComponent<PlayerMovement>());
//			Destroy(assassin.gameObject.GetComponent<NPCPresidentAI>());
//			Destroy(assassin.gameObject.GetComponent<NavMeshAgent>());
			gameObject.tag = "Untagged";
			GetComponent<Rigidbody>().AddForce(Vector3.Normalize(assassin.transform.position - transform.position) * 35, ForceMode.VelocityChange);

		}

	}
}