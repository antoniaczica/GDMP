using UnityEngine;
using System.Collections;

public class ShirtRandomiser : MonoBehaviour {

	public Material[] shirtMaterials;
	new public Renderer renderer;

	void Start() {
		renderer.material = shirtMaterials[Random.Range(0, shirtMaterials.Length)];
	}

}
