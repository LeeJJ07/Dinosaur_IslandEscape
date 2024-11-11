using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HakSeung
{
    public class UIManager : MonoBehaviour
    {
        private const string uiManagerObjectName = "_UIManager";
        private static UIManager s_Instance;
        //ÇöÀç ¾À¿¡ ¸Â´Â ÆË¾÷µéÀ» ¹Þ¾Æ¿Í¾ßµÊ
        Stack<CUIPopup> popupStack = new Stack<CUIPopup>();
        public static UIManager Instance
        {
            get
            {
                if(s_Instance == null)
                {
                    GameObject newUIManagerObject = new GameObject(uiManagerObjectName);
                    s_Instance = newUIManagerObject.AddComponent<UIManager>();
                }
                return s_Instance;

            }
        }

        private void Awake()
        {

            if (s_Instance != null && s_Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            s_Instance = this;

            DontDestroyOnLoad(this.gameObject);
        }



        public void ClosePopupUI()
        {

        }

    }
}
