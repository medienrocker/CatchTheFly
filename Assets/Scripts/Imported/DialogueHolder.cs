using UnityEngine;
using UnityEngine.UI;

public class DialogueHolder : MonoBehaviour {

	public string dialogue;

	DialogueManager dialogueManager;
	MessageHolder messageHolder;

	void Start() {
		dialogueManager = FindObjectOfType<DialogueManager>();
		messageHolder = FindObjectOfType<MessageHolder>();
	}

	void Update() {

	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.name == "Player" && messageHolder.hasMessage == true) {
			if (Input.GetKeyUp(KeyCode.Space)) {
				dialogueManager.ShowDialogueBox(dialogue, transform.parent.name);
			}
		}
	}
}
