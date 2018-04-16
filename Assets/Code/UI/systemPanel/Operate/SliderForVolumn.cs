using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderForVolumn : MonoBehaviour {

	//这个脚本应用于调整某个AudioSource的Slider上面

	public AudioSource theSource;
	private Slider theSlider;

	void Start()
	{
		theSlider = this.GetComponent <Slider> ();
		if(theSource)
		    theSlider.value = theSource.volume;
	}

	public void OnChangeSliderValue()
	{
		theSource.volume = theSlider.value;
	}

	public void changeSoundVolumn()
	{
		if (systemValues.thePlayer)
			systemValues.thePlayer.theAudioPlayer.theSource.volume = theSlider.value;
	}
}
