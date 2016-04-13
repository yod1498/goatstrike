using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	public KeyCode key;
	public AudioClip attackSFX;

	private Animator animator;
	private AudioSource audioSource;

	void Awake()
	{
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource> ();
	}
	
	void Update()
	{
		if (BattleController.IsBattleFinish){
			return;
		}

		if (Input.GetKeyDown(key)){
			PlayAnimation ();
		}

	}

	// press button from UI
	public void AttackFromUI(){
		if (BattleController.IsBattleFinish){
			return;
		}

		PlayAnimation ();
	}

	void PlayAnimation(){			
		PlaySoundEffect (attackSFX);
		animator.SetTrigger("Attack");
	}

	void PlaySoundEffect(AudioClip audioClip){
		audioSource.PlayOneShot (audioClip);
	}
}
