using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameStarter : MonoBehaviour {

	//这个脚本控制初始化
	public string theFighterName  = "";
	public Transform startPoint ;//游戏人物的初始点
	public cameraUse theCamera;//摄像机跟随控制单元
	public uiShowsForBasic theUIController;//UI刷新新信息控制单元
	GameObject theFighter ;
	public GameObject theForwardImage;//没有准备好就黑屏
	void Start()
	{
		Invoke ("makeStart", 2);
	}

	public  void makeStart()
	{
		theFighterName = systemValues.getNowPlayer ();

		if(systemValues.modeIndex == 1)//有些功能只在网络对战模式之下用就行
		theFighter  = PhotonNetwork.Instantiate("fighters/"+theFighterName , startPoint.transform.position, Quaternion.identity, 0);
		else if(systemValues.modeIndex == 0)
		theFighter = GameObject.Instantiate<GameObject>( Resources.Load<GameObject> ("fighters/"+theFighterName ));
		
		Invoke("makeNetStart",1f);
	}
	private void makeNetStart()
	{
		//theFighter.transform.position = startPoint.transform.position;

			PlayerBasic thePlayerPrivate = theFighter.GetComponent <PlayerBasic> ();

		    if (this.gameObject.tag != "AI") 
		    {
			   if (systemValues.thePlayer == null)
			   {
				systemValues.thePlayer = thePlayerPrivate;
				thePlayerPrivate.isMainfighter = true;
			  }
		    }
		    thePlayerPrivate.makeStart ();
		    thePlayerPrivate.GetComponent<attackLinkController> ().makeStart ();
		    theCamera.target = thePlayerPrivate.transform;
	     	thePlayerPrivate.GetComponent <move> ().makeStart ();
		    theUIController.makeStart ( thePlayerPrivate);
			theForwardImage.SetActive (false);//先不要删除，不知道什么时候还会用到
			this.GetComponent <MusicController> ().makeStart ();
	}
		
}
