using HakSeung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    
    private void Awake()
    {
        UIManager.Instance.UICashing<GameObject>(typeof(UIManager.ETestType), 0);
        UIManager.Instance.UICashing<GameObject>(typeof(UIManager.ETestType), 1);

    }

    void Start()
    {
        UIManager.Instance.CreateSceneUI(UIManager.ETestType.EventScenePanel.ToString(), 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            UIManager.Instance.ShowPopupUI(UIManager.ETestType.TutorialPopupPanel.ToString());
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            UIManager.Instance.ClosePopupUI();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            UIManager.Instance.ClearUIObj();
        }

    }
}
