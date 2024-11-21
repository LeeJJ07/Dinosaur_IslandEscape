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
        }

        private static UIManager s_Instance;
        
        private Dictionary<string, UnityEngine.Object> uiPrefabs = new Dictionary<string, UnityEngine.Object>();
        private Dictionary<string, CUIBase> uiObjs = new Dictionary<string, CUIBase>();

        private const string UIMANGEROBJECTNAME = "_UIManager";
        private const string PREFABSPATH = "Prefabs/UI/";

        private GameSceneController gameSceneController;

        
        private Stack<CUIPopup> popupStack;

        private int popupIndex = 0;

        public CUIScene CurSceneUI { get; private set; } = null;
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
                Destroy(CurSceneUI);
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

        //TODO<���н�> ShowPopupUI �����ϰ� ���־��̴� �˾� ���߿� �ѹ��� �� �˾����� �����ϴ� �޼��尡 �ʿ��ϴ�. 24/11/21
        public void ShowPopupUI(string key)
        {
            CUIPopup popUI = null;

            if (!uiPrefabs.ContainsKey(key))
            {
                Debug.Log($"PopupUI Key: {key}�� �������� �ʽ��ϴ�.");
                return;
            }
            
            if (uiObjs[key] != null)
            {
                if(popUI = uiObjs[key] as CUIPopup)
                {
                    popupStack.Push(popUI);
                    popupStack.Peek().Show();
                    ++popupIndex;
                }
                else
                    Debug.Log($"{key}�� CUIPopup�� ������ ���� �ʽ��ϴ�.");

                return;
            }
                

            if (popUI = Instantiate(uiPrefabs[key]).GetComponent<CUIPopup>())
            {
                popUI.transform.SetParent(MainCanvas.transform, false);

                uiObjs.Add(key, popUI);
                popupStack.Push(popUI);
                popupStack.Peek().Show();
                ++popupIndex;
            }
            else
                Debug.Log($"{key}�� CUIPopup�� ������ ���� �ʽ��ϴ�.");
        }

        public void ClosePopupUI()
        {
            if (popupStack.Count == 0)
                return;

            popupStack.Peek().Hide();
            popupStack.Pop();
            --popupIndex;
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

            popupStack.Peek().Hide();
            popupStack.Pop();
            --popupIndex;
        }

        public void CloseAllPopupUI()
        {
            while(popupStack.Count > 0)
                ClosePopupUI();
        }

    }
}
