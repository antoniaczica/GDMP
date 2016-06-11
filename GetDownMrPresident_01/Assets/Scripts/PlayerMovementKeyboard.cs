using UnityEngine;
using System.Collections;

public class PlayerMovementKeyboard : MonoBehaviour {

	public static PlayerMovementKeyboard main;

	Rigidbody rigid;
	public float speed = 0.1f;
	public float runMultiplyer = 2;
	public KeyCode runKey = KeyCode.LeftShift;
	float actSpeed;

	public float jumpRaycastDistance = 0.8f;
	public float jumpForce = 1.2f;

	public bool locked = true;

	void Start() {
		main = this;
		rigid = GetComponent<Rigidbody>();
		actSpeed = speed;
	}

	void Update() {
		//if (locked) {
		//	Cursor.visible = false;
		//	Cursor.lockState = CursorLockMode.Locked;
		//} else {
		//	Cursor.visible = true;
		//	Cursor.lockState = CursorLockMode.None;
		//}

		if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), jumpRaycastDistance)) {
			//jump
			rigid.AddForce(transform.TransformDirection(-Physics.gravity * jumpForce), ForceMode.VelocityChange);
		}
		if (Input.GetKey(runKey)) {
			//sprint
			actSpeed = speed * runMultiplyer;
		} else {
			actSpeed = speed;
		}
	}

	void FixedUpdate() {
		Vector3 v = new Vector3(Input.GetAxis("Horizontal2") * speed, 0, Input.GetAxis("Vertical2") * actSpeed);
		//v = transform.TransformDirection(v);
		v = Camera.main.transform.TransformDirection(v);
		v = Vector3.ProjectOnPlane(v, Vector3.down);

		if (locked) {
			//rigid.MovePosition(transform.position + v);
			Vector3 vv = v - rigid.velocity;
			//vv *= 100;
			rigid.AddForce(vv.x, 0, vv.z, ForceMode.Impulse);
			//v *= 10;
			//rigid.velocity = new Vector3(v.x, rigid.velocity.y, v.z);
		}

		rigid.AddForce(Physics.gravity * 1.5f, ForceMode.Acceleration);
	}

}
