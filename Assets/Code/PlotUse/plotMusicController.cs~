using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class plotMusicController : MonoBehaviour , plotActions  {

	//每一个剧本帧的音效控制单元
	public AudioClip theClip;
	private AudioSource theSouce;
	private static AudioSource  theSouceForOne ;//音乐播放器只有一个，但是是可以替换的
	void Start ()
	{
		theSouce = this.GetComponent <AudioSource> ();
	}

	//没办法，接口默认pulic
	//开始的时候统一调用
	public  void OnStart (plotItem theItem)
	{
		if (theSouce && theClip)
		{
			if (theSouceForOne)
				theSouceForOne.Stop ();
			theSouceForOne = theSouce;
			theSouceForOne.clip = theClip;
			theSouceForOne.loop = true;
			theSouceForOne.Play ();
		}
		print ("print the back music");
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
