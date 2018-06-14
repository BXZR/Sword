using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class equipPackage : MonoBehaviour {
	//这个类同来描述玩家的装备信息
	//包括背包中的装备，但前装备上的装备
	//网络同步的加成也是在这个脚本中完成的
	//此外还有装备的分类，表达和存储

	//网络控制节点=============================
	PhotonView photonView;
	private PlayerBasic thePlayer;
	//=========================================
	public List<equipBasics> allEquipsForSave;//没有被装备的装备都是在这里完成的

	public equipBasics thEquipForHeadUsed = null;//当前装备上的头部装备
	public equipBasics thEquipForBodyUsed = null;//当前装备上的身体装备
	public equipBasics thEquipForWeaponUsed = null;//当前装备上的武器装备
	public equipBasics thEquipForShoeUsed = null;//当前装备上的鞋子装备
	public equipBasics thEquipForExtraUsed1 = null;//当前装备上的饰品装备1
	public equipBasics thEquipForExtraUsed2 = null;//当前装备上的饰品装备2
	public equipBasics thEquipForGod = null;//当前装备上的神器
	public AudioClip theGetEquipSoundClip = null;//获得装备的时候播放的音效
	public Transform extraPackage ;//额外初始化包裹
//下面两个方法可能会有引用回收的问题，暂时先不用=====================================================================
//根据类别查询装备
//如果返回所有已经获得的装备，就直接访问allEquipsForSave
//	public List<equipBasics> getEquipWithType(equiptype theType)
//	{
//		List<equipBasics> theGets = new List<equipBasics> ();
//		for (int i = 0; i < allEquipsForSave.Count; i++) 
//		{
//			if (allEquipsForSave [i].theEquipType == theType)
//				theGets.Add (allEquipsForSave[i]);
//		}
//		return theGets;
//	}
//	public List<equipBasics> getEquipWithType(List<equipBasics> eqs,equiptype theType)
//	{
//		List<equipBasics> theGets = new List<equipBasics> ();
//		for (int i = 0; i < eqs .Count; i++) 
//		{
//			if (allEquipsForSave [i].theEquipType == theType)
//				theGets.Add (allEquipsForSave[i]);
//		}
//		return theGets;
//	}
//===================================================================================================================


	void Start()
	{
		thePlayer = this.GetComponent <PlayerBasic> ();
		allEquipsForSave = new List<equipBasics> ();

		//对于默认自带的装备，还是应该获取一下
		equipBasics[] eqs = this.GetComponentsInChildren<equipBasics> ();
		for (int i = 0; i < eqs.Length; i++) 
		{
			allEquipsForSave.Add (eqs [i]);
			eqs [i].gameObject.SetActive (false);
		}
		if (extraPackage) 
		{
			eqs = extraPackage.GetComponentsInChildren<equipBasics> ();
			for (int i = 0; i < eqs.Length; i++) 
			{
				allEquipsForSave.Add (eqs [i]);
				eqs [i].gameObject.SetActive (false);
			}
		}

	}


	//因为网络传输的传输的内容是需要传参数的，所以尽可能希望这个参数能足够简单
	//这就需要考虑一些限制的设计了
	public void getTheEquip( string equipName)
	{
		if (systemValues.theGameSystemMode == GameSystemMode.NET && photonView!= null)
		{
			photonView.RPC ("getEquipForPrivate", PhotonTargets.All, equipName);
		}
		else if (systemValues.theGameSystemMode == GameSystemMode.PC)
		{
			getEquipForPrivate(equipName);
		}
	}

	//真正获取装备的方法
	//用资源加载的方式来获得装备
	//也就是说，在游戏中看到的装备只是包含装备资源名字字符串的一个“别的东西”
	private void getEquipForPrivate(string theEquipName)
	{
		GameObject theEquipObj = GameObject.Instantiate<GameObject>( Resources.Load<GameObject> ("Equips/" + theEquipName));
		equipBasics theEquip = theEquipObj.GetComponent<equipBasics> ();
		allEquipsForSave.Add (theEquip);
		//至于 theEquipObj 想个办法看看怎么销毁或者显示一下
	}
 

	//给背包进行排序
	//这将会是一个开销挺大的方法，或有优化
	List<equipBasics> theSortBuffer = new List<equipBasics> ();
	List<equipBasics> theNewPackage = new List<equipBasics> ();
	equiptype[] thetypes = { equiptype.weapon, equiptype.body, equiptype.head, equiptype.shoe, equiptype.extra , equiptype.equipSkill  , equiptype.god };
	public void sortThePackage()
	{
		try
		{
		//这里还真的需要新建一个副本对象
		theNewPackage = new List<equipBasics> ();
		for (int k = 0; k < thetypes.Length; k++)
		{
			theSortBuffer.Clear ();
			for (int i = 0; i < allEquipsForSave.Count; i++) 
			{
				if (allEquipsForSave [i].theEquipType == thetypes[k] && allEquipsForSave [i]!=null)
					theSortBuffer.Add (allEquipsForSave [i]);
			}
//			if (theSortBuffer.Count == 0)
//				continue;
			
			quickSort (theSortBuffer, 0, theSortBuffer.Count-1);
			for (int i = 0; i < theSortBuffer.Count; i++)
				theNewPackage.Add (theSortBuffer[i]);
		}
		allEquipsForSave = theNewPackage;
		//print ("sorted count = "+ allEquipsForSave.Count);
		}
		catch(Exception X) 
		{
			print (X.Message);
		}
	}
		


	//初始化
	private void makeStart()
	{
		allEquipsForSave = new List<equipBasics> ();
	}

	void OnTriggerEnter(Collider collisioner)
	{
		if (collisioner.gameObject.tag.Equals ("equip")) 
		{
			equipBasics theEquip = collisioner.gameObject.GetComponent <equipBasics> ();
			if (allEquipsForSave.Contains (theEquip) == false)
				allEquipsForSave.Add (theEquip);
		    
			collisioner.gameObject.SetActive (false);

			if (thePlayer == systemValues.thePlayer) 
			{
				if (theEquip.theEquipType != equiptype.equipSkill)
					systemValues.messageTitleBoxShow ("获得装备\n【" + theEquip.equipName + "】");
				else
				//systemValues.messageTitleBoxShow ("获得图谱\n《"+systemValues.getEffectNameWithName( theEquip.equipName)+"》");
				systemValues.messageTitleBoxShow ("获得图谱\n《" + theEquip.equipExtraName + "》");
				systemValues.thePlayer.theAudioPlayer.playClip (theGetEquipSoundClip);
			}
		}
	}


//	void OnCollisionEnter(Collision collisioner)
//	{
//		if (collisioner.gameObject.tag == "equip") 
//		{
//			equipBasics theEquip = collisioner.gameObject.GetComponent <equipBasics> ();
//			if(allEquipsForSave.Contains(theEquip) == false)
//				allEquipsForSave.Add (theEquip);
//
//			collisioner.gameObject.SetActive(false);
//			systemValues.messageTitleBoxShow ("获得【"+theEquip.equipName+"】");
//			systemValues.thePlayer.theAudioPlayer.playClip (theGetEquipSoundClip);
//		}
//	}



	//工具方法

	private  void quickSort(List<equipBasics> theP, int low, int high)
	{
		if (low >= high)
			return;

		int first = low;
		int last = high;
		equipBasics keySave = theP [low];
		char  keyValue = theP[low].equipName[0];
		while (low < high)
		{
			while (low < high && theP[high].equipName[0] >= keyValue)
				high--;
			theP[low] = theP[high];
			while (low < high && theP[low].equipName[0] <= keyValue)
				low++;
			theP[high] = theP[low];
		}
		theP[low] = keySave;
		quickSort(theP, first, low - 1);
		quickSort(theP, low + 1, last);
	}
}
