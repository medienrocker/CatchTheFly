using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
	public GameObject dialogueBox;
	public Text dialogueText;
	public Text dialogueName;

	public bool isDialogueActive;

	void Start() {
		
	}

	void Update() {
		if (isDialogueActive && Input.GetKeyDown(KeyCode.Space)) {
			Disable();
		}
	}

	public void Disable() {
		dialogueBox.SetActive(false);
		isDialogueActive = false;
	}

	public void ShowDialogueBox(string _dialogue, string _name) {
		isDialogueActive = true;
		dialogueBox.SetActive(true);
		dialogueText.text = _dialogue;
		dialogueName.text = _name;
	}

		//public void ShowDialogueBox(string dialogue, string name) {
		//	isDialogueActive = true;
		//	dialogueBox.SetActive(true);
		//	dialogueText.text = dialogue;
		//	dialogueName.text = name;
		//}
	}
