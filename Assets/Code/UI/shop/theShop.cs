using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theShop : MonoBehaviour {

	//这是真正的商店逻辑
	public equipPackage thePackage;
	private  panelSoundController theSoundController;
	public equiptype theShopTypeNow = equiptype.all;
	private  int pageIndex = 0;//装备商店东西太多，应该分页显示
	private  int countPerPage = 15;//每一页显示的装备数量

	void Start()
	{
		thePackage = this.GetComponent <equipPackage> ();
		theSoundController = this.GetComponent <panelSoundController> ();
	}

	void OnEnable()
	{
		try{makeFlash ();}
		catch(System.Exception E) {print ("error!\n"+E.Message);}
	}



	//分页获取装备--------------------------------------------------------------------------------------
	public List<equipBasics> getEquipWithPage(equiptype  theTypeSelect, int pageMove = 0 , bool flashIndex = true )
	{
		if (flashIndex)
			pageIndex = 0;
		
		theShopTypeNow = theTypeSelect;

		thePackage.sortThePackage ();
		List<equipBasics> eqs = thePackage.allEquipsForSave.FindAll (a => a != null && a.isUsing == false && ( a.theEquipType == theTypeSelect || theTypeSelect == equiptype.all));

		int lastIndex = pageIndex + countPerPage;
		int getLength = lastIndex < eqs.Count ? countPerPage : (eqs.Count - pageIndex);

		List<equipBasics> eqs2 = eqs.GetRange (pageIndex , getLength);

		pageIndex = lastIndex;
		if (lastIndex >= eqs.Count || lastIndex < 0)
		{
			//print ("flash 0");
			pageIndex = 0;
		}
		return eqs2;
	}

	//----------------------------------------------------------------------------------------------------

	public void buyTheEquip()
	{
		if (systemValues.theEquipNowInShop == null)
		{
			systemValues.messageTitleBoxShow ("尚未选定装备");
			return;
		}

		if(systemValues.soulCount < systemValues.theEquipNowInShop.theSoulForThisEquip)
		{
			systemValues.messageTitleBoxShow ("当前灵力不足以兑换这个装备");
			return;
		}

		systemValues.soulCount -= systemValues.theEquipNowInShop.theSoulForThisEquip;
		thePackage.allEquipsForSave.Remove (systemValues.theEquipNowInShop);
		systemValues.thePlayer.GetComponent <equipPackage> ().allEquipsForSave.Add (systemValues.theEquipNowInShop);
		makeFlash ();
		theSoundController.makeSoundShow (0);
	}

	//这个游戏什么玩意都可以透支
	public void buyTheEquipHard()
	{
		if (systemValues.theEquipNowInShop == null)
		{
			systemValues.messageTitleBoxShow ("尚未选定装备");
			return;
		}

		if (systemValues.soulCount >= systemValues.theEquipNowInShop.theSoulForThisEquip)
			buyTheEquip ();
		else
		{
			int extraSoul = systemValues.theEquipNowInShop.theSoulForThisEquip - systemValues.soulCount;
			bool canChange = true;
			string changeInformation = systemValues.HpSpToLinginformation (extraSoul  , out  canChange ); 
			if (canChange) 
			{
				systemValues.choiceMessageBoxShow ("透支购买" , "需要透支\n"+changeInformation +"\n是否透支购买？" , true , new MesageOperate(hardChange));
			} 
			else 
			{
				systemValues.messageTitleBoxShow (changeInformation);
			}
			
		}
	}


	void hardChange()
	{
		int extraSoul = systemValues.theEquipNowInShop.theSoulForThisEquip - systemValues.soulCount;
		systemValues.HpSpToLing (extraSoul);
		buyTheEquip ();
	}
		

	public void makeFlash()
	{
		systemValues.theEquipNowInShop = null;
		equipInformationPanelInShop.makeFlash ();
		equipSelectTypeButton.flashThePanel ();
	}
}
