﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraUse : MonoBehaviour 
{
	//控制摄像机行为的脚本
	//这个脚本兼具smoothfollow和mouseLook的做法

	public  Transform target;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float sensitivityWheel = 1f;//增大减小的参数
	//模式1
	public float distanceForMode1 = 3.0f;
	public float height = 5.0f;
	public float heightDamping;
	//额外的视野偏差 ，因为人物的中心在于脚底，这不是很好的选择
	//这个偏移量也可以考虑用在做摄像机抖动的变化
	public Vector3 Offset = Vector3.zero;
	public float minimumY = -60F;
	public float maximumY = 60F;
	float rotationY = 0F;
	float YPositionFixed = 0;
	Vector3 positionStart = Vector3.zero;
	//模式2
	private float modeX2 = 0.0f;
	private float ModeY2 = 0.0f;
	public float  distanceForMode2 = 2.5f;
	public float yMinLimitForMode2 = 10;
	public float yMaxLimitForMode2 = 80;
	//视野距离
	public float minFov = 5f;//摄像头最近距离
	public float maxFov = 10f;//摄像头最远距离
	float fov ;//获取当前摄像机的视野参数
	//额外引用
	private Camera theCamera;

	//死了才会用到的模式
	public  bool DeadMode = false;
	public  PlayerBasic thePlayer;
	//计算用临时变量
	private move theMoveCopntroller;

	void Start ()
	{
		theCamera = this.GetComponent <Camera> ();
		fov = theCamera.fieldOfView;
	}
	void LateUpdate()
	{
		//systemValues.isSystemPanelOpened标记附带的是Time.TimeScale = 0
		//但是这个只是限制了Update的运行，不会影响到这里，所以用的是标记的方法进行阻隔
		if (!systemValues.isSystemUIUsing()) 
		{
			makeFov ();
			checkDeadMode ();
			if (!DeadMode)
			{
				if (Input.GetKey (KeyCode.LeftControl))
					mode2 ();
				else
					mode1 ();
			} 
			else 
			{
				mode2 ();
			}
		}
	}


	void checkDeadMode()
	{
		if(thePlayer)
		DeadMode = !thePlayer.isAlive;
	}

	//摄像机模式1，游戏人物跟随摄像机旋转
	void mode1()
	{
		if (!target)
			return;

		//var wantedHeight = target.position.y + height + heightOffset;
		//var currentHeight = transform.position.y;

		//currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

		float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
		rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
		rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
		transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);

		Vector3 nowForCamera = new Vector3 (0,transform.localEulerAngles.y,0);
		//修正摄像机的位置
		Vector3 thePositionWithAdd = Vector3.zero;
		if (positionStart == Vector3.zero) 
		{
			positionStart = this.transform.position;
		} 
		else 
		{
			YPositionFixed -= Input.GetAxis ("Mouse Y")*0.3f;
			YPositionFixed = Mathf.Clamp (YPositionFixed, -0.5f, 0.5f);
			thePositionWithAdd = new Vector3 (0, YPositionFixed, 0);
		}

		transform.position = target.position;
		transform.position -= Quaternion.Euler(nowForCamera) * Vector3.forward * distanceForMode1;
		// Set the height of the camera

		transform.position = new Vector3(transform.position.x ,target.transform.position .y + height , transform.position.z) + thePositionWithAdd + transform.rotation * Offset;


		Vector3 now = target.transform.rotation.eulerAngles;
		Vector3 rotation = new Vector3 (now.x , this.transform .rotation .eulerAngles.y , now.z);

		Quaternion aim = Quaternion.Euler(rotation);
		//target.transform.rotation= aim;
		//原则上插值法一定会有一个微妙的延迟，感觉只有大佬才有可能用到
		//但是这确实要比赋值有了一点发挥的余地，至少感觉更有操作的感觉了
		target.transform.rotation = Quaternion.Lerp (target.transform.rotation , aim , 30f*Time .deltaTime);
			
		if (!theMoveCopntroller)
			theMoveCopntroller = target.GetComponent<move> ();
		
		theMoveCopntroller.yNow = now.y;
	}

	//摄像机模式2，旋转摄像机
	static float ClampAngle (float angle , float min , float max) 
	{
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}

	void  mode2()
	{
		if (target) 
		{
			modeX2 += Input.GetAxis("Mouse X") * sensitivityX;
			ModeY2 -= Input.GetAxis("Mouse Y") * sensitivityY ;

			ModeY2  = ClampAngle(ModeY2, yMinLimitForMode2, yMaxLimitForMode2);

			var rotation = Quaternion.Euler(ModeY2, modeX2, 0);
			var position = rotation * new Vector3(0.0f, 0.0f, -distanceForMode2) +( target.position+ Offset);

			transform.rotation = rotation;
			transform.position = position;
		}
	}

	//修改前后视距
	void makeFov()
	{
		//print("FOV: "+fov);
		fov += Input.GetAxis("Mouse ScrollWheel") * -sensitivityWheel;//根据鼠标滚轮充值这个参数
		//print("FOV2: "+fov);
		fov = Mathf.Clamp(fov, minFov, maxFov);//限制参数的值
		theCamera.fieldOfView= fov;
	}
}
