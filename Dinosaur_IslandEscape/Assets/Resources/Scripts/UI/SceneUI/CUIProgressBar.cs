using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace HakSeung
{
    public class CUIProgressBar : CUIScene
    {
        [Header("Porgress Bar")]
        //[SerializeField] private float curProgress;
        [SerializeField]private float maxProgress;

        private const float maxFillAmount = 1f;
        private const float defaultMaxProgress = 0;

        public Image PrograssBarFill;


        public float MaxProgress { 
            set 
            {
                if(value > defaultMaxProgress)
                    maxProgress = value;
            } 
        }
        protected override void InitUI()
        {
            maxProgress = defaultMaxProgress;
        }


        private void FillProgressBar(float curProgress)
        {
            if (maxProgress <= defaultMaxProgress) return;

            if(curProgress <= maxProgress)
                PrograssBarFill.fillAmount = curProgress / maxProgress;
            else
                PrograssBarFill.fillAmount = maxFillAmount;
        }

    }
}
