using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Life : MonoBehaviour {
	private const int MAX_LIFE = 9;

	static public int lifeRemain = 0;

	public Text lifeRemainTxt;

	// Use this for initialization
	void Start () {
		//initial life
		lifeRemain = 0;
	}
	
	// Update is called once per frame
	void Update () {
		lifeRemainTxt.text = "" + lifeRemain;
	}

	public static void InCreaseLife (int life){
		if (lifeRemain >= MAX_LIFE)
			return;
		
		lifeRemain = lifeRemain + life;
	}

	public static int DeCreaseLife (int life){
		if (lifeRemain <= 0)
			return -1;
		
		lifeRemain = lifeRemain - life;

		return lifeRemain;
	}

	public static void ResetLife (){
		lifeRemain = 0;
	}
}
