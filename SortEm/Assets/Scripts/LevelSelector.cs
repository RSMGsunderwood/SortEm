using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class LevelSelector : MonoBehaviour {

	public Image star1, star2, star3;
	public Sprite coinSprite;
	public TextMeshProUGUI levelText;
	public int levelNum;
	bool isWorld = false;

	public void SetupSelector(int level, int displayNum, bool world){
		levelNum = level;
		isWorld = world;
		if (world) {
			levelText.text = "World " + displayNum.ToString ();
		} else {
			if (PlayerPrefs.HasKey (level + "star1")) {
				star1.sprite = coinSprite;
			}
			if (PlayerPrefs.HasKey (level + "star2")) {
				star2.sprite = coinSprite;
			}
			if (PlayerPrefs.HasKey (level + "star3")) {
				star3.sprite = coinSprite;
			}
			levelText.text = "Level " + displayNum.ToString ();
		}
	}

	public void Selected(){
		if (isWorld) {
			if (!GameHandler.Instance.choosingLevels) {
				GameHandler.Instance.SetupLevelSelectors (levelNum);
			}
		} else {
			if (GameHandler.Instance.choosingLevels) {
				GameHandler.Instance.LevelSelected (levelNum);
			}
		}
	}
}