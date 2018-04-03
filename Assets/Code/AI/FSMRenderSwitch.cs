using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMRenderSwitch : MonoBehaviour {

	//这个脚本不手动挂载！！！！！
	//这个脚本挂在AI人物的MeshRender子物体上面
	//在FSMStage上面Start里面自动挂载
	//用来控制FSMStage中计算的开始和结束

	//这并不是一个优秀的优化方法
	//实际上这有可能会有BUG，例如敌人没办法从玩家看不到的地方千里攻击玩家
	private FSMStage theStage = null ;

	void Start () 
	{
		theStage = this.GetComponentInParent<FSMStage> ();
	}


	void OnBecameVisible()
	{
		//AI重新激活
		theStage.makeAIStart();//如果一直看到，AI就会一直保持开启状态
	}

//	void OnBecameInvisible()
//	{
//		if(theStage)
//		theStage.theAiIsActing = false;
//	}

}
