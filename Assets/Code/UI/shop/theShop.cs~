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

	public void makeFlash()
	{
		systemValues.theEquipNowInShop = null;
		equipInformationPanelInShop.makeFlash ();
		equipSelectTypeButton.flashThePanel ();
	}
}
