using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

	public string levelName = "";
	public GameObject adsPanel;

	public void LoadScene () {
		BattleController.levelToLoadFromDeath = 0;
		SceneManager.LoadScene(levelName);
	}

	public void LoadCurrentLevel(){
		if (Life.DeCreaseLife (1) >= 0) {
			BattleController.levelToLoadFromDeath = BattleController.CurrentLevel;
			SceneManager.LoadScene (levelName);
		} else {
			adsPanel.SetActive (true);
		}
	}
}
