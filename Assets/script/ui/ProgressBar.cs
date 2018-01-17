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
        private UnityEngine.UI.Slider progressBar;

        private void Start()
        {
            progressNum = 0;
            progressBar = gameObject.GetComponent<UnityEngine.UI.Slider>();
            progressBar.value = progressNum;

            InvokeRepeating("AddProgress", 0, 0.1f);
        }

        private void AddProgress()
        {
            float interval = 0;
            float arg1 = progressNum;

            if (arg1 > 1)
            {
                CancelInvoke("AddProgress");
                progressNum = 1;
            }
            else if (arg1 > 0.6)
            {
                interval = 0.02f;
                progressNum += interval;
                if (arg1 < 1 && progressNum >= 1)
                {
                    Callback(3);
                }
            }
            else if (arg1 > 0.3)
            {
                interval = 0.015f;
                progressNum += interval;
                if (arg1 < 0.6 && progressNum >= 0.6)
                {
                    Callback(2);
                }
            }
            else
            {
                interval = 0.01f;
                progressNum += interval;
                if (arg1 < 0.3 && progressNum >= 0.3)
                {
                    Callback(1);
                }
            }
            progressBar.value = progressNum;
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
