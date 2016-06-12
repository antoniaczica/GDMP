using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerTakedown : MonoBehaviour {
    
    GameScore gameScore;

    public float minDistance = 1.5f;
	public int playerNum = 1;

    void Start()
    {
        gameScore = GameObject.FindGameObjectWithTag("Environment").GetComponent<GameScore>();
        playerNum = gameScore.getPlayerNum(this.name);
		//print ("getPLayer name: " + this.name);
    }
    
	void Update() {
		// Assuming that player 1 is assassin, will need to be changed when player switching is implemented
		//print("trynna udate : "+playerNum);
		if (playerNum != -1) {
			if (Input.GetAxis ("RightTrigger" + playerNum) == 1) {
			
				Takedown ();
			}
		}
	}
	public void Takedown() {
		PlayerTakedown[] targets = FindObjectsOfType<PlayerTakedown>();
		foreach (PlayerTakedown target in targets) {
			if (target.playerNum == this.playerNum)
				continue;
			if (Vector3.Distance(target.transform.position, this.transform.position) < minDistance) {
				print("takedonw!!!! " + target.playerNum + "---" + this.playerNum);
				print(Vector3.Distance(target.transform.position, transform.position));

				if (this.name.Equals("Dev_Player_01 (Assassin)")) {
					if (target.gameObject.CompareTag("President")) {
						RoundManager.main.PresidentDown();
					} else {
						break;
					}
				} else if (this.name.Equals("Dev_Player_01 (Bodyguard)")) {
					if (target.gameObject.CompareTag("President")) {
						continue;
					} else {
						RoundManager.main.PresidentSaved();
					}
				} else {
					continue;
				}

				target.gameObject.GetComponent<Rigidbody>().isKinematic = false;
				target.gameObject.GetComponent<Rigidbody>().useGravity = true;
				target.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				target.gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, 80));
				Destroy(target.gameObject.GetComponent<PlayerTakedown>());
				Destroy(target.gameObject.GetComponent<PlayerMovement>());
				Destroy(target.gameObject.GetComponent<NPCPresidentAI>());
				Destroy(target.gameObject.GetComponent<NavMeshAgent>());
				gameObject.tag = "Untagged";
				GetComponent<Rigidbody>().AddForce(Vector3.Normalize(target.transform.position - transform.position) * 35, ForceMode.VelocityChange);
				break;
			}
		}
	}

}
