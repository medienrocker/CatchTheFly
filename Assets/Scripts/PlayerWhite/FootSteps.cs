using System;
using UnityEngine;
using UnityEngine.Audio;

public class FootSteps : MonoBehaviour {

	[SerializeField] AudioClip[] clips;
	AudioSource audioSource;

	void Awake() {
		audioSource = GetComponent<AudioSource>();
	}

	void Step() {
		AudioClip clip = GetRandomClip();
		audioSource.PlayOneShot(clip);
	}

	AudioClip GetRandomClip() {
		return clips[UnityEngine.Random.Range(0, clips.Length)];
	}
}
