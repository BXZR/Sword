using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//一种专门用来存储attackLink内容信息的一个类
using System.Text;


public class attackLinkInformation
{
	public PlayerBasic thePlayer = null;
	public string attackLinkName = "";
	public string attackLinkString = "";
	public string attackLinkInformationText = "";
	public string theEffectForSelfName = "";
	public string theEffectForSelfInformaion = "";
	public string theEffectForEMYName = "";
	public string theEffectForEMYInformaion = "";
}

public enum crossFadeMode { play , crossfade , crossfadeFix }
public class attackLink : MonoBehaviour {

	//所有atackLink的父类，子类的atackLink将会继承所有的方法并通过重新attackLinkEffect方法获得新的技能效果

	public string skillName;//技能名称
	public  string attackLinkString;//用字符串表示输入的键位串
	private string [] attackLinkStringSplited;//用“;”分割，这样可以让不同的键位实现同一个技能而不增加新的attackLink

	private List<string> attackLinkStringBuff = new List<string> ();//因为自身也存在嵌套的选择，只好也有缓冲
	private List<int> attackLinkIndexBuff = new List<int> ();//因为自身也存在嵌套的选择，只好也有缓冲

	public string animationName ;//转变的动画状态机的状态名称
	protected Animator theAnimatorOfPlayer;//使用动作的人物的动画控制器
	protected CharacterController theController;//角色控制器，用于获取一些当前的状态例如是否在地面上面，此处会有很大的扩展
	protected char [] attackAray;//将输入字符串分解得到，用于逐位检测
	private bool isChangeAble = true;//是否可以更改按键，这个只有单个字符的动作可以更改，并且更改之后所有的连击字符串也会相应更改
	//**************上面是有关连招动画控制与检测的重要参数，下面是用于战斗计算等等的参数***********************/
	private PlayerBasic thePlayer;//这个招式所使用的人
	public float extraDamage = 0;//这个招式的额外伤害，所有的招式都拥有游戏人物基本的攻击力加成，然后技能本身有一个额外的伤害加成
	public float spUse =0;//使用这个招式所需要的能量值
	//public float area = 1f;//这个招式的作用范围
	//public bool isAOE =false;//这是不是一个范围伤害（暂定范围伤害就是攻击身边所有的单位，相交球）

	//因为已经确认了是二人格斗游戏，这个地方其实已经写死了
	//这段脚本会被weapon脚本在攻击得手的时候触发并且添加
	public string conNameToEMY ="";//这个招式可以为目标添加的脚本
	public string conNameToSELF ="";//这个招式可以为自身添加的脚本

	public AudioClip audioWhenAct;//在做出动作的时候就发的音效
	public AudioClip audioWhenAttack;//招式命中的时候的音效，修改的是player身上的音效
	//动作切换的方式
	public crossFadeMode crossMode = crossFadeMode.play;
	//[HideInInspector]//此效果没有必要在面板中被设置
	public int AIExtraValue = 0;//用于AI计算的额外参数

	public bool canLvup = true;//招式是否可以升级
	public int lvupCost  = 5;//招式升级消耗的魂
	public int theAttackLinkLv = 1;//当前招式等级
	public int theAttakLinkLvMax = 18;//最大等级上限
	public float extraDamageAdd = 0;//额外攻击伤害
	private int  soulCostWhenLvtoMax = 100;//等级满了之后继续叠加的消耗
	private  int  adderWhenLvtoMax = 1;//极其低性价比的叠加

