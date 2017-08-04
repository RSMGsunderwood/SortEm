using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameStats : MonoBehaviour {

	[Header("Star Reference")]
	public RectTransform star1;
	public RectTransform star2;
	public RectTransform star3;
	[Header("Percentage Reference")]
	public RectTransform percentStart;
	public RectTransform percentEnd;
	[Header("UI Elements")]
	public Slider pointSlider;
	public Text score;
	[HideInInspector] public bool star1Achieved = false;
	[HideInInspector] public bool star2Achieved = false;
	[HideInInspector] public bool star3Achieved = false;
	bool star1Trigger = false;
	bool star2Trigger = false;
	bool star3Trigger = false;
	float star1Percent=0, star2Percent=0, star3Percent=0;

	public void SetupEndStats(bool one, bool two, bool three){
		star1Achieved = one;
		star2Achieved = two;
		star3Achieved = three;
		score.text = "0";
		Vector2 lerpVec = new Vector2 (0, 0);
		float max = GameHandler.Instance.levelstar3 [GameHandler.Instance.currentLevel - 1]*1.3f;
		float ratio = GameHandler.Instance.levelstar1 [GameHandler.Instance.currentLevel - 1] / max;
		star1Percent = ratio;
		lerpVec.x = Mathf.Lerp (percentStart.anchoredPosition.x, percentEnd.anchoredPosition.x, ratio);
		star1.anchoredPosition = lerpVec;
		ratio = GameHandler.Instance.levelstar2 [GameHandler.Instance.currentLevel - 1] / max;
		star2Percent = ratio;
		lerpVec.x = Mathf.Lerp (percentStart.anchoredPosition.x, percentEnd.anchoredPosition.x, ratio);
		star2.anchoredPosition = lerpVec;
		ratio = GameHandler.Instance.levelstar3 [GameHandler.Instance.currentLevel - 1] / max;
		star3Percent = ratio;
		lerpVec.x = Mathf.Lerp (percentStart.anchoredPosition.x, percentEnd.anchoredPosition.x, ratio);
		star3.anchoredPosition = lerpVec;
		gameObject.SetActive (true);
		StartCoroutine("ShowStatsRoutine");
	}

	IEnumerator ShowStatsRoutine(){
		float lerpScore = 0;
		float mod = Mathf.Clamp ((GameHandler.Instance.levelstar3 [GameHandler.Instance.currentLevel - 1] - (GameHandler.Instance.score / 1000)), 0, Mathf.Infinity);
		for(float i=0;i<8f+mod;i+=Time.deltaTime){
			float tempLerp = ((float)GameHandler.Instance.score*(i/5f))/(float)GameHandler.Instance.levelstar3 [GameHandler.Instance.currentLevel - 1];
			if(star1Achieved&&!star1Trigger&&pointSlider.value>=star1Percent){
				if(!PlayerPrefs.HasKey (GameHandler.Instance.currentLevel+"star1")){
					PlayerPrefs.SetInt (GameHandler.Instance.currentLevel+"star1",1);
					GameHandler.Instance.AddCoins(2);
				}
				star1Trigger=true;
				pointSlider.value = star1Percent;
				yield return StartCoroutine(GrowStar(star1));
			}
			if(star2Achieved&&!star2Trigger&&pointSlider.value>=star2Percent){
				if(!PlayerPrefs.HasKey (GameHandler.Instance+"star2")){
					PlayerPrefs.SetInt (GameHandler.Instance+"star2",1);
					GameHandler.Instance.AddCoins(3);
				}
				star2Trigger=true;
				pointSlider.value = star2Percent;
				yield return StartCoroutine(GrowStar(star2));
			}
			if(star3Achieved&&!star3Trigger&&pointSlider.value>=star3Percent){
				if(!PlayerPrefs.HasKey (GameHandler.Instance+"star3")){
					PlayerPrefs.SetInt (GameHandler.Instance+"star3",1);
					GameHandler.Instance.AddCoins(5);
				}
				star3Trigger=true;
				pointSlider.value = star3Percent;
				yield return StartCoroutine(GrowStar(star3));
			}
			pointSlider.value = Mathf.SmoothStep (0,1,tempLerp);
			lerpScore = Mathf.SmoothStep (0,GameHandler.Instance.score,i/5f);
			score.text = lerpScore.ToString ("F0");
			yield return null;
		}
	}

	IEnumerator GrowStar(RectTransform star){
		float lerp;
		for(float i=0;i<.5f;i+=Time.deltaTime){
			lerp = Mathf.SmoothStep(1,1.3f,i/.5f);
			star.localScale = new Vector3(lerp,lerp,lerp);
			yield return null;
		}
		yield return new WaitForSeconds (1f);
	}

}