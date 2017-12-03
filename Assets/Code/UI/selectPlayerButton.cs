using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectPlayerButton : MonoBehaviour {

	//选择人物的按钮
	public Transform theShowPosition;//实例化游戏人物的位置
	private  static GameObject therPlayer;//私有引用
	public Text playerInformationText;//显示游戏人物信息的文本
	public Text playerTitleText;//显示游戏人物信息的文本

	public void getNextPlayer()
	{
		if(therPlayer!=null)
			Destroy(therPlayer.gameObject);

		GameObject theProfab = Resources.Load<GameObject> ("fighters/"+systemValues.getNextPlayer());
		makePlayer (theProfab);
	}

	public void getProPlayer()
	{
		if(therPlayer!=null)
			Destroy(therPlayer);

		GameObject theProfab = Resources.Load<GameObject> ("fighters/"+systemValues.getProPlayer());
		makePlayer (theProfab);
	}


	public void DestroyPlayerMode()
	{
		if(therPlayer!=null)
			Destroy(therPlayer.gameObject);
		
	}
	void makePlayer(GameObject theProfab)
	{
		therPlayer = GameObject.Instantiate (theProfab );
		therPlayer.transform.position = theShowPosition.transform.position;
		therPlayer.transform.localScale = new Vector3 (4,4,4);
		therPlayer.AddComponent<extraRotate> ();
		PlayerBasic thePlayerB = therPlayer.GetComponent<PlayerBasic> ();
		playerInformationText.text = thePlayerB.getPlayerInformation () + thePlayerB.getPlayerInformationExtra ();
		playerTitleText.text = "<color=#00FF00>"  + thePlayerB.ActerName + "</color>\n<color=#FF2400>" + systemValues.getTitleForPlayer ()+"</color>";
	}


}
