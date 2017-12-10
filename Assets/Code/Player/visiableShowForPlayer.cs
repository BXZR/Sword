using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visiableShowForPlayer : MonoBehaviour {

	//非主要任务的头顶血条只有在被看到的时候才会真正地被激活
	//需要meshrenderer
	PlayerBasic thePlayer;
	Renderer theRender;

	Transform theHPSlider;

	void Start () 
	{
		thePlayer = this.transform.root.GetComponent<PlayerBasic> ();
		theRender = this.GetComponent <Renderer> ();
		try
		{
			theHPSlider = this.transform.root.Find ("HPShow");
		}
		catch
		{
		}
	}
		
	void Update () 
	{
		//方法1，GUI方法，因为不存在遮挡，有点不想用
		//thePlayer.isShowing = theRender.isVisible;
		//方法2 SpriteRenderer方法
		if (theHPSlider)
		{
			theHPSlider.transform.rotation = Camera.main.transform.rotation;
			theHPSlider.transform.localScale = new Vector3 (thePlayer.ActerHp/thePlayer.ActerHpMax , 1,1);
		}
	}
}

 