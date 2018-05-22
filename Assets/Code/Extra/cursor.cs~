using UnityEngine;
using System.Collections;

public class cursor : MonoBehaviour {


	public Texture2D theNormalTexture;
	public Texture2D  EffectCursor;

	//下面这两个方法分别是mouseEnter和MouseExit中使用的
	//这些额外的接口是使用EventTrigger做的
	public void setEffectCursor()
	{
		Cursor .SetCursor (EffectCursor,new Vector2 (0,0),CursorMode.Auto);
	}

	public void setNormalCursor()
	{
		Cursor .SetCursor (theNormalTexture,new Vector2 (0,0),CursorMode.Auto);
	}
 
 
}
