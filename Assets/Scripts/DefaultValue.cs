using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefaultValue : MonoBehaviour {

	public static DefaultValue dv;

	public List<int> beatLevelScore =  new List<int>{5,20,30};
	public List<int> noOfEnemyInLevel =  new List<int>{1,2,3};
		
	void Awake () {
		dv = this;
	}
}
