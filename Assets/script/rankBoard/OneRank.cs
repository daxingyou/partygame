/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   OneRank.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-08.
   
*************************************************************/

using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine.UI;


namespace isletspace
{
    public struct RankVO
    {
        public int rank;
        public int score;
        public string id;
        public string name;
        public string pic_url;
    }

    public class OneRank : MonoBehaviour
    {
        Text RankObj;
        Image RankImgObj;
        Text ScoreObj;
        Text NameObj;
        IconManager HeadImgObj;

        void Awake()
        {
            RankObj = Utils.FindDirectChildComponent<Text>("Rank", transform);
            RankImgObj = Utils.FindDirectChildComponent<Image>("RankImg", transform);
            ScoreObj = Utils.FindDirectChildComponent<Text>("Score", transform);
            NameObj = Utils.FindDirectChildComponent<Text>("Name", transform);
            HeadImgObj = Utils.FindDirectChildComponent<IconManager>("HeadImg", transform);
        }

        public void SetAllData(RankVO data)
        {
            //print("   set all data  " + JsonConvert.SerializeObject(data) + "   >  " + NameObj);
            if (data.rank < 4)
            {
                RankImgObj.gameObject.SetActive(true);
                RankObj.gameObject.SetActive(false);

                string path = "img/rank_" + data.rank;
                RankImgObj.sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
            }
            else
            {
                RankImgObj.gameObject.SetActive(false);
                RankObj.gameObject.SetActive(true);

                RankObj.text = data.rank.ToString();
            }

            if (ScoreObj != null)
            {
                ScoreObj.text = "分数：" + data.score;
            }
            if (NameObj != null)
            {
                NameObj.text = data.name;
            }
            if (HeadImgObj != null)
            {
                HeadImgObj.SetFace(data.pic_url);
            }
        }

        public void PlayNewAni()
        {

        }
    }
}