	/****************************************特殊攻击方法组****************************************************/
	//攻击检测原理：
	//用相交求获取身边所有的单位
	//用坐标的方法检测这个单位是否能够被攻击
	//对所有可以被攻击的单位进行攻击（调用player的attack方法）
//	public void attackEffect()
//	{
//		emysList.Clear ();
//		emysListToDelete.Clear ();
//		emys = Physics.OverlapSphere (this.transform .root .position, area);
//		emysList = new List<Collider> (emys);
//		int i = 0;
//		for (i = 0; i < emysList.Count; i++) 
//		{
//            if (emysList [i].GetComponent<PlayerBasic> () == null || emysList [i].gameObject == this.transform.root.gameObject) 
//			{
//			emysListToDelete.Add (emysList [i]);
//			}
//		     //检查条件：向量检查朝向问题如果不是AOE攻击，就只能够攻击到一定角度范围的单位，因为是横版游戏，貌似没有来自左右的角度，所以用的是180度
//			 if (!isAOE ) 
//			 {
//				 if(checkCanAttack (emysList [i].transform) == false) 
//			     emysListToDelete.Add (emysList [i]);
//		     }
//		}
//		for (i = 0; i < emysListToDelete.Count; i++) 
//		{
//			emysList.Remove (emysListToDelete [i]);
//		}
//
//		for (i = 0; i < emysList.Count; i++)
//		{
//			 
//			thePlayer.OnAttack (emysList [i].GetComponent <PlayerBasic>());
//		}
//
//	}
//
//	bool checkCanAttack(Transform aimTransform)//如果不是AOE那么只有在正面的对手才可以被攻击到
//	{
//		Vector3 ToAim = (aimTransform.position - this.transform.position).normalized;//序列化就不用除以模了
//		Vector3 Forward = this.transform .forward.normalized;//序列化就不用除以模了
//		float cos = ToAim .x *Forward .x + ToAim .y * Forward .y + ToAim .z *Forward.z;
//		if (cos >= 0)
//			return true;
//		return false;
//	}
	/********************************************************************************************/
	public bool IsChangeAble()
	{
		return isChangeAble;
	}

	//有一些网络的因素在，有一些内容不得不在这里也显示
	void Start()
	{
		this.theAnimatorOfPlayer = this.GetComponentInParent<Animator> ();
	}
	//初始化的工作由attackLinkController统一调配
	public void makeStart()
	{
		makeAttackArray ();
		this.theAnimatorOfPlayer = this.GetComponentInParent<Animator> ();
		this.theController = this.GetComponentInParent<CharacterController> ();
		thePlayer = this.GetComponentInParent<PlayerBasic> ();

		photonView = PhotonView.Get(this);
	}

	public virtual  void makeAttackArray()//只在初始化的时候或者需要重新构建联机字符串的时候使用
	{
		if(!string .IsNullOrEmpty( attackLinkString) )//如果字符串为空则不做初始化
		{
		 attackLinkStringSplited = attackLinkString.Split (';');
		 attackAray = attackLinkStringSplited[0].ToCharArray ();//初始化数组
		}
		if (attackAray.Length > 1)
			isChangeAble = false;

		for (int i = 0; i < attackLinkStringSplited.Length; i++)
			attackLinkStringBuff.Add (attackLinkStringSplited[i]);
		
	}

	//返回这个动作的时间，用来控制显示效果的时间长短
	public virtual float actTimer()
	{
		float timer = 1f;
		try
		{
			timer = this.theAnimatorOfPlayer.GetCurrentAnimatorStateInfo (0).length;
			//print("get act length = " + timer);
		}
		catch 
		{
			timer = 1f;
		}
		return timer;
	}


	//在不改变下层逻辑的情况下做一次封装来保证网络做法

	public void attackLinkMain(int theAttackLinkIndex = 0)
	{
		if (systemValues.modeIndex == 1 && photonView != null)
		{
			this.photonView.RPC ("attackLinkEffect", PhotonTargets.All,theAttackLinkIndex);
			this.photonView.RPC ("playAttackLinkAction", PhotonTargets.All);
		}
		else if (systemValues.modeIndex == 0)
		{
			attackLinkEffect (theAttackLinkIndex);
			playAttackLinkAction ();
		}
	}


	//招式升级-----------------------------------------------
	[PunRPC]
	private void AttackLinkLvupNet()
	{
		if (this.theAttackLinkLv < this.theAttakLinkLvMax) 
		{
			if (systemValues.soulCount >= lvupCost)
			{
				this.extraDamage += extraDamageAdd;
				this.theAttackLinkLv++;
				systemValues.soulCount -= lvupCost;
					
			}
		} 
		else//升到顶级之后再一次增加只能够获得很微小的增益
		{
			extraDamageAdd = adderWhenLvtoMax;
			lvupCost = soulCostWhenLvtoMax;
			if (systemValues.soulCount >= lvupCost)
			{
				this.extraDamage += extraDamageAdd;
				systemValues.soulCount -= lvupCost;
			}
		}
	}

