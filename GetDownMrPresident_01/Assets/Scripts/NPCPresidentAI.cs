using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NPCPresidentAI : NPC {

	NavMeshAgent agent;
	Animator animator;
	bool atPodium = false;



	void Start() {
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponentInChildren<Animator>();
		StartCoroutine(Run());
    }

	void Update() {
		animator.SetFloat("Speed", agent.velocity.magnitude);
	}

	public void setAtPodium() {
		float rand = Random.Range(1f, 10f);
		if (rand < 5) {
			atPodium = true;
		} else {
			atPodium = false;
		}

	}

	public void GoToPodium() {
		setAtPodium();
		GameObject p = GameObject.Find("PodiumStep");
		Vector3 pod = p.transform.position;
		agent.SetDestination(new Vector3(pod.x, pod.y, pod.z));
	}


	public void RunAway(Vector3 origin) {
		StopCoroutine(Run());
		agent.SetDestination((transform.position - origin) * 100);

	}
	public void MoveRandom() {
		atPodium = false;
		agent.SetDestination(new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)));
	}

	IEnumerator Run() {
		if (atPodium) {
			GameObject p = GameObject.Find("PodiumStep");
			Vector3 pod = p.transform.position;
			agent.transform.LookAt(pod);
		}
		while (true) {
			if (atPodium) {
				MoveRandom();
			} else {
				GoToPodium();
			}

			while (agent.velocity.magnitude > 0)
				yield return null;
			yield return new WaitForSeconds(Random.Range(1f, 20f));
		}
	}

}
