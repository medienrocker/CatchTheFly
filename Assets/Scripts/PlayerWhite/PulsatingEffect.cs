using UnityEngine;

public class PulsatingEffect : MonoBehaviour {

	[Header("Pulse Variables")]
	public AnimationCurve expandCurve;
	public float expandAmount, expandSpeed;

	Vector3 startSize;
	Vector3 targetSize;
	float scrollAmount;
	
	void Start() {
		InitPulseEffectVariables();
	}

	void InitPulseEffectVariables() {
		startSize = transform.localScale;
		targetSize = startSize * expandAmount;
	}

	void Update() {
		MakeObjectPulse();
	}

	public void MakeObjectPulse() {
		scrollAmount += Time.deltaTime * expandSpeed;
		float _percent = expandCurve.Evaluate(scrollAmount);
		transform.localScale = Vector2.Lerp(startSize, targetSize, _percent);
	}
}
