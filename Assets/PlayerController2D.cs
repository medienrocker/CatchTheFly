using UnityEngine;

public class PlayerController2D : MonoBehaviour {

	Animator animator;
	Rigidbody2D rb2d;
	SpriteRenderer spriteRenderer;

	void Start() {
		animator = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update() {

	}
}
