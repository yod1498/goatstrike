using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class UnityAds : MonoBehaviour
{
	public GameObject adsPanel;
	public GameObject adsNotReadyPanel;

	public void ShowAd()
	{
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
		}
	}

	public void ShowRewardedAd()
	{
		if (Advertisement.IsReady ("rewardedVideo")) {
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show ("rewardedVideo", options);
		} else {
			closePanel ();
			adsNotReadyPanel.SetActive (true);
		}
	}

	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log ("The ad was successfully shown.");
			Analytics.CustomEvent("ShowAds", new Dictionary<string, object>
				{
					{ "highScore", Score.highScore },
					{ "level", BattleController.CurrentLevel }
				});
			Life.InCreaseLife (3);
			closePanel ();
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			closePanel ();
			adsNotReadyPanel.SetActive (true);
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			closePanel ();
			adsNotReadyPanel.SetActive (true);
			break;
		}
	}

	public void closePanel (){
		adsPanel.SetActive (false);
		adsNotReadyPanel.SetActive (false);
	}
}