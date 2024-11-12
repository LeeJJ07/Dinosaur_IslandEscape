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

        //TODO<�н�> sceneCanvas�� �Ҵ�� ĵ�������� ���� start game event end �� �װ��� �����ؼ� �� state�� �޾ƿ;ߵ�
        [SerializeField] private GameObject[] sceneCanvas = new GameObject[(int)ECanvasType.END]; 
        [SerializeField] private int curCanvasTypeIndex = (int)ECanvasType.END;
        
        private const string uiManagerObjectName = "_UIManager";
        private static UIManager s_Instance;

        private GameSceneController gameSceneController;

        //���� ���� �´� �˾����� �޾ƿ;ߵ�
        private Stack<CUIPopup> popupStack = new Stack<CUIPopup>();

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
            int nextCanvasIndex = (int)ECanvasType.END;

            //���� ���� ���� ���̸� �۵��� �ڵ�
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

                for (int i = (int)ECanvasType.START; i < sceneCanvas.Length; i++)
                {

                }

                sceneCanvas[(int)ECanvasType.START].SetActive(true);

                for (int i = (int)ECanvasType.START + 1; i < sceneCanvas.Length; i++)
                    sceneCanvas[i].SetActive(false);
            }
        }

        private void CanvasChange(int nextCanvasIndex)
        {
            sceneCanvas[curCanvasTypeIndex].SetActive(false);
            curCanvasTypeIndex = nextCanvasIndex;
            sceneCanvas[curCanvasTypeIndex].SetActive(true);
        }


        public void ClosePopupUI()
        {

        }

    }
}
