using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class equipSelectTypeButton : MonoBehaviour {

	//这个类用来描述背包里面的东西的分类显示
	public Transform theViewFather;//显示按钮的容器
	public equiptype theTypeSelect;//类别
	public bool isAllType = false;//选择类型，如果是treu这就是选择全部
	public GameObject theShowingButtonProfab;//显示按钮的预设物，需要各种初始化
	private equipSelectTypeButton  thisButton;//自身保存引用
	public static equipSelectTypeButton theButtonSave;//静态保存
	public Image SelectedHighLightPicture;//选中的时候的按钮


	public static void flashThePanel()
	{
		if (!theButtonSave)
			return;

		//theButtonSave.makePress ();
		if (theButtonSave.isAllType)
			theButtonSave.makeClickWithoutType ();
		else
			theButtonSave.makeClickWithType ();
	}


	//模拟按钮按下
	//这种情况只能在Start等等回调里面使用
	//单独调用是不起作用的
	public void makePress()
	{
		//print ("so pressed");
		//遮掩做是为了保证第一次打开五灵界面的时候能偶一个事先就被选中了
		PointerEventData theData = new PointerEventData (EventSystem.current );//创建事件数据
		//传值：大概理解是：目标Gameobject ，事件数据 ， 类型（与那边接收的时候做匹配（大概））
		ExecuteEvents .Execute<IPointerClickHandler> ( this.gameObject , theData ,ExecuteEvents.pointerClickHandler);
	}

	//按下按钮选择-------------------------------------------------------------------------------------------------------------------
	public void makeClickWithType()
	{
		//print ("select");
		equipPackage  thePackage = systemValues.thePlayer.GetComponent <equipPackage> ();
		thePackage.sortThePackage ();
		List<equipBasics> eqs = thePackage.allEquipsForSave.FindAll(a => a!= null &&  a.isUsing == false && a.theEquipType == theTypeSelect );
		//eqs = thePackage.getEquipWithType (eqs , theTypeSelect);
		makeShow (eqs);
		equipShowingButton.flashPicture ();
	}

	public void makeClickWithoutType()
	{
		//print ("select all");
		equipPackage thePackage = systemValues.thePlayer.GetComponent <equipPackage> ();
		thePackage.sortThePackage ();
		List<equipBasics> eqs = thePackage.allEquipsForSave.FindAll(a => a!= null && a.isUsing == false && a.theEquipType != equiptype.equipSkill  );
		makeShow (eqs);
		equipShowingButton.flashPicture ();
	}

	public void makeClickWithTypeFromShop()
	{
		//print ("select");
		equipPackage  thePackage = this.GetComponentInParent <equipPackage> ();
		thePackage.sortThePackage ();
		List<equipBasics> eqs = thePackage.allEquipsForSave.FindAll(a => a!= null &&  a.isUsing == false && a.theEquipType == theTypeSelect );
		//eqs = thePackage.getEquipWithType (eqs , theTypeSelect);
		makeShow (eqs);
		equipShowingButton.flashPicture ();
	}

	public void makeClickWithoutTypeFromShop()
	{
		//print ("select all");
		equipPackage thePackage = this.GetComponentInParent <equipPackage> ();
		thePackage.sortThePackage ();
		List<equipBasics> eqs = thePackage.allEquipsForSave.FindAll(a => a!= null && a.isUsing == false);
		makeShow (eqs);
		equipShowingButton.flashPicture ();
	}

	//-------------------------------------------------------------------------------------------------------------------

	//显示注灵内容
	public void makeShowEquipSkillAdder()
	{
		theButtonSave = thisButton ;
	}


	private void makeShow(List<equipBasics> eqs)
	{
		//因为所有的显示都是针对本机角色的
		if (!systemValues.thePlayer)
			return;

		//前期清理工作
		equipShowingButton []  es = theViewFather.GetComponentsInChildren<equipShowingButton>();
		//直接用lambda表达式查询吧还是
		systemValues. makeFather (eqs.Count , theViewFather);

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
				//theButton.GetComponentInChildren<Text> ().text = "";
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
		if (theButtonSave)
			theButtonSave.SelectedHighLightPicture.enabled = false;
		theButtonSave = thisButton ;
		if (theButtonSave)
			theButtonSave.SelectedHighLightPicture.enabled = true;
	}
		
	void Start()
	{
		thisButton = this;
		makePress ();//反正是初始化的时候调用一次Start，那就所有按钮都调用一次吧，竞争一下最后一个被按下的就是选中的
	}
}
