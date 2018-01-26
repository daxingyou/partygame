using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using Newtonsoft.Json;

public static class Utils
{
	public static T FindObject<T> (this MonoBehaviour obj) where T : class
	{
		T o = GameObject.FindObjectOfType (typeof(T)) as T;
		if (o == null)
			Debug.LogWarning (string.Format ("Game object '{0}' could not find object of type {1}.", obj.gameObject.name, typeof(T).Name));

		return o;
	}

	public static T FindObject<T> () where T : class
	{
		T[] objects = GameObject.FindObjectsOfType (typeof(T)) as T[];
		
		if (objects.Length == 0)
		{
			Debug.LogWarning (string.Format ("Could not find object of type {0}.", typeof(T).Name));
		}
		else if (objects.Length > 1)
		{
			Debug.LogWarning (string.Format ("More than one instance found of type {0}.", typeof(T).Name));
		}
		
		return objects [0];
	}

	public static T[] RandomSort<T> (T[] array)
	{
		int len = array.Length;
		System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int> ();
		T[] ret = new T[len];
		System.Random rand = new  System.Random ();
		int i = 0;
		while (list.Count < len)
		{
			int iter = rand.Next (0, len);
			if (!list.Contains (iter))
			{
				list.Add (iter);
				ret [i] = array [iter];
				i++;
			}

		}
		return ret;
	}

	public static T FindComponentInParents<T> (this MonoBehaviour obj) where T : UnityEngine.Component
	{
		return FindComponentInThisOrParents<T> (obj.transform.parent);
	}

	public static T FindComponentInThisOrParents<T> (Transform t) where T : UnityEngine.Component
	{
		Transform current = t;
		while (current != null)
		{
			T c = t.GetComponent<T> ();
			if (c != null)
			{
				return c;
			}
				
			current = current.parent;
		}
		
		return null;
	}

    public static Transform GetChildByName(string name, GameObject go)
    {
        Transform result = null;
        Transform t = go.transform.FindChild(name);

        if (t != null)
            return t;

        for (int i = 0; i < go.transform.childCount; i++)
        {
            t = go.transform.GetChild(i);
            result = GetChildByName(name, t.gameObject);
            if (result != null)
                return result;
        }
        return null;
    }

    public static T GetChildComponent<T>(Transform t)
    {
        T result = t.GetComponentInChildren<T>();
        if (result != null)
            return result;

        for (int i = 0; i < t.childCount; i++)
        {
            t = t.GetChild(i);
            result = GetChildComponent<T>(t);
            if (result != null)
                return result;
        }
        return default(T);
    }

    public static T FindDirectChildComponent<T>(string name, Transform obj)
    {
        var child = obj.Find(name);
        if (child == null)
        {
            return default(T);
        }

        return child.GetComponent<T>();
    }

    public static string GetLongName (Transform transform)
	{
		return transform == null ? "" : GetLongName (transform.parent) + "/" + transform.name;   
	}

	public static string GetLongNameList (UnityEngine.Component[] components)
	{
		return string.Join (", ", new List<UnityEngine.Component> (components).ConvertAll (c => GetLongName (c.transform)).ToArray ());
	}

	public static void Bar (string text, float ratio, int offset, Color color)
	{
		float padding = 10;
		float height = 20;
		GUI.color = color;
		GUI.Button (new Rect (padding, Screen.height - height - padding - offset * height, (Screen.width - 2 * padding) * ratio, height), text);
	}

