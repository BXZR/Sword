using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleMessageBox : MonoBehaviour {

	//标题消息框，就是在界面上方显示的无法控制的消息框
	//内容简短无交互

	float timer = 0f;
	float timerMax = 140f;

	//全局唯一
	public static titleMessageBox theMessageSave;

	//一个背景图
	public Texture2D theBackPicture;

	//GUI方法需要的参数
	//所有的东西都是按照屏幕百分比来计算
	private float widthForScreen = 0.3f;
	private float heightForScreen = 0.05f;

	private string stringForInformation = "";
	//GUI 额外设定
	GUIStyle GUIShowStyleForInformation;
	GUIStyle GUIShowStyleForBack;

	//显示信息的计算大多数都应该是立即数=====================================
	float startPointX = 100f;
	float startPointY = 100f;
	float width = 100f;
	float height = 100f;

	float left = 0f;
	float top = 0f;
	float right = 10f;
	float buttom = 10f;
	Rect theOverAllBackRect;
	Rect TheBackGroundRect;
	Rect theTextRect;

	void canculateShowValues()
	{
		startPointX = (1 - widthForScreen) * Screen.width / 2;
		startPointY = (1 - heightForScreen) * Screen.height / 2;
		startPointY *= 0.1f;
		width = widthForScreen * Screen.width;
		height = heightForScreen* Screen.width;

		left = width * 0.1f;
		top = height * 0.2f;
		right = width * 0.8f;
		buttom = height * 0.6f;

		theOverAllBackRect = new Rect (startPointX  ,startPointY , width, height);
		TheBackGroundRect = new Rect (0, 0, width, height );//背景
		theTextRect = new Rect (left , top ,right ,buttom );//文本

	}
	//=========================================================================
	//设定显示信息
	public void setInformation(string information = "" )
	{
		stringForInformation = information;
		timer = 0f;
	}
		
	//初始化
	private  void makeStart()
	{
		GUIShowStyleForInformation=new GUIStyle();
		GUIShowStyleForInformation.normal.textColor = Color.yellow;
		GUIShowStyleForInformation.fontStyle = FontStyle.Bold;
		GUIShowStyleForInformation.alignment = TextAnchor.MiddleCenter;
		GUIShowStyleForInformation.fontSize = 16;
		GUIShowStyleForInformation.wordWrap = true;

		GUIShowStyleForBack = new GUIStyle ();
		GUIShowStyleForBack.normal.background = theBackPicture;
	}


	void makeEnd()
	{
		CancelInvoke ();
		timer = 0f;
		this.enabled = false;
	}


	void OnEnable()
	{
		canculateShowValues ();
	}

	void Start()
	{
		makeStart ();
	}
		
	void OnGUI()
	{ 		
		timer ++;
		if (timer > timerMax) 
			makeEnd ();

		GUI.BeginGroup (theOverAllBackRect);
		GUI.Box (TheBackGroundRect, "" ,GUIShowStyleForBack );//背景
		GUI.Box (theTextRect, stringForInformation , GUIShowStyleForInformation );//文本
		GUI.EndGroup();
	}

}
