using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
[ExecuteInEditMode]
public class ClearPlayerPrefs : MonoBehaviour {

	public bool clearPrefs;

	#if UNITY_EDITOR
	void Update () {
		if (clearPrefs) {
			PlayerPrefs.DeleteAll ();
			clearPrefs = false;
		}
	}
	#endif
}
