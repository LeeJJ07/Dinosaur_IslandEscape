using JongJin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.InspectorCurveEditor;

namespace HakSeung
{

    public class UIManager : MonoBehaviour
    {
        enum ECanvasType
        {
            START,
            RUNNING,
            EVENT,
            ENDING,

            END
        }

        //TODO<학승> sceneCanvas에 할당될 캔버스들의 값을 start game event end 총 네개로 구성해서 그 state를 받아와야됨
        [SerializeField] private CUIScene[] sceneCanvas = new CUIScene[(int)ECanvasType.END];
        
        private const string uiManagerObjectName = "_UIManager";
        private static UIManager s_Instance;

        private GameSceneController gameSceneController;
        
        private Stack<CUIPopup> popupCanvasStack = new Stack<CUIPopup>();

        private int curCanvasTypeIndex = (int)ECanvasType.END;
        private int sortIndex;


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
            int nextCanvasIndex = (int)ECanvasType.END;

            //현재 씬이 게임 씬이면 작동될 코드
            switch (gameSceneController.CurState)
            {
                case JongJin.EGameState.RUNNING:
                    nextCanvasIndex = (int)ECanvasType.RUNNING;
                    break;
                case JongJin.EGameState.FIRSTMISSION:
                case JongJin.EGameState.SECONDMISSION:
                case JongJin.EGameState.THIRDMISSION:
                case JongJin.EGameState.TAILMISSION:
                    nextCanvasIndex = (int)ECanvasType.EVENT;
                    break;
            }

            if(curCanvasTypeIndex != nextCanvasIndex)
                CanvasChange(nextCanvasIndex);

        }

        private void Initialize()
        {
            if (curCanvasTypeIndex != (int)ECanvasType.END)
            {
                gameSceneController = GetComponent<GameSceneController>();

                /*for (int i = (int)ECanvasType.START; i < sceneCanvas.Length; i++)
                {
                    //캔버스들 받아오기
                }*/

                sceneCanvas[(int)ECanvasType.START].Show();

                for (int i = (int)ECanvasType.START + 1; i < sceneCanvas.Length; i++)
                    sceneCanvas[i].Hide();
            }
        }

        private void CanvasChange(int nextCanvasIndex)
        {
            sceneCanvas[curCanvasTypeIndex].Hide();
            curCanvasTypeIndex = nextCanvasIndex;
            sceneCanvas[curCanvasTypeIndex].Show();
        }


        public void ClosePopupUI()
        {

        }

    }
}
