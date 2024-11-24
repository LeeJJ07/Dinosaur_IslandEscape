using HakSeung;
using JongJin;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEditor.Rendering.InspectorCurveEditor;

namespace MyeongJin
{
	public class CCreatureHerd : MonoBehaviour
	{
		public IObjectPool<CCreatureHerd> Pool { get; set; }

		private Animator animator;

		private GameObject gameSceneController;
		private GameObject progressBar;
		private GameSceneController gamecSceneController;
		private EGameState curState;
		private EGameState oldState;

		private Vector3 startPosition;
		// <<

		private void Awake()
		{
			gameSceneController = GameObject.Find("GameSceneController");
			gamecSceneController = gameSceneController.GetComponent<GameSceneController>();
            progressBar = GameObject.Find("ProgressBar");
			if (progressBar == null)
				Debug.Log("Can't Find progressBar");
        }
		private void StateCheck()
		{
			curState = gamecSceneController.CurState;
		}
        private void Update()
        {
            if(progressBar == null)
                Debug.Log("Can't Find progressBar");
        }
        private bool IsStateChanged()
		{
			bool isChanged = false;

			if (oldState != curState)
				isChanged = true;

			oldState = curState;

			return isChanged;
		}
		private void OnDisable()
		{
			ResetObstacle();
		}
		public void ReturnToPool()
		{
			Pool.Release(this);
		}
        public void ReturnToPool(int fillValue)
        {
            progressBar.GetComponent<CUIProgressBar>().FillProgressBar(fillValue);
            Pool.Release(this);
        }
        public void ResetObstacle()
		{

		}
	}
}