	//获取路径//
	public static string GetDataPath ()
	{
		// Your game has read+write access to /var/mobile/Applications/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX/Documents
		// Application.dataPath returns ar/mobile/Applications/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX/myappname.app/Data             
		// Strip "/Data" from path

		return Application.persistentDataPath;

		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			string path = Application.dataPath.Substring (0, Application.dataPath.Length - 5);
			// Strip application name
			path = path.Substring (0, path.LastIndexOf ('/')); 
			return path + "/Documents";
		}
		else if (Application.platform == RuntimePlatform.Android)
		{
			return Application.persistentDataPath;
		}
		else
			//    return Application.dataPath + "/Resources";
			return Application.persistentDataPath;
	}

	public static string SecondToTimeStr (int seconds, bool showSec = false, bool showHour = true)
	{
		string str = "";
		int hour = 0;
		int minute = 0;
		int sec = 0;
		sec = seconds;
		if (sec > 60)
		{
			minute = sec / 60;
			sec = sec % 60;
		}

		if (minute > 60)
		{
			hour = minute / 60;
			minute = minute % 60;
		}
		string strHour = hour >= 10 ? hour.ToString () : "0" + hour;
		string strMinute = minute >= 10 ? minute.ToString () : "0" + minute;
		string strSecond = sec >= 10 ? sec.ToString () : "0" + sec;

		if (showHour)
		{
			str += strHour + ":";
		}
		str += strMinute;  //格式： 时：分
		if (showSec)
		{
			str += ":" + strSecond;
		}
		return str;
	}

	/// <summary>  
	/// 时间戳转为C#格式时间  
	/// </summary>  
	/// <param name="timeStamp">Unix时间戳格式</param>  
	/// <returns>C#格式时间</returns>  
	public static DateTime GetDateTimeByTimeStamp (long timeStamp)
	{  
		DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime (new DateTime (1970, 1, 1));  
		long lTime = long.Parse (timeStamp + "0000000");  
		TimeSpan toNow = new TimeSpan (lTime);  
		return dtStart.Add (toNow);  
	}

		
	/// <summary>  
	/// DateTime时间格式转换为Unix时间戳格式  
	/// </summary>  
	/// <param name="time"> DateTime时间格式</param>  
	/// <returns>Unix时间戳格式</returns>  
	public static long GetTimeStampByDateTime (System.DateTime time)
	{  
		System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime (new System.DateTime (1970, 1, 1));  
		return (long)(time - startTime).TotalSeconds;  
	}

	public static Vector3 GetSinLerp (Vector3 start, Vector3 end, float dRatio)
	{
		return start + (end - start) * Mathf.Sin (dRatio * Mathf.PI / 2f);
	}

	public static float GetSinLerp (float start, float end, float dRatio)
	{
		return start + (end - start) * Mathf.Sin (dRatio * Mathf.PI / 2f);
	}

	public static float GetSinLerpNegOne2One (float start, float end, float dRatio)
	{
		return start + (end - start) / 2 + 0.5f * Mathf.Sin (dRatio * Mathf.PI - Mathf.PI / 2) * (end - start);
	}

	public static int[] ConvertStringArrayToIntArray (string[] tmp)
	{
		int[] t = new int[tmp.Length];

		for (int i = 0; i < tmp.Length; i++)
		{
			t [i] = int.Parse (tmp [i]);
		}
		return t;
	}

	public static float[] ConvertStringArrayToFloatArray (string[] tmp)
	{
		float[] t = new float[tmp.Length];
		for (int i = 0; i < tmp.Length; i++)
		{
			t [i] = float.Parse (tmp [i]);
		}
		return t;
	}

	public static int Random (string[] range)
	{
		int index = UnityEngine.Random.Range (0, 10000);
		for (int i = 0; i < range.Length; i++)
		{
			string[] rg = range [i].Split ('-');
			int min = int.Parse (rg [0]);
			int max = int.Parse (rg [1]);
			if (min <= index && index <= max)
			{
				return i;
			}
		}
		return -1;
	}

