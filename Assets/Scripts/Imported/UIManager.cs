using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	[SerializeField] Slider healthBar;
	[SerializeField] Text hpText;
	public PlayerHealthManager playerHealth;
	[SerializeField] Text expText;
	[SerializeField] Text levelText;


	private static bool UIExists;

	PlayerStats playerStats;

	void Start() {
		if (!UIExists) {
			UIExists = true;
			DontDestroyOnLoad(transform.gameObject);
		}
		else {
			Destroy(gameObject);
		}

		playerStats = FindObjectOfType<PlayerStats>();
	}

	void Update() {
		healthBar.maxValue = playerHealth.maxHealth;
		healthBar.value = playerHealth.currentHealth;
		hpText.text = $"HP: {playerHealth.currentHealth.ToString()}/{playerStats.currentHP.ToString()}";
		expText.text = $"XP: {playerStats.currentExp.ToString()}/{playerStats.toLevelUp[playerStats.currentLevel + 1].ToString()}";
		levelText.text = $"Level: {playerStats.currentLevel}";
	}
}
