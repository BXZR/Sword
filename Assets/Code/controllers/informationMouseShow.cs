using UnityEngine;
using System.Collections;

public class informationMouseShow : MonoBehaviour {

	public  string showText; 
	public  bool information=false;
	float x;//状态显示的位置，x轴
	float y;//状态显示的位置，y轴
	public float height=40;
	public float width=70;
	public bool showTextHigh=false;

	public static informationMouseShow theShowingOne = null;
	GUIStyle theGUIStyle = new GUIStyle();

	public void setShow()
	{
		if (theShowingOne != null)
			theShowingOne.information = false;

		theShowingOne = this;
		information = true;

		Texture2D theBack = new Texture2D ((int)width,(int)height);
		theGUIStyle.normal.background = theBack;
		theGUIStyle.normal.textColor=new Color(0,0,0);   //设置字体颜色的
		theGUIStyle.fontSize = 15;       //当然，这是字体大小       GUIStyle bb=new GUIStyle();
	}

	void OnGUI()  
	{ 
		if(information)//如果开始显示
		{

			if(!showTextHigh)
			{
				if(Input .mousePosition .y >0&&Input .mousePosition .x <Screen .width -150)
				{
					GUI .Box (new Rect(x+40,y, width,height),showText,theGUIStyle );//动态显示装备信息
				}
				else if(Input .mousePosition .y >0&&Input .mousePosition .x >Screen .width -150)
				{
					GUI .Box (new Rect(x-width-40,y, width,height),showText,theGUIStyle );//动态显示装备信息
				}
				else if(Input .mousePosition .y <0&&Input .mousePosition .x <Screen .width -150)
				{
					GUI .Box (new Rect(x+40,0, width,height), showText,theGUIStyle );//动态显示装备信息
				}
				else if(Input .mousePosition .y <0&&Input .mousePosition .x >Screen .width -150)
				{
					GUI .Box (new Rect(x-width-40,0, width,height), showText,theGUIStyle);//动态显示装备信息
				}
			}
			else
			{
				if(Input .mousePosition .y >0&&Input .mousePosition .x <Screen .width -150)
				{
					GUI .Box (new Rect(x,y-50, width,height),showText ,theGUIStyle);//动态显示装备信息
				}
				else if(Input .mousePosition .y >0&&Input .mousePosition .x >Screen .width -150)
				{
					GUI .Box (new Rect(x-width,y-50, width,height),showText,theGUIStyle );//动态显示装备信息
				}
				else if(Input .mousePosition .y <0&&Input .mousePosition .x <Screen .width -150)
				{
					GUI .Box (new Rect(x+40,0, width,height), showText ,theGUIStyle);//动态显示装备信息
				}
				else if(Input .mousePosition .y <0&&Input .mousePosition .x >Screen .width -150)
				{
					GUI .Box (new Rect(x-width-40,0, width,height), showText ,theGUIStyle);//动态显示装备信息
				}
			}
			
		}
	}

	void Update () 
	{
	
		if(Input .GetMouseButtonDown (1))
		{
			information =false;
		}
		if(information)//背包里面有装备
		{
			x= Input .mousePosition .x ;//获取鼠标位置
			y=Screen .height - Input .mousePosition .y  ;//获取 鼠标位置
			 
		}
	}
}
