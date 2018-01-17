using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class Time : MonoBehaviour
    {
        private int countDown;
        private UnityEngine.UI.Text timeText;

        public event Action<int> callBack; //1: 倒计时结束 2:强制关闭

        public void StartCountDown(int time)
        {
            countDown = time;
            timeText = gameObject.GetComponent<UnityEngine.UI.Text>();
            timeText.text = time.ToString();
            InvokeRepeating("AddTime", 0, 1);
        }

        public void StopCountDown()
        {
            CancelInvoke("AddTime");
            timeText.text = "";
            if (callBack != null)
            {
                callBack(2);
            }
        }

        private void AddTime()
        {
            countDown -= 1;
            timeText.text = countDown.ToString();

            if (countDown < 1)
            {
                CancelInvoke("AddTime");
                if(callBack != null)
                {
                    callBack(1);
                }
            }
        }
    }
}
