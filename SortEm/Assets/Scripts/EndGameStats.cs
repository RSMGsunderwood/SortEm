using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameStats : MonoBehaviour {

	[Header("Star Reference")]
	public RectTransform star1;
	public RectTransform star2;
	public RectTransform star3;
	public Image starImage1;
	public Image starImage2;
	public Image starImage3;
	public Color goldStar;
	[Header("Percentage Reference")]
	public RectTransform percentStart;
	public RectTransform percentEnd;
	[Header("UI Elements")]
	public Slider pointSlider;
	public Text score;
	public Text buttonText;
	[HideInInspector] public bool star1Achieved = false;
	[HideInInspector] public bool star2Achieved = false;
	[HideInInspector] public bool star3Achieved = false;
	bool star1Trigger = false;
	bool star2Trigger = false;
	bool star3Trigger = false;
	bool statsAnimating;

	public void SetupEndStats(bool one, bool two, bool three){
		buttonText.text = "Skip";
		star1Trigger = star2Trigger = star3Trigger = false;
		star1Achieved = one;
		star2Achieved = two;
		star3Achieved = three;
		statsAnimating = true;
		score.text = "0";
		Vector2 lerpVec = new Vector2 (0, 0);
		float max = GameHandler.Instance.levelstar3 [GameHandler.Instance.currentLevel - 1]*1.3f;
		float ratio = GameHandler.Instance.levelstar1 [GameHandler.Instance.currentLevel - 1] / max;
		lerpVec.x = Mathf.Lerp (percentStart.anchoredPosition.x, percentEnd.anchoredPosition.x, ratio);
		star1.anchoredPosition = lerpVec;
		ratio = GameHandler.Instance.levelstar2 [GameHandler.Instance.currentLevel - 1] / max;
		lerpVec.x = Mathf.Lerp (percentStart.anchoredPosition.x, percentEnd.anchoredPosition.x, ratio);
		star2.anchoredPosition = lerpVec;
		ratio = GameHandler.Instance.levelstar3 [GameHandler.Instance.currentLevel - 1] / max;
		lerpVec.x = Mathf.Lerp (percentStart.anchoredPosition.x, percentEnd.anchoredPosition.x, ratio);
		star3.anchoredPosition = lerpVec;
		gameObject.SetActive (true);
		StartCoroutine("ShowStatsRoutine");
	}

	public void CloseStats(){
		gameObject.SetActive (false);
		GameHandler.Instance.FinishGame ();
	}

	public void ContinuePressed(){
		if (statsAnimating) {
			StopStatsRoutine ();
		} else {
			CloseStats();
		}
	}

	public void StopStatsRoutine(){
		StopCoroutine ("ShowStatsRoutine");
		StopCoroutine ("GrowStar");
		if (star1Achieved && !star1Trigger) {
			if(!PlayerPrefs.HasKey (GameHandler.Instance.currentLevel+"star1")){
				PlayerPrefs.SetInt (GameHandler.Instance.currentLevel+"star1",1);
				GameHandler.Instance.AddCoins(2);
			}
			star1.localScale = new Vector3 (1.3f, 1.3f, 1.3f);
			starImage1.color = goldStar;
		}
		if (star2Achieved && !star2Trigger) {
			if(!PlayerPrefs.HasKey (GameHandler.Instance.currentLevel+"star2")){
				PlayerPrefs.SetInt (GameHandler.Instance.currentLevel+"star2",1);
				GameHandler.Instance.AddCoins(3);
			}
			star2.localScale = new Vector3 (1.3f, 1.3f, 1.3f);
			starImage2.color = goldStar;
		}
		if (star3Achieved && !star3Trigger) {
			if(!PlayerPrefs.HasKey (GameHandler.Instance.currentLevel+"star3")){
				PlayerPrefs.SetInt (GameHandler.Instance.currentLevel+"star3",1);
				GameHandler.Instance.AddCoins(5);
			}
			star3.localScale = new Vector3 (1.3f, 1.3f, 1.3f);
			starImage3.color = goldStar;
		}
		score.text = GameHandler.Instance.score.ToString();
		pointSlider.value = Mathf.Lerp (0,1,GameHandler.Instance.score/(GameHandler.Instance.levelstar3 [GameHandler.Instance.currentLevel - 1]*1.3f));
		statsAnimating = false;
		buttonText.text = "Continue";
	}

	IEnumerator ShowStatsRoutine(){
		float lerpScore = 0;
		for (float i = 0; i < 5f; i += Time.deltaTime) {
			if(star1Achieved && !star1Trigger && Mathf.Floor(lerpScore) >= GameHandler.Instance.levelstar1 [GameHandler.Instance.currentLevel - 1]){
				if(!PlayerPrefs.HasKey (GameHandler.Instance.currentLevel+"star1")){
					PlayerPrefs.SetInt (GameHandler.Instance.currentLevel+"star1",1);
					GameHandler.Instance.AddCoins(2);
				}
				score.text = GameHandler.Instance.levelstar1 [GameHandler.Instance.currentLevel - 1].ToString ();
				star1Trigger=true;
				yield return StartCoroutine(GrowStar(star1, starImage1));
			}
			if(star2Achieved && !star2Trigger &&  Mathf.Floor(lerpScore) >= GameHandler.Instance.levelstar2 [GameHandler.Instance.currentLevel - 1]){
				if(!PlayerPrefs.HasKey (GameHandler.Instance.currentLevel+"star2")){
					PlayerPrefs.SetInt (GameHandler.Instance.currentLevel+"star2",1);
					GameHandler.Instance.AddCoins(3);
				}
				score.text = GameHandler.Instance.levelstar2 [GameHandler.Instance.currentLevel - 1].ToString ();
				star2Trigger=true;
				yield return StartCoroutine(GrowStar(star2, starImage2));
			}
			if(star3Achieved && !star3Trigger &&  Mathf.Floor(lerpScore) >= GameHandler.Instance.levelstar3 [GameHandler.Instance.currentLevel - 1]){
				if(!PlayerPrefs.HasKey (GameHandler.Instance.currentLevel+"star3")){
					PlayerPrefs.SetInt (GameHandler.Instance.currentLevel+"star3",1);
					GameHandler.Instance.AddCoins(5);
				}
				score.text = GameHandler.Instance.levelstar3 [GameHandler.Instance.currentLevel - 1].ToString ();
				star3Trigger=true;
				yield return StartCoroutine(GrowStar(star3, starImage3));
			}
			lerpScore = Mathf.SmoothStep (0,GameHandler.Instance.score,i/5f);
			pointSlider.value = Mathf.Lerp (0,1,lerpScore/(GameHandler.Instance.levelstar3 [GameHandler.Instance.currentLevel - 1]*1.3f));
			score.text = lerpScore.ToString ("F0");
			yield return null;
		}
		score.text = GameHandler.Instance.score.ToString();
		statsAnimating = false;
		buttonText.text = "Continue";
	}

	IEnumerator GrowStar(RectTransform star, Image starImage){
		float lerp;
		for(float i=0;i<.3f;i+=Time.deltaTime){
			lerp = Mathf.SmoothStep(1,1.3f,i/.3f);
			star.localScale = new Vector3(lerp,lerp,lerp);
			yield return null;
		}
		star.localScale = new Vector3 (1.3f, 1.3f, 1.3f);
		starImage.color = goldStar;
		yield return new WaitForSeconds (.5f);
	}

}