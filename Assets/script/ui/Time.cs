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
        private int time;
        private UnityEngine.UI.Text timeText;

        private void Start()
        {
            time = 30;
            timeText = gameObject.GetComponent<UnityEngine.UI.Text>();
            timeText.text = time.ToString();

            InvokeRepeating("AddTime", 0, 1);
        }

        private void AddTime()
        {
            time -= 1;
            timeText.text = time.ToString();

            if (time < 1)
            {
                CancelInvoke("AddTime");
                Callback(1);
            }
        }

        private void Callback(int tag)
        {
            if (tag == 1)
            {
                Debug.Log("========== Game Over");
            }
        }
    }
}
