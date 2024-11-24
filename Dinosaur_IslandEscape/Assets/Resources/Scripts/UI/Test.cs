using HakSeung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    
    private void Awake()
    {
        //����� UI �̸� Cashing �� ������ �� �ʹݿ� ������ ���־�� �Ѵ�.
        UIManager.Instance.UICashing<GameObject>(typeof(UIManager.ETestType), 0);
        UIManager.Instance.UICashing<GameObject>(typeof(UIManager.ETestType), 1);
        UIManager.Instance.UICashing<GameObject>(typeof(UIManager.ETestType), 2);
        
    }

    void Start()
    {
        //�� UI ���� ����� ������ �̸� �غ��� ����. Default�� 0���� ���� ���·� ������
        UIManager.Instance.CreateSceneUI(UIManager.ETestType.EventScenePanel.ToString(), 1);
        UIManager.Instance.CreateSceneUI(UIManager.ETestType.RunningCanvas.ToString(), 0);
    }

    // Update is called once per frame
    void Update()
    {
        #region �˾� UI ��� ����
        //�˾� UI ���̱�
        if (Input.GetKeyDown(KeyCode.P))
        {
            UIManager.Instance.ShowPopupUI(UIManager.ETestType.TutorialPopupPanel.ToString());
        }

        //�˾� UI �ݱ�
        if (Input.GetKeyDown(KeyCode.O))
        {
            UIManager.Instance.ClosePopupUI();
        }
        #endregion

        #region �� ���� ����
        //��UI ���� �տ� CreateSceneUI�� �� UI�� �ε����� ���� ���� ����
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UIManager.Instance.SceneUISwap(1);
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            UIManager.Instance.SceneUISwap(0);
        }
        #endregion

        #region �� �Ѿ �� ������ ȣ�����־�� �ϴ� �Լ���
        if (Input.GetKeyDown(KeyCode.I))
        {
            //�˾� UI �ı� 
            UIManager.Instance.ClearUIObj();
            //�� UI �ı�
            UIManager.Instance.ClearSceneUI();
        }

        #endregion


        //��Ʈ �� ����
        ((CUIEventPanel)UIManager.Instance.CurSceneUI).playerNotes[1].Show();
        //���α׷��� ���� �ƽ� �� ����
        ((CUIEventPanel)UIManager.Instance.CurSceneUI).ProgressBar.MaxProgress = 100f;
        //���α׷��� �� �� ����
        ((CUIEventPanel)UIManager.Instance.CurSceneUI).ProgressBar.FillProgressBar(10f);
    }
}
