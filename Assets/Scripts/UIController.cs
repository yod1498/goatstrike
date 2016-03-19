using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

	public string LevelName = "";

	public void LoadScene () {
		SceneManager.LoadScene(LevelName);
	}
}
