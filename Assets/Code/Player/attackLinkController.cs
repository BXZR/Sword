using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class attackLinkController :MonoBehaviour {

	private int index = 0;//当前全局搜索位置
	public attackLink[] attackLinks;//保存连招公式的数组
	private List<attackLink> attackLinkMayUsing;//当前可能使用的操作公式
	private List<attackLink> attackBeDelete;//删除中转站，记录需要删除的信息
	private Event events;//当前的获取的事件
	private KeyCode keyUse;// 检测到的键值
	public float timerForLinkAtack=0.5f;//必须在一定时间内按出来这个串，否则无法使用技能
	public float timerAddForCheck= 0.2f;//按对了一次按键得到的时间奖励
	//有问题就是，如果只是一个键位的普通攻击，这个时间奖励就会成为额外的等待时间而造成卡顿
	private float timerForLinkMax = 0.5f;//tim的erForLinkAtack的默认最大时间
	bool startTimer = false;//是否开启计时器
	public float timerDifficulty = 0.75f;//时间难度，越大难度越大
    //上面这些参数计算关乎于连招

	//下面的参数关乎于游戏整体的联系和结合
	private PlayerBasic thePlayer;//这个游戏人物的对象
	[HideInInspector]
	public Animator theAnimator;//动画控制器
	[HideInInspector]//为了保证设定面板的简洁，暂时隐藏之
	public string deadAnimatorName ="dead";//死亡动画状态名称(这个名字做成默认名字因为没有必要分化)
	[HideInInspector]//为了保证设定面板的简洁，暂时隐藏之
	public string dropAnimatorName ="drop";//跌倒动画状态名称(这个名字做成默认名字因为没有必要分化)
	[HideInInspector]//为了保证设定面板的简洁，暂时隐藏之
	public string beHitAnimatorName ="beHit";//跌倒动画状态名称(这个名字做成默认名字因为没有必要分化)
	[HideInInspector]//为了保证设定面板的简洁，暂时隐藏之
	public attackLink theAttackLinkNow = null;//当前选中的attackLink
	//上面这个字段用于AI对这个attackLink的值的重新学习，同时这个也是信息采集需要使用的引用

	//public string basicPunchKey = "J";//基础的拳头按键
	//public string basicKickKey = "K";//基础的踢腿按键

	private bool isStarted = false;//是否开启
	public bool canControll = true ;//是否可以通过玩家/AI进行操作



	//检测连招的方法，每一次按键都要求检测
	//这个是根据玩家的输入进行控制的方法，在制作玩家AI时需要继承并重写方法
	// useInterpret标记着是否使用对应的翻译机制
	//对于人的输入必须要用翻译，对于AI则不需要
	public virtual void makeAttack(string keyString,bool useInterpret = true) 
	{
		
		if (!canControll)
			return;//如果当前是不可操控的，那么直接返回
	 
		attackBeDelete.Clear ();//每一次检测都会刷新这个集合
		char keyChar=' ';

		if (string .IsNullOrEmpty( keyString) ==false)
		{ //此处有待商榷，用这种方式只能支持A-Z的按键输入（需要加入转换的方法）
            keyChar = keyString.ToCharArray() [0];
			//print ("theKeyChar = "+ keyChar);
			foreach (attackLink AL in attackLinkMayUsing) //输入符合要求，可以进行下一步的检测了
			{
//				char getChar = AL.getCharWithIndex (index); //获取char
//				//正是因为获取的是char，这个方法有很大的限制
//				//print (getChar);
//				if (   getChar != keyChar ) 
//				{
//					attackBeDelete .Add (AL);//将不符合规范的输入去除掉
//				}
				List<char> getChars = AL.getCharListWithIndex(index);

				//for (int i = 0; i < getChars.Count; i++)
				//	print (getChars[i]+"--");
				
				if (getChars.Contains (keyChar) == false)
				{
					attackBeDelete .Add (AL);//将不符合规范的输入去除掉
				}

			}
			foreach (attackLink  AL in attackBeDelete)
			{
				//因为List数据结构正在被使用（foreach），因此需要将删除与遍历分开进行
				attackLinkMayUsing.Remove (AL);//记录当前需要删除的技能
			}
			//清空缓冲区
			foreach (attackLink AL in attackLinkMayUsing) 
			{
				AL.flashBuffer (index , keyChar );	
			}

			if (attackLinkMayUsing.Count == 0) 
			{//如果当前已经没有用于检测的串，说明输入不对，重头开始
				flashLink ();//更新列表
				reMake();//完全重头开始
			}
			else 
		    {
				startTimer = true;//检测到了符合的公式，则开始计时
				timerForLinkAtack += (timerAddForCheck* 1/timerDifficulty);
				//有一点加成等待下一个键位的输入（手残党的福利）
            	check ();//进行检查
			}
		}
		else
		{
			flashLink ();//更新列表
			reMake();//完全重头开始
		}	
	}

	//一次性检查某一个连招是否已经成立
	//例如某个连招的按键为SDK那么如果参数为“SDK”就认为这个招式可以使用了
	//如果输入过长的字符串，以检测到的最后一个招式为准
	public virtual void makeAttackLink(string keyString,bool useInterpret = true) 
	{
		flashLink ();//强制刷新一下可能用到的attacklink
		keyString = keyString.Trim ();
		//print ("theAttackLinkString = |"+ keyString+"|");
		for (int i = 0; i < keyString.Length; i++) 
		{
			//print ("--> " + keyString [i].ToString ());
			makeAttack (keyString [i].ToString (), useInterpret);
		}
	 
	}



	void check()//真正进行检测的方法
	{
		bool isOver = false;//标记量
		theAttackLinkNow = null;//每次检查之前都需要清空引用
		foreach(attackLink AL in attackLinkMayUsing)
		{
			int selectAttackLinkIndex;
			if (AL.isCheckToOver(index + 1 , out selectAttackLinkIndex) )//因为长度和index的关系，就比本的要求就是输入正确（当然还有SP等等的需求）
			{
				//这个判断非常的重要，如果取消，任何攻击动作都有可能中间取消，这当然不符合我们的需求
				//print("index Selected is "+selectAttackLinkIndex);
			    //print("the selected attacklink's linkstring = "+ AL.attackLinkString);
				AL.attackLinkMain(selectAttackLinkIndex);//发生效果

				flashLink ();//更新列表
				reMake();//完全重头开始
				isOver = true;//标记量，是否已经使用了一个技能
				break;//每一次生效的连招只能够有一个
			}
		}
		if(!isOver )
			index ++;//向下一个目录前进
	}

	void reMake()//其他用于恢复初始状态的参数设置
	{
		timerForLinkAtack = timerForLinkMax*1/timerDifficulty; //更新时间到初始上线
		startTimer = false;//关闭计时器
		index = 0;//完全重头开始
	}


	//这个搜索方式是减法机制的，因此需要在初始化个更新的时候获得所有的公式
	//减法机制：每按一个键就会从公式集合中删除掉不符合规范的额公式
	//如果公式集合空，则重建公式集合，之前的探索完全作废
	void flashLink()
	{
		//print ("makeFlash");
		//清空并重新获取所有的公式
		attackLinkMayUsing.Clear ();
		for (int i = 0; i < attackLinks.Length; i++) 
		{
			attackLinkMayUsing.Add (attackLinks[i]);
		}
	}


  public void makeStart()//这个方法是用于连招本身的初始化方法
	{
		//只有特殊的条件之下才可以重新开启
		if (isStarted)
			return;

		//print ("the attacklink controller started");
		attackLinks = this.GetComponentsInChildren<attackLink> ();
		attackLinkMayUsing = new List<attackLink> (); //重建这个对象
		attackBeDelete = new List<attackLink> ();//重建对象
		timerForLinkAtack *= 1/timerDifficulty;//根据难度调整时间
		timerForLinkMax = timerForLinkAtack; //唯一的一次对时间上限的写操作
		flashLink ();//更新列表
		index = 0;//完全重头开始
		flashLink ();//列表更新
		reMake();//参数更新
		makStartExtra ();//其他的初始化

		foreach (attackLink A in attackLinks)
			A.makeStart ();
		
		isStarted = true;

	}
	private void makStartExtra()//这个是用于与工程中其他的脚本进行联系的初始化
	{
		thePlayer = this.GetComponentInChildren<PlayerBasic> ();//获取游戏玩家对象
		theAnimator = this.GetComponentInChildren<Animator>();
	}

    ///////////////////////////////////////////////////////////////////////////////////////////////以下是真正的调用方法入口

	//这个应该是因为OnGUI比起Update刷新快造成的
	//这个方法在Update里面是无法使用的，events会被放空
	void OnGUI()
	{ 
		if (isStarted && !this.gameObject .tag.Equals( "AI"))
		{
				//为了保证更强大的兼容性暂定使用Event的方法，虽然这种方法有额外的开销
				//InputString是一个非常强势的方法，但是比较有缺陷的就是只是针对有输出的键位有效果
				//例如QWE等等，但是对于ctrl等等貌似无效果（作为一种替换的键位监测方案放在这里）
				//此外值得注意的就是InputString检测到的键位输出是小写的
				//print (Input.inputString+" isUsed");
			if (Input.anyKeyDown && systemValues.checkCanAttackAct() ) 
			{
				events = Event.current;
				if (events != null) 
				{
					if (events.isKey && events.keyCode != KeyCode.None) 
					{
						keyUse = events.keyCode;
						string codeUse = keyUse.ToString ();
						//if (codeUse != "J")//完全鼠标操作
							makeAttack (codeUse);//进入连招检查
					}
					//这是非常大的鼻酸，目前还没有办法分出阿里鼠标不同按钮的不同 
					//也是以很暴力的方法啊
					else if ( events.isMouse)
					{
						makeAttack ("J");//进入连招检查
					}
				}
			}
		}
	}


	//额外的一些动作控制内容
	private void controllWithPlayer()
	{
		//theAnimator.speed = thePlayer.ActerAttackSpeedPercent;
		if (theAnimator && thePlayer && isStarted) 
		{
			theAnimator.SetFloat ("ATKSpeed", thePlayer.ActerAttackSpeedPercent); 
			theAnimator.SetFloat ("MoveSpeed", thePlayer.ActerMoveSpeedPercent); 
		}
	}
    

//	void Update()
//	{
//		
//	}
//
	//这里只对计时器有更新
	//有一些内容，例如计时器，需要常常更新
	void makeUpdate()
	{
		//有些东西应该保持实时，例如攻击速度的变化不允许有太多的延迟
		controllWithPlayer ();
		if (isStarted&& startTimer && thePlayer && thePlayer.isAlive) 
		{
			//存在一个等待的时间
			timerForLinkAtack -= Time.deltaTime;
			if (timerForLinkAtack <= 0) 
			{
				flashLink ();//列表更新
				reMake();//参数更新
			}
		}
		//如果不在攻击状态就重置招式攻击伤害
		if (theAnimator && !systemValues.isAttacking (theAnimator) && thePlayer)
			thePlayer.extraDamageForAnimation = 0;
	}
		
	//有一些网络必要的逻辑也需要用start进行初始化一下
	void Start()
	{
		attackLinkMayUsing = new List<attackLink> (); //重建这个对象
		attackBeDelete = new List<attackLink> ();//重建对象
		if (this.gameObject.tag == "AI") 
			makeStart ();
		else 
			//不是AI，也就是游戏主人公，才会需要实时刷新检测连招信息
			//同时这个时间也受到这个程序的统一时钟控制
			InvokeRepeating("makeUpdate" , 0, systemValues.updateTimeWait);
	}

	//void Update () 
	//{
	//}
}
