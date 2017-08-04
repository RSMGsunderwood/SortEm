using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

	public Image star1, star2, star3;
	public Text levelText;
	public Color goldStar;
	public int levelNum;

	public void SetupSelector(int level){
		levelNum = level;
		if(PlayerPrefs.HasKey (level+"star1")){
			star1.color = goldStar;
		}
		if(PlayerPrefs.HasKey (level+"star2")){
			star2.color = goldStar;
		}
		if(PlayerPrefs.HasKey (level+"star3")){
			star3.color = goldStar;
		}
		levelText.text = "Level " + level.ToString ();
	}

	public void Selected(){
		GameHandler.Instance.LevelSelected (levelNum);
	}
}