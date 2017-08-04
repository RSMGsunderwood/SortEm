using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ComboShow : MonoBehaviour {

	RectTransform m_rectTrasnsform;
	Text m_text;
	Vector2 cachePos;
	Color useColor;

	void Awake(){
		m_rectTrasnsform = GetComponent<RectTransform> ();
		m_text = GetComponent<Text> ();
	}

	public void ShowComboText(bool goodCombo){
		cachePos = m_rectTrasnsform.anchoredPosition;
		useColor.a = 1;
		m_text.text = "";
		if (goodCombo) {
			useColor = Color.green;
			for(int i=0;i<GameHandler.Instance.comboInARow;i++){
				m_text.text += "+";
			}
		} else {
			m_text.text = "MISS";
			useColor = Color.red;
		}
		StartCoroutine ("ShowComboRoutine");
	}

	IEnumerator ShowComboRoutine(){
		float lerp;
		for (float i=.4f; i>0; i-=Time.deltaTime) {
			lerp = Mathf.Lerp (cachePos.y, cachePos.y-50,i/.4f);
			useColor.a = Mathf.Lerp (0,1,i/.4f);
			m_rectTrasnsform.anchoredPosition = new Vector2(cachePos.x,lerp);
			m_text.color = useColor;
			yield return null;
		}
		gameObject.SetActive (false);
		GameHandler.Instance.comboShows.Push (this.gameObject);
	}

}
