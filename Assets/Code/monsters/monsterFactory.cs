using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterFactory : MonoBehaviour {

	public List<PlayerBasic> monsters = new List<PlayerBasic> ();
	public float timeWait = 10f;
	public int maxCount = 50;
	private int countNow = 0;

	//生产杂兵的工厂
	void Start () 
	{
		InvokeRepeating ("makeMonsters" , 0f , timeWait);		
	}
	
	void  makeMonsters()
	{
		if (monsters.Count <= 0 || countNow > maxCount)
			return;
		int index = Random.Range (0,monsters.Count);
		GameObject monster = (GameObject)GameObject.Instantiate (monsters[index].gameObject);
		monster.transform.position = this.transform.position;
		countNow++;
	}
}
