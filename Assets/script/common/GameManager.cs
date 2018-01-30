﻿/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   GameManager.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-15.
   
*************************************************************/

using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using DG.DemiLib;
using UnityEngine;

//TODO  Unity场景中的CheerSceneBase、SecondCheerSceneBase中的所有欢呼的人，并没有用统一的预制件做。需要重新创建。

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>

    public static class GameManager
    {
        public static List<float> PHASE_START_DELAY = new List<float>() { 8.133f, 5.579f, 7.1475f };

        public static int currentPlayNum = 0;
        public static event Action UpdatePlayNumCallback;

        public static void SetPlayNum(NetPacket msg)
        {
            currentPlayNum = int.Parse(msg.data);
            if (UpdatePlayNumCallback != null)
            {
                UpdatePlayNumCallback();
            }
        }

        public static int gamePhase = 0;
        public static event Action BeforeUpdateGamePhase;

        public static void InitGamePhase()
        {
            gamePhase = 0;
            phaseTime = 0;
        }

        public static void AddGamePhase()
        {
            if (BeforeUpdateGamePhase != null)
            {
                BeforeUpdateGamePhase();
            }
            gamePhase++;
            phaseTime = 0;
        }

        public static int phaseTime = 0;

        public static List<float> soloList = new List<float>();

        public static int MVPIndex = 999;
        public static List<RankVO> RankData;
        public static Action RankCallback;

        public static void SetRankData(string json)
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            List<RankVO> data = serializer.Deserialize(new JsonTextReader(sr), typeof(List<RankVO>)) as List<RankVO>;

            RankData = data;

            if(RankCallback != null)
            {
                RankCallback();
            }
        }
        
        public static void PickMVP()
        {
            if (RankData == null || RankData.Count == 0)
            {
                return;
            }
            MVPIndex = UnityEngine.Random.Range(0, 10);
            MVPIndex = MVPIndex%RankData.Count;
        }

        public static string GetMVPName()
        {
            if (MVPIndex == 999)
            {
                return "佚名";
            }
            return RankData[MVPIndex].name;
        }

        public static string GetMVPUrl()
        {
            if (MVPIndex == 999)
            {
                return "";
            }
            return RankData[MVPIndex].pic_url;
        }

        public static int GetMVPTarget()
        {
            if (MVPIndex == 999)
            {
                return 5;
            }
            int sum = 0;
            for (int i = 0; i < RankData[MVPIndex].pic_url.Length; ++i)
            {
                sum += (int)RankData[MVPIndex].pic_url[i];
            }
            return sum % 100;
        }
    }
}
