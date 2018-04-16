using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderForSystemSetting : MonoBehaviour {

	//系统设定slider
    
	private Slider theSlider;

	//设定FPS上限
	public  void setFPS()
	{
		if (!theSlider)
			theSlider = this.GetComponent <Slider> ();
		Application.targetFrameRate = (int)theSlider.value;
		//print("FPS --> "+Application.targetFrameRate );
//		Application.targetFrameRate 
//		描述：
//		命令游戏尝试以一个特定的帧率渲染。
//		设置targetFrameRate为-1（默认）使独立版游戏尽可能快的渲染，并且web播放器游戏以50-60帧/秒渲染，取决于平台。
//		注意设置targetFrameRate不会保证帧率，会因为平台的不同而波动，或者因为计算机太慢而不能取得这个帧率。
//		在编辑器中targetFrameRate被忽略。
	}
}
