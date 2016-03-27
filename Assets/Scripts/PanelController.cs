using UnityEngine;
using System.Collections;

public class PanelController : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown == true) {
			showPanel ();
		}
	}

	void OnMouseDown(){
		showPanel ();
	}
		
	void showPanel(){
		gameObject.SetActive (false);
	}
}