	//计算招式升级需要的魂元数量
	//以及招式升级带来的增益
	//这是一个统一的计算方法，其实就是为了简化配置的过程，所以难免会有扯淡的偏颇
	public void canculateCost()
	{
		int value = 7;
		if (!systemValues.isNullOrEmpty (this.conNameToSELF))
			value += 10;
		if (!systemValues.isNullOrEmpty(this.conNameToEMY))
			value += 10;
		if (extraDamage > 0)
			value += (int)extraDamage/2;
		
		lvupCost = (int)(value *0.15f) + theAttackLinkLv * 2 ;
		extraDamageAdd = value * 0.15f + (int)spUse*0.1f;
	}

	//网络版群体升级，单机版单独升级
	public void makeAttackLinkUp()
	{
		if (systemValues.modeIndex == 1 && photonView != null)
		{
			this.photonView.RPC ("AttackLinkLvupNet", PhotonTargets.All);
		}
		else if (systemValues.modeIndex == 0)
		{
			AttackLinkLvupNet ();
		}
	}
	//招式升级OVER-----------------------------------------------

	//攻击起手阶段的效果
	private void  playStarEffect()
	{
		effectBasic[] Effects = this.transform .root.GetComponentsInChildren<effectBasic> ();
		for (int i = 0; i < Effects.Length; i++)
			Effects [i].onAttackAction ();
	}


	[PunRPC]
	public  virtual void attackLinkEffect(int theAttackLinkIndex = 0)//连招的效果在这里写
	{
		//这里其实暂时规定使用某一个攻击动作的同时不会使用另一个攻击动作
		//也就是说攻击动作之间不会发生生任何干扰
		//此外频繁第取消后摇实际上会隐形消耗斗气，例如刚刚准备做一个消耗斗气的动作还没有动作就立刻做出下一个动作，那么这个斗气消耗是不会返还的
		//对于上述方法暂时使用状态值来强支计算能否转换
		//  && systemValues .canInteruptActionInAttack ( this.thePlayer .theAnimationController)  ) 
		//上面的检查方法被移动到了attackLinkController里面，在attackLink里面只管实现攻击效果，不再判断是否可以转移攻击效果（多次分时的检查可能会出现卡顿）
		if (string.IsNullOrEmpty (animationName) == false )
		{
			//这不仅是保险措施
			//也是基于网络的处理
			//因为网络对战状态下attackLinkController是不初始化的，因此这个也是不初始化的
			if (!thePlayer) 
			{
				thePlayer = this.transform.root.GetComponent<PlayerBasic> ();
			}
			//print ("play action");
			if (thePlayer) 
			{
				effectBasic[] Effects = thePlayer.GetComponentsInChildren<effectBasic> ();
				thePlayer.EffectAttackLinkIndex = theAttackLinkIndex;
				playStarEffect ();

				if (thePlayer.theAudioPlayer != null)
					thePlayer.theAudioPlayer.playAttackActSound (this.audioWhenAct);//播放攻击动作音效
				
				if (thePlayer.ActerSp >= this.spUse) 
				{
					thePlayer.ActerSp -= this.spUse;
				} 
				else 
				{
					//法力透支的计算过程
					float hpMinus = (this.spUse - thePlayer.ActerSp)*1.5f;
					hpMinus = Mathf.Clamp (hpMinus , 5f,100f);
					thePlayer.ActerHp -= hpMinus;
					thePlayer.ActerSp = 0;
					if (thePlayer.ActerHp < 10)
						thePlayer.ActerHp = 10f;//保护机制，在格斗游戏中没有透支身亡一说

					for (int i = 0; i < Effects.Length; i++) 
					{
						Effects [i].OnBeAttack (hpMinus);
					}
				}
				//但是为了保证我的个性，透支机制依旧存在，只是不会致命了

				for (int i = 0; i < Effects.Length; i++) 
				{
					Effects [i].SetAttackLinkIndex (theAttackLinkIndex);
					Effects [i].OnUseSP (this.spUse);
				}
			}


			thePlayer.extraDamageForAnimation = this.extraDamage;//用这样的方式修改真正的伤害
			if(thePlayer.theAudioPlayer!= null)
			thePlayer .theAudioPlayer .audioNow = this.audioWhenAttack;//确定命中的时候的音效

			thePlayer.conNameToEMY = this.conNameToEMY;
			thePlayer.conNameToSELF = this.conNameToSELF;
		}

		//print (skillName+" 发动！");
	}

