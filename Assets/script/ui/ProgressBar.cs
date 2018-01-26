using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Spine.Unity;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class ProgressBar : MonoBehaviour
    {
        public Image progressBar;
        public Transform effect;
        public Transform iconAni;

        private float percnetage;
        private float width;

        private void Start()
        {
            percnetage = 0;
            progressBar.fillAmount = percnetage;
            width = progressBar.rectTransform.rect.width;
        }

        private void OnGUI()
        {
            if (GUILayout.Button("action1"))
            {
                AddProgress(0.1f);
            }
        }

        public void AddProgress(float interval)
        {
            if(percnetage >= 1)
            {
                return;
            }

            float oldPercent = percnetage;
            percnetage += interval;
            
            if (oldPercent < 0.99f && percnetage >= 0.99f)
            {
                Callback(3);
            }
            if (oldPercent < 0.66f && percnetage >= 0.66f)
            {
                Callback(2);
            }
            if (oldPercent < 0.32f && percnetage >= 0.32f)
            {
                Callback(1);
            }

            float x = width * percnetage - width / 2;
            effect.DOLocalMoveX(x, 0.1f);
            progressBar.DOFillAmount(percnetage, 0.1f);
        }

        private void Callback(int tag)
        {
            switch (tag)
            {
                case 1:
                case 2:
                case 3:
                    var fire = Utils.FindDirectChildComponent<ProgressBarNode>("ImgFire" + tag, transform);
                    fire.NodeActive(tag);

                    StartCoroutine(AniRoute(tag, fire.transform));
                    
                    break;
                default:
                    break;
            }
        }

        private IEnumerator AniRoute(int tag, Transform target)
        {
            iconAni.gameObject.SetActive(true);
            iconAni.GetComponent<SkeletonGraphic>().Skeleton.SetSkin("fire_0" + tag);

            iconAni.DOLocalMove(target.localPosition, 1).From();
            iconAni.DOScale(Vector3.one * 0.03f, 1).From();

            SoundManager.Instance.PlayIcon();

            yield return new WaitForSeconds(0.9f);

            iconAni.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "animation2", false);

            yield return new WaitForSeconds(0.5f);

            iconAni.GetChild(0).gameObject.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            iconAni.GetChild(1).gameObject.SetActive(true);

            FlashScreen.Instance.DoCover(new Color(0.89f, 0.26f, 0.26f), 1);

            yield return new WaitForSeconds(0.8f);

            iconAni.GetChild(0).gameObject.SetActive(false);
            iconAni.GetChild(1).gameObject.SetActive(false);
            iconAni.gameObject.SetActive(false);
        }
    }
}
