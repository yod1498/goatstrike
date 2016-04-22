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
	public Text hintWhite;
	public Text hintBrown;
	public Text hintBlack;

	// modify these variables when changing number of levels
	// also modify GetSpawnPosition()
	public Transform[] oneEnemyPosition;//1 enemy
	public Transform[] twoEnemyPosition;//2 enemy
	public Transform[] threeEnemyPosition;//3 enemy
	public Transform[] fourEnemyPosition;//4 enemy
	public Transform[] fiveEnemyPosition;//5 enemy
	public Transform[] sixEnemyPosition;//6 enemy
	public Transform[] sevenEnemyPosition;//7 enemy
	public Transform[] eightEnemyPosition;//8 enemy
	public Transform[] nineEnemyPosition;//9 enemy
	public Transform[] tenEnemyPosition;//10 enemy
	// offset (X axis) for enemies's spawn  position (enemy will be placed from left to right)
	//private List<float> offsetSpawn = new List<float>{8.1f,7.2f,6.3f,5.4f,4.5f,3.6f,2.7f,1.8f,0.9f,0f};
	private List<float> offsetSpawnOneEnemy = new List<float>{1.5f};
	private List<float> offsetSpawnTwoEnemy = new List<float>{4.5f,2.7f};
	private List<float> offsetSpawnThreeEnemy = new List<float>{4.5f,2.7f,0.9f};
	private List<float> offsetSpawnFourEnemy = new List<float>{5f,3.5f,2f,0.5f};
	private List<float> offsetSpawnFiveEnemy = new List<float>{6.5f,5f,3.5f,2f,0.5f};
	private List<float> offsetSpawnSixEnemy = new List<float>{7.5f,6f,4.5f,3f,1.5f,0f};
	private List<float> offsetSpawnSevenEnemy = new List<float>{7.8f,6.5f,5.2f,3.9f,2.6f,1.3f,0f};
	private List<float> offsetSpawnEightEnemy = new List<float>{7.7f,6.6f,5.5f,4.4f,3.3f,2.2f,1.1f,0f};
	private List<float> offsetSpawnNineEnemy = new List<float>{8f,7f,6f,5f,4f,3f,2f,1f,0f};
	private List<float> offsetSpawnTenEnemy = new List<float>{8.1f,7.2f,6.3f,5.4f,4.5f,3.6f,2.7f,1.8f,0.9f,0f};

	void Awake () {
		enemyController = this;
		setHintVisibility (false);
	}

	// Generate enemy type and location
	public Dictionary<int, Enemy> SpawnEnemy(int numberOfEnemy, bool isShowHint, int level){

		Dictionary<int, Enemy> enemies = new Dictionary<int, Enemy>();

		//random position (left to right or top to buttom)
		List<int> orderNo = new List<int>();
		for (int i = 0; i < numberOfEnemy; i++) {
			orderNo.Add (i);
		}
		orderNo = RandomOrderNo (orderNo);

		for (int i = 1; i <= numberOfEnemy; i++) {
			Enemy enemy = GenerateEnemy (numberOfEnemy,orderNo[i-1], i-1, isShowHint, level);
			enemies.Add (i,enemy);
		}

		//show hint for tutorial
		if (isShowHint) {

			switch (level) {
			case 1:
				hintWhite.enabled = true;
				break;
			case 2:
				hintBrown.enabled = true;
				break;
			case 3:
				hintBlack.enabled = true;
				break;
			case 4:
				setHintMsg(numberOfEnemy);
				hintTxt.enabled = true;
				break;
			case 5:
				setHintMsg(numberOfEnemy);
				hintTxt.enabled = true;
				break;
			case 6:
				setHintMsg(numberOfEnemy);
				hintTxt.enabled = true;
				break;
			default:
				break;
			}

		} else {
			setHintVisibility (false);
		}

		return enemies;
	}

	// generate new enemy 
	// numberOfEnemy = number of all enemy 
	// orderNo = position for spawning (random from spawnpostion gameobject - 1 to 3 positions depending on noOfEnemy)
	// squenceNo =  enemies will be spawnned from left to right
	Enemy GenerateEnemy(int numberOfEnemy, int orderNo, int sequenceNo, bool isShowHint, int level){
		int keyCodeRepeat = 1;

		Transform spawnPosition = GetSpawnPosition (numberOfEnemy,orderNo);

		//Add offset (enemies will be spawnned from left to right)
		//If less enemy than the list, try to position enemy in the center
//		if (numberOfEnemy < offsetSpawn.Count) {
//			if ((offsetSpawn.Count - numberOfEnemy) >= 5) {
//				//1-5 enemies need to move to center
//				sequenceNo = sequenceNo + 5;
//			}else if ((offsetSpawn.Count - numberOfEnemy) < 5) {
//				//>6 enemies need to move from right to left
//				sequenceNo = sequenceNo + (offsetSpawn.Count - numberOfEnemy);
//			} else {
//				sequenceNo++;
//			}
//		}

		float offset = 0;

		switch (numberOfEnemy) {
		case 1:
			offset = offsetSpawnOneEnemy [sequenceNo];
			break;
		case 2:
			offset = offsetSpawnTwoEnemy [sequenceNo];
			break;
		case 3:
			offset = offsetSpawnThreeEnemy [sequenceNo];
			break;
		case 4:
			offset = offsetSpawnFourEnemy [sequenceNo];
			break;
		case 5:
			offset = offsetSpawnFiveEnemy [sequenceNo];
			break;
		case 6:
			offset = offsetSpawnSixEnemy [sequenceNo];
			break;
		case 7:
			offset = offsetSpawnSevenEnemy [sequenceNo];
			break;
		case 8:
			offset = offsetSpawnEightEnemy [sequenceNo];
			break;
		case 9:
			offset = offsetSpawnNineEnemy [sequenceNo];
			break;
		case 10:
			offset = offsetSpawnTenEnemy [sequenceNo];
			break;
		default:
			break;
		}

		//float offset = offsetSpawn [sequenceNo];
		Vector2 offsetSpawnPosition = new Vector2 (spawnPosition.position.x - offset,spawnPosition.position.y);

		Enemy enemy = (Enemy) Instantiate (RandomEnemy(level), offsetSpawnPosition, spawnPosition.rotation);
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
		case 6:
			spawnPosition = sixEnemyPosition [orderNo];
			break;
		case 7:
			spawnPosition = sevenEnemyPosition [orderNo];
			break;
		case 8:
			spawnPosition = eightEnemyPosition [orderNo];
			break;
		case 9:
			spawnPosition = nineEnemyPosition [orderNo];
			break;
		case 10:
			spawnPosition = tenEnemyPosition [orderNo];
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

	//level 1 to 3 (tutorial) - no random
	Enemy RandomEnemy(int level){
		if (level == 1) {
			return maulman;
		} else if (level == 2) {
			return bowman;
		} else if (level == 3) {
			return spearman;
		} else {
			int enemyNo = Random.Range (1, 4);
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
		}
		return maulman;
	}

	private void setHintMsg(int noOfEnemy){
		string hintMsg = "Hint: ";
		if (noOfEnemy <= 1){
			hintMsg = BattleController.hintMessage[0];
		}else{
			hintMsg = BattleController.hintMessage[1];
		}
		hintTxt.text = hintMsg;
	}

	public void setHintVisibility(bool isVisible){
		hintWhite.enabled = isVisible;
		hintBrown.enabled = isVisible;
		hintBlack.enabled = isVisible;
		hintTxt.enabled = isVisible;
	}
}