	//播放攻击招式动作
	[PunRPC]
	public void playAttackLinkAction()
	{
		switch( crossMode )
		{
		//平滑过渡
		case crossFadeMode.crossfade :
			{
				this.theAnimatorOfPlayer.CrossFade (animationName, 0.05f);
				//print ("animationName = " + animationName);
			}
			break;
			//一般播放
		case crossFadeMode.play :
			{
				this.theAnimatorOfPlayer.Play (animationName);
			}
			break;
			//强制播放(不太推荐的做法)
			//其实强制播放也没用，在这上一层会有检查
		case crossFadeMode.crossfadeFix :
			{
				this.theAnimatorOfPlayer.CrossFadeInFixedTime(animationName, 0.00f);
			}
			break;

		}
	}

	public char getCharWithIndex(int index)
	{
		if (attackAray.Length-1 < index)
			return ' ';
		else
		 return attackAray [index];//返回当前检测的字符
	}

	public List<char> getCharListWithIndex(int index)
	{
		List<char> allLinkChars = new List<char> ();
		for (int i = 0; i < attackLinkStringSplited.Length; i++) 
		{
			if (attackLinkStringSplited[i].Length-1 < index)
				allLinkChars.Add(' ');
			else
				allLinkChars.Add( attackLinkStringSplited[i][index]);//返回当前检测的字符
		}
		//for (int i = 0; i < allLinkChars.Count; i++)
		//	print ("get chars for check: " + allLinkChars [i]);
		
		return allLinkChars;
	}
	//每一次都要手工重置Buffer，实际上这是个挺麻烦并且不是很准确的做法
	public void flashBuffer(int index , char theKeyChar)
	{
		if (attackLinkStringBuff == null)
			attackLinkStringBuff = new List<string> ();
		else
			attackLinkStringBuff.Clear();

		if (attackLinkIndexBuff == null)
			attackLinkIndexBuff = new List<int> ();
		else
			attackLinkIndexBuff.Clear ();
		
		for (int i = 0; i < attackLinkStringSplited.Length; i++) 
		{
			if (attackLinkStringSplited[i].Length-1 >= index && attackLinkStringSplited[i][index] == theKeyChar)
			{
				attackLinkStringBuff.Add( attackLinkStringSplited[i]);//返回当前检测的字符
				attackLinkIndexBuff.Add(i);
			}
		}
	}

	//这个连招是不是完全被检测到了
	public bool isCheckToOver(int length , out int selectAttackLinkIndex)
	{
		//唯一一个的时候最多
		if (attackLinkString.Length == length)
		{
			selectAttackLinkIndex = -1;
			return true;
	    }
		//分享很复杂倒是不太推荐
		for (int i = 0; i < attackLinkStringBuff.Count; i++)
		{
			if (attackLinkStringBuff[i].Length == length)
			{
				selectAttackLinkIndex = attackLinkIndexBuff[i];
				return true;
			}
		}
		selectAttackLinkIndex = -1;
		return false;
	}


	PhotonView photonView;//网络控制单元

