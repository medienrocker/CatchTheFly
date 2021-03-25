using UnityEngine;
using UnityEngine.UI;

public class FloatingNumbers : MonoBehaviour {
	[SerializeField] float moveSpeed;
	[SerializeField] public int damageNumber;
	[SerializeField] Text displayNumber;

	void Update() {
		displayNumber.text = "+" + damageNumber.ToString();
		transform.position = new Vector3(
			transform.position.x, 
			transform.position.y + (moveSpeed * Time.deltaTime), 
			transform.position.z);
	}
}
