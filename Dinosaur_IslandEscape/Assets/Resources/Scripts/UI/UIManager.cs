using JongJin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.InspectorCurveEditor;

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
        //TODO<학승> SceneType에 따라서 캔버스 변경시키는 코드가 필요함 24/11/11
        //TODO<학승> EGameState에 End 추가 요청 해야됨 24/11/12
        [SerializeField] private Canvas[] sceneCanvas = new Canvas[(int)JongJin.EGameState.THIRDMISSION + 1]; 
        private const string uiManagerObjectName = "_UIManager";
        private static UIManager s_Instance;

        private GameSceneController gameSceneController;
        private Canvas curCanvas;

        //현재 씬에 맞는 팝업들을 받아와야됨
        private Stack<CUIPopup> popupStack = new Stack<CUIPopup>();


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
            gameSceneController = GetComponent<GameSceneController>();

            if (s_Instance != null && s_Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            s_Instance = this;

            DontDestroyOnLoad(this.gameObject);
        }

        private void Update()
        {
            switch (gameSceneController.CurState)
            {
                case JongJin.EGameState.RUNNING:
                    //TODO<학승> State변화 시 다른 Canvas 비활성화 하고 현재 캔버스만 작동시켜야됨 24/11/12
                    //sceneCanvas[(int)EGameState.RUNNING];
                    break;
                case JongJin.EGameState.FIRSTMISSION:
                    break;
                case JongJin.EGameState.SECONDMISSION:
                    break;
                case JongJin.EGameState.THIRDMISSION:
                    break;
                case JongJin.EGameState.TAILMISSION:
                    break;
            }

        }

        public void ClosePopupUI()
        {

        }

    }
}
