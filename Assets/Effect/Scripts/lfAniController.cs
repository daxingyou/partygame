using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class lfAniSpeed
{
    public string name;
    public float speed;
}

public class lfAniController : MonoBehaviour
{
    public GameObject model;

    public CharacterController cc;

    public float defaultSpeed = 0.5f;

    private string aniName = "idle";

    public bool isPlaying = false;

    private bool dieLock = false;

    public void fuhuo()
    {
        dieLock = false;
    }

    public string AniName
    {
        get { return aniName; }
        set
        {
            if (aniName==value)
                return;
            //Debug.LogError("变更动作:"+value);
            if (value == "fuhuo")
            {
                dieLock = false;
                value = "idle";
            }
            if (aniDict.ContainsKey(value))
            {
                if (dieLock)
                    return;
                if (value == "die")
                {
                    dieLock = true;
                }
                aniName = value;
                isPlaying = true;
                CurrtState = aniDict[aniName];
                CurrtState.speed = SpeedByName(aniName);
                model.GetComponent<Animation>().Play(aniName);
            }
        }
    }

    public AnimationState CurrtState;

    public List<lfAniSpeed> Speeds;

    public List<string> AniNames; 

    public Dictionary<string, AnimationState> aniDict = new Dictionary<string, AnimationState>();

    public float SpeedByName(string name)
    {
        for (int i = 0; i < Speeds.Count; i++)
        {
            if (Speeds[i].name == name)
                return Speeds[i].speed;
        }
        return 0;
    }
    


#if UNITY_EDITOR
    [ContextMenu("Init")]
#endif
    public void InitAnis()
    {
        if (Speeds == null)
        {
            Speeds = new List<lfAniSpeed>();
        }
        if (AniNames==null)
            AniNames=new List<string>();
        foreach (AnimationState animationState in model.GetComponent<Animation>())
        {
            if(!aniDict.ContainsKey(animationState.name))
                aniDict.Add(animationState.name, animationState);
            if (SpeedByName(animationState.name)<=0)
            {
                Speeds.Add(new lfAniSpeed() { name = animationState.name, speed = defaultSpeed });
            }
            if(!AniNames.Contains(animationState.name))
                AniNames.Add(animationState.name);
        }
    }

    public void Awake()
    {
        InitAnis();
    }

    public void Start()
    {
        #if UNITY_EDITOR
        if (this.GetComponent<lfAniControllerTest>() == null)
        {
            this.gameObject.AddComponent<lfAniControllerTest>();
        }
        #endif
    }

	public Action<string> AniStop;

    private void Update()
    {
        if (isPlaying)
        {
            if (!model.GetComponent<Animation>().isPlaying)
            {
                isPlaying = false;
				if(AniStop!=null)
				{
					AniStop(AniName);
				}
            }
        }
    }
}