﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extraRotate : MonoBehaviour {

	//这个脚本的作用仅仅在于人物选择的时候的人物模型的旋转

	public float RotateSpeed = 65f;//旋转

	public  Vector3 theRotateVector = new Vector3 (0,1,0);

	public bool isPrivate = true;

	public void makeDestory()//这个脚本的自毁
	{
		Destroy (this);
	}

	void Start()
	{
		theRotateVector *= RotateSpeed; 
	}

	void Update () 
	{
		if(isPrivate)
			this.transform.Rotate (theRotateVector* Time.deltaTime , Space.Self);
		else
			this.transform.Rotate (theRotateVector * Time.deltaTime , Space.World);
	}
}
