using UnityEngine;
using System.Collections;

public class PanelController : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown == true || Input.GetMouseButtonDown (0)) {
			gameObject.SetActive (false);
		}
	}
}
