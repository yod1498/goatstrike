using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

	public GameObject creditPanel;

	void Awake(){
//		creditPanel = GameObject.Find ("CreditPanel");
		creditPanel.SetActive (false);
	}
   

	public void gotoMenu(){
		SceneManager.LoadScene("Menu");
	}

	public void gotoStory(){
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
