using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extraWeapon : MonoBehaviour {

	private PlayerBasic thePlayer;//引用保存，各种信息调用
	private bool canMakeDamage = true;
	//这个额外武器最多能够对多少个目标造成伤害
	//因为有些武器是可以穿透的，但是效果不会相同
	public int damageCount = 3;

	void Update()
	{
		transform.Translate (new Vector3 (0,0,1) * 35*Time .deltaTime);
	}

	public void setPlayer(PlayerBasic thePlayerIn)
	{
		thePlayer = thePlayerIn;
	}
		
	private void extraDamageEffect(PlayerBasic playerAim)//额外添加挂在的计算脚本
	{
		
		if (string.IsNullOrEmpty (thePlayer . conNameToEMY) == false)//效果不可叠加
		{
			System.Type theType = System.Type.GetType (thePlayer.conNameToEMY);
				if(!playerAim.gameObject.GetComponent (theType))
			 {
				try
				{
						playerAim.gameObject.AddComponent (theType);
					//print("makeEffect3");
				}
				catch
				{
					//无法添加这个效果
					//那么就转换成伤害，造成2点真实伤害
					thePlayer.OnAttack (playerAim,2,true);
					//print ("canNotAddEMY");
				}
			}
			else
			{
					effectBasic theEffect = playerAim.gameObject.GetComponent (theType) as effectBasic;
				theEffect.updateEffect ();
				//print ("update");
			}
			thePlayer .conNameToEMY= "";
		}


	}

	List<PlayerBasic> attackAims = new List<PlayerBasic> ();
	void OnTriggerEnter(Collider collisioner)
	{ 

		if (thePlayer  ) 
		{

			PlayerBasic playerAim = collisioner.gameObject.GetComponent <PlayerBasic> ();
			if (playerAim == null)
				playerAim = collisioner.gameObject.GetComponentInParent<PlayerBasic> ();
			if (playerAim == null)
				playerAim = collisioner.gameObject.GetComponentInChildren<PlayerBasic> ();

			if (thePlayer && 　playerAim && playerAim.isAlive && playerAim != thePlayer &&  attackAims .Contains(playerAim) == false && attackAims.Count < damageCount)
			{
				attackAims.Add (playerAim);
				//print (playerAim.ActerHpSuck );
				//print (thePlayer .ActerWuliIner);

				thePlayer.OnAttack(playerAim);//造成直接的伤害
				extraDamageEffect (playerAim);//添加额外的计算脚本，每个脚本的效果由脚本自己决定
				//print("弹矢撞击！");
				//Destroy(this.gameObject);
			}
		} 

	} 
}