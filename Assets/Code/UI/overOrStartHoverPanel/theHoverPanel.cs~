using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class theHoverPanel : MonoBehaviour {

	//最上层的hover的界面
	//用来做最开始的缓冲界面和结束之后的总结界面
	//因此这个界面至少有两种模式

	private Text theShowStringText;//显示文字的text
	private Image theImage;//这个panel的背景图片
	private string theShowString = "";//显示的文字
	private string theNextSceneName = "allStartScene";
	private float timer = 4f;//操作时间

	public void makeTheStartMode()
	{
		
	}

	public void  makeTheEndMode(string thenformaiton = "" , bool isOver  = false  , string thisScnenName = "allStartScene" , string nextScnenName = "allStartScene")
	{
		theShowStringText = this.GetComponentInChildren <Text> ();
		theImage = this.GetComponent <Image> ();

		theShowStringText.text = "";
		theShowString = thenformaiton;
		//没有赢就重新来,否则就进入到下一个场景
		theNextSceneName = isOver ? nextScnenName : thisScnenName;
		//如果没有赢，剧本指针倒退
		if (!isOver)
			systemValues.flashStoryErrorMove ();
		
		StartCoroutine (startShow());
	}


	//=============================================================================//

	IEnumerator startShow()
	{
		theImage.CrossFadeAlpha (0,0.01f , true);
		yield return new WaitForSeconds(0.01f);
		theImage.CrossFadeAlpha (1,timer, true);
		yield return new WaitForSeconds(timer);
		theShowStringText.text = theShowString;
		yield return new WaitForSeconds(timer);
		try{ UnityEngine.SceneManagement.SceneManager.LoadScene (theNextSceneName); }
		catch { UnityEngine.SceneManagement.SceneManager.LoadScene ("allStartScene"); }
	}
}
