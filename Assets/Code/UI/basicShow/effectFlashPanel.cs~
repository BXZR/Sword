using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class effectFlashPanel : MonoBehaviour {

	//这个脚本专门用来控制游戏主人公身上的BUFF显示
	//动态生成和显示各种BUFF图标
	//BUFF 在这里为各种effect效果（不论好坏）
	//每一秒钟更新一次

	public GameObject theEffectShowButton;

	//生效颜色
	public Color EffectColor = Color.yellow;
	//失效颜色
	public Color NotEffectColor = Color.gray;

	//显示内容用这个容器来储存以及更新
	private List<ButtonEffectflasher> flashItem;
	List<ButtonEffectflasher> toDestroy;

	//这是一种很好实现的思路，但是也有致命缺点：开销太大并且增加额外显示不方便
	//更新方法
	//每间隔一定时间刷新就可以，用Invoke
	private void updateBuffShow()
	{
		//多个效果的时候新方法要比老方法开销大很多
		//十个效果时候 老方法7.1ms 新方法1.3ms
		//makeUpdateOld ();
		makeUpdateNew ();

	}


	void Start ()
	{
		flashItem = new List<ButtonEffectflasher> ();
		toDestroy = new List<ButtonEffectflasher> ();
		InvokeRepeating ("updateBuffShow" , 0f , systemValues.updateTimeWait);	
	}

	//这个方法保存已经存在的引用，这样可以减少获得引用的次数，，应该比老方法要高效一点
	void makeUpdateNew()
	{
		if (!systemValues.thePlayer)
			return;
		
		//尝试增加当前没有的effect
		//effectBasic[] theEffectbasics = systemValues.thePlayer.GetComponentsInChildren<effectBasic> ();
		//再一次强力优化，直接使用playerBasic里面保存的引用做就可以了，连获取都没有必要再次获取
		for (int i = 0; i < systemValues.thePlayer.Effects.Count; i++)
		{
			if (! systemValues.thePlayer.Effects[i].isShowing ())
				continue;
			
			if (!checkIfIsShowing ( systemValues.thePlayer.Effects[i])) //如果这个效果没有被展示
			{
				ButtonEffectflasher theNewEffect = new ButtonEffectflasher ();
				theNewEffect.makeStart (theEffectShowButton,this.transform, systemValues.thePlayer.Effects [i],EffectColor,NotEffectColor);
				flashItem.Add (theNewEffect);
			}
		}

		//更新显示 
		for (int i = 0; i < flashItem.Count; i++)
		{
			flashItem [i].makeUpdate();
		}

		//删除处理
		toDestroy.Clear ();
		for (int i = 0; i < flashItem.Count; i++)
		{
			if (flashItem [i].theEffect == null) 
			{
				toDestroy.Add (flashItem [i]);
			}
		}
		for (int i = 0; i < toDestroy.Count; i++) 
		{
			toDestroy [i].makeDestroy ();
			flashItem.Remove (toDestroy [i]);
		}
	}

	//检查这个效果是不是已经在显示中了
	bool  checkIfIsShowing(effectBasic theEffect)
	{
		for (int i = 0; i < flashItem.Count; i++) 
		{
			if (flashItem [i].theEffect == theEffect)
				return true;
		}
		return false;
	}

	//这个方法非常暴力，每一次检查都声称新的按钮，获得新的引用，并且还是多次，消耗很大
	void makeUpdateOld()
	{
		if (systemValues.thePlayer)
		{
			//清空当前存储内容
			informationMouseShow[] theBuffButtons = this.GetComponentsInChildren<informationMouseShow> ();
			for (int i = 0; i < theBuffButtons.Length; i++)
				Destroy (theBuffButtons [i].gameObject);
			//显示当前effectBuff信息
			effectBasic[] theEffectbasics = systemValues.thePlayer.GetComponentsInChildren<effectBasic> ();
			for (int i = 0; i < theEffectbasics.Length; i++)
			{
				if (theEffectbasics [i].isShowing ())
				{
					GameObject theButton = GameObject.Instantiate<GameObject> (theEffectShowButton);
					theButton.transform.SetParent (this.transform);
					//theButton.GetComponent <informationMouseShow> ().showText = theEffectbasics [i].theEffectName + "\n" + theEffectbasics [i].theEffectInformation;
					theButton.GetComponentInChildren<Text> ().text = theEffectbasics [i].getOnTimeFlashInformation ();

					Image front = theButton.transform.Find ("CoolingFrontPicture").GetComponent<Image> ();
					front.fillAmount = theEffectbasics [i].getEffectTimerPercent ();
					if (theEffectbasics [i].isEffecting)
						front.color = EffectColor;
					else
						front.color = NotEffectColor;
				}
			}
		}
	}
	

}

//将Button和effectBasic联系在一起
//减少GameObject.Instantiate以及销毁FameObject的数量
//这个地方的消耗实际上太过惊人了
class ButtonEffectflasher
{
	public GameObject theButton;
	public Text  theShowText;
	public Image theFillAmountImage;
	public effectBasic theEffect;
	public Color EffectColor;//生效颜色
	public Color NotEffectColor;//失效颜色
	private bool isEffecting = true;//保存引用，这样就不用每一次都颜色赋值了
	public void makeStart(GameObject theEffectShowButton , Transform father , effectBasic theEffectIn , Color effectColorIn , Color notEffectColorIn)
	{
		theButton = GameObject.Instantiate<GameObject> (theEffectShowButton);
		theButton.transform.SetParent (father.transform);
		//theButton.GetComponent <informationMouseShow> ().showText = theEffect.theEffectName + "\n" + theEffect.theEffectInformation;
		theShowText = theButton.GetComponentInChildren<Text> ();
		theFillAmountImage = theButton.transform.Find ("CoolingFrontPicture").GetComponent<Image> ();
		EffectColor = effectColorIn;
		NotEffectColor = notEffectColorIn;
		theEffect = theEffectIn;
		isEffecting = theEffectIn.isEffecting;
		theFillAmountImage.color = theEffect.isEffecting ? EffectColor : NotEffectColor;

	}

	public void makeUpdate()
	{
		if (theEffect) 
		{
			theShowText.text = theEffect.getOnTimeFlashInformation ();
			theFillAmountImage.fillAmount = theEffect.getEffectTimerPercent ();
			//改变状态的时候换颜色，顺带保存状态
			if (theEffect.isEffecting != isEffecting)
			{
				isEffecting = theEffect.isEffecting;
				theFillAmountImage.color = theEffect.isEffecting ? EffectColor : NotEffectColor;
			}
		}
	}
	public void makeDestroy()
	{
		//应用了null的判断来做，这样就有了通知自动销毁的感觉
		if (theEffect == null)  //效果消失就自我毁灭就好了
		{
			MonoBehaviour.Destroy (theButton.gameObject);
		}
	}
}
