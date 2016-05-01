using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

	public GameObject creditPanel;

	void Awake(){
		if (creditPanel != null){
			creditPanel.SetActive (false);
		}
	}
   

	public void gotoMenu(){
		SceneManager.LoadScene("Menu");
	}

	public void gotoStory(){
		//reset life to default value
		Life.ResetLife ();
		SceneManager.LoadScene("Story");
	}

	public void gotoCredit() {
		SceneManager.LoadScene("Credit");
	}

	public void gotoExit(){
		Application.Quit();
	}

	public void startGame() {
		SceneManager.LoadScene("Level1");
	}

    public static void gotoGame(){
		SceneManager.LoadScene("Level1");
    }

	public void showCredit(){
		creditPanel.SetActive (true);
	}
}
