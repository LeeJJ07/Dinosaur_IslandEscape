using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace MyeongJin
{
	public class CCreatureBackgroundPool : MonoBehaviour
	{
		public int maxPoolSize = 30;
		public int stackDefaultCapacity = 30;

		private int creatureNum = 0;

		private string smallPteranodonName = "Prefabs/Obstacle/Team/Background/BackgroundPteranodon";      // �������� �����ϴ� ���� ��ġ
		private GameObject smallPteranodon;

		private void Awake()
		{
			smallPteranodon = Resources.Load<GameObject>(smallPteranodonName);

			#region ������ ����ó��
			if (smallPteranodon != null)
			{
				Debug.Log($"������ '{smallPteranodonName}'��(��) Load �Ͽ����ϴ�.");
			}
			else
			{
				Debug.LogError($"������ '{smallPteranodonName}'��(��) ã�� �� �����ϴ�.");
				// ����ó�� �ڵ� �߰�
			}
			#endregion
		}
		private void Start()
		{
			for (int i = 0; i < maxPoolSize; i++)
			{
				CreatedPooledItem().ReturnToPool();
			}
		}
		public IObjectPool<CCreatureHerd> Pool
		{
			get
			{
				if (pool == null)
					pool = new ObjectPool<CCreatureHerd>(
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
		private IObjectPool<CCreatureHerd> pool;

		private CCreatureHerd CreatedPooledItem()
		{
			CCreatureHerd obstacle = null;

			var go = Instantiate(smallPteranodon);
			go.name = "SmallPteranodon";

			obstacle = go.AddComponent<CCreatureBackground>();

			obstacle.Pool = Pool;

			return obstacle;
		}
		private void OnReturnedToPool(CCreatureHerd obstacle)
		{
			obstacle.gameObject.SetActive(false);
		}
		private void OnTakeFromPool(CCreatureHerd obstacle)
		{
			obstacle.gameObject.SetActive(true);
		}
		private void OnDestroyPoolObject(CCreatureHerd obstacle)
		{
			Destroy(obstacle.gameObject);
		}
		public bool SpawnCreatureHerd(Vector3 position)
		{
            CCreatureHerd obstacle = Pool.Get();

			obstacle.transform.position = new Vector3(position.x + UnityEngine.Random.Range(-10, 11), position.y + 10, position.z + 30);

			return true;
		}
	}
}