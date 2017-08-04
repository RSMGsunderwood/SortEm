using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DropArea : MonoBehaviour {
	public int dropNum = 0;
	public Image dropImage;

	public void SetupDropArea(int num, Sprite img){
		dropNum = num;
		dropImage.sprite = img;
	}
}