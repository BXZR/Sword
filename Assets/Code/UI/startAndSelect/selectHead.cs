using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

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

	UIStateShowImage [] theImagesFrState;//多边形状态图，包含两个部分，背景图和前景图

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

	public void setStateImage(GameObject theImage)
	{
		theImagesFrState = theImage.GetComponentsInChildren<UIStateShowImage> ();
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
		playerTitleText.text = makeTitleText(thePlayerB);
		systemValues.setIndexForPlayer (indexForSystemValues);

		if (theStaticSelectedImage!= null)
			theStaticSelectedImage.gameObject.SetActive (false);
		theSelectedImage.SetActive (true);
		theStaticSelectedImage = theSelectedImage;

		EffectInformations = systemValues.getEffectInformationsMore (therPlayer,true);
		//print ("EffectInformations Count = "+EffectInformations.Count);
		makePlayerAttackAndEffect ();
		makeStateImage ();
	}

	//本来就卡，能少点GC就少一点吧......
	private string makeTitleText(PlayerBasic thePlayerB)
	{
		StringBuilder theStringBuilder = new StringBuilder ();
		theStringBuilder.Append (systemValues.playerNameColor);
		theStringBuilder.Append (thePlayerB.ActerName);
		theStringBuilder.Append (systemValues.colorEnd);
		theStringBuilder.Append ("\n");
		theStringBuilder.Append (systemValues.playerIntroductionColor);
		theStringBuilder.Append (systemValues.getTitleForPlayer (indexForSystemValues));
		theStringBuilder.Append (systemValues.colorEnd);
		return theStringBuilder.ToString ();
	}

	private void makeStateImage()
	{
		if (therPlayer) 
		{
			playerStar thePlayerStar = therPlayer.GetComponent <playerStar> ();
			if (thePlayerStar) 
			{
				foreach (UIStateShowImage S in theImagesFrState) 
				{
					S.makeClear ();
					S.makeDrawing (thePlayerStar.theValues, thePlayerStar.theTitles);
				}
			}
		} 
		else 
		{
			foreach (UIStateShowImage S in theImagesFrState) 
			{
				S.makeClear ();
				S.makeDrawing (new List<float> (), new List<string> ());
			}
		}

	}

	public void makePlayerAttackAndEffect()
	{
		skillEffectShowingItem[] olds = theshowContantFortheAttackEffectItem.GetComponentsInChildren<skillEffectShowingItem> ();
		if (EffectInformations.Count <= olds.Length) 
		{
			for (int i = 0; i < olds.Length; i++) 
			{
				if (i < EffectInformations.Count)
				{
					olds [i].makeClean ();
					olds[i].maketheItem (EffectInformations [i]);
				} 
				else 
				{
					Destroy (olds [i].gameObject);
				}
			}
		} 
		else
		{
			for (int i = 0; i < EffectInformations.Count; i++) 
			{
				if (i < olds.Length) 
				{
					olds [i].makeClean ();
					olds [i].maketheItem (EffectInformations [i]);
				} 
				else 
				{
					GameObject theItem = GameObject.Instantiate<GameObject> (theAttackEffectItemProfab);
					theItem.transform.SetParent (theshowContantFortheAttackEffectItem);
					theItem.transform.localPosition = new Vector3 (0,0,0);
					theItem.transform.localScale = new Vector3 (1,1,1);
					theItem.GetComponent <skillEffectShowingItem> ().maketheItem (EffectInformations[i]);
				}
			}
		}

//		skillEffectShowingItem[] olds = theshowContantFortheAttackEffectItem.GetComponentsInChildren<skillEffectShowingItem> ();
//		for (int i = 0; i < olds.Length; i++)
//			Destroy (olds[i].gameObject);
//		
//		for (int i = 0; i < EffectInformations.Count; i++)
//		{
//			GameObject theItem = GameObject.Instantiate<GameObject> (theAttackEffectItemProfab);
//			theItem.transform.SetParent (theshowContantFortheAttackEffectItem);
//			theItem.transform.localPosition = new Vector3 (0,0,0);
//			theItem.transform.localScale = new Vector3 (1,1,1);
//			theItem.GetComponent <skillEffectShowingItem> ().maketheItem (EffectInformations[i]);
//		}
	}
 

}
