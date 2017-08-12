using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroAnimation : MonoBehaviour {

	public IntroMoveOb squareOb;
	public IntroMoveOb triangleOb;
	public RectTransform squareSpawn;
	public RectTransform triangleSpawn;
	public RectTransform squareMove;
	public RectTransform triangleMove;

	void Awake(){
		squareOb.introScript = this;
		triangleOb.introScript = this;
		StartCoroutine ("SpawnSquare");
		StartCoroutine ("SpawnTriangle");
	}

	public void Reset(bool square){
		if (square) {
			StartCoroutine ("SpawnSquare");
		} else {
			StartCoroutine ("SpawnTriangle");
		}
	}

	IEnumerator SpawnSquare(){
		yield return new WaitForSeconds (Random.Range (1, 3f));
		Vector2 randomSpawn = new Vector2 (Random.Range (squareSpawn.anchoredPosition.x - 15f, squareSpawn.anchoredPosition.x + 15f), Random.Range (squareSpawn.anchoredPosition.y - 15f, squareSpawn.anchoredPosition.y + 15f));
		Vector2 randomMove = new Vector2 (Random.Range (squareMove.anchoredPosition.x - 15f, squareMove.anchoredPosition.x + 15f), Random.Range (squareMove.anchoredPosition.y - 15f, squareMove.anchoredPosition.y + 15f));
		squareOb.StartMove (randomSpawn, randomMove);
	}

	IEnumerator SpawnTriangle(){
		yield return new WaitForSeconds (Random.Range (1, 3f));
		Vector2 randomSpawn = new Vector2 (Random.Range (triangleSpawn.anchoredPosition.x - 15f, triangleSpawn.anchoredPosition.x + 15f), Random.Range (triangleSpawn.anchoredPosition.y - 15f, triangleSpawn.anchoredPosition.y + 15f));
		Vector2 randomMove = new Vector2 (Random.Range (triangleMove.anchoredPosition.x - 15f, triangleMove.anchoredPosition.x + 15f), Random.Range (triangleMove.anchoredPosition.y - 15f, triangleMove.anchoredPosition.y + 15f));
		triangleOb.StartMove (randomSpawn, randomMove);
	}

}