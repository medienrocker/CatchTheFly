using UnityEngine;

public class Player : MonoBehaviour {

	// Config
	[SerializeField] float runSpeed = 5f;
	[SerializeField] float jumpForce = 10f;
	[SerializeField] float climbSpeed = 10f;
	
	float gravityScaleAtStart;
	bool wasOnGround;

	// States
	//bool isAlive = true;

	// Caches and references
	Rigidbody2D myRigidbody2D;
	Animator myAnimator;
	[SerializeField] BoxCollider2D myBodyCollider;
	[SerializeField] BoxCollider2D myFeetCollider;

	[SerializeField] ParticleSystem footsteps;
	ParticleSystem.EmissionModule footstepsEmission;

	[SerializeField] ParticleSystem impactEffect;

	// Messages then Methods
	void Start() {
		myRigidbody2D = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		myBodyCollider = GetComponent<BoxCollider2D>();
		gravityScaleAtStart = myRigidbody2D.gravityScale;

		footstepsEmission = footsteps.emission;
	}

	void Update() {
		Run();
		Climb();
		Jump();
		FlipPlayerSprite();
		MakeImpactEffect();
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

		float moveY = Input.GetAxisRaw("Vertical");
		myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, moveY * climbSpeed);
		myRigidbody2D.gravityScale = 0f;

		bool hasVerticalSpeed = Mathf.Abs(myRigidbody2D.velocity.y) > Mathf.Epsilon; // means > 0
		myAnimator.SetBool("isClimbing", hasVerticalSpeed);
	}

	void Jump() {
		if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Climbing"))) {
			myAnimator.SetBool("isJumping", true);
			myAnimator.SetBool("isRunning", false);
			return; 
		}

		if (Input.GetButtonDown("Jump")) {
			Vector2 jumpVelocityToAdd = new Vector2(0f, jumpForce);
			myRigidbody2D.velocity += jumpVelocityToAdd;
		}

		myAnimator.SetBool("isJumping", false);
		//float jumpVelocityNormalized = Mathf.Sine(myRigidbody2D.velocity.y); // returns 1 or -1
		//myAnimator.SetFloat("JumpVelocity", jumpVelocityNormalized);
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
			impactEffect.gameObject.SetActive(true);
			impactEffect.Stop();
			//impactEffect.transform.position = transform.position;
			impactEffect.Play();
		}

		wasOnGround = isGrounded;
	}
}
