using UnityEngine;
using System.Collections;

public class ButtonPanelController : MonoBehaviour {

	public GameObject panel;

	public void closePanel (){
		panel.SetActive (false);
	}
}
