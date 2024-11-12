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
        //TODO<�н�> SceneType�� ���� ĵ���� �����Ű�� �ڵ尡 �ʿ��� 24/11/11
        //TODO<�н�> EGameState�� End �߰� ��û �ؾߵ� 24/11/12
        [SerializeField] private Canvas[] sceneCanvas = new Canvas[(int)JongJin.EGameState.THIRDMISSION + 1]; 
        private const string uiManagerObjectName = "_UIManager";
        private static UIManager s_Instance;

        private GameSceneController gameSceneController;
        private Canvas curCanvas;

        //���� ���� �´� �˾����� �޾ƿ;ߵ�
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
                    //TODO<�н�> State��ȭ �� �ٸ� Canvas ��Ȱ��ȭ �ϰ� ���� ĵ������ �۵����Ѿߵ� 24/11/12
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
