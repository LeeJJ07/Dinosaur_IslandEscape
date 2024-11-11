using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace MyeongJin
{
	public class CObstacleObjectPool : MonoBehaviour
	{
		public int maxPoolSize = 10;
		public int stackDefaultCapacity = 10;

        private string logName = "Prefabs/Obstacle/Personal/Log";		// 프리팹이 존재하는 폴더 위치
		private string rockName = "Prefabs/Obstacle/Personal/Rock";
		private GameObject log;
		private GameObject rock;

		private void Awake()
		{
			log = Resources.Load<GameObject>(logName);
			rock = Resources.Load<GameObject>(rockName);

			if (log != null)
			{
				Debug.Log($"프리팹 '{logName}'을(를) Load 하였습니다.");
			}
			else
			{
				Debug.LogError($"프리팹 '{logName}'을(를) 찾을 수 없습니다.");
				// 예외처리 코드 추가
			}
			if (rock != null)
			{
				Debug.Log($"프리팹 '{rockName}'을(를) Load 하였습니다.");
			}
			else
			{
				Debug.LogError($"프리팹 '{rockName}'을(를) 찾을 수 없습니다.");
				// 예외처리 코드 추가
			}
		}

		public IObjectPool<CObstacle> Pool
		{
			get
			{
				if (pool == null)
					pool = new ObjectPool<CObstacle>(
								CreatedPooledItem,
								OnTakeFromPool,
								OnReturnedToPool,
								OnDestroyPoolObject,
								true,
								stackDefaultCapacity,
								maxPoolSize);

				return pool;
			}
		}
		private IObjectPool<CObstacle> pool;

		private CObstacle CreatedPooledItem()
		{
			CObstacle obstacle;

			switch (UnityEngine.Random.Range(0,2))
			{
				case 0:
					var go = Instantiate(log);
					go.name = "Log";

					obstacle = go.AddComponent<CObstacle>();
					break;
				case 1:
					go = Instantiate(rock);
					go.name = "Stone";

					obstacle = go.AddComponent<CObstacle>();
					break;
				default:
					obstacle = null;
					break;
			}

			obstacle.Pool = Pool;

			return obstacle;
		}
		private void OnReturnedToPool(CObstacle obstacle)
		{
			obstacle.gameObject.SetActive(false);
		}

		private void OnTakeFromPool(CObstacle obstacle)
		{
			obstacle.gameObject.SetActive(true);
		}
		private void OnDestroyPoolObject(CObstacle obstacle)
		{
			Destroy(obstacle.gameObject);
		}
		public void SpawnObstacle(int lineNum, float zPosition)
		{
            //TODO < 문명진 > - -1.745f를 플레이어 간 간격으로 조정. - 2024.11.11 19:45
            float space = -1.745f;

			var obstacle = Pool.Get();

			obstacle.transform.position = new Vector3(lineNum * space, 0, zPosition);
        }
    }
}