using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour {

	private const string ENEMY_TAG = "Enemy";

	// modify these variables when changing number of levels
	// number of enemy in each level e.g. level 1-3 enemy = 1, level 4-6 enemy = 2  
	public static List<int> beatLevelScore =  new List<int>{3,6,9,15,20,25,30,35,40,9999};
	public static List<int> noOfEnemyInLevel =  new List<int>{1,2,3,4,5,6,7,8,9,10};
	public static List<bool> isShowHintInLevel =  new List<bool>{true,true,false,false,false,false,false,false,false,false};
	public static List<string> hintMessage =  new List<string>{"press the right keys","Destroy the ememy from left to right"};

	public static int maxCounter = 5;

	public AudioClip timeCountingSFX;
	public AudioClip timeOutSFX;
	public Text levelTxt;
	public Text readyTxt;
	public Image timeImg1;
	public Image timeImg2;
	public Image timeImg3;
	public Image timeImg4;
	public Image timeImg5;
	public Text timeText1;
	public Text timeText2;
	public Text timeText3;
	public Text timeText4;
	public Text timeText5;
	public Button startButton;
	public static int levelToLoadFromDeath = 0;
	public EnemyController enemyController;


	//private GameObject[] enemyPrefab;
	private static bool isBattleFinish;
	private static Enemy[] _enemies;
	private static int _noOfEnemyInBattle;
	private KeyCode startBattleKey = KeyCode.B;
	private static int currentLevel = 1;
	private AudioSource audioSource;
	private int isReadytoShareAchievement = 0; //0=false, 1=true

    void Awake()
    {
		//PlayerPrefs.DeleteAll();
		// restart level from current level or level 1
		if (levelToLoadFromDeath > 0) {
			RestartLevelFromCurrentLevel ();
		} else {
			ResetLevel();
		}
		
		startButton.gameObject.SetActive (true);
		isBattleFinish = true;
		readyTxt.enabled = false;
		//string levelname = SceneManager.GetActiveScene ().name;
		ResetTimeImage ();
		audioSource = GetComponent<AudioSource> ();

		// If the GoatStrikeHighLevel already exists, read it 
		if (PlayerPrefs.HasKey ("IsReadytoShareAchievement")) {
			isReadytoShareAchievement = PlayerPrefs.GetInt ("IsReadytoShareAchievement"); 
		}
		// Assign the currentLevelShared to GoatStrikeHighLevel 
		PlayerPrefs.SetInt ("IsReadytoShareAchievement", isReadytoShareAchievement);
    }
		
	void Update () {

		// For testing by pressing  'B'
		if (Input.GetKeyDown (startBattleKey)) {
			NextLevel();
		}

		int currentTimer = TimerController.getTimer ();

		if ((currentTimer > 0) && (currentTimer <= maxCounter)){
			//counterTxt.text = currentTimer.ToString ();
		}else if (currentTimer > maxCounter){
			//counterTxt.text = "0";
			TimeUp ();
		}

		if (!isBattleFinish) {
			DisplayTimeImg ();
			CheckMissMatchKey ();
		}
	}

	public void StartGame(){
		startButton.gameObject.SetActive (false);
		NextLevel ();
	}

	public static bool IsBattleFinish {
		get { return isBattleFinish; }
	}

	public static int CurrentLevel{
		get { return currentLevel; }
	}

	private static void BeginBattle(){
		setUpEnemies ();
		isBattleFinish = false;
		TimerController.ResetTimer ();
		TimerController.StartTimer ();
	}

	private static void setUpEnemies(){
		_noOfEnemyInBattle = getNoOfEnemy(currentLevel);
		bool showHint = isShowHintInLevel [_noOfEnemyInBattle - 1];
		Dictionary<int, Enemy> enemies = EnemyController.enemyController.SpawnEnemy (_noOfEnemyInBattle, showHint,currentLevel);
		int noOfEnemy = enemies.Count;
		_enemies = new Enemy[noOfEnemy];

		for (int i = 1; i <= noOfEnemy; i++) {
			_enemies [i - 1] = enemies [i];
			int noOfKeyCode = enemies [i].KeyCodeRepeat;
			//ememy that has to press key twice or mroe
			if (noOfKeyCode > 1) {
				_noOfEnemyInBattle = _noOfEnemyInBattle + noOfKeyCode;
			}
		}
	}
		
	// return no of enemy in the current level
	private static int getNoOfEnemy(int currentLevel){
		for (int i = 0; i < beatLevelScore.Count; i++) {
			if (currentLevel <= beatLevelScore [i]) {
				return noOfEnemyInLevel [i];
			}
		}
		return 1;
	}

	private static void destroyEnemies(){
		if (_noOfEnemyInBattle > 0) {
			GameObject[] enemies = GameObject.FindGameObjectsWithTag (ENEMY_TAG);
			for (int i = 0; i < enemies.Length; i++) {
				//enemies [i].SetActive (false);
				enemies [i].GetComponent<Enemy>().EnemyDie ();
				Destroy (enemies [i]);
			}
		}
	}

	private static void playEnemiesAttack(){
		if (_noOfEnemyInBattle > 0) {
			GameObject[] enemies = GameObject.FindGameObjectsWithTag (ENEMY_TAG);
			for (int i = 0; i < enemies.Length; i++) {
				//enemies [i].SetActive (false);
				enemies [i].GetComponent<Enemy>().EnemyWin();
			}
		}
	}

	void ResetTimeImage(){
		timeImg1.enabled = false;
		timeImg2.enabled = false;
		timeImg3.enabled = false;
		timeImg4.enabled = false;
		timeImg5.enabled = false;

		timeText1.enabled = false;
		timeText2.enabled = false;
		timeText3.enabled = false;
		timeText4.enabled = false;
		timeText5.enabled = false;
	}

	void DisplayTimeImg(){
		int currentTime = TimerController.getTimer ();

		switch (currentTime) {
		case 5:
                if (!timeImg5.enabled){
					PlaySoundEffect (timeCountingSFX);
				}
				timeImg5.enabled = true;
				timeText5.enabled = true;
                
			break;
		case 4:
                if (!timeImg4.enabled)
                {
				PlaySoundEffect (timeCountingSFX);
                }
                timeImg4.enabled = true;
				timeText4.enabled = true;
                    
                break;
		case 3:
                if (!timeImg3.enabled)
                {
				PlaySoundEffect (timeCountingSFX);
                }
                timeImg3.enabled = true;
				timeText3.enabled = true;
                
                break;
		case 2:
                if (!timeImg2.enabled)
                {
				PlaySoundEffect (timeCountingSFX);
                }
                timeImg2.enabled = true;
				timeText2.enabled = true;
                
                break;
		case 1:
                if (!timeImg1.enabled)
                {
				PlaySoundEffect (timeCountingSFX);
                }
                timeImg1.enabled = true;
				timeText1.enabled = true;
                
                break;
		default:
			break;
		}
	}

	void CheckMissMatchKey(){
		int checkMatchCode = 0;

		Dictionary<int, KeyCode> keyPressedOrder = TimerController.GetPressedKeys ();

		//KeyCode[] keycodes = enemy.getKeyPress ();

		if ((keyPressedOrder.Count > 0) && (_enemies.Length > 0)) {

			int keyOrder = 1;

			for (int i = 0; i < _enemies.Length; i++) {
				Enemy enemy = _enemies [i]; 
				int keyCodeRepeat = enemy.KeyCodeRepeat;
				for (int j = 0; j < keyCodeRepeat; j++) {
					//Waiting for user to press the next key
					//check only if next key is pressed.
					//if user do not press the key, it will time out
					if (keyOrder <= keyPressedOrder.Count){
						if (enemy.KeyCode.ToString () == keyPressedOrder [keyOrder].ToString ()) {
							checkMatchCode++;
							// play animation
							enemy.EnemyHit ();
						} else {
							// play animation
							enemy.EnemyWin ();
							DoLose ();
						}
						keyOrder++;
					}
				}
			}
		}

		if (checkMatchCode == _noOfEnemyInBattle) {
			DoWin ();
		} 	
	}

	void TimeUp(){
		DoLose ();
	}
		
	void DoWin(){
		enemyController.setHintVisibility (false);
		Score.PassLevel (currentLevel);
		destroyEnemies ();
		isBattleFinish = true;
		TimerController.ResetTimer ();
		ResetTimeImage ();
		LeveUp ();
    }

	void DoLose (){
		playEnemiesAttack ();
		PlaySoundEffect (timeOutSFX);
		isBattleFinish = true;
		ResetTimeImage ();
		TimerController.ResetTimer ();
		StartCoroutine(LoadNextLevel(3f,"Gameover"));
    }

	IEnumerator LoadNextLevel(float delayTime, string levelName){
		yield return new WaitForSeconds(delayTime);
		SceneManager.LoadScene(levelName);
	}

	// restart from level 1
	void ResetLevel(){
		currentLevel = 1;
		Score.ResetScore ();
		levelTxt.text = "LV : " + currentLevel;		
	}

	// restart from current level
	void RestartLevelFromCurrentLevel(){
		currentLevel = levelToLoadFromDeath;
		//Score.ResetScore ();
		levelTxt.text = "LV : " + currentLevel;	
	}

	void LeveUp(){
		currentLevel++;
		levelTxt.text = "LV : " + currentLevel;

		// if level 10,20,30... show level up share
		if (currentLevel % ShareHighScore.LEVEL_SHARE_ACHIEVEMENT == 0) {
			//ShareHighScore.isReadytoShareAchievement = true;
			isReadytoShareAchievement = 1;

			PlayerPrefs.SetInt ("IsReadytoShareAchievement", isReadytoShareAchievement);
		}

		NextLevel ();
	}

	void NextLevel(){
		//readyTxt.text = "Ready!";
		readyTxt.text = "LV:" + currentLevel;
		readyTxt.enabled = true;
		StartCoroutine(ChangeReadyTextToGo(2f));
		StartCoroutine(ReadyToFight(3f));
	}

	IEnumerator ChangeReadyTextToGo(float delayTime){
		yield return new WaitForSeconds(delayTime);
		readyTxt.text = "GO!!!";
	}

	IEnumerator ReadyToFight(float delayTime){
		yield return new WaitForSeconds(delayTime);
		readyTxt.enabled = false;
		BattleController.BeginBattle();
	}

	void PlaySoundEffect(AudioClip audioClip){
		//AudioSource.PlayClipAtPoint (audioClip,gameObject.transform.position);
		audioSource.PlayOneShot (audioClip);
	}
}
