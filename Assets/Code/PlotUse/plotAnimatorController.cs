using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plotAnimatorController : MonoBehaviour , plotActions  {

	//每一个剧本帧的音效控制单元
	public GameObject thePlayer;
	//动作名称
	public string theStateName;
	//结束的时候的动作名称（可以为空）
	public string theStateNameForEnd;
	public bool loop = false;
	private Animator theAnimator;
	//没办法，接口默认pulic
	//开始的时候统一调用
	public  void OnStart (plotItem theItem)
	{
		theAnimator = thePlayer.GetComponentInChildren<Animator> ();
		if (theAnimator)
			theAnimator.Play (theStateName);
	}

	//结束的时候统一调用
	public  void OnEnd()
	{
	}

	//每一帧更新的时候统一调用
	public  void OnUpdate()
	{
		if (theAnimator && loop)
			theAnimator.Play (theStateName);
	}

	public void OnControlEnd()
	{
		if (theAnimator && string.IsNullOrEmpty(theStateNameForEnd) == false)
			theAnimator.Play (theStateNameForEnd);
	}

}
