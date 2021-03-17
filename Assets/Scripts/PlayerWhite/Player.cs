using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	// Config
	[SerializeField] float runSpeed = 12f;
	[SerializeField] float jumpForce = 40f;
	[SerializeField] float climbSpeed = 12f;

	public float jumpMultiplier = 1f;
	float gravityScaleAtStart;
	bool wasOnGround;

	// States
	bool isAlive = true;
	public bool isHighJumping;
	bool isJumping;

	// Caches and references
	Rigidbody2D myRigidbody2D;
	Animator myAnimator;
	SpriteRenderer mySpriteRenderer;

	[SerializeField] BoxCollider2D myBodyCollider;
	[SerializeField] BoxCollider2D myFeetCollider;

	[SerializeField] ParticleSystem footsteps;
	ParticleSystem.EmissionModule footstepsEmission;

	[SerializeField] ParticleSystem impactEffect;
	[SerializeField] ParticleSystem dieEffect;
	[SerializeField] Vector2 deathKick = new Vector2(25, 50);

	Color originalSpriteColor;


	// Messages then Methods
	void Start() {
		//isAlive = true;
		if (!isAlive) {return;}

		myRigidbody2D = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		myBodyCollider = GetComponent<BoxCollider2D>();
		mySpriteRenderer = GetComponent<SpriteRenderer>();

		originalSpriteColor = mySpriteRenderer.color;

		gravityScaleAtStart = myRigidbody2D.gravityScale;

		footstepsEmission = footsteps.emission;
	}

	void Update() {
		Run();
		Climb();
		if (jumpMultiplier < 1f) {
			jumpMultiplier = 1f;
		}
		Jump();
		Crouch();
		FlipPlayerSprite();
		MakeImpactEffect();
		//if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy"))) {
		//	Die();
		//}

		ChangeColor();
	}

	void ChangeColor() {
		if (isHighJumping) {
			mySpriteRenderer.color = Color.blue;
		}
		else {
			mySpriteRenderer.color = originalSpriteColor;
		}
	}

	void Run() {
		float moveX = Input.GetAxisRaw("Horizontal");
		myRigidbody2D.velocity = new Vector2(moveX * runSpeed, myRigidbody2D.velocity.y);

		bool hasHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon; // means > 0
		myAnimator.SetBool("isRunning", hasHorizontalSpeed);

		MakeFootstepDust(moveX);
	}


	void Climb() {
		if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) {
			myAnimator.SetBool("isClimbing", false);
			myRigidbody2D.gravityScale = gravityScaleAtStart;
			return; 
		}

		//float moveY = Input.GetAxisRaw("Vertical");
		//myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, moveY * climbSpeed);
		//myRigidbody2D.gravityScale = 0f;

		float moveYUp = Input.GetAxisRaw("VerticalOnlyUp");
		myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, moveYUp * climbSpeed);
		myRigidbody2D.gravityScale = 0f;

		bool hasVerticalSpeed = Mathf.Abs(myRigidbody2D.velocity.y) > Mathf.Epsilon; // means > 0
		myAnimator.SetBool("isClimbing", hasVerticalSpeed);
	}

	void Jump() {
		if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Climbing"))) {
			isJumping = true;
			myAnimator.SetBool("isJumping", isJumping);
			myAnimator.SetBool("isRunning", false);
			return; 
		}

		if (Input.GetButtonDown("Jump")) {
			Vector2 jumpVelocityToAdd = new Vector2(0f, jumpForce * jumpMultiplier);
			myRigidbody2D.velocity += jumpVelocityToAdd;
		}
		isJumping = false;
		myAnimator.SetBool("isJumping", isJumping);
	}

	void Crouch() {
		if (!Input.GetButtonDown("Jump") && !isJumping) {
			if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
				myAnimator.SetBool("isRunning", false);
				myAnimator.SetBool("isCrouching", true);
				myRigidbody2D.velocity = Vector2.zero;
			}
		}

		if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)) {
			myAnimator.SetBool("isCrouching", false);
		}
	}

	public void Die() {
			isAlive = false;
			myAnimator.SetTrigger("Die");
			GetComponent<Rigidbody2D>().velocity = deathKick;
			StartCoroutine(DieDelay(.5f,2f)); // wait .5 sec to explode and 2 seconds to reload scene
	}

	IEnumerator DieDelay(float secondsToExplode, float secondsToReload) {
		yield return new WaitForSeconds(secondsToExplode);
		Instantiate(dieEffect, transform.position, Quaternion.Euler(0,0,180));
		mySpriteRenderer.enabled = false;
		myRigidbody2D.isKinematic = true;
		yield return new WaitForSeconds(secondsToReload);
		SceneManager.LoadScene(0);
	}

	void FlipPlayerSprite() {
		bool hasHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon; // means > 0
		if (hasHorizontalSpeed) {
			transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.velocity.x), transform.localScale.y);
		}
	}

	// Particle effects
	void MakeFootstepDust(float movement) {
		if (Mathf.Abs(movement) > Mathf.Epsilon && myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Climbing"))) {
			footstepsEmission.rateOverTime = 50f;
		}
		else {
			footstepsEmission.rateOverTime = 0f;
		}
	}

	void MakeImpactEffect() {
		bool isGrounded = myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Climbing"));
		if (!wasOnGround && isGrounded) {
			myAnimator.SetTrigger("Landing");
			impactEffect.gameObject.SetActive(true);
			impactEffect.Stop();
			//impactEffect.transform.position = transform.position;
			impactEffect.Play();
		}

		wasOnGround = isGrounded;
	}

}
