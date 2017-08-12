using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroMoveOb : MonoBehaviour {

	RectTransform obRect;
	Image obImage;
	Vector2 startTran = Vector2.zero;
	Vector2 endTran = Vector2.zero;
	[HideInInspector] public IntroAnimation introScript;

	void Awake(){
		obRect = this.GetComponent<RectTransform> ();
		obImage = this.GetComponent<Image> ();
		gameObject.SetActive (false);
	}

	public void StartMove(Vector2 start, Vector2 moveTo){
		startTran = start;
		endTran = moveTo;
		obRect.anchoredPosition = startTran;
		gameObject.SetActive (true);
		StartCoroutine ("MoveRoutine");
	}

	IEnumerator MoveRoutine(){
		Vector2 lerp = Vector2.zero;
		Color tempC = obImage.color;
		float flerp = 0;
		for (float i = 0; i < 10f; i += Time.deltaTime) {
			if (i < 2) {
				tempC.a = Mathf.SmoothStep (0, 1, i / 2f);
				obImage.color = tempC;
			}
			lerp = Vector2.Lerp (startTran, endTran, i/10f);
			flerp = i * 50f;
			obRect.anchoredPosition = lerp;
			obRect.localEulerAngles = new Vector3 (0, 0, flerp);
			yield return null;
		}
		tempC.a = 1;
		obImage.color = tempC;
		obRect.anchoredPosition = endTran;
		flerp = 1;
		for(float i = 0; i < 1f; i += Time.deltaTime) {
			flerp = Mathf.SmoothStep (0, 1, (1 - i) / 1f);
			obRect.localScale = new Vector3 (flerp, flerp, flerp);
			yield return null;
		}
		gameObject.SetActive (false);
		obRect.localScale = Vector3.one;
		introScript.Reset (this == introScript.squareOb);
	}
}