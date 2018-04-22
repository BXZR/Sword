using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class theMessageBoxPanel : MonoBehaviour {

	//全局唯一
	public static theMessageBoxPanel theMessageSave;

	//做一个有点意境的消息框吧
	//OnGUI模式之下这两个无效
	public Text theTitleText;
	public Text theInformationText;
	//一个背景图
	public Texture2D theBackPicture;

	//GUI方法需要的参数
	//所有的东西都是按照屏幕百分比来计算
	private float widthForScreen = 0.4f;
	private float heightForScreen = 0.24f;
	private string stringForTitle = "";
	private string stringForInformation = "";
	public bool isAutoResize = false;

	//有一个缩放的效果
	private bool isClosing = false;//开始还是关闭
	private float showPercent = 0.0f;
	private float showPercentAdder = 0.1f;

	//额外的定时显示
	private bool withTimer = false;
	private float timer = 15f;

	//GUI 额外设定
	GUIStyle GUIShowStyleForTitle;
	GUIStyle GUIShowStyleForInformation;
	GUIStyle GUIShowStyleForBack;
	public GUISkin theButtonGUISkin;

	//设定显示信息
	public void setInformation(string title = "", string information = "" )
	{
		theTitleText.text = title;
		theInformationText.text = information;
		stringForTitle = title;
		stringForInformation = information;
		if (isAutoResize) 
		{
			float arr = Mathf.Clamp( (float)stringForInformation.Split ('\n').Length / 7 ,1f,2.8f);
			setSize (new Vector2(1f , arr));
		}
		systemValues.isMessageBoxShowing = true;
	}
	//设定显示时间
	//不设定就是不用计时器
	public void setTimer ( float timerIn)
	{
		timer = timerIn;
		withTimer = true;
	}

	void OnDestroy()
	{
		systemValues.isMessageBoxShowing = false;
	}
		
	//自我销毁
	public void makeEnd()
	{
		isAutoResize = false;
		isClosing = false;
		enabled = false;
		showPercent = 0f;
		systemValues.isMessageBoxShowing = false;
	}
		
	//初始化
	private  void makeStart()
	{
		GUIShowStyleForTitle =new GUIStyle();
		GUIShowStyleForTitle.normal.textColor = Color.green;
		GUIShowStyleForTitle.fontStyle = FontStyle.Bold;
		GUIShowStyleForTitle.alignment = TextAnchor.UpperCenter;
		GUIShowStyleForTitle.fontSize = 19;

		GUIShowStyleForInformation=new GUIStyle();
		GUIShowStyleForInformation.normal.textColor = Color.yellow;
		GUIShowStyleForInformation.fontStyle = FontStyle.Bold;
		GUIShowStyleForInformation.alignment = TextAnchor.UpperCenter;
		GUIShowStyleForInformation.fontSize = 16;
		GUIShowStyleForInformation.wordWrap = true;

		GUIShowStyleForBack = new GUIStyle ();
		GUIShowStyleForBack.normal.background = theBackPicture;
	}

	void Start()
	{
		makeStart ();
	}

	void OnEnable()
	{
		canculateValuesForGUI ();
	}

	void Update()
	{
		if (withTimer) 
		{
			timer -= Time.deltaTime;
			if (timer < 0)
				makeEnd();
		}
	}

	//设定大小
	//用的是百分比
	public void setSize ( Vector2 theSize)
	{
		float widthSave = widthForScreen;
		float heightSave = heightForScreen;
		widthBasic *= theSize.x;
		heightBasic *= theSize.y;

		if (widthBasic< widthSave)
			widthBasic = widthSave;
		if (heightBasic < heightSave)
			heightBasic = heightSave;
	}

	//提前就把一些内容计算好了======================================================================================================
	float startPointXBasic = 10f;
	float startPointYBasic = 10f;
	float widthBasic = 10f;
	float heightBasic = 10f;
	private  void canculateValuesForGUI()
	{
		startPointXBasic = Screen.width * 0.5f;   
		startPointYBasic = Screen.height * 0.5f; 

		widthBasic = widthForScreen * Screen.width;
		heightBasic = heightForScreen* Screen.width;
	}
	//============================================================================================================================
	//messageOnGUI的做法
	void OnGUI()
	{ 
		//print ("show the messageBox");
		float startPointX = startPointXBasic * (1 - widthForScreen*showPercent);
		float startPointY = startPointYBasic * (1 - widthForScreen*showPercent);
		startPointY *= 0.4f;//稍微向上移动一点
		float width = widthBasic*showPercent;
		float height = heightBasic*showPercent;

		GUI.BeginGroup (new Rect (startPointX  ,startPointY , width, height));
		GUI.Box (new Rect (0, 0, width, height ), "" ,GUIShowStyleForBack );//背景
		GUI.Box (new Rect (width/3,  height * 0.05f , width/3, height*0.12f ), stringForTitle ,GUIShowStyleForTitle);//标题
		GUI.Box (new Rect (width*0.1f, height* 0.2f , width*0.8f, height*3/5 ), stringForInformation , GUIShowStyleForInformation);//文本
		string showOnButton = withTimer? "我已知晓("+timer.ToString("f0")+")" : "我已知晓";
		if (GUI.Button (new Rect (width * 2 / 5, Mathf.Max( height * 4 / 5, height-80), width / 5, 40), showOnButton , theButtonGUISkin.button)) {makeEnd ();}

		GUI.EndGroup ();

		if(showPercent  < 1 && !isClosing)
			showPercent += showPercentAdder;

		if(isClosing)
			showPercent -= showPercentAdder;
	}

}
