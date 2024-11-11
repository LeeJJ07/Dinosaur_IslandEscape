using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace HakSeung
{
    public abstract class CUIBase : MonoBehaviour
    {
        private void Awake()
        {
            InitUI();
        }

        /// <summary>
        /// UI를 보이기 위한 활성화 메서드
        /// </summary>
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// UI를 숨기기 위한 비활성화 메서드
        /// </summary>
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// UI초기값 설정을 위한 메서드
        /// </summary>
        protected abstract void InitUI();

        
    }
}
