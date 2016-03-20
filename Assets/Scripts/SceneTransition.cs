using UnityEngine;
using System.Collections;

public class SceneTransition : StateMachineBehaviour 
{

	override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
		SceneController.gotoGame ();
	}

}
