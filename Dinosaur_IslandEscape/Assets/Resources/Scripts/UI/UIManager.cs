using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HakSeung
{
  /*  enum SceneType
    {
        START,
        RUNNING,
        ENDING,
        
        END
    }*/
    public class UIManager : MonoBehaviour
    {
        //TODO<�н�> SceneType�� ���� ĵ���� �����Ű�� �ڵ尡 �ʿ��� 24/11/11
        private const string uiManagerObjectName = "_UIManager";
        private static UIManager s_Instance;
        //���� ���� �´� �˾����� �޾ƿ;ߵ�
        Stack<CUIPopup> popupStack = new Stack<CUIPopup>();

        //Canvas[] canvas = new Canvas[];
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
