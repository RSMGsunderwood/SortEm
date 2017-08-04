using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopUpController : MonoBehaviour {

	public Image popUpDark;

	public void EnablePopUp(){
		gameObject.SetActive (true);
		popUpDark.enabled = true;
		StartCoroutine ("PopUpRoutine");
	}

	public void DisablePopUp(){
		StopCoroutine ("PopUpRoutine");
		StartCoroutine ("FadePopUpOut");
	}

	IEnumerator PopUpRoutine(){
		Color fade = Color.black;
		float scaling = 0;
		RectTransform rect = GetComponent<RectTransform> ();
		for(float i=0;i<.25f;i+=Time.deltaTime){
			fade.a = Mathf.SmoothStep (0,.5f,i/.25f);
			scaling = Mathf.SmoothStep(0,1,i/.25f);
			popUpDark.color = fade;
			rect.localScale = new Vector3(scaling,scaling,scaling);
			yield return null;
		}
		fade = Color.black;
		fade.a = .5f;
		popUpDark.color = fade;
		rect.localScale = Vector3.one;
		Time.timeScale = 0;
	}

	IEnumerator FadePopUpOut(){
		Color fade = Color.black;
		float scaling = 0;
		RectTransform rect = GetComponent<RectTransform> ();
		for(float i=.25f;i>0;i-=Time.deltaTime){
			fade.a = Mathf.SmoothStep (0,.5f,i/.25f);
			scaling = Mathf.SmoothStep(0,1,i/.25f);
			popUpDark.color = fade;
			rect.localScale = new Vector3(scaling,scaling,scaling);
			yield return null;
		}
		fade = Color.black;
		fade.a = 0;
		popUpDark.color = fade;
		rect.localScale = Vector3.zero;
		popUpDark.enabled = false;
		gameObject.SetActive (false);
		GameHandler.Instance.closePopUp.SetActive (false);
	}

}
