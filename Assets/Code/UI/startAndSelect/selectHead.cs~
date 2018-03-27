using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectHead : MonoBehaviour {

	//选择人物的时候点选的按钮
	private string theFightName = "";//对应的战士的名称
	//显示游戏人物信息的文本
	private Text playerInformationText;
	//显示游戏人物信息的文本
	private Text playerTitleText;

	//目标位置
	private Transform thetransForShow;
	//引用
	public   static GameObject therPlayer;
	//系统配置文件类的标记
	private int  indexForSystemValues;

	//显示游戏人物技能的item
	public GameObject theAttackEffectItemProfab;
	//显示游戏人物技能的位置
	public Transform theshowContantFortheAttackEffectItem;

	public GameObject theSelectedImage;

	public static   GameObject   theStaticSelectedImage = null;

	List < attackLinkInformation> EffectInformations = new List<attackLinkInformation> ();

	public void makeStart(string playerName , Text theTitleText , Text theDetailText , 
	GameObject  theAttackEffectItemProfabIn , Transform theshowContantFortheAttackEffectItemIn ,Transform thetransForShowIn , int indexForSystemValuesIn)
	{ 
		theFightName = playerName;
		playerInformationText = theDetailText;
		playerTitleText = theTitleText;
		thetransForShow = thetransForShowIn;
		indexForSystemValues = indexForSystemValuesIn;

		theAttackEffectItemProfab = theAttackEffectItemProfabIn;
		theshowContantFortheAttackEffectItem = theshowContantFortheAttackEffectItemIn;

	}


	public void makePlayer()
	{
		if(therPlayer!=null)
			Destroy(therPlayer);
		
		GameObject theProfab = Resources.Load<GameObject> ("fighters/"+theFightName);
		therPlayer = GameObject.Instantiate (theProfab );
		therPlayer.transform.position = thetransForShow .position;
		therPlayer.transform.localScale = new Vector3 (4,4,4);
		therPlayer.AddComponent<extraRotate> ();
		therPlayer.AddComponent<fixPosition> ();
		PlayerBasic thePlayerB = therPlayer.GetComponent<PlayerBasic> ();
		playerInformationText.text = thePlayerB.getPlayerInformation () + thePlayerB.getPlayerInformationExtra ();
		playerTitleText.text = systemValues.playerNameColor  + thePlayerB.ActerName + systemValues.colorEnd+"\n"+systemValues.playerIntroductionColor + systemValues.getTitleForPlayer (indexForSystemValues)+systemValues.colorEnd;
		systemValues.setIndexForPlayer (indexForSystemValues);

		if (theStaticSelectedImage!= null)
			theStaticSelectedImage.gameObject.SetActive (false);
		theSelectedImage.SetActive (true);
		theStaticSelectedImage = theSelectedImage;

		EffectInformations = systemValues.getEffectInformationsMore (therPlayer,true);
		print ("EffectInformations Count = "+EffectInformations.Count);
		makePlayerAttackAndEffect ();
	}

	public void makePlayerAttackAndEffect()
	{
		skillEffectShowingItem[] olds = theshowContantFortheAttackEffectItem.GetComponentsInChildren<skillEffectShowingItem> ();
		for (int i = 0; i < olds.Length; i++)
			Destroy (olds[i].gameObject);
		
		for (int i = 0; i < EffectInformations.Count; i++)
		{
			GameObject theItem = GameObject.Instantiate<GameObject> (theAttackEffectItemProfab);
			theItem.transform.SetParent (theshowContantFortheAttackEffectItem);
			theItem.transform.localPosition = new Vector3 (0,0,0);
			theItem.transform.localScale = new Vector3 (1,1,1);
			theItem.GetComponent <skillEffectShowingItem> ().maketheItem (EffectInformations[i]);
		}
	}
 

}
