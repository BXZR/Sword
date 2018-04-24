using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameStarter : MonoBehaviour {

	//这个脚本控制初始化
	public string theFighterName  = "";
	public Transform [] startPoint ;//游戏人物的初始点
	public cameraUse theCamera;//摄像机跟随控制单元
	public uiShowsForBasic theUIController;//UI刷新新信息控制单元
	GameObject theFighter ;
	public GameObject theForwardImage;//没有准备好就黑屏
	public GameObject theDeadPanel;//记录下来这个引用
	private MusicController theMusicController;//音乐控制单元
	void Start()
	{
		systemValues.theDeadPanel = theDeadPanel;
		theDeadPanel.SetActive (false);
		Invoke ("makeStart", 2.5f);
	}

	public  void makeStart()
	{
		theMusicController = this.GetComponent <MusicController> ();
		theMusicController.playBackMusic (systemValues.getBackMusicName(),false);
		theMusicController.makeStart ();

		theFighterName = systemValues.getNowPlayer ();

		if (systemValues.modeIndex == 1) //有些功能只在网络对战模式之下用就行
		{
			int indexUse = 0;
			try
			{
				indexUse = (PhotonNetwork.room.PlayerCount+1) % (startPoint.Length);
			}
			catch
			{
				indexUse = Random.Range (0,startPoint.Length);
			}


			theFighter = PhotonNetwork.Instantiate ("fighters/" + theFighterName, startPoint [0].transform.position, Quaternion.identity, 0);
			theFighter.transform.position = startPoint[indexUse].transform.position;
		}
		
		else if (systemValues.modeIndex == 0) 
		{
			//注意目前开始点不多，最多到6，否则会有重复
			int indexUse = 0;
			theFighter = GameObject.Instantiate<GameObject> (Resources.Load<GameObject> ("fighters/" + theFighterName));
			theFighter.transform.position = startPoint[indexUse].transform.position;
		}
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
				systemValues.thePlayerAnimator = thePlayerPrivate.GetComponentInChildren<Animator> ();
				thePlayerPrivate.isMainfighter = true;
				thePlayerPrivate.gameObject.AddComponent<BloodScaner> ();//这个游戏对象拥有观察血量的权利
			  }
		    }
		    //thePlayerPrivate.makeStart ();
		    thePlayerPrivate.makeStartForPrivate();
		    thePlayerPrivate.GetComponent<attackLinkController> ().makeStart ();
		    theCamera.target = thePlayerPrivate.transform;
		    theCamera.thePlayer = thePlayerPrivate;
	     	thePlayerPrivate.GetComponent <move> ().makeStart ();
		    theUIController.makeStart ( thePlayerPrivate);
			theForwardImage.SetActive (false);//先不要删除，不知道什么时候还会用到
		 
	}

	//额外整体控制
	//仅仅作为示例使用
//	void Update ()
//	{
//		if(Input .GetKeyDown(KeyCode .Z))
//		{
//			systemValues.thePlayer.addEffects ("effectHpUp");
//		}
//	}
		
}
