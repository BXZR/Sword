using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameIntroductionPanel : MonoBehaviour {


	private int index = 0;
	public Image theShowImage;
	private List<Sprite> thePictures;

	public void makeShow()
	{
		List<Sprite> theSprites = systemValues.getGameIntroduction ();
		if (theSprites.Count <= 0)
			return;

		thePictures = theSprites;
		index = 0;
		theShowImage.sprite = theSprites [index];
		this.gameObject.SetActive (true);
	}


	public void add()
	{
		index++;
		if (index >= thePictures.Count)
			index = 0;
		theShowImage.sprite = thePictures[index];
	}

	public void minus()
	{
		//这个就不回退了
		index--;
		if (index < 0)
			index = 0;
		theShowImage.sprite = thePictures[index];
	}

	public void makeEnd()
	{
		this.gameObject.SetActive (false);
	}



}
