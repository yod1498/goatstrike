using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Life : MonoBehaviour {
	private const int MAX_LIFE = 9;
	private const int DEFAULT_LIFE = 3;

	public static int lifeRemain = DEFAULT_LIFE;

	public Text lifeRemainTxt;

	void Awake() {
		// If the GoatStrikeLifeRemain already exists, read it 
		if (PlayerPrefs.HasKey ("GoatStrikeLifeRemain")) {
			lifeRemain = PlayerPrefs.GetInt ("GoatStrikeLifeRemain"); 
		}
		// Assign the lifeRemain to GoatStrikeLifeRemain 
		PlayerPrefs.SetInt ("GoatStrikeLifeReamin", lifeRemain);
	}

//	// Use this for initialization
//	void Start () {
//		//initial life
//		lifeRemain = 3;
//
//		// Update GoatStrikeHighScore in PlayerPrefs if necessary 
//		if (highScore > PlayerPrefs.GetInt ("GoatStrikeHighScore")) {
//			PlayerPrefs.SetInt ("GoatStrikeHighScore", highScore);
//		}
//	}
	
	// Update is called once per frame
	void Update () {
		lifeRemainTxt.text = "" + lifeRemain;
	}

	public static void InCreaseLife (int life){
		if (lifeRemain >= MAX_LIFE)
			return;
		
		lifeRemain = lifeRemain + life;

		SyncLifeRemainToPref (lifeRemain);
	}

	public static int DeCreaseLife (int life){
		if (lifeRemain <= 0)
			return -1;
		
		lifeRemain = lifeRemain - life;

		SyncLifeRemainToPref (lifeRemain);

		return lifeRemain;
	}

	public static void ResetLife (){
		lifeRemain = DEFAULT_LIFE;
		SyncLifeRemainToPref (lifeRemain);
	}

	private static void SyncLifeRemainToPref(int lifeRemain){
		// Update GoatStrikeHighScore in PlayerPrefs 
		PlayerPrefs.SetInt ("GoatStrikeLifeRemain", lifeRemain);
	}
}
