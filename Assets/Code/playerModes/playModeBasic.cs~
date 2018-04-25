using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playModeBasic : MonoBehaviour {

   //游戏玩法的基类
   //玩家应该是可以选择不同的玩法来进行游戏的
   //不同的游戏玩法会有不同的那么一点点的额外逻辑
   //而这一点额外附加的逻辑应该从这里进行分支处理
   //这里的控制是在systemValue也就是GM里面进行控制的

	//游戏玩法开始的时候的准备工作
	public virtual void OnGameStart(){}
	//玩法刷新的时候的需要做的事情，例如计时器和刷新BOSS
	//刷新的时间按照按照系统的时间控制单元的形式处理
	public virtual void OnGameUpdate(){}
	//检查方法，检查这个玩法是不是已经完事了
	public virtual bool isGameOver(){return false;}
	//结束方法
	public virtual void OnGameEnd(){}
}
