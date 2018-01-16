/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   GameManager.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-15.
   
*************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public static class GameManager
    {
        public static int currentPlayNum;
        public static event Action UpdatePlayNumCallback;

        public static void SetPlayNum(NetPacket msg)
        {
            currentPlayNum = int.Parse(msg.data);
            if (UpdatePlayNumCallback != null)
            {
                UpdatePlayNumCallback();
            }
        }
    }
}
