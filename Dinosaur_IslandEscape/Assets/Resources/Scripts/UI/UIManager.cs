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

        //Todo<���н�> �ӽ÷� �⺻ Find�� ���� �޾ƿ� ���� Tag�� �̸��̴� ���� �ؾߵ�
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
                 Debug.Log("�ε� ����: " + PREFABSPATH + $"�� {uiName}�� �������� �ʽ��ϴ�.");

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
                Debug.Log($"SceneUI Key: {key}�� �������� �ʽ��ϴ�.");
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
                    Debug.Log($"{key}���� ����");
            }
            else
                Debug.Log($"{key}�� CUIScene�� ��ӹ��� �ʴ� Ÿ���Դϴ�.");

        }

        //TODO<���н�> ShowPopupUI �����ϰ� ���־��̴� �˾� ���߿� �ѹ��� �� �˾����� �����ϴ� �޼��尡 �ʿ��ϴ�. 24/11/21 if�� ���� �������ϼ����ʿ�
        public bool ShowPopupUI(string key)
        {
            CUIPopup popUI = null;

            if (!uiPrefabs.ContainsKey(key))
            {
                Debug.Log($"PopupUI Key: {key}�� �������� �ʽ��ϴ�.");
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
                    Debug.Log($"{key}�� CUIPopup�� ������ ���� �ʽ��ϴ�.");
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
                Debug.Log($"{key}�� CUIPopup�� ������ ���� �ʽ��ϴ�.");
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
                Debug.Log($"popupUi: {popupUI.UIName}�� ���� �� �����ϴ�.");
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
                Debug.Log(items.name + "�ı�");
            }
            uiObjs.Clear();
            PopupUIStack.Clear();
        }
        


    }
}
