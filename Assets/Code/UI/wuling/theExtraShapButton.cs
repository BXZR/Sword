using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//额外不同形状的Button


//[RequireComponent(typeof(PolygonCollider2D))]
public class theExtraShapButton :MonoBehaviour// Image 
{

	void Start()
	{
		this.GetComponent<Image> ().alphaHitTestMinimumThreshold = 0.1f;
	}
//	void OnMouseDown()
//	{
//		Debug.Log("Touched!");
//	}
///////////////////////////////////////
//	private PolygonCollider2D _polygon = null;
//	private PolygonCollider2D polygon 
//	{
//		get{
//			if(_polygon == null )
//				_polygon = GetComponent<PolygonCollider2D>();
//			return _polygon;
//		}
//	}
//	protected theExtraShapButton()
//	{
//		useLegacyMeshGeneration = true;
//	}
//	protected override void OnPopulateMesh(VertexHelper vh)
//	{
//		vh.Clear();
//	}
//	public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
//	{
//		if (eventCamera == null)
//			print ("no event camera");
//		Vector3 theA = eventCamera.ScreenToWorldPoint (screenPoint);
//			return polygon.OverlapPoint(theA );
//	}
}
