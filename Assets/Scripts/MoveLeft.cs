using UnityEngine;

public class MoveLeft : MonoBehaviour {
	public float moveSpedd;

	void Start() {

	}

	void Update() {
		transform.Translate(Vector3.left * moveSpedd * Time.deltaTime);
	}
}
