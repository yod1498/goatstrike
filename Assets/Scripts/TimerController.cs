using UnityEngine;
using System.Collections.Generic;

public class TimerController : MonoBehaviour {

	public KeyCode key1 = KeyCode.A;
	public KeyCode key2 = KeyCode.S;
	public KeyCode key3 = KeyCode.D;

	private static Dictionary<int, KeyCode> keyPressedOrder = new Dictionary<int, KeyCode> ();
	//private bool isButtonPressed;
	private static float timer;
	private static bool isTimerStarted;

	// Use this for initialization
	void Awake () {
		//isButtonPressed = false;
		isTimerStarted = false;
		timer = 1f;
		keyPressedOrder.Clear ();
	}
	
	// Update is called once per frame
	void Update () {
		if (timer <= BattleController.maxCounter) {
			if (Input.GetKeyDown (key1)) {
				AddPressedKey (key1);
			} else if (Input.GetKeyDown (key2)) {
                AddPressedKey (key2);
			} else if (Input.GetKeyDown (key3)) {
                AddPressedKey (key3);
			}
		}


		if (isTimerStarted) {
			CountTimer ();
		}
	}

	public static void StartTimer(){
		if (isTimerStarted)
			return;

		timer = 1;
		isTimerStarted = true;
	}

	public static void ResetTimer (){
		isTimerStarted = false;
		timer = 1;
		keyPressedOrder.Clear ();
	}

	public static int getTimer(){
		return Mathf.FloorToInt(timer);
	}

	public static Dictionary<int, KeyCode> GetPressedKeys (){
		return keyPressedOrder;
	}

	private void CountTimer(){
		timer += Time.deltaTime;
	}

	private void AddPressedKey (KeyCode keyCode){
		int order = keyPressedOrder.Count + 1;
		keyPressedOrder.Add (order, keyCode);
		//isButtonPressed = true;
	}
}
