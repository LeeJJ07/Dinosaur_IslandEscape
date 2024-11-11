using HakSeung;
using JongJin;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.InspectorCurveEditor;

namespace MyeongJin
{
	public class CGenerator : MonoBehaviour
	{
		[SerializeField] GameObject player1Node;
		[SerializeField] GameObject player2Node;

		private CObstacleObjectPool obstaclePool;
		private CCreatureHerdPool creatureHerdPool;
		private GameObject gameSceneController;
		private GameObject[] playerNode;

        private int playerCount = 2;
		private float[] obstacleGenerateChance;
		private float[] creatureHerdGenerateChance;
		private System.Random random = new System.Random();

		private bool isPlayerRunning = false;
		private bool isSpawnSection = false;

		private int curGeneratePosition;
		private int oldGeneratePosition;

		RunningState runningState;
		EGameState curState;

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


            obstaclePool = gameObject.AddComponent<CObstacleObjectPool>();
			creatureHerdPool = gameObject.AddComponent<CCreatureHerdPool>();

			gameSceneController = GameObject.Find("GameSceneController");
            runningState = gameSceneController.GetComponent<RunningState>();
            curState = gameSceneController.GetComponent<GameSceneController>().CurState;
        }
		private void Update()
		{
			UpdateCurState();
			switch(curState)
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

                    if (IsSpawnTime())
                        CheckCanSpawnCreatureHerd();
                    break;
                case EGameState.SECONDMISSION:

                    break;
                case EGameState.THIRDMISSION:

                    break;
            }
		}
		private void UpdateCurState()
		{
            curState = gameSceneController.GetComponent<GameSceneController>().CurState;
        }
        // TODO < ������ > - OnGUI �޼ҵ� ������ �ٸ� ȣ�� �޼ҵ�(Update or Observer)�� �����ؾ���. - 2024.11.09 17:05
        private void OnGUI()
		{
			if (GUILayout.Button("Generate"))
			{
				isPlayerRunning = !isPlayerRunning;
				Timer = 0;
			}
		}
		private void CheckCanSpawnObstacle()
		{
			for (int i = 0; i < playerCount; i++)
			{
				if (CanSpawnObstacle(obstacleGenerateChance[i]))
				{
					// TODO < ������ > - "10"�� RubberBand Size�� �ٲ���� ��. - 2024.11.11 18:55

					obstaclePool.SpawnObstacle(i, runningState.GetPlayerDistance(i) + 10);
					playerNode[i].GetComponent<CUINote>().Show();

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
					if (!IsSpawnHerd(i))
						break;

					ResetChance(creatureHerdGenerateChance, i);
				}
				else
					ChanceUp(creatureHerdGenerateChance, i);
			}
		}
		private bool IsSpawnHerd(int i)
		{
			return creatureHerdPool.SpawnPteranodon(i);

        }
		private bool IsSpawnSection(out int curGeneratePosition)
		{
			curGeneratePosition = (int)runningState.FirstRankerDistance;

			return curGeneratePosition != oldGeneratePosition && !Convert.ToBoolean((curGeneratePosition % 16));
		}
		private bool IsSpawnTime()
		{
			return !Convert.ToBoolean((Timer % 250));
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