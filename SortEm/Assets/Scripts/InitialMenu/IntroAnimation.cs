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
		StartCoroutine ("SpawnSquare");
		StartCoroutine ("SpawnTriangle");
	}

	IEnumerator SpawnSquare(){
		yield return new WaitForSeconds (Random.Range (1, 3f));
	}

	IEnumerator SpawnTriangle(){
		yield return new WaitForSeconds (Random.Range (1, 3f));
	}

}