using JongJin;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.InspectorCurveEditor;

namespace HakSeung
{
    public class UIManager : MonoBehaviour
    {
        public enum ESceneUIType
        {
            START,
            RUNNING,
            EVENT,
            ENDING,

            END
        }

        public enum EPopupUIType
        {

            END
        }

        //TODO<�н�> sceneCanvas�� �Ҵ�� ĵ�������� ���� start game event end �� �װ��� �����ؼ� �� state�� �޾ƿ;ߵ�
        [SerializeField] private CUIScene[] sceneUIs = new CUIScene[(int)ESceneUIType.END];
        [SerializeField] private CUIPopup[] popupUIs = new CUIPopup[(int)EPopupUIType.END];

        private const string uiManagerObjectName = "_UIManager";
        private static UIManager s_Instance;

        private GameSceneController gameSceneController;

        private Stack<CUIPopup> popupStack;

        private int curSceneCanvasTypeIndex = (int)ESceneUIType.END;


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

            Initialize();
        }

        private void Update()
        {
            //TODO<�н�> 
            //1. �������� ����Ȯ��
            //2. ���� ���� ���Ӿ��̸� �������� �̺�Ʈ state���� Ȯ��
            // 24/11/12

            
/*            int nextCanvasIndex = (int)ESceneUIType.END;

            //���� ���� ���� ���̸� �۵��� �ڵ�
            switch (gameSceneController.CurState)
            {
                case JongJin.EGameState.RUNNING:
                    nextCanvasIndex = (int)ESceneUIType.RUNNING;
                    break;
                case JongJin.EGameState.FIRSTMISSION:
                case JongJin.EGameState.SECONDMISSION:
                case JongJin.EGameState.THIRDMISSION:
                case JongJin.EGameState.TAILMISSION:
                    nextCanvasIndex = (int)ESceneUIType.EVENT;
                    break;
            }

            if(curSceneCanvasTypeIndex != nextCanvasIndex)
                SceneCanvasChange(nextCanvasIndex);*/

        }

        private void Initialize()
        {
            if (curSceneCanvasTypeIndex != (int)ESceneUIType.END)
            {
                gameSceneController = GetComponent<GameSceneController>();

                /*for (int i = (int)ECanvasType.START; i < sceneCanvas.Length; i++)
                {
                    //ĵ������ �޾ƿ���
                }*/

                sceneUIs[(int)ESceneUIType.START].Show();

                for (int i = (int)ESceneUIType.START + 1; i < sceneUIs.Length; i++)
                    sceneUIs[i].Hide();
            }
        }

        void UIBind<T>(System.Type type) where T : UnityEngine.Object
        {
            if (!type.IsEnum)
                return;

            string[] uiNames = Enum.GetNames(type);

            UnityEngine.Object[] uiObjects = new UnityEngine.Object[uiNames.Length];

            for (int i = 0; i < uiNames.Length; i++)
            {
                uiObjects[i] = //
            }

        }

        /*public T ShowPopupUI<T>(EPopupUIType popupType) where T : CUIPopup
        {
            T popup =  CUIPopup

            popupIndex++;
            return popup;
        }

        public void ClosePopupUI()
        {
            if (popupStack.Count == 0)
                return;

            popupStack.Pop();
            popupIndex--;
        }

        public void ClosePopupUI(CUIPopup popupUI)
        {
            if (popupStack.Count == 0)
                return;

            if (popupStack.Peek() != popupUI)
            {
                Debug.Log($"popupUi: {popupUI.UIName}�� ���� �� �����ϴ�.");
                return;
            }

            popupStack.Pop();
            popupIndex--;
        }

        public void CloseAllPopupUI()
        {
            while(popupStack.Count > 0)
            {
                ClosePopupUI();
            }
        }*/

    }
}
