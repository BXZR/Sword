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
		this.GetComponent <NavMeshAgent> ().SetDestination (this.transform .position);
	}
    //很多操作都是连续的，对于AI来说或许用连续的方法计算会比较好
	void Update () 
	{
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
