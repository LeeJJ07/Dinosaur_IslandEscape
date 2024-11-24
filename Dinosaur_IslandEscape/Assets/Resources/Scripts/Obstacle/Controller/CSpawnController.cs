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
		[SerializeField] GameObject player1Node;
		[SerializeField] GameObject player2Node;
		[SerializeField] GameObject missionGround;

		private CObstacleObjectPool obstaclePool;
		private CCreatureHerdPool creatureHerdPool;
		private CCreatureBackgroundPool creatureBackgroundPool;
		private CFlyPool flyPool;
		private CVolcanicAshPool volcanicAshPool;
		private GameObject gameSceneController;
		private GameObject[] playerNode;

		public Vector3 MissionGroundPos { get; private set; }

		private int playerCount = 2;
		private float[] obstacleGenerateChance;
		private float[] creatureHerdGenerateChance;
		private System.Random random = new System.Random();

		private bool isPlayerRunning = false;
		private bool isChangedState = false;
		private bool isThirdMissionGenerate = false;

		private int curGeneratePosition;
		private int oldGeneratePosition;

		private RunningState runningState;
		private EGameState curState;
		private GameSceneController gamecSceneController;

		// TODO < ������ > - ���� �÷��̾��� ��ġ�� �޾ƾ� ��. - 2024.11.07 16:10
		public int FirstPlayerPosition { get; private set; }
		/// <summary>
		/// ���� �ͷ� ���� �ֱ�� Timer�� ���� �ð��� �� ������ Ȯ�������� �����ϰ� ����.
		/// </summary>
		public int Timer { get; private set; }

		private void Start()
		{
			obstacleGenerateChance = new float[playerCount];
			for (int i = 0; i < playerCount; i++)
				obstacleGenerateChance[i] = 0.2f;

			creatureHerdGenerateChance = new float[playerCount];
			for (int i = 0; i < playerCount; i++)
				creatureHerdGenerateChance[i] = 0.2f;

			playerNode = new GameObject[playerCount];
			playerNode[0] = player1Node;
			playerNode[1] = player2Node;

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
					TimerRunning();

					if (IsSpawnTime(150))
						SpawnCreatureHerdBackground();

					if (IsSpawnTime(600))
						CheckCanSpawnCreatureHerd();
					break;
				case EGameState.SECONDMISSION:
					TimerRunning();

					if (IsSpawnTime(300))
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

		private void SpawnCreatureHerdBackground()
		{
			creatureBackgroundPool.SpawnCreatureHerd(MissionGroundPos);
		}

		private void UpdateCurState()
		{
			curState = gamecSceneController.CurState;
		}
		private void CheckCanSpawnObstacle()
		{
			for (int i = 0; i < playerCount; i++)
			{
				if (CanSpawnObstacle(obstacleGenerateChance[i]))
				{
					// TODO < ������ > - "10"�� RubberBand Size�� �ٲ���� ��. - 2024.11.11 18:55

					obstaclePool.SpawnObstacle(i, runningState.GetPlayerDistance(i) + 10);

					ResetChance(obstacleGenerateChance, i);
				}
				else
					ChanceUp(obstacleGenerateChance, i);
			}
		}
		private void CheckCanSpawnCreatureHerd()
		{
			for (int i = 0; i < playerCount; i++)
			{
				if (CanSpawnObstacle(creatureHerdGenerateChance[i]))
				{
					if (IsSpawnHerd(i))
					{
						ResetChance(creatureHerdGenerateChance, i);
						break;
					}
				}
				else
					ChanceUp(creatureHerdGenerateChance, i);
			}
		}
		private void GenerateFly()
		{
			flyPool.SpawnFly(MissionGroundPos);
		}
        private void GenerateVolcanicAsh()
		{
			volcanicAshPool.SpawnVolcanicAshes(MissionGroundPos);
        }
        /// <summary>
        /// ū �ͷ�/�Ǿ ���� �� true�� ��ȯ�Ͽ� �ش� Line���� �� �̻� ��ȯ���� ����. ��, ���� �� ������� �Ѹ����� ����
        /// </summary>
        private bool IsSpawnHerd(int i)
		{
			return creatureHerdPool.SpawnCreatureHerd(i, MissionGroundPos);
		}
		private bool IsSpawnSection(out int curGeneratePosition)
		{
			curGeneratePosition = (int)runningState.FirstRankerDistance;

			return curGeneratePosition != oldGeneratePosition && !Convert.ToBoolean((curGeneratePosition % 16));
		}
		private bool IsSpawnTime(int time)
		{
			return !Convert.ToBoolean((Timer % time));
		}
		private void TimerRunning()
		{
			Timer++;
		}
		private bool CanSpawnObstacle(float chance)
		{
			float randomValue = (float)random.NextDouble();
			return randomValue < chance;
		}
		private void ResetChance(float[] arr, int index)
		{
			arr[index] = 0.2f;
		}
		private void ChanceUp(float[] arr, int index)
		{
			arr[index] += 0.2f;
		}
	}
}