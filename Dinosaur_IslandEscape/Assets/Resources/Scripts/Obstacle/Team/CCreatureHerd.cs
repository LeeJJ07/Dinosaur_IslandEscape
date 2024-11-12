using JongJin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEditor.Rendering.InspectorCurveEditor;

namespace MyeongJin
{
	public class CCreatureHerd : MonoBehaviour
	{
		public IObjectPool<CCreatureHerd> Pool { get; set; }

		private GameObject gameSceneController;
		private GameSceneController gamecSceneController;
		private EGameState curState;
		private EGameState oldState;

		private float timeToCheckPosition = 1f;
		private float moveSpeed = 10f;

		//TODO < 문명진 > - destructPosition Value를 Dinosaur의 끝 부분으로 변경해야함. - 2024.11.07 4:10
		[SerializeField]
		private int destructPosition = -10;
		// <<

		private void Start()
		{
			gameSceneController = GameObject.Find("GameSceneController");
			gamecSceneController = gameSceneController.GetComponent<GameSceneController>();
		}

		private void Update()
		{
			FlyPteranodon();
		}
		private void OnEnable()
		{
			StartCoroutine(CheckPosition());
		}
		IEnumerator CheckPosition()
		{
			yield return new WaitForSeconds(timeToCheckPosition);

			StateCheck();

            if (IsStateChanged())
			{
				ReturnToPool();
				yield return null;
            }

			StartCoroutine(CheckPosition());

			if (this.transform.position.z < destructPosition)
				ReturnToPool();
		}
		private void StateCheck()
		{
			curState = gamecSceneController.CurState;
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

		private void ReturnToPool()
		{
			Pool.Release(this);
		}
		private void ResetObstacle()
		{

		}
		private void FlyPteranodon()
		{
			Debug.Log("Fly Pteranodon!");

			this.transform.Translate(Vector3.forward * Time.deltaTime * -moveSpeed);
		}
	}
}