using UnityEngine;
using System.Collections;
using Facebook.Unity;
using System.Collections.Generic;
using System;

public class FacebookScript : MonoBehaviour {

	public GameObject facebookPanel;
	public GameObject faceboookNotReadyPanel;
	public GameObject adsPanel;

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

	private void CallFBInviteFriend(){
		#if UNITY_IOS 
			//FB.Mobile.AppInvite(new Uri("https://fb.me/810530068992919"), callback: this.HandleResult);
			FB.Mobile.AppInvite(new Uri("https://fb.me/810530068992919"), new Uri("http://i.imgur.com/zkYlB.jpg"), this.HandleResult);
		#endif

		#if UNITY_ANDROID
			//FB.Mobile.AppInvite(new Uri("https://fb.me/892708710750483"), callback: this.HandleResult);
			FB.Mobile.AppInvite(new Uri("https://fb.me/892708710750483"), new Uri("http://i.imgur.com/zkYlB.jpg"), this.HandleResult);
		#endif
	}

	protected void HandleResult(IResult result)
	{
		if (result == null)
		{
//			this.LastResponse = "Null Response\n";
//			LogView.AddLog(this.LastResponse);
//			Debug.Log ("FB Log = 1");
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
//			Debug.Log ("FB Log = 2");
			closePanel ();
			faceboookNotReadyPanel.SetActive (true);
		}
		else if (result.Cancelled)
		{
//			this.Status = "Cancelled - Check log for details";
//			this.LastResponse = "Cancelled Response:\n" + result.RawResult;
//			LogView.AddLog(result.RawResult);
//			Debug.Log ("FB Log = 3");
			closePanel ();
			faceboookNotReadyPanel.SetActive (true);
		}
		else if (!string.IsNullOrEmpty(result.RawResult))
		{
//			this.Status = "Success - Check log for details";
//			this.LastResponse = "Success Response:\n" + result.RawResult;
//			LogView.AddLog(result.RawResult);
//			Debug.Log ("FB Log = 4");
			PlayerPrefs.SetInt ("GoatStrikeInviteFriendStatus", (int) InviteFriendStatus.shared);
			Life.InCreaseLife (3);
			closePanel ();
		}
		else
		{
//			this.LastResponse = "Empty Response\n";
//			LogView.AddLog(this.LastResponse);
//			Debug.Log ("FB Log = 5");
			closePanel ();
			faceboookNotReadyPanel.SetActive (true);
		}
	}

}
