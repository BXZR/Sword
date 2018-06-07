using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class plotSoundController : MonoBehaviour , plotActions  {

	//每一个剧本帧的音效控制单元
	public AudioClip theClip;
	private AudioSource theSouce;
	void Start ()
	{
		theSouce = this.GetComponent <AudioSource> ();
		theSouce.volume = 0.45f;
	}
	
	//没办法，接口默认pulic
	//开始的时候统一调用
	public  void OnStart (plotItem theItem)
	{
		if (theSouce)
			theSouce.PlayOneShot (theClip);
	}

	//结束的时候统一调用
	public  void OnEnd()
	{
	}

	//每一帧更新的时候统一调用
	public  void OnUpdate()
	{
	}

	//强制到达结束状态
	public void OnControlEnd()
	{

	}

}
