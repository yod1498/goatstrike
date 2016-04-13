using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

	public string LevelName = "";

	public void LoadScene () {
		BattleController.levelToLoadFromDeath = 0;
		SceneManager.LoadScene(LevelName);
	}

	public void LoadCurrentLevel(){
		BattleController.levelToLoadFromDeath = BattleController.CurrentLevel;
		SceneManager.LoadScene(LevelName);
	}
}
