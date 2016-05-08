using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	public KeyCode keyCode;
	public ParticleSystem CE;
	public GameObject o1, o2, o3, o4, o5, o6 , o7;
	public float minRandScale = 0.6f;
	public float maxRandScale = 0.8f;

	//private bool isShowHint;
	private int keyCodeRepeat;
	private Animator animator;
	//private Vector2 position;

//	public Enemy(KeyCode newKeyCode, int newKeyCodeRepeat, Vector2 newPosition){
//		keyCode = newKeyCode;
//		keyCodeRepeat = newKeyCodeRepeat;
//		position = newPosition;
//	}

	void Awake(){
		this.transform.parent = GameObject.Find("Enemy").transform;

		float scale = Random.Range (minRandScale, maxRandScale);

		this.transform.localScale = new Vector3 (scale, scale, scale);

		animator = gameObject.GetComponent<Animator> ();
		//isShowHint = false;
	}

	public KeyCode KeyCode{
		get{return keyCode;}
	}

	public int KeyCodeRepeat{
		get{return keyCodeRepeat;}
		set{ keyCodeRepeat = value; }
	}

//	public bool IsShowHint{
//		set{ isShowHint = value; }
//	}

	public void isShowHint(bool isShowHint){
		GameObject[] hints = GameObject.FindGameObjectsWithTag ("Hint");
		for (int i = 0; i < hints.Length; i++) {
			hints[i].SetActive(isShowHint);
		}
	}

	public void EnemyWin (){
		animator.SetBool ("Attack",true);
	}

	public void EnemyHit (){
		animator.SetBool ("Hit",true);
	}

	public void EnemyDie (){

		ParticleSystem particle = Instantiate(CE, transform.position, Quaternion.identity) as ParticleSystem;

		Transform body = gameObject.transform;

		// instantiate die animation and sound effect
		GameObject Organ_1 = Instantiate(o1, body.position, body.rotation) as GameObject;
		Organ_1.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(4,11), Random.Range(-4, 11));
		GameObject Organ_2 = Instantiate(o2, body.position, body.rotation) as GameObject;
		Organ_2.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(4, 11), Random.Range(-4, 11));
		GameObject Organ_3 = Instantiate(o3, body.position, body.rotation) as GameObject;
		Organ_3.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(4, 11), Random.Range(-4, 11));
		GameObject Organ_4 = Instantiate(o4, body.position, body.rotation) as GameObject;
		Organ_4.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(4, 11), Random.Range(-4, 11));
		GameObject Organ_5 = Instantiate(o5, body.position, body.rotation) as GameObject;
		Organ_5.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(4, 11), Random.Range(-4, 11));
		GameObject Organ_6 = Instantiate(o6, body.position, body.rotation) as GameObject;
		Organ_6.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(4, 11), Random.Range(-4, 11));
		GameObject Organ_7 = Instantiate(o7, body.position, body.rotation) as GameObject;
		Organ_7.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(4, 11), Random.Range(-4, 11));

		// destroy enemy object and die animation
		Destroy(particle.gameObject, particle.duration);
		Destroy(Organ_1, particle.duration);
		Destroy(Organ_2, particle.duration);
		Destroy(Organ_3, particle.duration);
		Destroy(Organ_4, particle.duration);
		Destroy(Organ_5, particle.duration);
		Destroy(Organ_6, particle.duration);
		Destroy(Organ_7, particle.duration);
	}
}
