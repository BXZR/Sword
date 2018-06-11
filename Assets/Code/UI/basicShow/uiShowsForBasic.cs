using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiShowsForBasic : MonoBehaviour {

	//这个脚本用来表现HPSP头像等等的基础内容
	public Slider theHpSlider;
	public Slider theSpSlider;
	public Slider theShieldSlider;
	public Slider theHpBackSlider;
	public Image thePlayerImage;
	public Image jingyanImage;
	public Text thePlayerNameText;//用来显示名字
	public Text theLvText;//用来显示等级
	private  PlayerBasic thePlayer;

	public void makeStart(PlayerBasic thePlayer)
	{
		this.thePlayer = thePlayer;
		string headName = systemValues.getHeadPictureName (thePlayer.ActerName);
		try
		{
			thePlayerImage.sprite = systemValues.makeLoadSprite ("playerHeadPicture/"+headName);
		}
		catch
		{
			thePlayerImage.gameObject.SetActive (false);//如果没有图就干脆就不显示吧
		}
		thePlayerNameText.text = makeNameText (this.thePlayer.ActerName);

		Cursor.visible = false;//不显示鼠标

		//数值更新都是用这个方法更新的，可以大量减少额外的update计算
		InvokeRepeating ("makeUpdate", 0, systemValues.updateTimeWait );//需要完全实时显示的BasicUI
		InvokeRepeating ("makeFakeUpdate", 0, systemValues.updateTimeWait * 3f );//其实并不需要完全实时显示的BasicUI

	}
	//插值流血
	private void makeLoseShow()
	{
		//插值法计算失血
		//并且，只有在失血的时候才会生效
		//其中0.2是插值的速度
		if (theHpBackSlider) 
		{
			if (theHpBackSlider.value > theHpSlider.value)
				theHpBackSlider.value = Mathf.Lerp (theHpBackSlider.value, theHpSlider.value, 0.2f);
			//else
				//theHpBackSlider.value = theHpSlider.value;
		}
	}	

	//刷新方法-----------------------------------------------------------------------------
	//实时刷新
	void makeUpdate () 
	{
		theHpSlider.value = thePlayer.ActerHp / thePlayer.ActerHpMax;
		theSpSlider.value = thePlayer.ActerSp / thePlayer.ActerSpMax;
		theShieldSlider.value = thePlayer.ActerShieldHp / thePlayer.ActerHpMax;
		makeLoseShow ();
	}
	//伪实时刷新
	void  makeFakeUpdate()
	{
		jingyanImage.fillAmount = thePlayer.jingyanNow / thePlayer.jingyanMax;
		theLvText.text = thePlayer.playerLv.ToString();//"Lv." + thePlayer.playerLv;
	}

	//刷新方法-----------------------------------------------------------------------------

	string makeNameText (string playerName)
	{
		string showName = "";
		for (int i = 0; i < playerName.Length; i++) 
		{
			for(int j = 0 ; j < i; j++)
				showName += " ";
			showName += playerName [i];
			showName += "\n";
		}
		return showName;
	}
}
