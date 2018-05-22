using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class theDeadPanel : MonoBehaviour {

	public Image theShowingImage;
	private Image theImage;
	private Text theText;
	float timer = 4f;
	string theTextToShow = "";
	public void makeStart ()
	{
		theImage = this.GetComponent <Image> ();
		theText = this.GetComponentInChildren<Text> ();
		theText.text = "";
		theTextToShow = "胜败，不过是兵家常事,\n我们可以，还可以从头再来。";
		StartCoroutine (startShow());
	}
	public void makeStart(string thenformaiton , bool isOver  = false)
	{
		if (isOver)//完成了就不用发表遗憾感言了
			theShowingImage.enabled = false;
		
		theImage = this.GetComponent <Image> ();
		theText = this.GetComponentInChildren<Text> ();
		theText.text = "";
		theTextToShow = thenformaiton;
		StartCoroutine (startShow());
	}
	
	IEnumerator startShow()
	{
		theImage.CrossFadeAlpha (0,0.01f , true);
		yield return new WaitForSeconds(0.01f);
		theImage.CrossFadeAlpha (1,timer , true);
		yield return new WaitForSeconds(timer);
		theText.text = theTextToShow;
		yield return new WaitForSeconds(timer);
		UnityEngine.SceneManagement.SceneManager.LoadScene ("allStartScene");
	}
}
