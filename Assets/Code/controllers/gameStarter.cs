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
		theFighter  = PhotonNetwork.Instantiate("fighters/"+theFighterName , startPoint.transform.position, Quaternion.identity, 0);

		//GameObject theFighter = GameObject.Instantiate<GameObject>( Resources.Load<GameObject> ("fighters/"+theFighterName ));
		Invoke("makeNetStart",1f);
	}
	private void makeNetStart()
	{
		//theFighter.transform.position = startPoint.transform.position;
		systemValues.thePlayer = theFighter.GetComponent <PlayerBasic> ();
		systemValues.thePlayer.makeStart ();
		systemValues.thePlayer.GetComponent<attackLinkController> ().makeStart ();
		theCamera.target = systemValues.thePlayer.transform;
		systemValues.thePlayer.GetComponent <move> ().makeStart ();
		theUIController.makeStart (systemValues.thePlayer);
		theForwardImage.SetActive (false);//先不要删除，不知道什么时候还会用到
		this.GetComponent <MusicController>().makeStart();
	}
		
}
