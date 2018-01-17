using UnityEngine;
using System.Collections;
using System;

public class liuguang : MonoBehaviour {

	public Material mat;

	/// <summary>
	/// 循环一次所用时间
	/// </summary>
	public float loopSecond = 4f;

	/// <summary>
	/// 循环的最小值
	/// </summary>
	public float minValuex = 0;

	/// <summary>
	/// 循环的最大值
	/// </summary>
	public float maxValuex = 1;


	/// <summary>
	/// 循环的最小值
	/// </summary>
	public float minValuey = 0;
	
	/// <summary>
	/// 循环的最大值
	/// </summary>
	public float maxValuey = 1;


	// Use this for initialization
	void Start () {
	
	}

	DateTime lastTime; 

	// Update is called once per frame
	void LateUpdate () 
	{
				float valuex = mat.mainTextureOffset.x;
				float valuey = mat.mainTextureOffset.y;

				float second = (float)(DateTime.Now - lastTime).TotalSeconds;
				lastTime = DateTime.Now;
				float bilix = (maxValuex - minValuex) * (second / loopSecond);
				float biliy = (maxValuey - minValuey) * (second / loopSecond);
				valuex += bilix;
				valuey += biliy;
				if (valuex > maxValuex)
						valuex = minValuex;
				if (bilix < minValuex)
						bilix = maxValuex;
				if (valuey > maxValuey)
						valuey = minValuey;
				if (valuey < minValuey)
						valuey = maxValuey;
				mat.mainTextureOffset = new Vector2 (valuex, valuey);	
	}
}
