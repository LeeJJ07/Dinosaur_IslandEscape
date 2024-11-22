using JongJin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
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
        
        public enum ETestType
        {
            RunningScenePanel,
            TutorialPopupPanel,
            TestCanvas,

        }

        private static UIManager s_Instance;
        
        private Dictionary<string, UnityEngine.Object> uiPrefabs = new Dictionary<string, UnityEngine.Object>();
        private Dictionary<string, CUIBase> uiObjs;

        private const string UIMANGEROBJECTNAME = "_UIManager";
        private const string PREFABSPATH = "Prefabs/UI/";

        private GameSceneController gameSceneController;

        private int popupIndex = 0;

        public CUIScene CurSceneUI { get; private set; } = null;
        public Stack<CUIPopup> PopupUIStack { get; private set; } = null;
        public GameObject MainCanvas { private get; set; }
       

        public static UIManager Instance
        {
            get
            {
                if(s_Instance == null)
                {
                    GameObject newUIManagerObject = new GameObject(UIMANGEROBJECTNAME);
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

            Initialzie();
        }

        //Todo<이학승> 임시로 기본 Find를 통해 받아옴 추후 Tag던 이름이던 참조 해야됨
        private void Initialzie()
        {
            MainCanvas = GameObject.Find("MainCanvas");
            uiPrefabs = new Dictionary<string, UnityEngine.Object>();
            uiObjs = new Dictionary<string, CUIBase>();
            PopupUIStack = new Stack<CUIPopup>();
        }

        public UnityEngine.Object UICashing<T>(System.Type type, int enumIndex) where T : UnityEngine.Object
        { 
            if (!type.IsEnum)
                return null;

            string uiName = type.GetEnumName(enumIndex) ;

            if (uiPrefabs.ContainsKey(uiName))
                return uiPrefabs[uiName];

            T uiObj = Resources.Load<T>(PREFABSPATH + $"{uiName}");
             
            if(uiObj == null)
                 Debug.Log("로드 실패: " + PREFABSPATH + $"에 {uiName}는 존재하지 않습니다.");

            uiPrefabs.Add(uiName, uiObj);

            return uiPrefabs[uiName];
        }


        public void ShowSceneUI(string key)
        {
            CUIScene sceneUI = null;

            if (CurSceneUI != null)
            {
                CurSceneUI.Hide();
                Destroy(CurSceneUI.gameObject);
            }

            if (!uiPrefabs.ContainsKey(key))
            {
                Debug.Log($"SceneUI Key: {key}가 존재하지 않습니다.");
                return;
            }
            
            if (sceneUI = uiPrefabs[key].GetComponent<CUIScene>())
            {
                if (CurSceneUI = Instantiate(sceneUI))
                {
                    CurSceneUI.transform.SetParent(MainCanvas.transform, false);
                    CurSceneUI.Show();
                }
                else
                    Debug.Log($"{key}생성 실패");
            }
            else
                Debug.Log($"{key}는 CUIScene를 상속받지 않는 타입입니다.");

        }

        //TODO<이학승> ShowPopupUI 제외하고 자주쓰이는 팝업 나중에 한번만 뜰 팝업들을 정리하는 메서드가 필요하다. 24/11/21 if문 보기 안좋으니수정필요
        public bool ShowPopupUI(string key)
        {
            CUIPopup popUI = null;

            if (!uiPrefabs.ContainsKey(key))
            {
                Debug.Log($"PopupUI Key: {key}가 존재하지 않습니다.");
                return false;
            }

            if (uiObjs.ContainsKey(key))
            {
                if (popUI = uiObjs[key] as CUIPopup)
                {
                    PopupUIStack.Push(popUI);
                    PopupUIStack.Peek().Show();
                    ++popupIndex;
                }
                else
                {
                    Debug.Log($"{key}는 CUIPopup을 가지고 있지 않습니다.");
                    return false;
                }
            }
            else if (popUI = Instantiate(uiPrefabs[key]).GetComponent<CUIPopup>())
            {
                popUI.transform.SetParent(MainCanvas.transform, false);

                uiObjs.Add(key, popUI);
                PopupUIStack.Push(popUI);
                PopupUIStack.Peek().Show();
                ++popupIndex;
            }
            else
            {
                Debug.Log($"{key}는 CUIPopup을 가지고 있지 않습니다.");
                return false;
            }

            PopupUIStack.Peek().gameObject.transform.SetAsLastSibling();
            return true;

        }

        public void ClosePopupUI()
        {
            if (PopupUIStack.Count == 0)
                return;

            PopupUIStack.Peek().Hide();
            PopupUIStack.Pop();
            --popupIndex;
        }

        public void ClosePopupUI(CUIPopup popupUI)
        {
            if (PopupUIStack.Count == 0)
                return;

            if (PopupUIStack.Peek() != popupUI)
            {
                Debug.Log($"popupUi: {popupUI.UIName}은 닫을 수 없습니다.");
                return;
            }

            PopupUIStack.Peek().Hide();
            PopupUIStack.Pop();
            --popupIndex;
        }

        public void CloseAllPopupUI()
        {
            while(PopupUIStack.Count > 0)
                ClosePopupUI();
        }

        public void ClearUIObj()
        {
            if (uiObjs.Count == 0)
                return;

            CloseAllPopupUI();

            foreach (CUIBase items in uiObjs.Values)
            {
                Destroy(items.gameObject);
                Debug.Log(items.name + "파괴");
            }
            uiObjs.Clear();
            PopupUIStack.Clear();
        }
        


    }
}
