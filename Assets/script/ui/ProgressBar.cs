using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class ProgressBar : MonoBehaviour
    {
        public Image progressBar;
        public Transform effect;

        private float percnetage;
        private float width;

        private void Start()
        {
            percnetage = 0;
            progressBar.fillAmount = percnetage;
            width = progressBar.rectTransform.rect.width;

            //InvokeRepeating("AddProgress", 0, 0.1f);
        }

        public void AddProgress(float interval)
        {
            if(percnetage >= 1)
            {
                return;
            }

            float oldPercent = percnetage;
            if (oldPercent >= 0.65)
            {
                percnetage += interval;
                if (oldPercent < 1 && percnetage >= 1)
                {
                    Callback(3);
                }
            }
            else if (oldPercent >= 0.32)
            {
                percnetage += interval;
                if (oldPercent < 0.65f && percnetage >= 0.65f)
                {
                    Callback(2);
                }
            }
            else
            {
                percnetage += interval;
                if (oldPercent < 0.32f && percnetage >= 0.32f)
                {
                    Callback(1);
                }
            }

            float x = width * percnetage - width / 2;
            effect.DOLocalMoveX(x, 0.1f);
            progressBar.DOFillAmount(percnetage, 0.1f);
        }

        private void AddProgress()
        {
            float interval = 0;
            float duration = 0.1f;
            float arg1 = percnetage;

            if (arg1 >= 1)
            {
                CancelInvoke("AddProgress");
                percnetage = 1;
                duration = 0.01f;
            }
            else if (arg1 >= 0.65)
            {
                interval = 0.01f;
                percnetage += interval;
                if(arg1 < 1 && percnetage >= 1)
                {
                    Callback(3);
                }
            }
            else if (arg1 >= 0.32)
            {
                interval = 0.008f;
                percnetage += interval;
                if(arg1 < 0.65f && percnetage >= 0.65f)
                {
                    Callback(2);
                }
            }
            else
            {
                interval = 0.005f;
                percnetage += interval;
                if (arg1 < 0.32f && percnetage >= 0.32f)
                {
                    Callback(1);
                }
            }
            float x = width * percnetage - width / 2;
            effect.DOLocalMoveX(x, duration);
            progressBar.DOFillAmount(percnetage, duration);
        }

        private void Callback(int tag)
        {
            switch (tag)
            {
                case 1:
                case 2:
                case 3:
                    var fire = Utils.FindDirectChildComponent<Image>("ImgFire" + tag, transform);
                    fire.sprite = Resources.Load("Textures/img_fire_0" + tag, typeof(Sprite)) as Sprite;
                    break;
                default:
                    break;
            }
        }
    }
}
