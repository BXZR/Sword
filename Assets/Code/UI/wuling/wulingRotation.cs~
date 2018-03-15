using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wulingRotation : MonoBehaviour {

	//五灵轮盘，生生不息
	//五灵轮盘在选择不同的元素的时候的旋转
	//其实就是改变z轴的旋转欧拉角
	//顺带做插值

	private Quaternion theAimRotation = Quaternion.identity;
	RectTransform theTransform;
	//旋转变换所用的时间长度
	public float rotateSlerpTimer = 0.75f;
	//计算得到的旋转速度
	private float roateteSpeed = 2;

	public void rotateFixed(float aim)
	{
		theAimRotation = Quaternion.Euler (0, 0 , aim );
		//print ("rotateSpeed = "+ roateteSpeed );
	}

	void Start()
	{
		roateteSpeed = 7f;
		theTransform = this.GetComponent<RectTransform> ();
	}

	void Update()
	{
		theTransform.rotation = Quaternion.RotateTowards (theTransform.rotation , theAimRotation, roateteSpeed );
	}
}
