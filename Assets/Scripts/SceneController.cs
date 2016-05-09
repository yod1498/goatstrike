using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

	public GameObject creditPanel;
	public GameObject sceneTransitionPanel;
	public AudioClip clickSFX;
	private float clickSFXLenth;
	private static AudioSource audioSource;

	void Awake(){
		if (creditPanel != null){
			creditPanel.SetActive (false);
		}

		if (sceneTransitionPanel != null){
			sceneTransitionPanel.SetActive (false);
		}
			
		audioSource = GetComponent<AudioSource> ();
		if (clickSFX != null)
			clickSFXLenth = clickSFX.length;
	}
   

	public void gotoMenu(){
		PlaySoundEffect (clickSFX);
		sceneTransitionPanel.SetActive (true);
		StartCoroutine(loadNewScene("Menu"));
	}

	public void gotoStory(){
		PlaySoundEffect (clickSFX);
		sceneTransitionPanel.SetActive (true);
		//reset life to default value
		Life.ResetLife ();
		StartCoroutine(loadNewScene("Story"));
	}

	public void gotoCredit() {
		PlaySoundEffect (clickSFX);
		sceneTransitionPanel.SetActive (true);
		SceneManager.LoadScene("Credit");
	}

	public void gotoExit(){
		PlaySoundEffect (clickSFX);
		sceneTransitionPanel.SetActive (true);
		Application.Quit();
	}

	public void startGame() {
		PlaySoundEffect (clickSFX);
		GameObject.Find ("story").SetActive (false);
		sceneTransitionPanel.SetActive (true);
		StartCoroutine(loadNewScene("Level1"));
	}

	//called when user hit skip button
	public void gotoGame(){
		PlaySoundEffect (clickSFX);
		sceneTransitionPanel.SetActive (true);
		StartCoroutine(loadNewScene("Level1"));
	}

	//called by SceneTransition when story end
	public static void gotoGame(AudioClip audioClip){
		GameObject.Find ("story").SetActive (false);
		GameObject.Find ("Canvas").SetActive (false);
		SceneManager.LoadScene("Level1");;
    }

	public void showCredit(){
		PlaySoundEffect (clickSFX);
		creditPanel.SetActive (true);
	}

	//load scene after clickSFX end 
	IEnumerator loadNewScene(string sceneName){
		yield return new WaitForSeconds(clickSFXLenth);
		SceneManager.LoadScene(sceneName);
	}

	public static void PlaySoundEffect(AudioClip audioClip){
		//AudioSource.PlayClipAtPoint (audioClip,gameObject.transform.position);
		audioSource.PlayOneShot (audioClip);
	}
}
