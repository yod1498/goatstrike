﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;


public class Score : MonoBehaviour {
	static public int highScore = 0;
	static public int currentScore = 0;
	static public bool isNewHighScore = false;

	public Text scoreTxt;
	public Text highScoreTxt;

	void Awake() {
		isNewHighScore = false;

		// If the GoatStrikeHighScore already exists, read it 
		if (PlayerPrefs.HasKey ("GoatStrikeHighScore")) {
			highScore = PlayerPrefs.GetInt ("GoatStrikeHighScore"); 
		}
		// Assign the high score to GoatStrikeHighScore 
		PlayerPrefs.SetInt ("GoatStrikeHighScore", highScore);
	}


	void Update () {
		highScoreTxt.text = "BEST LV: "+highScore;
		scoreTxt.text = "LEVEL: "+currentScore;

		if (currentScore > highScore) {
			highScore = currentScore;
			isNewHighScore = true;
		}

		// Update GoatStrikeHighScore in PlayerPrefs if necessary 
		if (highScore > PlayerPrefs.GetInt ("GoatStrikeHighScore")) {
			PlayerPrefs.SetInt ("GoatStrikeHighScore", highScore);
			Analytics.CustomEvent("HighScore", new Dictionary<string, object>
				{
					{ "highScore", highScore },
					{ "level", BattleController.CurrentLevel }
				});
		}
	}

	public static void PassLevel (int level){
		//currentScore = currentScore + (level * 10);
		currentScore = level;

		#if UNITY_IOS 
		LeaderboardManager.ReportScore(currentScore,Leaderboard.leaderBoardID);
		#endif
	}

	public static void ResetScore (){
		currentScore = 0;
	}
}
