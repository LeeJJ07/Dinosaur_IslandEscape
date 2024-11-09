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
        static int distance = 0;

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
		public void SpawnObstacle()
		{
			// TODO < 문명진 > - 1등 플레이어의 일정거리 앞에 생성하도록 position 변경해야함. - 2024.11.07 16:10
			
			int playerCount = 2;
			int space = 10;

			for (int i = 0; i < playerCount; ++i)
			{
				if (Convert.ToBoolean(UnityEngine.Random.Range(0, 2)))
					continue;
				var obstacle = Pool.Get();

				obstacle.transform.position = new Vector3(i * space, 0, distance);
			}

			distance += 10;
        }
    }
}