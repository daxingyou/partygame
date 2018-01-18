using DG.Tweening;
using UnityEngine;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class ProgressBar : MonoBehaviour
    {
        private float progressNum;
        private UnityEngine.UI.Image progressBar;
        private float width;
        private Transform effect;

        private void Start()
        {
            progressNum = 0;
            progressBar = GameObject.Find("Img").GetComponent<UnityEngine.UI.Image>();
            progressBar.fillAmount = progressNum;
            effect = GameObject.Find("Node").transform;
            width = progressBar.preferredWidth;

            InvokeRepeating("AddProgress", 0, 0.1f);
        }

        private void AddProgress()
        {
            float interval = 0;
            float duration = 0.1f;
            float arg1 = progressNum;

            if (arg1 >= 1)
            {
                CancelInvoke("AddProgress");
                progressNum = 1;
                duration = 0.01f;
            }
            else if (arg1 >= 0.65)
            {
                interval = 0.01f;
                progressNum += interval;
                if(arg1 < 1 && progressNum >= 1)
                {
                    Callback(3);
                }
            }
            else if (arg1 >= 0.32)
            {
                interval = 0.008f;
                progressNum += interval;
                if(arg1 < 0.65f && progressNum >= 0.65f)
                {
                    Callback(2);
                }
            }
            else
            {
                interval = 0.005f;
                progressNum += interval;
                if (arg1 < 0.32f && progressNum >= 0.32f)
                {
                    Callback(1);
                }
            }
            float x = width * progressNum - width / 2;
            effect.DOLocalMoveX(x, duration);
            progressBar.DOFillAmount(progressNum, duration);
        }

        private void Callback(int tag)
        {
            if(tag ==1)
            {
                var fire = GameObject.Find("ImgFire1").GetComponent<UnityEngine.UI.Image>();
                fire.sprite = Resources.Load("Textures/img_fire_01", typeof(Sprite)) as Sprite;
            }
            else if(tag == 2)
            {
                var fire = GameObject.Find("ImgFire2").GetComponent<UnityEngine.UI.Image>();
                fire.sprite = Resources.Load("Textures/img_fire_02", typeof(Sprite)) as Sprite;
            }
            else if (tag == 3)
            {
                var fire = GameObject.Find("ImgFire3").GetComponent<UnityEngine.UI.Image>();
                fire.sprite = Resources.Load("Textures/img_fire_03", typeof(Sprite)) as Sprite;
            }
        }
    }
}
