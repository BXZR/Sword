using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class equipSelectTypeButton : MonoBehaviour {

	//这个类用来描述背包里面的东西的分类显示
	public Transform theViewFather;//显示按钮的容器
	public equiptype theTypeSelect;//类别
	public GameObject theShowingButtonProfab;//显示按钮的预设物，需要各种初始化
	private GameObject thisButton;//自身保存引用
	public static GameObject theButtonSave;//静态保存

	public static  void flashThePanel()
	{
		PointerEventData theData = new PointerEventData (EventSystem.current );//创建事件数据
		//传值：大概理解是：目标Gameobject ，事件数据 ， 类型（与那边接收的时候做匹配（大概））
		ExecuteEvents .Execute<IPointerClickHandler> ( theButtonSave, theData ,ExecuteEvents.pointerClickHandler);
	}


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
			if (eqs [i] == null || eqs[i].isUsing)
				continue;
			GameObject theButton = GameObject.Instantiate<GameObject> (theShowingButtonProfab);
			theButton.transform.SetParent (theViewFather.transform);
			theButton.GetComponentInChildren<Text> ().text = "";
			//theButton.GetComponentInChildren<Text> ().text = eqs [i].equipName;
			theButton.GetComponent <equipShowingButton> ().theEquip = eqs [i];
			print ("equipPicture/" +eqs[i].theEquipType +"/"+eqs[i].equipPictureName);

			theButton.GetComponent <Image> ().sprite =  systemValues.makeLoadSprite ("equipPicture/" +eqs[i].theEquipType +"/"+ eqs[i].equipPictureName);
            //因为有grid控件，所以这些都没有必要使用了
		}
		theButtonSave = thisButton ;
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
			if (eqs [i] == null || eqs[i].isUsing)
				continue;
			GameObject theButton = GameObject.Instantiate<GameObject> (theShowingButtonProfab);
			theButton.transform.SetParent (theViewFather.transform);
			theButton.GetComponentInChildren<Text> ().text = "";
			//theButton.GetComponentInChildren<Text> ().text = eqs [i].equipName;
			theButton.GetComponent <equipShowingButton> ().theEquip = eqs [i];
			theButton.GetComponent <Image> ().sprite =  systemValues.makeLoadSprite ("equipPicture/" +eqs[i].theEquipType +"/"+eqs[i].equipPictureName);
			//因为有grid控件，所以这些都没有必要使用了
		}
		theButtonSave = thisButton ;
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

	void Start()
	{
		thisButton =  this.gameObject;
	}
}
