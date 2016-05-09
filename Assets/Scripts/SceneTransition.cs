using UnityEngine;
using System.Collections;

public class SceneTransition : StateMachineBehaviour 
{
	public AudioClip clickSFX;

	override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
		animator.Stop();
		SceneController.gotoGame (clickSFX);
	}

}
