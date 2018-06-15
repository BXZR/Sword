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
	public static equipSelectTypeButton theButtonSaveForPlayer;//静态保存
	public static equipSelectTypeButton theButtonSaveForShop;//静态保存
	public Image SelectedHighLightPicture;//选中的时候的按钮
	public bool isPlayerPackage = true;//因为背包一共两种可能，一种是游戏玩家身上的，另一种是商店的

	public static void flashThePanel()
	{
		if (theButtonSaveForPlayer ) 
		{
			if (theButtonSaveForPlayer.isAllType)
				theButtonSaveForPlayer.makeClickWithoutType ();
			else
				theButtonSaveForPlayer.makeClickWithType ();
		} 
		if (theButtonSaveForShop) 
		{
			if (theButtonSaveForShop.isAllType)
				theButtonSaveForShop.makeClickWithoutTypeFromShop ();
			else
				theButtonSaveForShop.makeClickWithTypeFromShop ();
		}

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
		if (!systemValues.thePlayer)
			return;

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
		if (!systemValues.thePlayer)
			return;
		
		equipPackage thePackage = systemValues.thePlayer.GetComponent <equipPackage> ();
		thePackage.sortThePackage ();
		List<equipBasics> eqs = thePackage.allEquipsForSave.FindAll(a => a!= null && a.isUsing == false && a.theEquipType != equiptype.equipSkill  );
		makeShow (eqs);
		equipShowingButton.flashPicture ();
	}

	public void makeClickWithTypeFromShop()
	{
		//print ("select");
		theShop  sp = this.GetComponentInParent <theShop> ();
		if (sp && sp.thePackage)
		{
			equipPackage thePackage = sp.thePackage;
			thePackage.sortThePackage ();
			List<equipBasics> eqs = thePackage.allEquipsForSave.FindAll (a => a != null && a.isUsing == false && a.theEquipType == theTypeSelect);
			//eqs = thePackage.getEquipWithType (eqs , theTypeSelect);
			makeShow (eqs);
			equipShowingButton.flashPicture ();
		}
	}

	public void makeClickWithoutTypeFromShop()
	{
		//print ("select all");
		theShop  sp = this.GetComponentInParent <theShop> ();
		if (sp && sp.thePackage)
		{
			equipPackage thePackage = sp.thePackage;
			thePackage.sortThePackage ();
			List<equipBasics> eqs = thePackage.allEquipsForSave.FindAll (a => a != null && a.isUsing == false);
			makeShow (eqs);
			equipShowingButton.flashPicture ();
		}
	}

	//-------------------------------------------------------------------------------------------------------------------


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
				makeEquipShowingButton (es[i] , eqs[i]);
			}
			for (; i < eqs.Count; i++) 
			{
				equipShowingButton theButton = GameObject.Instantiate<GameObject> (theShowingButtonProfab).GetComponent<equipShowingButton>();
				theButton.transform.SetParent (theViewFather.transform);
				makeEquipShowingButton (theButton , eqs[i]);
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
				makeEquipShowingButton (es[i] , eqs[i]);
			}
			for (; i>=0 && i < es.Length; i++) 
			{
				Destroy (es [i].gameObject);
			}
		}

		makeSave ();
	}

	void makeEquipShowingButton(equipShowingButton theButtonEQ , equipBasics theEquip)
	{
		//equipShowingButton theButtonEQ = theButton.GetComponent <equipShowingButton> ();
		theButtonEQ.makeStart ();
		theButtonEQ.theEquip = theEquip;
		if (theButtonEQ is equipShopSelectButton)
			(theButtonEQ as equipShopSelectButton).theValueText.text =theEquip.theSoulForThisEquip.ToString();

		theButtonEQ .theEquipImage.sprite  = systemValues.makeLoadSprite ("equipPicture/" + theEquip.theEquipType + "/" + theEquip.equipPictureName);
	}


	void  makeSave()
	{
		if (isPlayerPackage)
		{
			if (theButtonSaveForPlayer)
				theButtonSaveForPlayer.SelectedHighLightPicture.enabled = false;
			theButtonSaveForPlayer = thisButton;
			if (theButtonSaveForPlayer)
				theButtonSaveForPlayer.SelectedHighLightPicture.enabled = true;
		} 
		else 
		{
			if (theButtonSaveForShop)
				theButtonSaveForShop.SelectedHighLightPicture.enabled = false;
			theButtonSaveForShop = thisButton;
			if (theButtonSaveForShop)
				theButtonSaveForShop.SelectedHighLightPicture.enabled = true;
		}
	}
		
	void Start()
	{
		thisButton = this;
		//makePress ();//反正是初始化的时候调用一次Start，那就所有按钮都调用一次吧，竞争一下最后一个被按下的就是选中的
	}
}
