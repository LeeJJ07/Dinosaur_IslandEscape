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

        //TODO<학승> sceneCanvas에 할당될 캔버스들의 값을 start game event end 총 네개로 구성해서 그 state를 받아와야됨
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
            //TODO<학승> 
            //1. 씬에대한 정보확인
            //2. 현재 씬이 게임씬이면 러닝인지 이벤트 state인지 확인
            // 24/11/12

            
/*            int nextCanvasIndex = (int)ESceneUIType.END;

            //현재 씬이 게임 씬이면 작동될 코드
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
                    //캔버스들 받아오기
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
                Debug.Log($"popupUi: {popupUI.UIName}은 닫을 수 없습니다.");
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
