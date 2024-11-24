using HakSeung;
using JongJin;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


namespace HakSeung
{
    public class CUITutorialPopup : CUIPopup
    {
        enum TutorialImage
        {
            Running,
            Jump,
            Heart
        }

        private bool isPlayingEffect = false;

        [SerializeField] private Image guideImage;
        [SerializeField] private Image timerFillImage;
        [SerializeField] private TextMeshProUGUI timerCountText;
        //TODO <학승> - 상수 7 넣어놓은 것 나중에 state에 맞게 처리해 놔야됨
        [SerializeField] private Sprite[] guideSprites = new Sprite[7];
        [SerializeField] private float effectDuration;
        protected override void InitUI()
        {
            effectDuration = 0.5f;
        }

        public void TimerUpdate(float curTime)
        {
            if (curTime < 0)
                curTime = 0;
            
            timerFillImage.fillAmount = Mathf.Clamp(curTime * 0.1f, 0f, 1f);
            timerCountText.text = curTime.ToString();
        }

        public override void Show()
        {
            base.Show();
            StartCoroutine(PlayPopupEffect());
        }

        protected override IEnumerator PlayPopupEffect()
        {
            if (isPlayingEffect)
                yield break;
            else
                isPlayingEffect = true;

            float elapsedTime = 0f;
            Vector3 startScale = Vector3.zero;
            Vector3 endScale = Vector3.one;

            if (baseRectTransform == null)
            {
                Debug.LogError("baseRectTransform이 존재하지 않음");
                yield break;
            }

            while (effectDuration > elapsedTime)
            {
                float timeProgress = elapsedTime / effectDuration ;
                baseRectTransform.localScale = Vector3.Lerp(startScale, endScale, timeProgress);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            baseRectTransform.localScale = endScale;

            isPlayingEffect = false;

        }






    }
}
