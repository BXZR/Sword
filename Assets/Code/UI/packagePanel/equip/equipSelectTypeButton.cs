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
		equipPackage  thePackage = systemValues.thePlayer.GetComponent <equipPackage> ();
		thePackage.sortThePackage ();
		List<equipBasics> eqs = thePackage.allEquipsForSave.FindAll(a => a!= null &&  a.isUsing == false  );
		eqs = thePackage.getEquipWithType (eqs , theTypeSelect);
		makeShow (eqs);
	}

	public void makeClickWithoutType()
	{
		equipPackage thePackage = systemValues.thePlayer.GetComponent <equipPackage> ();
		thePackage.sortThePackage ();
		List<equipBasics> eqs = thePackage.allEquipsForSave.FindAll(a => a!= null && a.isUsing == false  );
		makeShow (eqs);
	}


	private void makeShow(List<equipBasics> eqs)
	{
		//因为所有的显示都是针对本机角色的
		if (!systemValues.thePlayer)
			return;

		//前期清理工作
		equipShowingButton []  es = theViewFather.GetComponentsInChildren<equipShowingButton>();
		//直接用lambda表达式查询吧还是
		makeFather (eqs.Count);

		if (eqs.Count > es.Length) 
		{
			int i = 0;
			for (; i < es.Length; i++) 
			{
				es [i].theEquip = eqs [i];
				es [i].GetComponent <Image> ().sprite = systemValues.makeLoadSprite ("equipPicture/" + eqs [i].theEquipType + "/" + eqs [i].equipPictureName);
			}
			for (; i < eqs.Count; i++) 
			{
				GameObject theButton = GameObject.Instantiate<GameObject> (theShowingButtonProfab);
				theButton.transform.SetParent (theViewFather.transform);
				theButton.GetComponentInChildren<Text> ().text = "";
				//theButton.GetComponentInChildren<Text> ().text = eqs [i].equipName;
				theButton.GetComponent <equipShowingButton> ().theEquip = eqs [i];
				theButton.GetComponent <Image> ().sprite = systemValues.makeLoadSprite ("equipPicture/" + eqs [i].theEquipType + "/" + eqs [i].equipPictureName);
				//因为有grid控件，所以这些都没有必要使用了
			}
			//print ("重建次数："+( i- es.Length));
		}
		else
		{
			//print ("deletes343434");
			int i = 0;
			for (; i < eqs.Count; i++) 
			{
				es [i].theEquip = eqs [i];
				es [i].GetComponent <Image> ().sprite = systemValues.makeLoadSprite ("equipPicture/" + eqs [i].theEquipType + "/" + eqs [i].equipPictureName);
			}
			for (; i>=0 && i < es.Length; i++) 
			{
				Destroy (es [i].gameObject);
			}
		}
		theButtonSave = thisButton ;
	}


	//根据数组长度修改content的height
	void makeFather(int count)
	{
		GridLayoutGroup theGroup = theViewFather.GetComponent<GridLayoutGroup> ();
		RectTransform theFatherRect = theViewFather.GetComponent<RectTransform> ();

		int countPerLine = (int)( ( theFatherRect.rect.width - theFatherRect.rect.xMin ) / theGroup.cellSize.x);
		int lines = count / countPerLine + 1;
		//print ("CL = "+ countPerLine);
		//print ("lines = "+ lines);
		//print ("heightPerLine = "+theGroup.cellSize.y);
		float height = 30 + (int)(theGroup.cellSize.y)  * lines ;
		//print ("height = "+ height);

		Rect newRect = new Rect (0,0,theFatherRect.rect.width , height);
		theFatherRect.SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical,  height);
		//额外增加一点点数值以备不测
	}

	void Start()
	{
		thisButton =  this.gameObject;
	}
}
