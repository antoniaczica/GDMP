using UnityEngine;
using System.Collections;

public class AnimateMaterialOfffset : MonoBehaviour {

	public Vector2 speed = new Vector2 (0.2f, 0.2f);
	Vector2 off = new Vector2 ();

	Material mat;

	// Use this for initialization
	void Start () {
		mat = GetComponent<Renderer> ().sharedMaterial;
	}
	
	// Update is called once per frame
	void Update () {
		off += speed * Time.deltaTime;
		mat.SetTextureOffset ("_MainTex", off);
	}
}
