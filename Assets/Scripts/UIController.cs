using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

	public string levelName = "";
	public GameObject adsPanel;
	public GameObject facebookPanel;

	private enum InviteFriendStatus {notyet, shared, cancelled};
	int userInviteFriendStatus = (int) InviteFriendStatus.notyet;

	void Awake() {
		//PlayerPrefs.DeleteAll ();
		// If the GoatStrikeInviteFriendStatus already exists, read it 
		if (PlayerPrefs.HasKey ("GoatStrikeInviteFriendStatus")) {
			userInviteFriendStatus = PlayerPrefs.GetInt ("GoatStrikeInviteFriendStatus"); 
		}
		// Assign the userInviteFriendStatus to GoatStrikeInviteFriendStatus 
		PlayerPrefs.SetInt ("GoatStrikeInviteFriendStatus", userInviteFriendStatus);
	}

	public void LoadMenuScene () {
		BattleController.levelToLoadFromDeath = 0;
		Life.ResetLife ();
		PlayerPrefs.SetInt ("GoatStrikeInviteFriendStatus", (int) InviteFriendStatus.notyet);
		SceneManager.LoadScene(levelName);
	}

	public void LoadCurrentLevel(){
		if (Life.DeCreaseLife (1) >= 0) {
			BattleController.levelToLoadFromDeath = BattleController.CurrentLevel;
			SceneManager.LoadScene (levelName);
		} else {
			userInviteFriendStatus = PlayerPrefs.GetInt ("GoatStrikeInviteFriendStatus"); 

			//invite friend first. after that show ads
			switch (userInviteFriendStatus) {
			case (int) InviteFriendStatus.notyet:
				facebookPanel.SetActive (true);
				break;
			case (int)InviteFriendStatus.shared:
				adsPanel.SetActive (true);
				break;
			case (int) InviteFriendStatus.cancelled:
				adsPanel.SetActive (true);
				break;
			default:
				adsPanel.SetActive (true);
				break;
			}
			//adsPanel.SetActive (true);
			//facebookPanel.SetActive (true);
		}
	}
}
