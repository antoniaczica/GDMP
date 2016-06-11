using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	//public static PlayerMovement main;

	Rigidbody rigid;
	Animator animator;
    
    GameScore gameScore;

    public int playerNum = 1;

	public float speed = 1.8f;
	public float runMultiplier = 2f;
    float actSpeed;

	public bool locked = true;

	void Start() {
		rigid = GetComponent<Rigidbody>();
		animator = GetComponentInChildren<Animator>();
		actSpeed = speed;

        gameScore = GameObject.FindGameObjectWithTag("Environment").GetComponent<GameScore>();
        playerNum = gameScore.getPlayerNum(this.name);
    }

	void Update() {
		if (locked) {
			Cursor.visible = false;
			//Cursor.lockState = CursorLockMode.Locked;
		} else {
			Cursor.visible = true;
			//Cursor.lockState = CursorLockMode.None;
		}
        
        // Toggle Run
		//if (Input.GetButton("Run" + playerNum) == true) {
		//	actSpeed = speed * runMultiplier;
		//} else {
		//	actSpeed = speed;
		//}

		animator.SetFloat("Speed", rigid.velocity.magnitude);
	}

	void FixedUpdate() {
        Vector3 v = new Vector3(Input.GetAxis("LeftStickX" + playerNum) * actSpeed, 0, Input.GetAxis("LeftStickY" + playerNum) * actSpeed);
        
        v = Camera.main.transform.TransformDirection(v);
        v = Vector3.ProjectOnPlane(v, Vector3.down);

        if (locked){
            // Movement
            Vector3 vv = v - rigid.velocity;
            rigid.AddForce(vv.x, 0, vv.z, ForceMode.Impulse);
        }

        // Jump force
        rigid.AddForce(Physics.gravity, ForceMode.Acceleration);

        // Rotation
        if (rigid.velocity.magnitude > 0.3f)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(rigid.velocity), 0.1f);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

}
