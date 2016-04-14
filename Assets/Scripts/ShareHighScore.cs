using UnityEngine;
using System.Collections;

public class ShareHighScore : MonoBehaviour {

	public GameObject facebookSharePanel;

	// Use this for initialization
	void Start () {
		if (Score.isNewHighScore) {
			facebookSharePanel.SetActive (true);
			Score.isNewHighScore = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
