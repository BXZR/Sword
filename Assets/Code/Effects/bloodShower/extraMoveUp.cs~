using UnityEngine;
using System.Collections;

public class extraMoveUp : MonoBehaviour
{
	//显示生命变化信息的数值的向上移动(使用插值的方法)

	private bool isStarted = false;
	private Vector3 theTextMoveAim ;//这个3dtext的移动目标，移动到某地方之后就不再移动了
	//可以累加显示数值
	private float valueSave = 0;
	//TextMesh引用
	private TextMesh theTextMesh ;
	float timerLife = 0.5f;//显示（生存）的时间

	//显示的颜色或者其他设置信息在这里实现
	// 0 自己打出来的伤害
	// 1 自己受到的伤害
	// 2 自己恢复的生命 
	public void makeColor(int mode = 0)
	{
		switch (mode)
		{
		case 0:
			theTextMesh.color = Color.yellow;
			break;
		case 1:
			theTextMesh.color = Color.red;
			break;
		case 2:
			theTextMesh.color = Color.green;
			break;
		case 3:
			theTextMesh.color = Color.magenta;
			break;
		}
	}

	//也可直接做颜色赋值
	public void makeColor(Color A)
	{
		theTextMesh.color = A;
	}

	public void  makeStart(Vector3 theTextMoveAimIn,string theShowText,float lifeTimeIn = 1f)
	{
		theTextMoveAim = theTextMoveAimIn;
		theTextMesh = this.GetComponentInChildren<TextMesh> ();
		theTextMesh .text = theShowText;
		timerLife = lifeTimeIn;
		isStarted = true;
		Invoke ("makeEnd", 3f);//最多持续秒，必须要消失
	}

	public void  makeStart(Vector3 theTextMoveAimIn,float showValue,float lifeTimeIn = 1f)
	{
		valueSave = showValue;
		theTextMoveAim = theTextMoveAimIn;
		theTextMesh = this.GetComponentInChildren<TextMesh> ();
		theTextMesh .text = showValue.ToString ("f0");
		timerLife = lifeTimeIn;
		isStarted = true;
		Invoke ("makeEnd", 3f);//最多持续秒，必须要消失
	}

	void makeEnd()
	{
		CancelInvoke ();
		systemValues.addIntoTheTextPool (this.gameObject);
		isStarted = false;
		valueSave = 0;
		timerLife = 0.5f;//显示（生存）的时间
	}

	public void  makeUpdate(float showValue,float lifeTimeAdd = 0.1f)
	{
		valueSave += showValue;
		theTextMesh .text = valueSave.ToString ("f0");
		timerLife += lifeTimeAdd;
	}

	void Update ()
	{
		if (isStarted) 
		{
			this.transform.LookAt (Camera.main.transform);
			this.transform.position = Vector3.Lerp (this.transform.position, theTextMoveAim, Time.deltaTime / 2);
			timerLife -= Time.deltaTime;
			if (timerLife < 0)
				systemValues.addIntoTheTextPool (this.gameObject);
				//Destroy (this.gameObject);
		}
	}
}

