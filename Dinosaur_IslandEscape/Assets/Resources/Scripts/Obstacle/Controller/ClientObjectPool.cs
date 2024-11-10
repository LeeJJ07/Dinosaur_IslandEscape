using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyeongJin
{
	public class ClientObjectPool : MonoBehaviour
	{
		private CObstacleObjectPool obstaclePool;
		private CCreatureHerdPool creatureHerdPool;

		private int playerCount = 2;
		private float[] obstacleLine;
		private float[] creatureHerdLine;
        private System.Random random = new System.Random();

        private void Start()
		{
            obstacleLine = new float[playerCount];
            for (int i = 0; i < playerCount; i++)
                obstacleLine[i] = 0.2f;

            creatureHerdLine = new float[playerCount];
            for (int i = 0; i < playerCount; i++)
                creatureHerdLine[i] = 0.2f;

            obstaclePool = gameObject.AddComponent<CObstacleObjectPool>();
			creatureHerdPool = gameObject.AddComponent<CCreatureHerdPool>();
		}

		// TODO < 문명진 > - OnGUI 메소드 내용을 다른 호출 메소드(Update or Observer)로 변경해야함. - 2024.11.09 17:05
		private void OnGUI()
		{
			if (GUILayout.Button("Spawn Obstacle"))
			{
				for (int i = 0; i < playerCount; i++)
				{
					if (CanSpawnObstacle(obstacleLine[i]))
					{
						obstaclePool.SpawnObstacle();
						ResetChance(obstacleLine, i);
					}
					else
						ChanceUp(obstacleLine, i);
				}
			}

			if (GUI.Button(new Rect(10, 10, 50, 50), "Spawn Pteranodon"))
			{
                for (int i = 0; i < playerCount; i++)
                {
                    if (CanSpawnObstacle(creatureHerdLine[i]))
                    {
                        creatureHerdPool.SpawnPteranodon();
                        ResetChance(creatureHerdLine, i);
                    }
                    else
                        ChanceUp(creatureHerdLine, i);
                }
            }
		}

		bool CanSpawnObstacle(float chance)
		{
            float randomValue = (float)random.NextDouble();
			return randomValue < chance;
        }
        void ResetChance(float[] arr, int index)
        {
            arr[index] = 0.2f;
        }
        void ChanceUp(float[] arr, int index)
		{
            arr[index] += 0.2f;
		}
    }
}