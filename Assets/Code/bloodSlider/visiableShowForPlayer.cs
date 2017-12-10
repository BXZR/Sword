using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visiableShowForPlayer : MonoBehaviour {

	//非主要任务的头顶血条只有在被看到的时候才会真正地被激活
	//需要meshrenderer
	PlayerBasic thePlayer;
	Renderer theRender;
	//修改成定好的情况，减少一点代码负担吧
	public Transform theHPSlider;
	public Transform theHPSliderBack;
	float XStart  = 5;

	void Start () 
	{
		Invoke ("makeStart",3f);
	}


	public  void makeStart()
	{
		thePlayer = this.transform.root.GetComponent<PlayerBasic> ();

		if (thePlayer.isMainfighter)
		{
			Destroy (theHPSlider.gameObject);
			Destroy (theHPSliderBack.gameObject);
			Destroy (this);
		} 
		else
		{
			theRender = this.GetComponentInChildren <Renderer> ();
			XStart = theHPSlider.transform.localScale.x;
			InvokeRepeating ("hpUpdate", 0.5f, 0.1f);
		}
	}
	private void hpUpdate()
	{
		//方法2 SpriteRenderer方法
		if (theHPSlider && theRender.isVisible)
		{
			theHPSliderBack.transform.rotation = Camera.main.transform.rotation;
			theHPSlider.transform.rotation = Camera.main.transform.rotation;
			float XValue = XStart * thePlayer.ActerHp / thePlayer.ActerHpMax;
			XValue = Mathf.Clamp (XValue , 0,1);
			theHPSlider.transform.localScale = new Vector3 (XValue, 1,1);
		}
	}

	void Update () 
	{
		//方法1，GUI方法，因为不存在遮挡，有点不想用
		//thePlayer.isShowing = theRender.isVisible;
	}
}

 