	//获取简略的信息，这就足够了
	public string getInformationSimple(bool withName = true)
	{
		StringBuilder theString = new StringBuilder ();
		if (withName)
		{
			theString .Append( this.skillName);
			if (canLvup)
				theString .Append("[可升级]");
			else
				theString .Append("[不可升级]");
			theString.Append ("\n");
		}

		if (canLvup) 
		{
			theString.Append ( "招式等级：" );
			theString.Append ( this.theAttackLinkLv );
			theString.Append ( "/");
			theString.Append ( this.theAttakLinkLvMax );
			theString.Append ( "\n");
		}

		if (this.extraDamage > 0) 
		{
			if (!this.thePlayer) 
			{
				theString.Append ("额外伤害：");
				theString.Append (this.extraDamage.ToString ("f0"));
				theString.Append ("\n");
			}
			else 
			{
				theString.Append ("伤害：(" );
				theString.Append (this.thePlayer.ActerWuliDamage.ToString("f0"));
				theString.Append ("+" );
				theString.Append (systemValues.BESkillColor);
				theString.Append ((int) this.extraDamage);
				theString.Append (systemValues.colorEnd );
				theString.Append (")\n");
			}
		}
		else
		{
			theString.Append ("额外伤害：");
			theString.Append (this.extraDamage );
			theString.Append ("\n");
		}

		//information += "触发方式："+ systemValues.getAttacklinkInformationTranslated(this.attackLinkString) + "\n";
		theString.Append ("触发方式：");
		for (int i = 0; i < attackLinkStringSplited.Length; i++) 
		{
			theString.Append (systemValues.getAttacklinkInformationTranslated(attackLinkStringSplited[i]));
			if(i<attackLinkStringSplited.Length-1 )
				theString.Append (" / ");
		}
		theString.Append ("\n");

		theString.Append ("斗气消耗：");
		theString.Append (this.spUse.ToString("f0") );

		return theString.ToString();
	}


	public string getInformation()//获取连招的信息
	{
		StringBuilder theString = new StringBuilder ();
		theString.Append( "招式名称：" );
		theString.Append( this.skillName);
		theString.Append( "\n");

		if (canLvup)
		{
			theString.Append( "招式等级：");
			theString.Append( this.theAttackLinkLv);
			theString.Append( "/");
			theString.Append( this.theAttakLinkLvMax);		
			theString.Append( "\n");

		}

		if (this.extraDamage > 0) 
		{
			if (!this.thePlayer) 
			{
				theString.Append( "额外伤害：");
				theString.Append( this.extraDamage.ToString("f0"));
				theString.Append( "\n");
			}
			else
			{
				theString.Append ("伤害：(");
				theString.Append ( this.thePlayer.ActerWuliDamage.ToString("f0") );
				theString.Append ( "+" );
				theString.Append ( systemValues.BESkillColor);
				theString.Append ( (int) this.extraDamage );
				theString.Append ( systemValues.colorEnd );
				theString.Append (")\n");
			}
		}
		else
		{
			theString.Append ("额外伤害：");
			theString.Append (this.extraDamage );
			theString.Append ("\n");
		}

		theString.Append ("触发方式：");
		for (int i = 0; i < attackLinkStringSplited.Length; i++) 
		{
			theString.Append (systemValues.getAttacklinkInformationTranslated(attackLinkStringSplited[i]));
			if(i<attackLinkStringSplited.Length-1 )
				theString.Append (" / ");
		}
		theString.Append ("\n");

		theString.Append ("斗气消耗：");
		theString.Append (this.spUse);
		theString.Append ("\n");

		if(canLvup  )
		{
			theString.Append("\n");
			if (this.theAttackLinkLv < this.theAttakLinkLvMax) 
			{
				theString.Append("升级所需灵力：");
				theString.Append(this.lvupCost);
				theString.Append("\n");
				theString.Append("升级增加额外伤害：");
				theString.Append((int)this.extraDamageAdd );
				theString.Append("\n");
			}
			else
			{
				theString.Append("等级已满\n");

				extraDamageAdd = adderWhenLvtoMax;
				lvupCost = soulCostWhenLvtoMax;

				theString.Append("可以消耗");
				theString.Append(soulCostWhenLvtoMax);
				theString.Append("灵力继续增加");
				theString.Append(extraDamageAdd);
				theString.Append("额外伤害\n");
			}
		}

		return theString.ToString ();
	}

}
