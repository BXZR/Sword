using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipPersonForAllInforamtion : MonoBehaviour {

  //查询当前游戏人物的基础信息的按钮
	public void makeClick()
	{
		if (systemValues.thePlayer)
		{
			string theShowString = systemValues.thePlayer.getPlayerInformation ();
			systemValues.messageBoxShow ("[人物信息]",theShowString , true);
		}
	}
}
