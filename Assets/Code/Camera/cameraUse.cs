using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraUse : MonoBehaviour 
{
	//控制摄像机行为的脚本
	//这个脚本兼具smoothfollow和mouseLook的做法

	public  Transform target;
	public float distance = 10.0f;
	public float height = 5.0f;
	public float heightDamping;
	public float heightOffset = 1.0f;//额外的视野偏差高度，因为人物的中心在于脚底，这不是很好的选择
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float minimumY = -60F;
	public float maximumY = 60F;
	float rotationY = 0F;

	void LateUpdate()
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
		transform.position = target.position;
		transform.position -= Quaternion.Euler(nowForCamera) * Vector3.forward * distance;
		// Set the height of the camera
		transform.position = new Vector3(transform.position.x ,target.transform.position .y + height + heightOffset , transform.position.z);


		Vector3 now = target.transform.rotation.eulerAngles;
		Vector3 rotation = new Vector3 (now.x , this.transform .rotation .eulerAngles.y , now.z);
		Quaternion aim = Quaternion.Euler(rotation);
		target.transform.rotation= aim;
		target.GetComponent<move> ().yNow = now.y;

	}
}
