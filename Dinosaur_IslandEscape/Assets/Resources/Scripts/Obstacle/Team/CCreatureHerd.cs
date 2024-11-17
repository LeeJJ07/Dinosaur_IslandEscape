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
		private GameSceneController gamecSceneController;
		private EGameState curState;
		private EGameState oldState;

		private float timeToCheckPosition = 1f;
		private bool isTouch = false;

		// >>: Stoop
		private Transform[] controlPoints; // 제어점 (최소 4개 필요)

		private float moveSpeed = 10f;          // 이동 속도
		private float t = 0f;             // Catmull-Rom 곡선의 시간 변수
		private int currentSegment = 0;   // 현재 이동 중인 곡선 구간
		// << 

		//TODO < 문명진 > - destructPosition Value를 Dinosaur의 끝 부분으로 변경해야함. - 2024.11.07 4:10
		[SerializeField]
		private int destructPosition = -10;
		// <<

		private void Start()
		{
			controlPoints = GameObject.FindGameObjectsWithTag("ControlPoint")
							  .Select(obj => obj.transform)
							  .ToArray();
			gameSceneController = GameObject.Find("GameSceneController");
			gamecSceneController = gameSceneController.GetComponent<GameSceneController>();
			this.transform.position = controlPoints[0].position;
        }

		private void Update()
		{
			if (isTouch)
				Climb();
			else
				Stoop();
		}
		private void OnEnable()
		{
			StartCoroutine(CheckPosition());
		}
		IEnumerator CheckPosition()
		{
			yield return new WaitForSeconds(timeToCheckPosition);

			//StateCheck();

   //         if (IsStateChanged())
			//{
			//	ReturnToPool();
			//	yield return null;
   //         }

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
		private void Stoop()
		{
			Debug.Log("DownFall Pteranodon!");

			if (controlPoints.Length < 4) return;

			Transform p0 = controlPoints[currentSegment];
			Transform p1 = controlPoints[currentSegment + 1];
			Transform p2 = controlPoints[currentSegment + 2];
			Transform p3 = controlPoints[currentSegment + 3];

			// Catmull-Rom 곡선 공식 사용
			Vector3 newPosition = CatmullRom(p0.position, p1.position, p2.position, p3.position, t);
			transform.position = newPosition;

			// 곡선의 t 값을 점진적으로 업데이트 (speed로 곡선 이동 속도 조정)
			t += Time.deltaTime * moveSpeed / Vector3.Distance(p1.position, p2.position);

			// t가 1에 도달하면 다음 구간으로 전환
			if (t >= 1f)
			{
				t = 0f; // 곡선 시간 초기화
				currentSegment++;

				// 마지막 구간을 지나면 스크립트 중지
				if (currentSegment == 1)
					this.GetComponentInChildren<Animator>().SetBool("isTouch", true);

                if (currentSegment >= controlPoints.Length - 3)
				{
					currentSegment = 0;
					enabled = false;
					Debug.Log("Catmull-Rom Spline 경로 끝까지 도착했습니다!");
				}
			}
		}
		private Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			return 0.5f * (
				(2f * p1) +
				(-p0 + p2) * t +
				(2f * p0 - 5f * p1 + 4f * p2 - p3) * t * t +
				(-p0 + 3f * p1 - 3f * p2 + p3) * t * t * t);
		}
		private void Climb()
		{
			Debug.Log("Climb Pteranodon!");

			this.transform.Translate(Vector3.forward * Time.deltaTime * +moveSpeed);
		}
	}
}