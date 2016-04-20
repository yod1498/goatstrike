using UnityEngine;
using System.Collections;

public class ShareHighScore : MonoBehaviour {

	// if level 10,20,30... share level up on FB	
	public const int LEVEL_SHARE_ACHIEVEMENT = 10;
	// if level > 10 share high score on FB	
	public const int LEVEL_SHARE_HIGH_SCORE = 10;

	public GameObject facebookSharePanel;
	public GameObject facebookShareLevelUpPanel;
	//public static bool isReadytoShareAchievement = false;

	// current level that already shared on Facebook 
	public int currentLevelShared = 0;

	private int isReadytoShareAchievement = 0; //0=false, 1=true

	void Awake() {
		//isReadytoShareAchievement = 0;

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
		isReadytoShareAchievement = PlayerPrefs.GetInt ("IsReadytoShareAchievement"); 

		if ((isReadytoShareAchievement==1) && (currentLevelShared < BattleController.CurrentLevel)){
			currentLevelShared = BattleController.CurrentLevel;
			PlayerPrefs.SetInt ("GoatStrikeHighLevel", currentLevelShared);

			isReadytoShareAchievement = 0;
			PlayerPrefs.SetInt ("IsReadytoShareAchievement", isReadytoShareAchievement);
			facebookShareLevelUpPanel.SetActive (true);
		} else {
			// if not level up and high score, show high score share
			if ((Score.isNewHighScore) && (BattleController.CurrentLevel > LEVEL_SHARE_HIGH_SCORE)) {
				facebookSharePanel.SetActive (true);
				Score.isNewHighScore = false;
			}
		}
	}
}
