using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace HakSeung
{
    public abstract class CUIBase : MonoBehaviour
    {
        [SerializeField]private string uiName = null;

        public string UIName { get { return uiName; } }

        private void Awake()
        {
            InitUI();
        }

        /// <summary>
        /// UI를 보이기 위한 활성화 메서드
        /// </summary>
        public virtual void Show()
        {
            Debug.Log($"UI/<color=yellow>{canvasName}</color> 활성화");
            gameObject.SetActive(true);
        }

        /// <summary>
        /// UI를 숨기기 위한 비활성화 메서드
        /// </summary>
        public virtual void Hide()
        {
            Debug.Log($"UI/<color=yellow>{canvasName}</color> 비활성화");
            gameObject.SetActive(false);
        }

        /// <summary>
        /// UI초기값 설정을 위한 메서드
        /// </summary>
        protected abstract void InitUI();

        
    }
}
