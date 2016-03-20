using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

	public static EnemyController enemyController;

	public Enemy maulman;
	public Enemy bowman;
	public Enemy spearman;
	public Text hintTxt;

	// modify these variables when changing number of levels
	// also modify GetSpawnPosition()
	public Transform[] oneEnemyPosition;//1 enemy
	public Transform[] twoEnemyPosition;//2 enemy
	public Transform[] threeEnemyPosition;//3 enemy
	public Transform[] fourEnemyPosition;//4 enemy
	public Transform[] fiveEnemyPosition;//5 enemy
	// enemies's spawn position from left to right
	private List<float> offsetSpawn = new List<float>{4.2f,3.2f,2.2f,1.2f,0f};

	void Awake () {
		enemyController = this;
	}

	// Generate enemy type and location
	public Dictionary<int, Enemy> SpawnEnemy(int numberOfEnemy, bool isShowHint){

		Dictionary<int, Enemy> enemies = new Dictionary<int, Enemy>();

		//random position (left to right or top to buttom)
		List<int> orderNo = new List<int>();
		for (int i = 0; i < numberOfEnemy; i++) {
			orderNo.Add (i);
		}
		orderNo = RandomOrderNo (orderNo);

		for (int i = 1; i <= numberOfEnemy; i++) {
			Enemy enemy = GenerateEnemy (numberOfEnemy,orderNo[i-1], i-1, isShowHint);
			enemies.Add (i,enemy);
		}

		//hint at the buttom of the screen
		if (isShowHint) {
			setHintMsg(numberOfEnemy);
			hintTxt.enabled = true;
		} else {
			hintTxt.enabled = false;
		}

		return enemies;
	}

	// generate new enemy 
	// numberOfEnemy = number of all enemy 
	// orderNo = position for spawning (random from spawnpostion gameobject - 1 to 3 positions depending on noOfEnemy)
	// squenceNo =  enemies will be spawnned from left to right
	Enemy GenerateEnemy(int numberOfEnemy, int orderNo, int sequenceNo, bool isShowHint){
		int keyCodeRepeat = 1;

		Transform spawnPosition = GetSpawnPosition (numberOfEnemy,orderNo);

		//Add offset (enemies will be spawnned from left to right)
		//If less enemy than the list, position its in center
		if (numberOfEnemy < offsetSpawn.Count) {
			sequenceNo++;
		}

		float offset = offsetSpawn [sequenceNo];
		Vector2 offsetSpawnPosition = new Vector2 (spawnPosition.position.x - offset,spawnPosition.position.y);

		Enemy enemy = (Enemy) Instantiate (RandomEnemy(), offsetSpawnPosition, spawnPosition.rotation);
		enemy.KeyCodeRepeat = keyCodeRepeat;
		enemy.isShowHint (isShowHint);
		return enemy;
	}

	// get position for spawning enemey
	// noOfEnemy = no of enemy in level 
	// orderNo = position for spawning (random from spawnpostion gameobject - 1 to 3 positions depending on noOfEnemy)
	Transform GetSpawnPosition (int noOfEnemy, int orderNo){
		Transform spawnPosition;

		switch (noOfEnemy) {
		case 1:
			spawnPosition = oneEnemyPosition [orderNo];
			break;
		case 2:
			spawnPosition = twoEnemyPosition [orderNo];
			break;
		case 3:
			spawnPosition = threeEnemyPosition [orderNo];
			break;
		case 4:
			spawnPosition = fourEnemyPosition [orderNo];
			break;
		case 5:
			spawnPosition = fiveEnemyPosition [orderNo];
			break;
		default:
			spawnPosition = oneEnemyPosition [orderNo];
			break;
		}
		return spawnPosition;
	}

	//Random spawn position (left to right)
	List<int> RandomOrderNo (List<int> orderNo){
		for (int i = 0; i < orderNo.Count; i++) {
			int temp = orderNo[i];
			int randomIndex = Random.Range(i, orderNo.Count);
			orderNo[i] = orderNo[randomIndex];
			orderNo[randomIndex] = temp;
		}

		return orderNo;
	}

	Enemy RandomEnemy(){
		int enemyNo = Random.Range (1,4);
		switch (enemyNo) {
		case 1:
			return maulman;
		case 2:
			return bowman;
		case 3:
			return spearman;
		default:
			break;
		}
		return maulman;
	}

	private void setHintMsg(int noOfEnemy){
		string hintMsg = "Hint:";
		if (noOfEnemy <= 1){
			hintMsg = hintMsg + BattleController.hintMessage[0];
		}else{
			hintMsg = hintMsg + BattleController.hintMessage[1];
		}
		hintTxt.text = hintMsg;
	}


}
