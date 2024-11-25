using HakSeung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HakSeung.UIManager;

namespace JongJin
{
	public class FirstMissionState : MonoBehaviour, IGameState
	{
		private bool isSuccess = false;
		private bool isMissionFinished = false;
		private bool isWait = false;
		private float timer = 60f;

		private GameObject player1;
		private GameObject player2;

        private void Awake()
        {
			player1 = GameObject.FindWithTag("Player1");
			player2 = GameObject.FindWithTag("Player2");
        }
        public void EnterState()
		{
			isSuccess = false;
			player1.transform.localScale = Vector3.one * 1.5f;
			player2.transform.localScale = Vector3.one * 1.5f;
            timer = 60f;
		}
		public void UpdateState()
		{
			DecreaseTime();

            SetTimer();
            CheckProgressBar();
		}

		public void ExitState()
        {
            ((CUIEventPanel)UIManager.Instance.CurSceneUI).progressBar.Init();
            UIManager.Instance.SceneUISwap((int)ESceneUIType.RunningCanvas);
            player1.transform.localScale = Vector3.one;
            player2.transform.localScale = Vector3.one;
        }
		public bool IsFinishMission(out bool success)
		{
			success = false;
			if (isSuccess)
			{
				if (!isWait)
					StartCoroutine("Stay");
				success = true;
				return isMissionFinished;
			}
			if (timer <= 0)
			{
				if (!isWait)
					StartCoroutine("Stay");
                return isMissionFinished;
			}
			return false;
		}
		private void DecreaseTime()
		{
			timer -= Time.deltaTime;
		}
		private void CheckProgressBar()
		{
			if (((CUIEventPanel)UIManager.Instance.CurSceneUI).progressBar.isProgressBarFullFilled)
				isSuccess = true;
		}
		private IEnumerator Stay()
		{
			isWait = true;
            yield return new WaitForSeconds(3.0f);
			isMissionFinished = true;
        }
        private void SetTimer()
        {
            ((CUIEventPanel)UIManager.Instance.CurSceneUI).SetTimer(timer);
        }
    }
}