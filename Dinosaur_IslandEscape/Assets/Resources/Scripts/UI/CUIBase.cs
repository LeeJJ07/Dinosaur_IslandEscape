using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace HakSeung
{
    public abstract class CUIBase : MonoBehaviour
    {
        public string canvasName = "?";

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
