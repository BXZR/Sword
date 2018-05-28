using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

	//极简主义的移动代码
	//通过向AnimatorController传入当前的两个轴的值来操纵动画的合成和播放
	[HideInInspector]//为了保证设定面板的简洁，暂时隐藏之
	public string forwardAxisName = "Vertical";//向前移动的轴名称
	[HideInInspector]//为了保证设定面板的简洁，暂时隐藏之
	public string upAxisName = "Horizontal";//向上移动的轴名称

	private float speedNow =0.6f;//一般来说都是先移动的
	public float speedNormal =0.6f;//一般移动的移动速度
	public float speedRun =1.25f;//狂奔的移动速度
	private float keyNow = 0f;//长按达到一秒钟才可以切换 这个是当前的计时器
	private float keyTimer =0.5f;//长按达到一定时间才可以切换，也就是奔跑的起步时间
	public float jumpMaxHeight =1.5f;//跳跃最高高度

	private CharacterController theController;//角色控制器
	private  Animator theAnimatorOfPlayer;//动画控制器
	private float margin =0.5f;//距离地面距离，小于这个距离被认为是在地面上
	private float overGroundTimer = 0f;//离开地面的时间,离开地面时间越长，迫使下降的数量就会越大
	//所以，即便是一直按住向上的键位，也会因为时间的因素下降
	//////////////////////////////////////////////////////////////////////////////////////////////////////

	//这是一些计算用的值，保留其引用不知道能不能起到一点点的优化作用
	float forwardA = 0;//记录输入轴横轴的值，减少Input的获取
	float upA = 0;//记录输入轴的纵轴的值减少Input的获取
	public bool canMove = true;//此单位可以被移动
	public bool canGravity = true;//存在重力（大多数情况下是需要考虑重力的）
	//是否开始运动标记
	private bool isStarted = false;//在makeStart里面才会进行唯一一次的初始化
	private float savedYaw = 0;//跟随鼠标转身的时候的动作需要用这个判断是不是开启
	private float yawChangeGate = 10;//超过这个数值就需要做动作了
	public float yNow = 0;//需要传入的标记
	//游戏玩家引用
	private PlayerBasic thePlayer;
	PhotonView photonView;//网络控制单元
	//有关跳跃的逻辑都在这里
	private float jumpTimer = 0f;
	private float jumpTimerMax= 1.1f;
	public bool isJumping = false;
	public bool isOverWall = false;//站在墙边可以进行多段跳跃

	public bool ____________________________________;//这个是分界线，没有实际功用
	public AudioClip theMoveSound;//移动脚步声
	public AudioClip theJumpSound;//跳跃开始声音

	Vector3 moveDirection = Vector3.zero;

	public void makeStart()//初始化方法，由总控单元统一进行初始化
	{
		if (isStarted == false)
		{
			isStarted = true;
			//获取组件并且保留引用
			//允许这些组件是再组物体上面，这是因为这里面的资源是拼凑得到的，并不规整，在这里不得不使用一些折衷
			theController = this.GetComponentInChildren<CharacterController> ();
			theAnimatorOfPlayer = this.GetComponentInChildren<Animator> ();
			thePlayer = this.GetComponent<PlayerBasic> ();
			photonView = PhotonView.Get(this);
		}
	}
		
	//这个用射线的方法其实不是很好用，有时候会返回一个不太对的值
	//其原因是本游戏中所有的任务的默认坐标都是贴地的
	bool IsGrounded()
	{    
		return Physics.Raycast(transform.position, -this.transform .up,  margin);    
	}  

	//计时器和额外加速在这里控制
	private void timerCheck(float forwardA , float upA)
	{
		if (Mathf.Abs (forwardA ) > 0.1f || Mathf.Abs (upA) > 0.1f) {//如果有输入就逐步进行检测，长按与短按的时间并不一样
			keyNow += Time.deltaTime;//使用这个计时器进行计时

			if (this.transform.position.y > 0.6f) 
			{
				keyNow += Time.deltaTime*2;//半空中速度积累速度更快
			}

			//切换速度
			speedNow =  keyNow > keyTimer ? speedRun : speedNormal;
		} 
		else 
		{
			keyNow = 0;//归零
			speedNow = speedNormal;
		}
	}
	public void MoveForwardBack(float  AxisValue)
	{
		//print (this.gameObject .name +" is moving forward");
		float ZMove = 0f;
		//单机动作控制
		this.theAnimatorOfPlayer.SetFloat ("forward", AxisValue);//播放动画,具体内容需要看controller //////////////////////////////////

		ZMove = (speedNow ) * AxisValue * Time.deltaTime;
		moveDirection.z += ZMove * thePlayer.ActerMoveSpeedPercent;//向着正方向移动会有来自方向的速度加成同样在后退的时候速度会相对较低

		Vector3 moveDirectionAction = transform.rotation * moveDirection;//旋转角度加权
		//在一定高度的半空中有一定的移动速度加成
		//这个效果只有在跳跃的时候才会触发
		if (jumpTimer > jumpTimerMax * 0.5f && isJumping)
			moveDirectionAction.z += ZMove * 0.25f;//在半空中有额外25%的凌空移动速度效率;
		
//		if (this.gameObject.tag == "AI")
//		{
//			if (theController && theController.enabled)//有时候需要强制无法移动
//				print("AI可以移动" + moveDirectionAction.z );
//		}
	}

	void MoveLeftAndRight(float AxisValue , float forwardA)
	{
		
		//动作设定
		float minus = yNow - savedYaw;
		if (Mathf.Abs (minus) > 10)
		{
			savedYaw = yNow;
			if(forwardA<0)
			{
				//单机动作控制
				if(systemValues.theGameSystemMode == GameSystemMode.PC)
					playModeAnimations("rotatePoseBack"); 
				if(systemValues.theGameSystemMode == GameSystemMode.NET)//有些功能只在网络对战模式之下用就行
				   this.photonView.RPC("playModeAnimations",PhotonTargets.All,"rotatePoseBack");
			}
			else
			{
				//单机动作控制
				if(systemValues.theGameSystemMode == GameSystemMode.PC)
					playModeAnimations("rotatePoseForward"); 
				if(systemValues.theGameSystemMode == GameSystemMode.NET)//有些功能只在网络对战模式之下用就行
				   this.photonView.RPC("playModeAnimations",PhotonTargets.All,"rotatePoseForward");
			}
			//.SetFloat ("up", Mathf.Asin (minus) * 0.5f);//播放动画,具体内容需要看controller
		} 
		else
		{
			//单机动作控制
			this.theAnimatorOfPlayer.SetFloat ("up", AxisValue);//播放动画,具体内容需要看controller //////////////////////////////////
		}

		//移动设定
		if (AxisValue > 0.1f && AxisValue < 0.5f)
			AxisValue = Mathf.Max (AxisValue, 0.5f);
		float XAdd =  speedNow * AxisValue * Time.deltaTime;
		//下面注释的两行是一个很好的思想，但是因为y周上面的移动出现跳变，会有较大幅度的上下抖动
		moveDirection.x  += XAdd * thePlayer.ActerMoveSpeedPercent;//漫游之移动

	}



	public void makeJump(bool useSP = true)
	{
		if (isJumping == false)
		{
			//单机动作控制
			//this.theAnimatorOfPlayer.Play ("jump");//////////////////////////////////
			if (systemValues.theGameSystemMode == GameSystemMode.NET)//有些功能只在网络对战模式之下用就行
			this.photonView.RPC ("playModeAnimations", PhotonTargets.All, "jump");
			else if (systemValues.theGameSystemMode == GameSystemMode.PC)
				playModeAnimations ("jump");

			jumpTimer = jumpTimerMax;

			if(useSP)
			{
			//耗蓝控制--------------------------------------------------
			float spUse = thePlayer.ActerSpMax * 0.05f;//施展轻功是需要消耗真气的;
			if (systemValues.theGameSystemMode == GameSystemMode.PC)
			UseSP (spUse);
			if (systemValues.theGameSystemMode == GameSystemMode.NET)//有些功能只在网络对战模式之下用就行
			this.photonView.RPC ("UseSP", PhotonTargets.All, spUse);
			//-----------------------------------------------------------
			}
			thePlayer.theAudioPlayer.playClip (theJumpSound);
			isJumping = true;
		}
	}
	void Jump()
	{
		//刷新初始值
		//Vector3 jumpAction = Vector3.zero;

		//按键检测
		if (Input.GetKeyDown (KeyCode.Space) ) 
		{

			makeJump ();
			//小于一定的阀值就不可以“凌空提气”
			//但是说实话“凌空提气的效果并不是非常的好”
			if(thePlayer.ActerSp / thePlayer.ActerSpMax > 0.4f)
			{
				if(isOverWall  )
				{
					isOverWall = false;
					overGroundTimer -= 0.05f;//减少重力持续，这样就像是继续向上用力冲
					if (overGroundTimer < 0)
						overGroundTimer = 0;
					jumpTimer += 0.15f ;//如果正在跳跃就增加跳跃持续时间
					//耗蓝控制--------------------------------------------------
					float spUse = thePlayer.ActerSpMax * 0.05f;//施展轻功是需要消耗真气的;
					if(systemValues.theGameSystemMode == GameSystemMode.PC)
						UseSP(spUse);
					if(systemValues.theGameSystemMode == GameSystemMode.NET)//有些功能只在网络对战模式之下用就行
						this.photonView.RPC("UseSP",PhotonTargets.All,spUse);
					//-----------------------------------------------------------

				}

			  else if(isJumping)
			   {
					overGroundTimer -= 0.04f;//减少重力持续，这样就像是继续向上用力冲
					if (overGroundTimer < 0)
						overGroundTimer = 0;
					jumpTimer += 0.07f ;//如果正在跳跃就增加跳跃持续时间
					//耗蓝控制--------------------------------------------------
					float spUse = thePlayer.ActerSpMax * 0.01f;//施展轻功是需要消耗真气的;
					if(systemValues.theGameSystemMode == GameSystemMode.PC)
						UseSP(spUse);
					if(systemValues.theGameSystemMode == GameSystemMode.NET)//有些功能只在网络对战模式之下用就行
						this.photonView.RPC("UseSP",PhotonTargets.All,spUse);
					//-----------------------------------------------------------

				}
			}
		}
		//如果正在跳跃
		if (isJumping ) 
		{
			//thePlayer.ActerSp -= thePlayer.ActerSpUp * Time.deltaTime;
			jumpTimer -= Time.deltaTime *2f;

			if (theController && theController.enabled)//有时候需要强制无法移动
			{
				float adder =  (speedNow == speedNormal ? 6f:9f) *thePlayer.ActerMoveSpeedPercent;
				adder = Mathf.Clamp (adder ,  6f , 10f);
				//jumpTimer越来越小表现为上冲余力越来越不足
				//jumpAction  += new Vector3 (0,jumpTimer *2f,0) * Time .deltaTime * adder;
				moveDirection += new Vector3 (0,jumpTimer *2f,0) * Time .deltaTime * adder;
			}

			if (IsGrounded() && jumpTimer < 0) 
			{
				thePlayer.theAudioPlayer.playClip (theMoveSound);
				isJumping = false;
			}
		}
			


	}

	//重力控制
	//有关重力的计算不论是否可以移动都应该进行
	void gravtyMove()
	{
		//重力与是否跳跃也并没有关联
		if (theController　 && canGravity) 
		{
			//自编写的伪重力公式随着在半空中的时间的长短获得一个不断增加的向下移动的趋势
			//重力会持续存在的
			moveDirection.y -= ((overGroundTimer * 3) + 5) * Time.deltaTime;
			if (isJumping) 
			{
				overGroundTimer += Time.deltaTime;//不在地上就进行计时，获得随着离地时间线性增长的向下移动的趋势
				//这里不适合恒力作为重力模拟
			} 
			else 
			{
				overGroundTimer = 0f;//归零
			}
		}
	}

	[PunRPC]
	private void UseSP(float SPUse)
	{
		if (this.thePlayer == null)
			this.thePlayer = this.GetComponent <PlayerBasic> ();

		if (this.thePlayer.ActerSp > SPUse) 
		{
			this.thePlayer.ActerSp -= SPUse;
		}
		else
		{
			//移动中的透支情况和攻击中的法力并不完全相同
			float damageUse = SPUse - thePlayer.ActerSp;
			damageUse = Mathf.Clamp (damageUse , 5f,100f);
			thePlayer.ActerSp = 0;
			thePlayer.ActerHp -= damageUse;
			if (thePlayer.ActerHp < 10)
				thePlayer.ActerHp = 10f;//保护机制，在格斗游戏中没有透支身亡一说
		}
	}

	//按住左边shift键，移动速度增加
	//其实这个功能主要用处是耗蓝的统一处理，至于位移，应该直接用transform来侦测就比较好
	private bool isShifting = false;
	private float speedAdderWithShift = 0;//消耗真气的疯狂移动
	private  void fastMoveCheck()
	{
		if (Input.GetKey (KeyCode.LeftShift))
		{
			//有些功能只在网络对战模式之下用就行
			if (systemValues.theGameSystemMode == GameSystemMode.NET && this.photonView) 
				this.photonView.RPC ("makeShift", PhotonTargets.All);
			else if (systemValues.theGameSystemMode == GameSystemMode.PC)
				makeShift();
		}
		if (Input.GetKeyUp (KeyCode.LeftShift) || thePlayer.ActerSp <20)
		{
			//有些功能只在网络对战模式之下用就行
			if (systemValues.theGameSystemMode == GameSystemMode.NET && this.photonView)
				this.photonView.RPC ("makeShiftEnd", PhotonTargets.All);
			else if (systemValues.theGameSystemMode == GameSystemMode.PC)
				makeShiftEnd();
		}
	}

	[PunRPC]
	void makeShiftEnd()
	{
		if (isShifting == true) 
		{
			isShifting = false;
			speedNormal -= speedAdderWithShift;
			speedRun -= speedAdderWithShift;
		}
	}
	[PunRPC]
	void  makeShift()
	{
		if(isShifting == false)
		{
			isShifting = true;
			speedAdderWithShift  = speedNormal * 0.75f;
			speedNormal += speedAdderWithShift;
			speedRun += speedAdderWithShift;
		}
		if(thePlayer)
		    UseSP( thePlayer.ActerSpUp* Time.deltaTime * 2f);
	}

	//移动的究极大方法
	[PunRPC]
	void moveForAll(float forwardA , float upA)
	{
		if (!isStarted)
			return;
		
		//MoveForwardBack(forwardA);
		//MoveLeftAndRight (upA,forwardA );
		Jump();
		//fastMoveCheck ();
		//timerCheck ();
	}

	[PunRPC]
	void playModeAnimations(string nameIn)
	{
		if (this.theAnimatorOfPlayer) 
		{
			//print (this.gameObject.name+"--" + nameIn);
			this.theAnimatorOfPlayer.Play (nameIn);
		}
	}
	[PunRPC]
	void playModeAnimationsAxis( string AxisName, float value)
	{
		if(!this.theAnimatorOfPlayer)
			theAnimatorOfPlayer = this.GetComponentInChildren<Animator> ();
		print (this.thePlayer.ActerName +"--" + value);
		 this.theAnimatorOfPlayer.SetFloat (AxisName, value);
	}


	//获取输入并做出一点限制
	void getInput()
	{
		forwardA = Input.GetAxis (forwardAxisName);
		upA = Input.GetAxis (upAxisName);
		forwardA = Mathf.Abs (forwardA) < 0.6f ? 0f : forwardA;
		upA = Mathf.Abs (upA) < 0.6f ? 0f : upA;
	}

	void trueMove()
	{
		if (theController && theController.enabled)//有时候需要强制无法移动
			theController.Move (transform.rotation *moveDirection);//真实地进行行动(因为使用的是characterController，因此使用坐标的方式似乎比较稳妥)
		if (this.transform.position.y >= jumpMaxHeight)//高度达到一定限制之后不再允许继续向上移动
			this.transform.position = new Vector3 (this.transform.position.x, jumpMaxHeight, this.transform.position.z);
	}


	void playModeSound(float forwardA , float upA)
	{
		if (isJumping || !IsGrounded())
			return;
		if (Mathf.Abs (forwardA) > 0.25f || Mathf.Abs (upA) > 0.25f) 
		{
			//print ("play move sound");
			thePlayer.theAudioPlayer.playClip (theMoveSound,false);
		}
	}

	//移动的计算因为是人看到的，所以还是应该更加连贯
	void Update ()
	{
		if (!isStarted || !canMove )
			return;

		moveDirection = Vector3.zero;
		getInput ();
		gravtyMove ();
		MoveForwardBack(forwardA);
		MoveLeftAndRight (upA,forwardA );
		Jump();
		fastMoveCheck ();
		timerCheck (forwardA , upA);
		playModeSound (forwardA, upA);
		trueMove ();//真正的移动

		//这是一个更加强制的网络移动的调用，但是实际上没有要这样做
		//因为transform的观察已经包含了位移
		//所以只需要在适当的时候作出相应的动画就可以了
		//但是为了保证更多的属性的控制，应该懂这种方法
		//this.photonView.RPC ("moveForAll", PhotonTargets.All, forwardA, upA);
		if(this.photonView == null)
			photonView = PhotonView.Get(this);
		
		//moveForAll (forwardA, upA);
	}

	void Start ()
	{
		if (this.gameObject.Equals("AI"))//AI用的是自动导航组件，这个脚本仅仅针对玩家控制的单位
		{
			Destroy (this);
			//makeStart (); 
		}
		theAnimatorOfPlayer = this.GetComponentInChildren<Animator> ();

	}

	void OnTriggerEnter(Collider A)
	{
		if (A.tag.Equals( "Wall")) 
		{
			//print ("is over wall");
			isOverWall = true;
		} 
		else 
		{
			isOverWall = false;
		}

	}
}
