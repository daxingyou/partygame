using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


[RequireComponent(typeof(lfAniController))]
public class lfAniControllerTest:MonoBehaviour
{
    public lfAniController LfController;

    public string AniName;

    public List<string> AniNames;

    public bool Change = false;

    public void TestAni()
    {
        LfController.AniName = this.AniName;
    }

    public void Update()
    {
        if (Change)
        {
            Change = false;
            if (LfController.AniName != this.AniName)
            {
                LfController.AniName = this.AniName;
            }
        }
	}

    public void Awake()
    {
        LfController = this.GetComponent<lfAniController>();
        AniName = LfController.AniName;
		LfController.AniStop += onAniStop;
        LfController.InitAnis();
        AniNames = LfController.AniNames;
    }
	
	void onAniStop (string _AniName)
	{
		AniName = _AniName;
	}
}
