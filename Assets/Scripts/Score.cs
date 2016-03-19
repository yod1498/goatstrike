using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	static public int highScore = 0;
	static public int currentScore = 0;

	public Text scoreTxt;
	public Text highScoreTxt;

	void Awake() {
		// If the GoatStrikeHighScore already exists, read it 
		if (PlayerPrefs.HasKey ("GoatStrikeHighScore")) {
			highScore = PlayerPrefs.GetInt ("GoatStrikeHighScore"); 
		}
		// Assign the high score to GoatStrikeHighScore 
		PlayerPrefs.SetInt ("GoatStrikeHighScore", highScore);
	}


	void Update () {
		highScoreTxt.text = "Best: "+highScore;
		scoreTxt.text = "Score: "+currentScore;

		if (currentScore > highScore) {
			highScore = currentScore;
		}

		// Update GoatStrikeHighScore in PlayerPrefs if necessary 
		if (highScore > PlayerPrefs.GetInt ("GoatStrikeHighScore")) {
			PlayerPrefs.SetInt ("GoatStrikeHighScore", highScore);
		}
	}

	public static void PassLevel (int level){
		currentScore = currentScore + (level * 10);
	}

	public static void ResetScore (){
		currentScore = 0;
	}
}
