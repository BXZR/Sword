using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageInOut : MonoBehaviour {

	//开场和结束的时候的幕布效果
	private Image theImage;
	public float timer = 2.5f;


	void Start()
	{
		theImage = this.GetComponent<Image> ();
		makeStart ();
	}

	public void makeStart()
	{
		StartCoroutine (startUse());
	}

	public void makeEnd()
	{
		this.gameObject.SetActive (true);
		StartCoroutine (endUse());
	}

	IEnumerator startUse()
	{
		theImage.CrossFadeAlpha (1,0.01f , true);
		yield return new WaitForSeconds(0.01f);
		theImage.CrossFadeAlpha (0,timer , true);
		yield return new WaitForSeconds(timer);

		this.gameObject.SetActive (false);
	}

	IEnumerator endUse()
	{
		theImage.CrossFadeAlpha (0,0.01f , true);
		yield return new WaitForSeconds(0.01f);
		theImage.CrossFadeAlpha (1,timer , true);
		yield return new WaitForSeconds(timer);
	}
}
