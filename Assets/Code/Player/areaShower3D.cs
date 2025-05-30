﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class areaShower3D : MonoBehaviour {

	public float innerRadius = 0.2f;     //内半径
	public int Segments = 60;         //分割数  

	public MeshFilter meshFilterForAttackArea;
	public MeshFilter meshFilterForSearchArea;

	public PlayerBasic thePlayer;
	private bool started = false;

	void Start()
	{
		systemValues.theAreaRenders.Add (this.gameObject);
		started = true;
		this.gameObject.SetActive (false);
	}

	void OnEnable()
	{
		if(started)
		    makeFlash ();
	}


	public void makeFlash()
	{
		if(thePlayer)
		    makeAreaShow (thePlayer.theAttackAreaLength , thePlayer.theAttackAreaAngel , thePlayer.theViewAreaLength, thePlayer.theViewAreaAngel);
	}

	public void makeAreaShow (float theAttackAreaLength , float theAttackAreaAngel , float theSearchLength , float theSearchAngel)
	{
		meshFilterForAttackArea.mesh =  CreateMesh(theAttackAreaLength , 0.2f , theAttackAreaAngel , 60);
		meshFilterForSearchArea.mesh =  CreateMesh(theSearchLength , 0.2f , theSearchAngel , 60);
		meshFilterForAttackArea.transform.localRotation = Quaternion.Euler (0f, -90f+theAttackAreaAngel*0.5f,0f);
		meshFilterForSearchArea.transform.localRotation = Quaternion.Euler (0f, -90f+theSearchAngel*0.5f,0f);
	}



	private  Mesh CreateMesh(float radius, float innerradius ,float angledegree,int segments)
	{
		//vertices(顶点):
		int vertices_count = segments* 2+2;              //因为vertices(顶点)的个数与triangles（索引三角形顶点数）必须匹配
		Vector3[] vertices = new Vector3[vertices_count];       
		float angleRad = Mathf.Deg2Rad * angledegree;
		float angleCur = angleRad;
		float angledelta = angleRad / segments;
		for(int i=0;i< vertices_count; i+=2)
		{
			float cosA = Mathf.Cos(angleCur);
			float sinA = Mathf.Sin(angleCur);

			vertices[i] = new Vector3(radius * cosA, 0, radius * sinA);
			vertices[i + 1] = new Vector3(innerradius * cosA, 0, innerradius * sinA);
			angleCur -= angledelta;
		}

		//triangles:
		int triangle_count = segments * 6;
		int[] triangles = new int[triangle_count];
		for(int i=0,vi=0;i<triangle_count;i+=6,vi+=2)   
		{
			triangles[i] = vi;
			triangles[i + 1] = vi+3;
			triangles[i + 2] = vi + 1;
			triangles[i + 3] =vi+2;
			triangles[i + 4] =vi+3;
			triangles[i + 5] =vi;
		}

		//uv:
		Vector2[] uvs = new Vector2[vertices_count];
		for (int i = 0; i < vertices_count; i++)
		{
			uvs[i] = new Vector2(vertices[i].x / radius / 2 + 0.5f, vertices[i].z / radius / 2 + 0.5f);
		}

		//负载属性与mesh
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uvs;
		return mesh;
	}
}
