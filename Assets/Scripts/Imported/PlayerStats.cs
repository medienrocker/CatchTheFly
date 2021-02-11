using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
	public int currentLevel;
	public int currentExp;
	public int[] toLevelUp;

	public int[] hpLevels;
	public int[] attackLevels;
	public int[] defenseLevels;

	public int currentHP;
	public int currentAttack;
	public int currentDefense;

	PlayerHealthManager playerHealth;

	void Start() {
		currentHP = hpLevels[1];
		currentAttack = attackLevels[1];
		currentDefense = defenseLevels[1];

		playerHealth = FindObjectOfType<PlayerHealthManager>();
	}

	void Update() {
		if (currentExp >= toLevelUp[currentLevel]) {
			LevelUp();			
		}
	}

	void LevelUp() {
		currentExp -= toLevelUp[currentLevel];
		currentLevel++;

		playerHealth.maxHealth = currentHP;
		playerHealth.currentHealth += currentHP - hpLevels[currentLevel - 1];

		currentHP = hpLevels[currentLevel];
		currentAttack = attackLevels[currentLevel];
		currentDefense = defenseLevels[currentLevel];
	}

	public void AddExperience(int expToAdd) {
		currentExp += expToAdd;
	}
}
