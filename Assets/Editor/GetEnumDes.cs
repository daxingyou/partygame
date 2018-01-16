// 通过枚举加载获得路径
using System.ComponentModel;
using System.Reflection;

public class GetEnumDes {
    public static string get(System.Enum en)
    {
        System.Type type = en.GetType();
        MemberInfo[] memInfo = type.GetMember(en.ToString());

        if (memInfo != null && memInfo.Length > 0)
        {
            object[] attrs = memInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

            if (attrs != null && attrs.Length > 0)
                return ((DescriptionAttribute)attrs[0]).Description;
        }
        return en.ToString();
    }
}

/* --- e.g. ---
public enum UIPage
{
    [Description("Prefabs/UI/Null")]
    Null = 0,
    [Description("Prefabs/UI/LoadingPage")]
    LoadingPage = 1,
    [Description("Prefabs/UI/LoginPage")]
    LoginPage = 2,
}

string strPrefabeName = GetEnumDes(page);
*/