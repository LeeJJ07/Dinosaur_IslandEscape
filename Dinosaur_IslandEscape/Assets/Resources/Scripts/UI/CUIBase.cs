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
        /// UI�� ���̱� ���� Ȱ��ȭ �޼���
        /// </summary>
        public virtual void Show()
        {
            Debug.Log($"UI/<color=yellow>{canvasName}</color> Ȱ��ȭ");
            gameObject.SetActive(true);
        }

        /// <summary>
        /// UI�� ����� ���� ��Ȱ��ȭ �޼���
        /// </summary>
        public virtual void Hide()
        {
            Debug.Log($"UI/<color=yellow>{canvasName}</color> ��Ȱ��ȭ");
            gameObject.SetActive(false);
        }

        /// <summary>
        /// UI�ʱⰪ ������ ���� �޼���
        /// </summary>
        protected abstract void InitUI();

        
    }
}
