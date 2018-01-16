using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class ProgressBar : MonoBehaviour
    {
        private float progressNum;
        private UnityEngine.UI.Image progressBar;
        private UnityEngine.UI.Text te;
        private float x0;
        private float y0;
        private float x1;
        private float y1;
        private float x2;
        private float y2;
        private float t;

        private void Start()
        {
            progressNum = 0;
            progressBar = GameObject.Find("Img").GetComponent<UnityEngine.UI.Image>();
            progressBar.fillAmount = progressNum;

            InvokeRepeating("AddProgress", 0, 1);

            te = GameObject.Find("Progress").GetComponent<UnityEngine.UI.Text>();
            x0 = -300;
            y0 = 0;
            x1 = 300;
            y1 = 0;
            x2 = 0;
            y2 = 300;
            t = 0;
            InvokeRepeating("Run", 0, 0.01f);
        }

        private void Run()
        {
            float x = (1 - t) * (1 - t) * x0 + 2 * t * (1 - t) * x2 + t * t * x1;
            float y = (1 - t) * (1 - t) * y0 + 2 * t * (1 - t) * y2 + t * t * y1;
            t += 0.01f;
            te.transform.localPosition = new Vector2(x, y);
            if (t > 1)
            {
                CancelInvoke("Run");
            }
        }

        private void AddProgress()
        {
            float interval = 0;
            float duration = 1;
            float arg1 = progressNum;

            if (arg1 > 1)
            {
                CancelInvoke("AddProgress");
                progressNum = 1;
                duration = 0.01f;
            }
            else if (arg1 > 0.6)
            {
                interval = 0.2f;
                progressNum += interval;
                if (arg1 < 1 && progressNum >= 1)
                {
                    Callback(3);
                }
            }
            else if (arg1 > 0.3)
            {
                interval = 0.15f;
                progressNum += interval;
                if (arg1 < 0.6 && progressNum >= 0.6)
                {
                    Callback(2);
                }
            }
            else
            {
                interval = 0.1f;
                progressNum += interval;
                if (arg1 < 0.3 && progressNum >= 0.3)
                {
                    Callback(1);
                }
            }
            progressBar.DOFillAmount(progressNum, duration);
        }

        private void Callback(int tag)
        {
            if(tag == 1)
            {
                Debug.Log("========== section 1");
            }
            else if(tag == 2)
            {
                Debug.Log("========== section 2");
            }
            else if (tag == 3)
            {
                Debug.Log("========== end");
            }
        }
    }
}
