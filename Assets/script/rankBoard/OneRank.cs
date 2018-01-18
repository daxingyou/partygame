/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   OneRank.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-08.
   
*************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace isletspace
{
    public struct RankVO
    {
        public int id;
        public int rank;
        public int score;
        public string name;
        public string pic_url;
    }

    public class OneRank : MonoBehaviour
    {
        Text RankObj;
        Text ScoreObj;
        Text NameObj;
        Image HeadImgObj;

        void Awake()
        {
            RankObj = transform.Find("Rank").GetComponent<Text>();
            ScoreObj = transform.Find("Score").GetComponent<Text>();
            NameObj = transform.Find("Name").GetComponent<Text>();
            HeadImgObj = transform.Find("HeadImg").GetComponent<Image>();
        }

        public void SetAllData(RankVO data)
        {
            RankObj.text = data.rank.ToString();
            ScoreObj.text = "分数：" + data.score.ToString();
            NameObj.text = data.name;
            //HeadImgObj.renderer;
        }

        public void PlayNewAni()
        {

        }
    }
}