//	public static uint GetDateTimeByTimeStamp (long timeStamp)
//	{
//		System.DateTime dtStart = System.TimeZone.CurrentTimeZone.ToLocalTime (new System.DateTime (1970, 1, 1));
//		System.TimeSpan toNow = new System.TimeSpan (timeStamp * 10000000); 
//		string sDate = dtStart.Add (toNow).ToString ("yyyyMMdd");
//		return uint.Parse (sDate);
//	}

	public static string GetEnumDes (Enum en)
	{
		Type type = en.GetType ();
		MemberInfo[] memInfo = type.GetMember (en.ToString ());

		if (memInfo != null && memInfo.Length > 0)
		{
			object[] attrs = memInfo [0].GetCustomAttributes (typeof(System.ComponentModel.DescriptionAttribute), false);

			if (attrs != null && attrs.Length > 0)
				return ((DescriptionAttribute)attrs [0]).Description;
		}

		return en.ToString ();
	}

	#region [颜色：16进制转成RGB]

	/// <summary>
	/// [颜色：16进制转成RGB]
	/// </summary>
	/// <param name="strColor">设置16进制颜色 [返回RGB]</param>
	/// <returns></returns>
	public static Color colorHx16toRGB (string strHxColor)
	{
		Color c = Color.white;
		ColorUtility.TryParseHtmlString (strHxColor, out c);
		return c;
	}

	#endregion

	public static string GetStreamingPath()
	{
		#if UNITY_EDITOR
		return "file://" + Application.dataPath + "/StreamingAssets/";
		#elif UNITY_ANDROID
		return "jar:file://" + Application.dataPath + "!/assets/";
		#elif UNITY_IPHONE
		return Application.dataPath + "/Raw/";
		#elif UNITY_STANDALONE_WIN 
		return "file://" + Application.dataPath + "/StreamingAssets/";
		#else
		return string.Empty;
		#endif
	}

	///<summary>
	///由秒数得到日期几天几小时。。。
	///</summary
	///<param name="t">秒数</param>
	///<param name="type">0：转换后带秒，1:转换后不带秒</param>
	///<returns>几天几小时几分几秒</returns>
	public static string parseTimeSeconds (int t, int type)
	{
		System.Text.StringBuilder strBuilder = new System.Text.StringBuilder ();
		int day, hour, minute, second;
		if (t >= 86400) //天,
		{
			day = Convert.ToInt16 (t / 86400);
			hour = Convert.ToInt16 ((t % 86400) / 3600);
			minute = Convert.ToInt16 ((t % 86400 % 3600) / 60);
			second = Convert.ToInt16 (t % 86400 % 3600 % 60);
			strBuilder.Append (day);
			strBuilder.Append ("天");
			strBuilder.Append (hour);
			strBuilder.Append ("小时");
			strBuilder.Append (minute);
			strBuilder.Append ("分钟");
			if (type == 0)
			{
				strBuilder.Append (second);
				strBuilder.Append ("秒");
			}
		}
		else if (t >= 3600)//时,
		{
			hour = Convert.ToInt16 (t / 3600);
			minute = Convert.ToInt16 ((t % 3600) / 60);
			second = Convert.ToInt16 (t % 3600 % 60);
			strBuilder.Append (hour);
			strBuilder.Append ("小时");
			strBuilder.Append (minute);
			strBuilder.Append ("分钟");
			if (type == 0)
			{
				strBuilder.Append (second);
				strBuilder.Append ("秒");
			}
		}
		else if (t >= 60)//分
		{
			minute = Convert.ToInt16 (t / 60);
			second = Convert.ToInt16 (t % 60);
			strBuilder.Append (minute);
			strBuilder.Append ("分钟");
			if (type == 0)
			{
				strBuilder.Append (second);
				strBuilder.Append ("秒");
			}
		}
		else
		{
			second = Convert.ToInt16 (t);
			strBuilder.Append (second);
			strBuilder.Append ("秒");
		}
		return strBuilder.ToString();
	}

    public static string BuildFileMd5(String filename)
    {
        string filemd5 = null;
        try
        {
            using (var fileStream = File.OpenRead(filename))
            {
                var md5 = MD5.Create();
                var fileMD5Bytes = md5.ComputeHash(fileStream);//计算指定Stream 对象的哈希值                                     
                filemd5 = FormatMD5(fileMD5Bytes);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning(ex.Message);
        }
        return filemd5;
    }

    public static string FormatMD5(Byte[] data)
    {
        return System.BitConverter.ToString(data).Replace("-", "").ToLower();//将byte[]装换成字符串
    }

    public static string ToStr(object obj)
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
    }

    public static string GetShotName(int num,int Decimal = 0)
    {
        float n = 0;
        if (num < 1000)
        {
            return num.ToString();
        }
        else if (num < 10000)
        {
            n = num / 1000f;
            return n.ToString("F" + Decimal)+"千";
        }
        else if (num < 10000000)
        {
            n = num / 10000f;
            return n.ToString("F" + Decimal) + "万";
        }
        n = num / 10000000f;
        return n.ToString("F" + Decimal) + "千万";
    }

    public static string Decode(ref StringBuilder cache)
    {
        //Debug.Log("  Raw  DAtga   :" + cache);
        string[] subdata = cache.ToString().Split(new char[] { ':' }, 2);

        if (subdata.Length < 2) //没接到分割符
        {
            return null;
        }
        //Debug.Log("    splitt  Data   " + subdata[0] + "  -  " + subdata[1]);

        int len = int.Parse(subdata[0]);
        if (len > subdata[1].Length) //接受的数据长度不够
        {
            return null;
        }

        string data = subdata[1].Substring(0, len);
        cache.Remove(0, len + 1 + subdata[0].Length);
        return data;
    }



    public static string Decode(ref List<byte> cache)
    {
        //首先要获取长度，整形4个字节，如果字节数不足4个字节
        if (cache.Count < 4)
        {
            return null;
        }

        var cachedata = Encoding.UTF8.GetString(cache.ToArray());


        //Debug.Log("  Raw  DAtga   :" + JsonConvert.SerializeObject(cache));
        string[] subdata = cachedata.ToString().Split(new char[] { ':' }, 2);

        if (subdata.Length < 2) //没接到分割符
        {
            return null;
        }
        //Debug.Log("    splitt  Data   " + subdata[0] + "  -  " + subdata[1]);

        int headlen = subdata[0].Length + 1;

        int len = int.Parse(subdata[0]);
        if (len >= subdata[1].Length) //接受的数据长度不够
        {
            return null;
        }

        string data = subdata[1].Substring(0, len);
        
        //讲剩余没处理的消息存入消息池
        cache.RemoveRange(0, len + headlen);

        return data;
    }



    public static void ChildLook(Transform obj)
    {
        if (obj == null)
        {
            return;
        }
        for (int i = 0; i < obj.childCount; i++)
        {
            var c = obj.GetChild(i);
            c.LookAt(obj);
        }
    }
}