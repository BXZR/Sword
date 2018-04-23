using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicPanelController : MonoBehaviour {

	//这个脚本处理基础界面BasicPanel的处理
	//显示人物当前的基本属性

	public Image theHeadImage;//人物的头像
	public Text thePlayerName;//人物的名字
	public Slider theHpSlider;//生命值 + 护盾值
	public Slider theSPSLider;//斗气数
	public Text theHpText;//生命值 + 护盾值
	public Text theSpText;//斗气值
	public Text theExtraValueInformationText;//其余的战斗数值信息显示
	public Text theBasicEffectInformationText;//战斗被动特性显示

	public GameObject theStateImageB;//多边形状态图
	public GameObject theStateImageF;//多边形状态图

	private bool loaded = false;//是否已经加载完成，加载时只需要做一次就可以了
	//头像图等等一些内容只需要加载一次就可以了
	private  void  makeLoad()
	{
		if ( loaded == false && systemValues.thePlayer != null) 
		{
			PlayerBasic thePlayer = systemValues.thePlayer;
			string headName = systemValues.getHeadPictureName (thePlayer.ActerName);
			try 
			{
				theHeadImage.sprite = systemValues.makeLoadSprite ("playerHeadPicture/" + headName);
			}
			catch 
			{
				theHeadImage.gameObject.SetActive (false);//如果没有图就干脆就不显示吧
			}
			loaded = true;
		}
	}

	void Start()
	{
		makeLoad ();
	}


	void  showStateImage(GameObject thePlayer)
	{
		playerStar thePlayerStar = thePlayer.GetComponent <playerStar> ();
		UIStateShowImage BB = theStateImageB . GetComponent <UIStateShowImage> ();
		BB .makeClear ();
		BB.makeDrawing (thePlayerStar.theValues, thePlayerStar.theTitles);
		UIStateShowImage FF = theStateImageF . GetComponent <UIStateShowImage> ();
		FF .makeClear ();
		FF .makeDrawing (thePlayerStar.theValues, thePlayerStar.theTitles);

//		UIStateShowImage [] theImagesFrState =  theStateImage.GetComponentsInChildren<UIStateShowImage> ();
//		if (thePlayer) 
//		{
//			playerStar thePlayerStar = thePlayer.GetComponent <playerStar> ();
//			if (thePlayerStar) 
//			{
//				foreach (UIStateShowImage S in theImagesFrState) 
//				{
//					S.makeClear ();
//					S.makeDrawing (thePlayerStar.theValues, thePlayerStar.theTitles);
//				}
//			}
//		} 
//		else 
//		{
//
//			foreach (UIStateShowImage S in theImagesFrState) 
//			{
//				S.makeClear ();
//				S.makeDrawing (new List<float> (), new List<string> ());
//			}
//		}

	}

	void OnEnable () 
	{
		if(systemValues.thePlayer)
		{	
			thePlayerName.text = systemValues.thePlayer.ActerName +"  (Lv."+systemValues.thePlayer.playerLv+")";
			theExtraValueInformationText.text = systemValues.thePlayer.getPlayerInformation (false);
			float theHpValue = systemValues.thePlayer.ActerHp / systemValues.thePlayer.ActerHpMax;
			float theSpValue = systemValues.thePlayer.ActerSp / systemValues.thePlayer.ActerSpMax;
			theHpSlider.value = theHpValue;//生命值 + 护盾值
			theSPSLider.value = theSpValue;//斗气数
			theHpText.text = systemValues.thePlayer.ActerHp.ToString("f0")  +"+" +systemValues.thePlayer.ActerShieldHp.ToString("f0") +"/"+ systemValues.thePlayer.ActerHpMax.ToString("f0");//生命值+ 护盾值
			theSpText.text = systemValues.thePlayer.ActerSp.ToString("f0") +"/"+ systemValues.thePlayer.ActerSpMax.ToString("f0");//斗气值 
			//自己的技能效果获得一次
			theBasicEffectInformationText.text = systemValues.getBasicBEEffectInformation () ;
			showStateImage (systemValues.thePlayer.gameObject);
		}

	}
	
   
}
