using UnityEngine;
using UnityEngine.UI;

public class MessageHolder : MonoBehaviour {

	DialogueManager dialogManager;

	public GameObject messageBox;
	public Text messageText;
	public string message;
	public bool hasMessage;

	void Start() {
		dialogManager = FindObjectOfType<DialogueManager>();
	}

	void Update() {

	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.name == "Player" && hasMessage && dialogManager.isDialogueActive == false) {
			ShowMessage();
		}
		else {
			HideMessage();
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		HideMessage();
	}

	public void ShowMessage() {
		messageBox.SetActive(true);
		messageText.text = message;
	}

	public void HideMessage() {
		messageBox.SetActive(false);
	}
}
