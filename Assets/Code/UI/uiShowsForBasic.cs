using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiShowsForBasic : MonoBehaviour {

	//这个脚本用来表现HPSP头像等等的基础内容
	public Slider theHpSlider;
	public Slider theSpSlider;
	public Slider theHpBackSlider;
	public Image thePlayerImage;
	public Text thePlayerNameText;//用来显示名字
	private  PlayerBasic thePlayer;
	private bool isStarted = false;//只有在开始之后才会刷心
	public void makeStart(PlayerBasic thePlayer)
	{
		this.thePlayer = thePlayer;
		string headName = systemValues.getHeadPictureName (thePlayer.ActerName);
		try
		{
		  thePlayerImage.sprite = makeLoadSprite ("playerHeadPicture/"+headName);
		}
		catch
		{
			thePlayerImage.gameObject.SetActive (false);//如果没有图就干脆就不显示吧
		}
		thePlayerNameText.text = makeNameText (this.thePlayer.ActerName);
		isStarted = true;
		//数值更新都是用这个方法更新的，可以大量减少额外的update计算
		InvokeRepeating ("makeUpdate", 0, systemValues.updateTimeWait);

	}
	//插值流血
	private void makeLoseShow()
	{
		//插值法计算失血
		//并且，只有在失血的时候才会生效
		//其中0.4是插值的速度
		if (theHpBackSlider) 
		{
			if (theHpBackSlider.value > theHpSlider.value)
				theHpBackSlider.value = Mathf.Lerp (theHpBackSlider.value, theHpSlider.value, 0.1f);
			else
				theHpBackSlider.value = theHpSlider.value;
		}
	}	
	//加载图像
	public Sprite makeLoadSprite(string textureName)
	{
		//textureName = "people/noOne";
		Texture2D theTextureIn = Resources.Load <Texture2D> (textureName);
		return Sprite .Create(theTextureIn,new Rect (0,0,theTextureIn.width,theTextureIn.height),new Vector2 (0,0));
	}
	//实时刷新
	void makeUpdate () 
	{
		if (isStarted) 
		{
			theHpSlider.value = thePlayer.ActerHp / thePlayer.ActerHpMax;
			theSpSlider.value = thePlayer.ActerSp / thePlayer.ActerSpMax;
			makeLoseShow ();
		}
	}

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
