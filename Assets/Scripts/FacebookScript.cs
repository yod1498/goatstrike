using UnityEngine;
using System.Collections;
using Facebook.Unity;
using System.Collections.Generic;
using System;

public class FacebookScript : MonoBehaviour {

	private const string IMAGE_FOR_FB = "http://www.imi.co.th/apps/goatstrike/GoatStrikeFB.jpg";
	private const string LINK_FOR_FB =	"http://www.imi.co.th/apps/goatstrike/";
	private const string IOS_URL = "https://fb.me/810530068992919";
	private const string ANDROID_URL = "https://fb.me/892708710750483";

	public GameObject facebookPanel;
	public GameObject faceboookNotReadyPanel;
	public GameObject adsPanel;
	public GameObject facebookSharePanel;
	public GameObject facebookShareLevelUpPanel;

	private enum InviteFriendStatus {notyet, shared, cancelled};
	int userInviteFriendStatus = (int) InviteFriendStatus.notyet;

	// Awake function from Unity's MonoBehavior
	void Awake ()
	{
		// If the GoatStrikeInviteFriendStatus already exists, read it 
		if (PlayerPrefs.HasKey ("GoatStrikeInviteFriendStatus")) {
			userInviteFriendStatus = PlayerPrefs.GetInt ("GoatStrikeInviteFriendStatus"); 
		}
		// Assign the userInviteFriendStatus to GoatStrikeInviteFriendStatus 
		PlayerPrefs.SetInt ("GoatStrikeInviteFriendStatus", userInviteFriendStatus);

		if (!FB.IsInitialized) {
			// Initialize the Facebook SDK
			FB.Init(InitCallback, OnHideUnity);
		} else {
			// Already initialized, signal an app activation App Event
			FB.ActivateApp();
		}
	}
		
	public void CallFBLogin()
	{		
		if (!FB.IsLoggedIn) {
			FB.LogInWithReadPermissions (new List<string> () { "public_profile", "email", "user_friends" }, AuthCallback);
		} else {
			CallFBInviteFriend ();
		}
	}

	public void CancleInviteFriend (){
		PlayerPrefs.SetInt ("GoatStrikeInviteFriendStatus", (int) InviteFriendStatus.cancelled);
		adsPanel.SetActive (true);
	}

	public void closePanel (){
		facebookPanel.SetActive (false);
		faceboookNotReadyPanel.SetActive (false);
	}

	public void CallFBShare(){
		string title = "Goat Strike : Best Level";
		string description = "My best level is " + Score.currentScore + ". Beat me if you can!";
		Uri photo = new Uri (IMAGE_FOR_FB);
		FB.ShareLink(new Uri(LINK_FOR_FB),title,description,photo,callback: ShareCallback);
	}

	public void CallFBShareAchievement(){
		string title = "Goat Strike : Achievement";
		string description = "I unlocked level " + BattleController.CurrentLevel + ". Beat me if you can!";
		Uri photo = new Uri (IMAGE_FOR_FB);
		FB.ShareLink(new Uri(LINK_FOR_FB),title,description,photo,callback: ShareAchievementCallback);
	}


	private void InitCallback ()
	{
		if (FB.IsInitialized) {
			// Signal an app activation App Event
			FB.ActivateApp();
			// Continue with Facebook SDK
			// ...
			//Debug.Log("Initialized the Facebook SDK");
		} else {
			//Debug.Log("Failed to Initialize the Facebook SDK");
		}
	}

	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		} else {
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}

	private void AuthCallback (ILoginResult result) {
		if (FB.IsLoggedIn) {
			// AccessToken class will have session details
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			// Print current access token's User ID
			Debug.Log(aToken.UserId);
			// Print current access token's granted permissions
			foreach (string perm in aToken.Permissions) {
				Debug.Log(perm);
			}

			CallFBInviteFriend ();
		} else {
//			Debug.Log("User cancelled login");
			closePanel ();
			faceboookNotReadyPanel.SetActive (true);
		}
	}

	public void CallFBInviteFriend(){
		#if UNITY_IOS 
		FB.Mobile.AppInvite(new Uri(IOS_URL), new Uri(IMAGE_FOR_FB), this.HandleResult);
		#endif

		#if UNITY_ANDROID
		FB.Mobile.AppInvite(new Uri(ANDROID_URL), new Uri(IMAGE_FOR_FB), this.HandleResult);
		#endif
	}

	protected void HandleResult(IResult result)
	{
		if (result == null)
		{
//			this.LastResponse = "Null Response\n";
//			LogView.AddLog(this.LastResponse);
			closePanel ();
			faceboookNotReadyPanel.SetActive (true);
			return;
		}

//		this.LastResponseTexture = null;

		// Some platforms return the empty string instead of null.
		if (!string.IsNullOrEmpty(result.Error))
		{
//			this.Status = "Error - Check log for details";
//			this.LastResponse = "Error Response:\n" + result.Error;
//			LogView.AddLog(result.Error);
			closePanel ();
			faceboookNotReadyPanel.SetActive (true);
		}
		else if (result.Cancelled)
		{
//			this.Status = "Cancelled - Check log for details";
//			this.LastResponse = "Cancelled Response:\n" + result.RawResult;
//			LogView.AddLog(result.RawResult);
			closePanel ();
			faceboookNotReadyPanel.SetActive (true);
		}
		else if (!string.IsNullOrEmpty(result.RawResult))
		{
//			this.Status = "Success - Check log for details";
//			this.LastResponse = "Success Response:\n" + result.RawResult;
//			LogView.AddLog(result.RawResult);
			PlayerPrefs.SetInt ("GoatStrikeInviteFriendStatus", (int) InviteFriendStatus.shared);
			Life.InCreaseLife (2);
			closePanel ();
		}
		else
		{
//			this.LastResponse = "Empty Response\n";
//			LogView.AddLog(this.LastResponse);
			closePanel ();
			faceboookNotReadyPanel.SetActive (true);
		}
	}

	private void ShareCallback (IShareResult result) {
		if (result.Cancelled || !String.IsNullOrEmpty(result.Error)) {
			//Debug.Log("ShareLink Error: "+result.Error);
			facebookSharePanel.SetActive (false);
		} else if (!String.IsNullOrEmpty(result.PostId)) {
			// Print post identifier of the shared content
			//Debug.Log(result.PostId);
			facebookSharePanel.SetActive (false);
		} else {
			// Share succeeded without postID
			//Debug.Log("ShareLink success!");
			Life.InCreaseLife (1);
			facebookSharePanel.SetActive (false);
		}
	}

	private void ShareAchievementCallback (IShareResult result) {
		if (result.Cancelled || !String.IsNullOrEmpty(result.Error)) {
			//Debug.Log("ShareLink Error: "+result.Error);
			facebookShareLevelUpPanel.SetActive (false);
		} else if (!String.IsNullOrEmpty(result.PostId)) {
			// Print post identifier of the shared content
			//Debug.Log(result.PostId);
			facebookShareLevelUpPanel.SetActive (false);
		} else {
			// Share succeeded without postID
			//Debug.Log("ShareLink success!");
			Life.InCreaseLife (2);
			facebookShareLevelUpPanel.SetActive (false);
		}
	}

}

