using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace MyeongJin
{
	public class CFlyPool : MonoBehaviour
	{
		public int maxPoolSize = 5;
		public int stackDefaultCapacity = 5;

		private string flyName = "Prefabs/Obstacle/Team/SecondMission/Fly";      // �������� �����ϴ� ���� ��ġ
		private GameObject fly;
		private Dictionary<bool, Vector3> flyDictionary = new Dictionary<bool, Vector3>();
		private Vector3 basePosition = new Vector3(0, 4.5f, -15);
		private Vector3[] flyOffset;

		private void Awake()
		{
			fly = Resources.Load<GameObject>(flyName);

			#region ������ ����ó��
			if (fly != null)
			{
				Debug.Log($"������ '{flyName}'��(��) Load �Ͽ����ϴ�.");
			}
			else
			{
				Debug.LogError($"������ '{flyName}'��(��) ã�� �� �����ϴ�.");
				// ����ó�� �ڵ� �߰�
			}
			#endregion
		}
		private void Start()
		{
			//flyOffset[0] = new Vector3(-4, -1.5f, 0);
			//flyOffset[1] = new Vector3(-2, 1.5f, 0);
			//flyOffset[2] = new Vector3(0, -1.5f, 0);
			//flyOffset[3] = new Vector3(2, 1.5f, 0);
			//flyOffset[4] = new Vector3(4, -1.5f, 0);

			for (int i = 0; i < maxPoolSize; i++)
            {
				//flyDictionary.Add(false, basePosition + flyOffset[i]);
            }

			for (int i = 0; i < maxPoolSize; i++)
			{
				CreatedPooledItem().ReturnToPool();
			}
		}
		public IObjectPool<CFly> Pool
		{
			get
			{
				if (pool == null)
					pool = new ObjectPool<CFly>(
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
		private IObjectPool<CFly> pool;

		private CFly CreatedPooledItem()
		{
			CFly obstacle = null;

				var go = Instantiate(fly);
				go.name = "Fly";

				obstacle = go.AddComponent<CFly>();

			obstacle.Pool = Pool;

			return obstacle;
		}
		private void OnReturnedToPool(CFly obstacle)
		{
			obstacle.gameObject.SetActive(false);
		}
		private void OnTakeFromPool(CFly obstacle)
		{
			obstacle.gameObject.SetActive(true);
		}
		private void OnDestroyPoolObject(CFly obstacle)
		{
			Destroy(obstacle.gameObject);
		}
		public bool SpawnCreatureHerd(int lineNum, Vector3 position)
		{
			// TODO < ������ > - space�� Line�� x���� �޾Ƽ� ����ؾ� ��. - 2024.11.11 17:30
			float space = 4f;

			CFly obstacle = null;
			List<CFly> temp = new List<CFly>();

			obstacle = Pool.Get();

			obstacle.transform.position = new Vector3(lineNum * space + position.x - 2, 0, 0);

			return true;
		}
	}
}