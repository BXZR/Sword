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

	public  void makeStart()
	{
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
	}
		
}
