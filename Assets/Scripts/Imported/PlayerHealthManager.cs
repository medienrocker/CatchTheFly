using UnityEngine;

public class PlayerHealthManager : MonoBehaviour {

	public int maxHealth;
	public int currentHealth;

	public bool isFlashing;
	[SerializeField] float flashLength;
	float flashCounter;
	SpriteRenderer playerSprite;
	float r, g, b;

	void Start() {
		SetHealth();

		playerSprite = GetComponent<SpriteRenderer>();
		r = playerSprite.color.r;
		g = playerSprite.color.g;
		b = playerSprite.color.b;
	}

	void Update() {
		if (currentHealth <= 0) {
			gameObject.SetActive(false);
		}

		if (isFlashing) {

			if (flashCounter > flashLength * 0.66) {
				playerSprite.color = new Color(r, g, b, 0f);
			}
			else if (flashCounter > flashLength * 0.33) {
				playerSprite.color = new Color(r, g, b, 1f);
			}
			else if (flashCounter > 0) {
				playerSprite.color = new Color(r, g, b, 0f);
			}
			else {
				playerSprite.color = new Color(r, g, b, 1f);
				isFlashing = false;
			}

			flashCounter -= Time.deltaTime;
		}

	}

	void SetHealth() {
		currentHealth = maxHealth;
	}

	public void TakeHit(int damage) {
		currentHealth -= damage;

		//isFlashing = true;
		//flashCounter = flashLength;
	}
}
