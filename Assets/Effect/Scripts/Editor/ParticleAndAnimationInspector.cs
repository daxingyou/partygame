using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(ParticleAndAnimation))]
public class ParticleAndAnimationInspector : Editor 
{
	ParticleAndAnimation pa;
	
	void OnEnable()
	{
		pa = target as ParticleAndAnimation;
	}

    private bool defualti = false;
	public override void OnInspectorGUI ()
	{
		this.serializedObject.Update ();
//		base.OnInspectorGUI ();
        defualti = EditorGUILayout.Toggle("默认编辑器", defualti);
        if (defualti)
        {
            DrawDefaultInspector();
        }
        else
        {
            SerializedProperty so = this.serializedObject.FindProperty("Scale");
            so.floatValue = EditorGUILayout.FloatField("粒子缩放倍数", so.floatValue);

            so = this.serializedObject.FindProperty("allParticleSystem");
            so.boolValue = EditorGUILayout.Toggle("自动包含所有粒子", so.boolValue);

            so = this.serializedObject.FindProperty("allAnimation");
            so.boolValue = EditorGUILayout.Toggle("自动包含所有动画", so.boolValue);

            so = this.serializedObject.FindProperty("allSub");
            so.boolValue = EditorGUILayout.Toggle("自动包含所有子控制器", so.boolValue);

			so = this.serializedObject.FindProperty("aniController");
			so.objectReferenceValue = EditorGUILayout.ObjectField("动作控制器",so.objectReferenceValue,typeof(lfAniController));

			if(so.objectReferenceValue!=null)
			{
				so = this.serializedObject.FindProperty("ChangeAni");
				so.stringValue = EditorGUILayout.TextField("启动切换动作",so.stringValue);
				
				so = this.serializedObject.FindProperty("ChangeStopAni");
				so.stringValue = EditorGUILayout.TextField("结束切换动作",so.stringValue);
			}

			so = this.serializedObject.FindProperty("mAnimators");
			so.arraySize = EditorGUILayout.IntField("k动画数量", so.arraySize);


            for (int i = 0; i < so.arraySize; i++)
            {
                EditorGUILayout.LabelField("第:" + i + "段动画");
                SerializedProperty sub = so.GetArrayElementAtIndex(i);
                SerializedProperty so1 = sub.FindPropertyRelative("ani");
                so1.objectReferenceValue = EditorGUILayout.ObjectField("动画控制器", so1.objectReferenceValue, typeof(Animator));
                so1 = sub.FindPropertyRelative("aniName");
                so1.stringValue = EditorGUILayout.TextField("动画名称", so1.stringValue);
                so1 = sub.FindPropertyRelative("stateName");
                so1.stringValue = EditorGUILayout.TextField("状态名", so1.stringValue);
                so1 = sub.FindPropertyRelative("second");
                so1.floatValue = EditorGUILayout.FloatField("延迟播放时间", so1.floatValue);
            }

            so = this.serializedObject.FindProperty("mAnimators2");
            so.arraySize = EditorGUILayout.IntField("k动画数量（暂停用）", so.arraySize);

            for (int i = 0; i < so.arraySize; i++)
            {
                EditorGUILayout.LabelField("第:" + i + "段动画");
                SerializedProperty sub = so.GetArrayElementAtIndex(i);
                SerializedProperty so1 = sub.FindPropertyRelative("ani");
                so1.objectReferenceValue = EditorGUILayout.ObjectField("动画控制器", so1.objectReferenceValue, typeof(Animator));
                so1 = sub.FindPropertyRelative("aniName");
                so1.stringValue = EditorGUILayout.TextField("动画名称", so1.stringValue);
                so1 = sub.FindPropertyRelative("stateName");
                so1.stringValue = EditorGUILayout.TextField("状态名", so1.stringValue);
                so1 = sub.FindPropertyRelative("second");
                so1.floatValue = EditorGUILayout.FloatField("延迟播放时间", so1.floatValue);
            }
        }

		//this.serializedObject.Update ();
		this.serializedObject.ApplyModifiedProperties ();

		if(GUILayout.Button("PlayLoop"))
			pa.PlayLoop();
		if(GUILayout.Button("PlayOnce"))
			pa.PlayOnce();
		if(GUILayout.Button("Stop"))
			pa.Stop();

	}
}
