using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipPackage : MonoBehaviour {
	//这个类同来描述玩家的装备信息
	//包括背包中的装备，但前装备上的装备
	//网络同步的加成也是在这个脚本中完成的
	//此外还有装备的分类，表达和存储

	//网络控制节点=============================
	PhotonView photonView;
	//=========================================
	public List<equipBasics> allEquipsForSave;//没有被装备的装备都是在这里完成的

	public equipBasics thEquipForHeadUsed = null;//当前装备上的头部装备
	public equipBasics thEquipForBodyUsed = null;//当前装备上的身体装备
	public equipBasics thEquipForWeaponUsed = null;//当前装备上的武器装备
	public equipBasics thEquipForShoeUsed = null;//当前装备上的鞋子装备
	public equipBasics thEquipForExtraUsed1 = null;//当前装备上的饰品装备1
	public equipBasics thEquipForExtraUsed2 = null;//当前装备上的饰品装备2

	//根据类别查询装备
	//如果返回所有已经获得的装备，就直接访问allEquipsForSave
	public List<equipBasics> getEquipWithType(equiptype theType)
	{
		List<equipBasics> theGets = new List<equipBasics> ();
		for (int i = 0; i < allEquipsForSave.Count; i++) 
		{
			if (allEquipsForSave [i].theEquipType == theType)
				theGets.Add (allEquipsForSave[i]);
		}
		return theGets;
	}

	//因为网络传输的传输的内容是需要传参数的，所以尽可能希望这个参数能足够简单
	//这就需要考虑一些限制的设计了
	public void getTheEquip( string equipName)
	{
		if (systemValues.modeIndex == 1 && photonView!= null)
		{
			photonView.RPC ("getEquipForPrivate", PhotonTargets.All, equipName);
		}
		else if (systemValues.modeIndex == 0)
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
 
	//初始化
	private void makeStart()
	{
		allEquipsForSave = new List<equipBasics> ();
	}

	void OnTriggerEnter(Collider collisioner)
	{
		if (collisioner.gameObject.tag == "equip") 
		{
			equipBasics theEquip = collisioner.gameObject.GetComponent <equipBasics> ();
			if(allEquipsForSave.Contains(theEquip) == false)
				allEquipsForSave.Add (theEquip);
		    
			collisioner.gameObject.SetActive(false);
		}
	}
}
