using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectHeadForStory : MonoBehaviour {

	private string theFightName = "";
	//显示游戏人物信息的文本
	private Text playerInformationText;

	//目标位置
	private Transform thetransForShow;
	//引用
	public   static GameObject therPlayer;
	//系统配置文件类的标记
	private int  indexForSystemValues;

	public GameObject theSelectedImage;

	public static   GameObject   theStaticSelectedImage = null;

	public void makeStart(string playerName , Transform  thetransForShowIn , Text showText , int indexForSystemValuesIn)
	{ 
		theFightName = playerName;
		thetransForShow = thetransForShowIn;
		indexForSystemValues = indexForSystemValuesIn;
		playerInformationText = showText;
	}


	public void makePlayer()
	{

		if (therPlayer != null)
		{
			//正在显示的单位没有必要重新创建一次
			if (theFightName + "(Clone)" == therPlayer.name)
				return;
			Destroy (therPlayer);
		}

		GameObject theProfab = Resources.Load<GameObject> ("fighters/"+theFightName);
		therPlayer = GameObject.Instantiate (theProfab );
		therPlayer.transform.position = thetransForShow .position;
		therPlayer.transform.localScale = new Vector3 (12f,12f,12f);
		therPlayer.AddComponent<extraRotate> ();
		therPlayer.AddComponent<fixPosition> ();

		playerInformationText.text = systemValues.getStorySimpleInformation (this.indexForSystemValues);
		systemValues.setIndexForPlayer (indexForSystemValues);

		if (theStaticSelectedImage!= null && theStaticSelectedImage!= theSelectedImage)
			theStaticSelectedImage.gameObject.SetActive (false);
		theSelectedImage.SetActive (true);
		theStaticSelectedImage = theSelectedImage;
	}

}
