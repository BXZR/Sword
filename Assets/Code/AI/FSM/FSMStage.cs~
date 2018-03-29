using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSMStage : effectBasic {

	//操作FSM的操作类，也就是整个AI任务的操作类
	private NavMeshAgent theMoveController;//AI人物的移动控制类
	private attackLinkController theAttackLlinkController;//AI人物的动作控制类
	public FSMBasic theStateNow = new FSM_Search();
	private PlayerBasic thethis; 
	private Animator theAnimator;
	public float AIThinkTimer = 1.5f;//AI每隔一段时间再进行思考
	public float AIThinkTimerMax = 1.5f;

	//仇恨时间
	public float angerTimer = 3f;//仇恨时间，也是追击的总时长
	public float angetTimerMax = 3f;//仇恨时间上限

	bool isDeadMake = false;

	void Start ()
	{
		thethis = this.GetComponent <PlayerBasic> ();
		theMoveController = this.GetComponentInChildren<NavMeshAgent> ();
		theAttackLlinkController = this.GetComponent<attackLinkController> ();
		theAnimator = this.GetComponentInChildren<Animator> ();
		//这里需要建立一个初始的状态
		theStateNow.makeState (theMoveController , theAttackLlinkController , theAnimator ,thethis);
		theStateNow.OnFSMStateStart ();
		//print ("AI stage is inited");
	}

	public override bool isBE ()
	{
		return true;
	}

	public override bool isShowing ()
	{
		return false;
	}

	public override void OnBeAttack (PlayerBasic attacker)
	{
		//Vector3 minus = new Vector3 (0f , attacker.transform.rotation.eulerAngles.y - this.transform.rotation.eulerAngles.y , 0f);
		//this.transform.rotation = Quaternion.Lerp(this.transform.rotation , Quaternion.Euler(minus + this.transform .rotation.eulerAngles) , 360f);
		this.transform.LookAt (attacker.transform);
		try
		{
		this.GetComponent <NavMeshAgent> ().SetDestination (this.transform .position);
		}
		catch
		{
			print ("当前处于无法被设置目标的状态，因此AI会继续向原先的目的地移动");
		}
		angerTimer = angetTimerMax;//收到攻击的时候刷新仇恨
	}


	void angerCanculate()
	{
		if (theStateNow != null && thethis.isAlive) 
		{
			if (theStateNow.geID () == 1)//攻击状态
			{
				angerTimer = angetTimerMax;//攻击的时候刷新仇恨
			}
			else if(theStateNow.geID () == 2 || theStateNow.geID () == 3)//跳跃和追击状态实际上都是在追杀目标
			{
				angerTimer -= Time.deltaTime;
				if (angerTimer < 0) //追太久就不要追下去了
				{
					this.GetComponent <NavMeshAgent> ().enabled = true;
					FSM_Search search = new FSM_Search ();
					search.makeState (this.theMoveController, this.theAttackLlinkController,this.theAnimator, theStateNow .theThis);
					theStateNow =  search;
				}
			}
		}
	}

    //很多操作都是连续的，对于AI来说或许用连续的方法计算会比较好
	void Update () 
	{
		angerCanculate ();

		if (theStateNow != null && thethis.isAlive) 
		{
			
			//AI操作
			//print ("AI is acting");
			theStateNow.actInThisState ();
			//AI转换状态
			AIThinkTimer -= Time.deltaTime;
			if (AIThinkTimer < 0) 
			{
				AIThinkTimer = AIThinkTimerMax;
				//print ("stateNowID = "+ theStateNow.geID());
				FSMBasic theStateNew = theStateNow.moveToNextState ();//思考进入到下一个状态或许可以慢一点进行
				if (theStateNew.geID () != theStateNow.geID ()) 
				{
					theStateNow.OnFSMStateEnd ();//结束效果
					theStateNow = theStateNew;
					theStateNew.OnFSMStateStart ();//开始效果
				}
			}
		}
		else if (isDeadMake == false)
		{
			isDeadMake = true;
			this.GetComponent <NavMeshAgent> ().enabled = false;
			this.gameObject.AddComponent<Rigidbody> ();
			this.gameObject.AddComponent<BoxCollider> ();
			Destroy (this.gameObject ,15f);//尸体保留久一点似乎更好玩（尸体也是路障啊）
		}
	}
}
