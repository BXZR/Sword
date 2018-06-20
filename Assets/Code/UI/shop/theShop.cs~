using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theShop : MonoBehaviour {

	//这是真正的商店逻辑
	public equipPackage thePackage;
	private  panelSoundController theSoundController;
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
