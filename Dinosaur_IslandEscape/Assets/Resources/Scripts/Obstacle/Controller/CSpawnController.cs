using HakSeung;
using JongJin;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.InspectorCurveEditor;

namespace MyeongJin
{
	public class CSpawnController : MonoBehaviour
    {
        public Vector3 MissionGroundPos { get; private set; }

        [SerializeField] private GameObject missionGround;

		private CObstacleObjectPool obstaclePool;
		private CCreatureHerdPool creatureHerdPool;
		private CCreatureBackgroundPool creatureBackgroundPool;
		private CFlyPool flyPool;
		private CVolcanicAshPool volcanicAshPool;
		private GameObject gameSceneController;
		private GameObject swatter;

        private string swatterPath = "Prefabs/Obstacle/Team/SecondMission/Swatter";      // 프리팹이 존재하는 폴더 위치

        private float creatureTimer = 0;
        private float flyTimer = 0;
        private float backgroundTimer = 0;
        private int playerCount = 2;
		private bool isThirdMissionGenerate = false;

		private int curGeneratePosition;
		private int oldGeneratePosition;

		private RunningState runningState;
		private EGameState curState;
		private GameSceneController gamecSceneController;

		// TODO < 문명진 > - 실제 플레이어의 위치를 받아야 함. - 2024.11.07 16:10
		public int FirstPlayerPosition { get; private set; }
		/// <summary>
		/// 현재 익룡 생성 주기는 Timer가 일정 시간이 될 때마다 확률적으로 생성하고 있음.
		/// </summary>
		public int Timer { get; private set; }

		private void Start()
		{
            swatter = Resources.Load<GameObject>(swatterPath);

            MissionGroundPos = missionGround.GetComponent<Transform>().position;

			obstaclePool = gameObject.AddComponent<CObstacleObjectPool>();
			creatureHerdPool = gameObject.AddComponent<CCreatureHerdPool>();
			creatureBackgroundPool = gameObject.AddComponent<CCreatureBackgroundPool>();
			flyPool = gameObject.AddComponent<CFlyPool>();
			volcanicAshPool = gameObject.AddComponent<CVolcanicAshPool>();

			gameSceneController = GameObject.Find("GameSceneController");
			gamecSceneController = gameSceneController.GetComponent<GameSceneController>();
			runningState = gameSceneController.GetComponent<RunningState>();

			curState = gamecSceneController.CurState;
		}
		private void Update()
		{
			UpdateCurState();

			switch (curState)
			{
				case EGameState.RUNNING:
					if (IsSpawnSection(out curGeneratePosition))
						CheckCanSpawnObstacle();
					oldGeneratePosition = curGeneratePosition;
					break;
				case EGameState.TAILMISSION:

					break;
				case EGameState.FIRSTMISSION:
                    TimerIncrease();

                    if (IsBackgroundSpawnTime(1.5f))
						SpawnCreatureHerdBackground();


                    if (IsCreatureSpawnTime(6))
						CheckCanSpawnCreatureHerd();
					break;
				case EGameState.SECONDMISSION:
                    TimerIncrease();

					if (IsFlySpawnTime(3))
						GenerateFly();
					break;
				case EGameState.THIRDMISSION:
					if (!isThirdMissionGenerate)
					{
						isThirdMissionGenerate = true;

						GenerateVolcanicAsh();
					}
					break;
			}
		}
		public void GenerateSwatter(int playerIndex, int vertical)
		{
            var go = Instantiate(swatter);
            go.name = "Swatter" + playerIndex;

            go.AddComponent<CSwatter>();

			go.GetComponent<CSwatter>().Init(playerIndex, vertical);
        }
		private void TimerIncrease()
		{
            creatureTimer += Time.deltaTime;
			flyTimer += Time.deltaTime;
			backgroundTimer += Time.deltaTime;
        }
		private void SpawnCreatureHerdBackground()
		{
			creatureBackgroundPool.SpawnCreatureHerd(MissionGroundPos);
			backgroundTimer = 0;
        }

		private void UpdateCurState()
		{
			curState = gamecSceneController.CurState;
		}
		private void CheckCanSpawnObstacle()
		{
			// TODO < 문명진 > - "10"을 RubberBand Size로 바꿔줘야 함. - 2024.11.11 18:55
			int playerindex = UnityEngine.Random.Range(0, playerCount);

			GameObject obstacle = obstaclePool.SpawnObstacle(playerindex, runningState.GetPlayerDistance(playerindex) + 10);

			((CUIRunningCanvas)UIManager.Instance.CurSceneUI).playerNotes[playerindex].Show(obstacle, playerindex);
		}
		private void CheckCanSpawnCreatureHerd()
		{
			IsSpawnHerd(UnityEngine.Random.Range(0, playerCount));

			creatureTimer = 0;
		}
		private void GenerateFly()
		{
			flyPool.SpawnFly(MissionGroundPos);
			flyTimer = 0;
        }
		private void GenerateVolcanicAsh()
		{
			volcanicAshPool.SpawnVolcanicAshes(MissionGroundPos);
		}
		/// <summary>
		/// 큰 익룡/악어를 생성 시 true를 반환하여 해당 Line에는 더 이상 소환되지 않음. 즉, 라인 수 상관없이 한마리만 생성
		/// </summary>
		private void IsSpawnHerd(int i)
        {
            creatureHerdPool.SpawnCreatureHerd(i, MissionGroundPos);
		}
		private bool IsSpawnSection(out int curGeneratePosition)
		{
			curGeneratePosition = (int)runningState.FirstRankerDistance;

			return curGeneratePosition != oldGeneratePosition && !Convert.ToBoolean((curGeneratePosition % 16));
		}
		private bool IsCreatureSpawnTime(float time)
		{
			return creatureTimer > time;
		}
        private bool IsBackgroundSpawnTime(float time)
        {
            return backgroundTimer > time;
        }
        private bool IsFlySpawnTime(float time)
        {
            return flyTimer > time;
        }
	}
}