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
	private float keyTimer =2f;//长按达到一定时间才可以切换 
	public float jumpMaxHeight =1.5f;//跳跃最高高度

	private CharacterController theController;//角色控制器
	private  Animator theAnimatorOfPlayer;//动画控制器
	private float margin =0.1f;//距离地面距离，小于这个距离被认为是在地面上
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

	public void makeStart()//初始化方法，由总控单元统一进行初始化
	{
		if (isStarted == false)
		{
			isStarted = true;
			//获取组件并且保留引用
			//允许这些组件是再组物体上面，这是因为这里面的资源是拼凑得到的，并不规整，在这里不得不使用一些折衷
			theController = this.GetComponentInChildren<CharacterController> ();
			theAnimatorOfPlayer = this.GetComponentInChildren<Animator> ();
		}
	}
		
	//这个用射线的方法其实不是很好用，有时候会返回一个不太对的值
	//其原因是本游戏中所有的任务的默认坐标都是贴地的
	bool IsGrounded()
	{    
		return Physics.Raycast(transform.position, -this.transform .up,  margin);    
	}  

	//计时器和额外加速在这里控制
	private void timerCheck()
	{
		if (Mathf.Abs (Input.GetAxis (forwardAxisName)) > 0.1f || Mathf.Abs (Input.GetAxis (upAxisName)) > 0.1f) {//如果有输入就逐步进行检测，长按与短按的时间并不一样
			keyNow += Time.deltaTime;//使用这个计时器进行计时

			if (theController.isGrounded == false || this.transform.position.y > 0.6f) 
			{
				keyNow += Time.deltaTime*2;//半空中速度积累速度更快
			}

			if (keyNow > keyTimer)//切换速度
				speedNow = speedRun;
			else
				speedNow = speedNormal;
		} 
		else 
		{
			keyNow = 0;//归零
			speedNow = speedNormal;
		}
	}
	public void MoveForwardBack(float  AxisValue)
	{
		Vector3 moveDirection = Vector3.zero;//刷新值这个值只需要计算位置增量就可以了
		float ZMove = 0f;
		this.theAnimatorOfPlayer.SetFloat ("forward", forwardA);//播放动画,具体内容需要看controller
		if (canMove ) 
		{
			ZMove = (speedNow ) * forwardA * Time.deltaTime;
		    moveDirection.z += ZMove;//向着正方向移动会有来自方向的速度加成同样在后退的时候速度会相对较低
		}

		Vector3 moveDirectionAction = transform.rotation * moveDirection;//旋转角度加权
		//在一定高度的半空中有一定的移动速度加成
		//这个效果只有在跳跃的时候才会触发
		if (this.transform.position.y > jumpMaxHeight / 2 && isJumping)
			moveDirectionAction.z += ZMove * 0.15f;//在半空中有额外25%的横向移动速度;

		if (theController && theController.enabled)//有时候需要强制无法移动
			theController.Move (moveDirectionAction);//真实地进行行动(因为使用的是characterController，因此使用坐标的方式似乎比较稳妥)

	}

	void MoveLeftAndRight(float AxisValue , float forwardA)
	{
		Vector3 moveDirection = Vector3.zero;
		//动作设定
		float minus = yNow - savedYaw;
		if (Mathf.Abs (minus) > 10)
		{
			savedYaw = yNow;
			if(forwardA<0)
			this.theAnimatorOfPlayer.Play ("rotatePoseBack");
			else
			this.theAnimatorOfPlayer.Play ("rotatePoseForward");
			//.SetFloat ("up", Mathf.Asin (minus) * 0.5f);//播放动画,具体内容需要看controller
		} 
		else
		{
			this.theAnimatorOfPlayer.SetFloat ("up", upA);//播放动画,具体内容需要看controller
		}
		if (canMove)
		{
			//移动设定
			if (AxisValue > 0.1f && AxisValue < 0.5f)
				AxisValue = Mathf.Max (AxisValue, 0.5f);
			float XAdd =  speedNow * AxisValue * Time.deltaTime;
			//下面注释的两行是一个很好的思想，但是因为y周上面的移动出现跳变，会有较大幅度的上下抖动
			moveDirection.x  += XAdd;//漫游之移动
		}

		if (theController && theController.enabled)//有时候需要强制无法移动
			theController.Move (transform.rotation *moveDirection);//真实地进行行动(因为使用的是characterController，因此使用坐标的方式似乎比较稳妥)
	}

	//有关跳跃的逻辑都在这里
	private float jumpTimer = 0f;
	private float jumpTimerMax= 1.2f;
	public bool isJumping = false;

	void Jump()
	{
		//刷新初始值
		Vector3 jumpAction = Vector3.zero;
		//按键检测
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			this.theAnimatorOfPlayer.Play ("jump");
			if (isJumping == false) 
			{
				jumpTimer = jumpTimerMax;
				isJumping = true;
			}
			else
			{
				overGroundTimer -= 0.05f;//减少重力持续，这样就像是继续向上用力冲
				if (overGroundTimer < 0)
					overGroundTimer = 0;
				jumpTimer += 0.15f ;//如果正在跳跃就增加跳跃持续时间
				systemValues.thePlayer.ActerSp *= 0.9f;//半空中施展轻功是需要消耗真气的
			}
		}
		//如果正在跳跃
		if (isJumping) 
		{
			jumpTimer -= Time.deltaTime;
			if (jumpTimer < 0) isJumping = false;

			if (theController && theController.enabled)//有时候需要强制无法移动
			{
				float adder = speedNow == speedNormal ? 8f:10f;
				//jumpTimer越来越小表现为上冲余力越来越不足
				jumpAction  += new Vector3 (0,jumpTimer,0) * Time .deltaTime * adder;
			}
		}

		//重力控制
		//有关重力的计算不论是否可以移动都应该进行
		//重力与是否跳跃也并没有关联
		if (theController　 && canGravity) {
			//自编写的伪重力公式随着在半空中的时间的长短获得一个不断增加的向下移动的趋势
			//重力会持续存在的
			jumpAction.y -= ((overGroundTimer * 4) + 3) * Time.deltaTime;
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

	   theController.Move (jumpAction);//真实地进行行动(因为使用的是characterController，因此使用坐标的方式似乎比较稳妥)
	   if (this.transform.position.y >= jumpMaxHeight)//高度达到一定限制之后不再允许继续向上移动
		this.transform.position = new Vector3 (this.transform.position.x, jumpMaxHeight, this.transform.position.z);

	}


	void Update ()
	{
		if (!isStarted)
			return;
		forwardA = Input.GetAxis (forwardAxisName);
		upA = Input.GetAxis (upAxisName);
	    MoveForwardBack(forwardA);
		MoveLeftAndRight (upA,forwardA );
	    Jump();
		timerCheck ();
	}
}
