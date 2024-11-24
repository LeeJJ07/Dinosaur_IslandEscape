using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace HakSeung
{
    public class CUIProgressBar : MonoBehaviour
    {
        [Header("Porgress Bar")]
        //[SerializeField] private float curProgress;
        [SerializeField]private float maxProgress;
        [SerializeField]private Image PrograssBarFill;

        private const float maxFillAmount = 1f;
        private const float defaultMaxProgress = 0;


        private void Awake()
        {
            Init();
        }

        public float MaxProgress { 
            set 
            {
                if(value > defaultMaxProgress)
                    maxProgress = value;
            } 
        }
        private void Init()
        {
            maxProgress = defaultMaxProgress;
        }

        public void FillProgressBar(float curProgress)
        {
            if (maxProgress <= defaultMaxProgress) return;

            if(curProgress <= maxProgress)
                PrograssBarFill.fillAmount = curProgress / maxProgress;
            else
                PrograssBarFill.fillAmount = maxFillAmount;
        }

    }
}
