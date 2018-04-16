using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleMessageBox : MonoBehaviour {

	//标题消息框，就是在界面上方显示的无法控制的消息框
	//内容简短无交互

	private int timer = 0;
	private int timerMax = 200;
	//全局唯一
	public static titleMessageBox theMessageSave;

	//一个背景图
	public Texture2D theBackPicture;

	//GUI方法需要的参数
	//所有的东西都是按照屏幕百分比来计算
	private float widthForScreen = 0.2f;
	private float heightForScreen = 0.05f;

	private string stringForInformation = "";
	//GUI 额外设定
	GUIStyle GUIShowStyleForInformation;
	GUIStyle GUIShowStyleForBack;

	//设定显示信息
	public void setInformation(string information = "" )
	{
		stringForInformation = information;

	}


	//消息框全局唯一
	private void makeAlone()
	{
		if (titleMessageBox.theMessageSave != null)
			Destroy (titleMessageBox.theMessageSave.gameObject);
		titleMessageBox.theMessageSave = this;

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
		Destroy (this.gameObject , 2f);
	}

	void Start()
	{
		makeAlone ();
		makeStart ();
	}


	void OnGUI()
	{ 
		timer ++;
		if (timer > timerMax)
			Destroy (this.gameObject);
		
		float startPointX = (1 - widthForScreen) * Screen.width / 2;
		float startPointY = (1 - heightForScreen) * Screen.height / 2;
		startPointY *= 0.1f;
		float width = widthForScreen * Screen.width;
		float height = heightForScreen* Screen.width;
		GUI.BeginGroup (new Rect (startPointX  ,startPointY , width, height));
		GUI.Box (new Rect (0, 0, width, height ), "" ,GUIShowStyleForBack );//背景
		GUI.Box (new Rect (width*0.1f, height* 0.2f , width*0.8f, height*3/5 ), stringForInformation , GUIShowStyleForInformation );//文本
		GUI.EndGroup ();
	}

}
