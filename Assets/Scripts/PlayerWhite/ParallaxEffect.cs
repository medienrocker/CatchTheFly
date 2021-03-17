using UnityEngine;

public class ParallaxEffect : MonoBehaviour {
	float length, startPos;
	[SerializeField] float parallaxAmount;
	[SerializeField] GameObject cam;

	void Start() {
		startPos = transform.position.x;
		length = GetComponent<SpriteRenderer>().bounds.size.x;
	}

	void Update() {
		float dist = (cam.transform.position.x * parallaxAmount);
		transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
	}
}
