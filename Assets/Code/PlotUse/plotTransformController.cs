using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plotTransformController : MonoBehaviour  , plotActions {

	//每一帧的剧本操作的Trans的控制单元
	//其实大多只有旋转和移动
	//旋转和移动都通过插值来实现

	//受到控制的目标
	public Transform theTransToControl;
	//旋转的角度(大多的旋转都是Y轴的旋转)
	public float YAxisRotateAngle;
    //需要移动到的位置
	public Transform theAimTransform;
	//角度目标
	private Quaternion theAimQuaternion;

	private plotItem theItem;
	float speedForRotate = 1f;
	float speedForMove = 1f;
	//没办法，接口默认pulic
	//开始的时候统一调用
	public  void OnStart (plotItem theItemIn)
	{
		Vector3 newVector = theTransToControl.localRotation.eulerAngles + new Vector3 (0f,YAxisRotateAngle,0f);
		theAimQuaternion = Quaternion.Euler (newVector);
		//print ("aim r = "+theAimQuaternion.eulerAngles);
		theItem = theItemIn;

		speedForRotate =  theItem.theTimeForThisItem == 0 ? 0f : 1f/theItem.theTimeForThisItem ;

		if (theAimTransform && theTransToControl)
		{
			if (theItem.theTimeForThisItem > 0)
				speedForMove = Vector3.Distance (theTransToControl.position, theAimTransform.position) / (theItem.theTimeForThisItem * 0.8f);
			else
				speedForMove = 0f;
		}
	}

	//结束的时候统一调用
	public  void OnEnd()
	{
		if (theAimTransform)
			theTransToControl.position = theAimTransform.position;
		theTransToControl.rotation = theAimQuaternion;
	}
	//每一帧更新的时候统一调用
	public  void OnUpdate()
	{
		//print ("transform controlling");
		theTransToControl.localRotation = Quaternion.Lerp (theTransToControl.rotation , theAimQuaternion , speedForRotate  );
		if(theAimTransform && theTransToControl)
			theTransToControl.position = Vector3.MoveTowards(theTransToControl.position , theAimTransform.position , Time.deltaTime * speedForMove);
		//print (theTransToControl.localRotation );
	}

	//强制到达结束状态
	public void OnControlEnd()
	{

	}
}
