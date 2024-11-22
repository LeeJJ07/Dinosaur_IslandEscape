using HakSeung;
using JongJin;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace HakSeung
{
    public class CUITutorialPopup : CUIPopup
    {
        [SerializeField] private Image guideImage;
        [SerializeField] private Image timerFillImage;
        [SerializeField] private TextMeshProUGUI timerCountText;
        //TODO <학승> - 상수 7 넣어놓은 것 나중에 state에 맞게 처리해 놔야됨
        [SerializeField] private Sprite[] guideSprites = new Sprite[7];
        protected override void InitUI()
        {

        }

        public void TimerUpdate(float curTime)
        {
            if (curTime < 0)
                curTime = 0;
            
            timerFillImage.fillAmount = curTime * 0.1f;
            timerCountText.text = curTime.ToString();
        }






    }
}
