using UnityEngine;
using System.Collections;

public class ShareHighScore : MonoBehaviour {

	public GameObject facebookSharePanel;
	public GameObject facebookShareLevelUpPanel;

	// current level that already shared on Facebook 
	public int currentLevelShared = 0;

	void Awake() {
		// If the GoatStrikeHighLevel already exists, read it 
		if (PlayerPrefs.HasKey ("GoatStrikeHighLevel")) {
			currentLevelShared = PlayerPrefs.GetInt ("GoatStrikeHighLevel"); 
		}
		// Assign the currentLevelShared to GoatStrikeHighLevel 
		PlayerPrefs.SetInt ("GoatStrikeHighLevel", currentLevelShared);
	}

	// Use this for initialization
	void Start () {
		// if level 10,20,30... show level up share
		if ((BattleController.CurrentLevel % 10 == 0) && (currentLevelShared < BattleController.CurrentLevel)){
			currentLevelShared = BattleController.CurrentLevel;
			PlayerPrefs.SetInt ("GoatStrikeHighLevel", currentLevelShared);
			facebookShareLevelUpPanel.SetActive (true);
		} else {
			// if not level up and high score, show high score share
			if (Score.isNewHighScore) {
				facebookSharePanel.SetActive (true);
				Score.isNewHighScore = false;
			}
		}
	}
}
