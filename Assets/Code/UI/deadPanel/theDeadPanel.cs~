using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class theDeadPanel : MonoBehaviour {

	private Image theImage;
	private Text theText;
	float timer = 4f;

	public void makeStart ()
	{
		theImage = this.GetComponent <Image> ();
		theText = this.GetComponentInChildren<Text> ();
		theText.text = "";
		StartCoroutine (startShow());
	}
	
	IEnumerator startShow()
	{
		theImage.CrossFadeAlpha (0,0.01f , true);
		yield return new WaitForSeconds(0.01f);
		theImage.CrossFadeAlpha (1,timer , true);
		yield return new WaitForSeconds(timer);
		theText.text = "胜败，不过是兵家常事,\n我们可以，还可以从头再来。";
		yield return new WaitForSeconds(timer);
		UnityEngine.SceneManagement.SceneManager.LoadScene ("allStartScene");
	}
}
