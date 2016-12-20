using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

	public string levelName = "";
	public GameObject adsPanel;
	public GameObject facebookPanel;
	public GameObject sceneTransitionPanel;
	public AudioClip clickSFX;
	private float clickSFXLenth;
	private static AudioSource audioSource;

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

		if (sceneTransitionPanel != null){
			sceneTransitionPanel.SetActive (false);
		}
			
		audioSource = GetComponent<AudioSource> ();
		if (clickSFX != null)
			clickSFXLenth = clickSFX.length;
	}

	public void LoadMenuScene () {
		BattleController.levelToLoadFromDeath = 0;
		PlayerPrefs.SetInt ("GoatStrikeInviteFriendStatus", (int) InviteFriendStatus.notyet);

		PlaySoundEffect (clickSFX);
		sceneTransitionPanel.SetActive (true);
		StartCoroutine(loadNewScene(levelName));
	}

	public void LoadCurrentLevel(){
		PlaySoundEffect (clickSFX);

		if (Life.DeCreaseLife (1) >= 0) {
			BattleController.levelToLoadFromDeath = BattleController.CurrentLevel;
			sceneTransitionPanel.SetActive (true);
			StartCoroutine(loadNewScene(levelName));
		} else {
			userInviteFriendStatus = PlayerPrefs.GetInt ("GoatStrikeInviteFriendStatus"); 

			//DISABLE FB for now
			//invite friend first. after that show ads
//			switch (userInviteFriendStatus) {
//			case (int) InviteFriendStatus.notyet:
//				facebookPanel.SetActive (true);
//				break;
//			case (int)InviteFriendStatus.shared:
//				adsPanel.SetActive (true);
//				break;
//			case (int) InviteFriendStatus.cancelled:
//				adsPanel.SetActive (true);
//				break;
//			default:
//				adsPanel.SetActive (true);
//				break;
//			}
			adsPanel.SetActive (true);
			//facebookPanel.SetActive (true);
		}
	}

	//load scene after clickSFX end 
	IEnumerator loadNewScene(string sceneName){
		yield return new WaitForSeconds(clickSFXLenth);
		SceneManager.LoadScene(sceneName);
	}

	void PlaySoundEffect(AudioClip audioClip){
		audioSource.PlayOneShot (audioClip);
	}
}
