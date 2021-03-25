using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour {

	[SerializeField]
	TMP_Text scoreText;
	int scoreValue = 0;
	//bool doublePickUp;

	void Start() {
		//doublePickUp = false;
		UpdateScoreText();
	}

	void Update() {
	}

	public void AddPoints(int scorePoints) {
		scoreValue += scorePoints;
		UpdateScoreText();
		AudioManager.instance.Play("GetPoints");
	}

	void UpdateScoreText() {
		scoreText.text = scoreValue.ToString();
	}
}
