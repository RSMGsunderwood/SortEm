using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Handles object dragging within game
/// </summary>
public class DraggableObject : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler {
	
	RectTransform m_transform = null;
	Image dragImage;
	public int dropNum = 0;
	public int spawnPoint;
	public bool grabbed = false;
	public bool canBeGrabbed = true;
	Color dColor;

	void Start () {
		m_transform = GetComponent<RectTransform>();
		dragImage = GetComponent<Image> ();
		dColor = dragImage.color;
	}

	//When an object is picked up, it will change status
	public void OnBeginDrag(PointerEventData eventData){
		if(canBeGrabbed)
			grabbed = true;
	}

	//Moves object around!
	public void OnDrag(PointerEventData eventData)
	{
		if (canBeGrabbed) {
			Vector3 move = new Vector3 (0, 0, 0);

			//Adjusts x data if within bounds
			if (m_transform.position.x + eventData.delta.x > (0 + (m_transform.sizeDelta.x / 2)) 
				&& m_transform.position.x + eventData.delta.x < (Screen.width - (m_transform.sizeDelta.x / 2))) {
				move.x = eventData.delta.x;
			}

			//Adjusts y data if within bounds
			if (m_transform.position.y + eventData.delta.y > (0 + (m_transform.sizeDelta.y / 2)) 
				&& m_transform.position.y + eventData.delta.y + 50 < (Screen.height - (m_transform.sizeDelta.y / 2))) {
				move.y = eventData.delta.y;
			}

			m_transform.position += move;
		}
	}

	//Ending drag will result in the object being disabled
	public void OnEndDrag(PointerEventData eventData){
		if (canBeGrabbed) {
			bool pass = false;
			foreach (DropArea drp in GameHandler.Instance.dropAreas) {
				if (drp.dropNum == dropNum) {
					RectTransform cage = drp.GetComponent<RectTransform> ();
					if (m_transform.anchoredPosition.x + 25 < cage.anchoredPosition.x + (cage.sizeDelta.x / 2)
						&& m_transform.anchoredPosition.x + 25 > cage.anchoredPosition.x - (cage.sizeDelta.x / 2)
						&& m_transform.anchoredPosition.y + 25 < cage.anchoredPosition.y + (cage.sizeDelta.y / 2)
						&& m_transform.anchoredPosition.y - 25 > cage.anchoredPosition.y - (cage.sizeDelta.y / 2)) {
						pass = true;
					} else {
						pass = false;
					}
				}
			}
			if (pass) {
				StartCoroutine ("GoodAnimation");
				if (GameHandler.Instance.boss1) {
					if (dropNum == 4) {
						GameHandler.Instance.StartCoroutine ("DamageBoss");
					} else {
						GameHandler.Instance.xBoxCounter++;
					}
				}
				GameHandler.Instance.comboInARow++;
			} else {
				StartCoroutine ("BadAnimation");
				GameHandler.Instance.comboInARow = 0;
			}
			GameHandler.Instance.comboInARow = Mathf.Clamp (GameHandler.Instance.comboInARow, 0, 14);
			if (GameHandler.Instance.comboInARow > 0) {
				GameHandler.Instance.ShowComboMsg (m_transform.anchoredPosition, true);
			} else {
				GameHandler.Instance.ShowComboMsg (m_transform.anchoredPosition, false);
			}
			GameHandler.Instance.AddScore ((int)Mathf.Round (100 * ((Mathf.Clamp (GameHandler.Instance.comboInARow * .3f, 0, 100)))));
			grabbed = false;
			canBeGrabbed = false;
		}
	}

	IEnumerator GoodAnimation(){
		Color tweenToColor = dColor;
		Vector3 rotationVec = Vector3.zero;
		Vector3 scaleVec = Vector3.one;
		int rndDir = Random.Range (0, 2);
		if (rndDir == 0)
			rndDir=-1;
		for (float i=0; i<.5f; i+=Time.deltaTime) {
			tweenToColor = Color.Lerp(dColor, Color.white,i/.5f);
			tweenToColor.a = Mathf.Lerp (1,0,i/.5f);
			rotationVec.z = Mathf.Lerp (0, 90*rndDir, i/.5f);
			scaleVec = Vector3.Lerp (Vector3.one, Vector3.zero, i/.5f);
			m_transform.eulerAngles = rotationVec;
			m_transform.localScale = scaleVec;
			dragImage.color = tweenToColor;
			yield return null;
		}
		SendDraggableBack ();
	}

	IEnumerator BadAnimation(){
		Color tweenToColor = dColor;
		Vector3 rotationVec = Vector3.zero;
		Vector3 scaleVec = Vector3.one;
		int rndDir = Random.Range (0, 2);
		if (rndDir == 0)
			rndDir=-1;
		for (float i=0; i<.5f; i+=Time.deltaTime) {
			tweenToColor = Color.Lerp(dColor, Color.red,i/.5f);
			tweenToColor.a = Mathf.Lerp (1,0,i/.5f);
			rotationVec.z = Mathf.Lerp (0, 90*rndDir, i/.5f);
			scaleVec = Vector3.Lerp (Vector3.one, Vector3.zero, i/.5f);
			m_transform.eulerAngles = rotationVec;
			m_transform.localScale = scaleVec;
			dragImage.color = tweenToColor;
			yield return null;
		}
		SendDraggableBack ();
	}

	public void SendDraggableBack(bool showResults = true){
		StopCoroutine ("GoodAnimation");
		StopCoroutine ("BadAnimation");
		GameHandler.Instance.spawnBool [spawnPoint] = false;
		gameObject.SetActive (false);
		GameHandler.Instance.possibleSpawns.Add (spawnPoint);
		GameHandler.Instance.boxes.Push (this.gameObject);
		GameHandler.Instance.boxesInPlay.Remove (this.gameObject);
		m_transform.eulerAngles = Vector3.zero;
		m_transform.localScale = Vector3.one;
		dragImage.color = dColor;
		canBeGrabbed = true;
		GameHandler.Instance.boxesDone++;
		if (GameHandler.Instance.boxesDone >= GameHandler.Instance.levelBoxes [GameHandler.Instance.currentLevel - 1] && showResults) {
			GameHandler.Instance.LastBox();
		}
	}
}