using UnityEngine;

public class CameraController : MonoBehaviour {

	[SerializeField]
	GameObject followTarget;
	[SerializeField]
	float moveSpeed;

	Vector3 targetPos;

	static bool cameraExists;

	void Start() {
		if (!cameraExists) {
			cameraExists = true;
			DontDestroyOnLoad(transform.gameObject);
		}
		else {
			Destroy(gameObject);
		}
	}

	void Update() {
		targetPos = new Vector3(
			followTarget.transform.position.x, 
			followTarget.transform.position.y, 
			transform.position.z);

		transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
	}
}
