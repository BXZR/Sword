using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class equipSelectTypeButton : MonoBehaviour {

	//这个类用来描述背包里面的东西的分类显示
	public Transform theViewFather;//显示按钮的容器
	public equiptype theTypeSelect;//类别
	public GameObject theShowingButtonProfab;//显示按钮的预设物，需要各种初始化
	//按下按钮选择
	public void makeClickWithType()
	{
        //前期清理工作
		equipShowingButton []  es = theViewFather.GetComponentsInChildren<equipShowingButton>();
		for (int i = 0; i < es.Length; i++)
			Destroy (es[i].gameObject);
		//因为所有的显示都是针对本机角色的
		if (!systemValues.thePlayer)
			return;
		
		equipPackage  thePackage = systemValues.thePlayer.GetComponent <equipPackage> ();
		if (!thePackage)
			return;
		
		List<equipBasics> eqs = thePackage.getEquipWithType (theTypeSelect);
		makeFather (eqs.Count);
		for (int i = 0; i < eqs.Count; i++) 
		{
			GameObject theButton = GameObject.Instantiate<GameObject> (theShowingButtonProfab);
			theButton.transform.SetParent (theViewFather.transform);
			theButton.GetComponentInChildren<Text> ().text = eqs [i].equipName;
            //因为有grid控件，所以这些都没有必要使用了
		}
	}

	public void makeClickWithoutType()
	{
		//前期清理工作
		equipShowingButton []  es = theViewFather.GetComponentsInChildren<equipShowingButton>();
		for (int i = 0; i < es.Length; i++)
			Destroy (es[i].gameObject);
		//因为所有的显示都是针对本机角色的
		if (!systemValues.thePlayer)
			return;

		equipPackage  thePackage = systemValues.thePlayer.GetComponent <equipPackage> ();
		if (!thePackage)
			return;


		List<equipBasics> eqs = thePackage.allEquipsForSave;
		makeFather (eqs.Count);
		for (int i = 0; i < eqs.Count; i++) 
		{
			GameObject theButton = GameObject.Instantiate<GameObject> (theShowingButtonProfab);
			theButton.transform.SetParent (theViewFather.transform);
			theButton.GetComponentInChildren<Text> ().text = eqs [i].equipName;
			//因为有grid控件，所以这些都没有必要使用了
		}
	}

	//根据数组长度修改content的height
	void makeFather(int count)
	{
		GridLayoutGroup theGroup = theViewFather.GetComponent<GridLayoutGroup> ();
		RectTransform theFatherRect = theViewFather.GetComponent<RectTransform> ();
		float height = (float)(30+ theGroup.cellSize.y * count /( theFatherRect.rect.width / theGroup.cellSize.x));
		Rect newRect = new Rect (0,0,theFatherRect.rect.width , height);
		theFatherRect.SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical,  height);
		//额外增加一点点数值以备不测
	}
}
