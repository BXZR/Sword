﻿using UnityEngine;
using System.Collections;

public class informationMouseShow : MonoBehaviour {

	public  string showText; 
	public  bool information=false;
	float x;//状态显示的位置，x轴
	float y;//状态显示的位置，y轴
	public float height=40;
	public float width=70;
	public bool showTextHigh=false;

	void Start () 
	{
	}
	void OnGUI()  
	{ 
		if(information)//如果开始显示
		{

			if(!showTextHigh)
			{
				if(Input .mousePosition .y >0&&Input .mousePosition .x <Screen .width -150)
				{
					GUI .Box (new Rect(x+20,y, width,height),showText );//动态显示装备信息
				}
				else if(Input .mousePosition .y >0&&Input .mousePosition .x >Screen .width -150)
				{
					GUI .Box (new Rect(x-width-20,y, width,height),showText );//动态显示装备信息
				}
				else if(Input .mousePosition .y <0&&Input .mousePosition .x <Screen .width -150)
				{
					GUI .Box (new Rect(x+20,0, width,height), showText );//动态显示装备信息
				}
				else if(Input .mousePosition .y <0&&Input .mousePosition .x >Screen .width -150)
				{
					GUI .Box (new Rect(x-width-20,0, width,height), showText);//动态显示装备信息
				}
			}
			else
			{
				if(Input .mousePosition .y >0&&Input .mousePosition .x <Screen .width -150)
				{
					GUI .Box (new Rect(x,y-50, width,height),showText );//动态显示装备信息
				}
				else if(Input .mousePosition .y >0&&Input .mousePosition .x >Screen .width -150)
				{
					GUI .Box (new Rect(x-width,y-50, width,height),showText );//动态显示装备信息
				}
				else if(Input .mousePosition .y <0&&Input .mousePosition .x <Screen .width -150)
				{
					GUI .Box (new Rect(x+20,0, width,height), showText );//动态显示装备信息
				}
				else if(Input .mousePosition .y <0&&Input .mousePosition .x >Screen .width -150)
				{
					GUI .Box (new Rect(x-width-20,0, width,height), showText);//动态显示装备信息
				}
			}
			
		}
	}
	void   OnHover()
	{
		
		if(information==true)
		{
			//cursors.GetComponent <cursor >().setCursor (0);
			information=false;
			x=0;y=0;
		}//鼠标进入则显示
		
		else 
		{
			information=true;
		}
		
	}

	public void clicked()
	{
		information = !information;
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