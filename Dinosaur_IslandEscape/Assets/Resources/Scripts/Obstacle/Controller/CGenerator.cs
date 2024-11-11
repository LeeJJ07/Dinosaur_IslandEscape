using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyeongJin
{
	public class CGenerator : MonoBehaviour
	{
		private CObstacleObjectPool obstaclePool;
		private CCreatureHerdPool creatureHerdPool;

		private int playerCount = 2;
		private float[] obstacleGenerateChance;
		private float[] creatureHerdGenerateChance;
		private System.Random random = new System.Random();

		private bool isPlayerRunning = false;
		private bool isSpawnSection = false;

		// TODO < 문명진 > - 실제 플레이어의 위치를 받아야 함. - 2024.11.07 16:10
		public int FirstPlayerPosition { get; private set; }
		/// <summary>
		/// 현재 익룡 생성 주기는 Timer가 일정 시간이 될 때마다 확률적으로 생성하고 있음.
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

			obstaclePool = gameObject.AddComponent<CObstacleObjectPool>();
			creatureHerdPool = gameObject.AddComponent<CCreatureHerdPool>();
		}
		private void FixedUpdate()
		{
			if (isPlayerRunning)
			{
				Running();

				if (IsSpawnSection())
					CheckCanSpawnObstacle();
			}
			else
			{
				TimerRunning();

				if (IsSpawnTime())
					CheckCanSpawnCreatureHerd();
			}
		}
		// TODO < 문명진 > - OnGUI 메소드 내용을 다른 호출 메소드(Update or Observer)로 변경해야함. - 2024.11.09 17:05
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
                    // TODO < 문명진 > - SpawnObstacle(트랙 번호, 트랙의 플레이어 위치)로 변경해줘야함. - 2024.11.11 14:40
					// 현재는 1등 위치로만 기준 되어 있음.
                    obstaclePool.SpawnObstacle(i, FirstPlayerPosition);
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
					creatureHerdPool.SpawnPteranodon(i);
					ResetChance(creatureHerdGenerateChance, i);
				}
				else
					ChanceUp(creatureHerdGenerateChance, i);
			}
		}
		private bool IsSpawnSection()
		{
			return !Convert.ToBoolean((FirstPlayerPosition % 10));
		}
		private bool IsSpawnTime()
		{
			return !Convert.ToBoolean((Timer % 50));
		}
		private void Running()
		{
			FirstPlayerPosition++;
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