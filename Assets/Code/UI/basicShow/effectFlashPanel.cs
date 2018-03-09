using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class effectFlashPanel : MonoBehaviour {

	//这个脚本专门用来控制游戏主人公身上的BUFF显示
	//动态生成和显示各种BUFF图标
	//BUFF 在这里为各种effect效果（不论好坏）
	//每一秒钟更新一次

	public GameObject theEffectShowButton;

	//更新方法
	//每间隔一定时间刷新就可以，用Invoke
	private void updateBuffShow()
	{
		if (systemValues.thePlayer)
		{
			//清空当前存储内容
			informationMouseShow[] theBuffButtons = this.GetComponentsInChildren<informationMouseShow> ();
			for (int i = 0; i < theBuffButtons.Length; i++)
				Destroy (theBuffButtons [i].gameObject);
			//显示当前effectBuff信息
			effectBasic[] theEffectbasics = systemValues.thePlayer.GetComponentsInChildren<effectBasic> ();
			for (int i = 0; i < theEffectbasics.Length; i++)
			{
				if (theEffectbasics [i].isShowing ())
				{
					GameObject theButton = GameObject.Instantiate<GameObject> (theEffectShowButton);
					theButton.transform.SetParent (this.transform);
					theButton.GetComponent <informationMouseShow> ().showText = theEffectbasics [i].theEffectName + "\n" + theEffectbasics [i].theEffectInformation;
					theButton.GetComponentInChildren<Text> ().text = theEffectbasics [i].theEffectName;
					if (theEffectbasics [i].isEffecting == false)
						theButton.GetComponentInChildren<Text> ().text += "\n[失效]";
				}
			}
		}
	}


	void Start ()
	{
		InvokeRepeating ("updateBuffShow" , 0f , 0.2f);	
	}
	

}
