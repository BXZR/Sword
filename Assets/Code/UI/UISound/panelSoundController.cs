using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelSoundController : MonoBehaviour {

	// 包含一堆音效的Panel
	public AudioClip[]  theClips;

	public void makeSoundShow(int index)
	{
		if(theClips.Length>index && systemValues.thePlayer)
			systemValues.thePlayer.theAudioPlayer.playClip (theClips[index]);
	}
}
