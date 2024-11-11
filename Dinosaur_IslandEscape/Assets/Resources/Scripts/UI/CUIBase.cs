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
        /// UI�� ���̱� ���� Ȱ��ȭ �޼���
        /// </summary>
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// UI�� ����� ���� ��Ȱ��ȭ �޼���
        /// </summary>
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// UI�ʱⰪ ������ ���� �޼���
        /// </summary>
        protected abstract void InitUI();

        
    }
}
